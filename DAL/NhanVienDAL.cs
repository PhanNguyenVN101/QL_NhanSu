using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhanVienDAL:ConnectDB
    {
        IMongoCollection<NhanVien> collection = GetCollection<NhanVien>("NhanVien");
        public List<NhanVien> getNVs()
        {
            var filter = Builders<NhanVien>.Filter.Empty;
            var results = collection.Find(filter).ToList();
            return results;
        }

        public NhanVien getNV(string MaNV)
        {
            var filter = Builders<NhanVien>.Filter.Eq(o=>o.MaNV, MaNV);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }
        public NhanVien getNV_CCCD(string cccd)
        {
            var filter = Builders<NhanVien>.Filter.Eq(o=>o.CCCD, cccd);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }
        public void Insert(NhanVien nv)
        {
            nv = new NhanVien(nv.MaNV, nv.CCCD, nv.HoTen, 
                nv.NgaySinh, nv.GioiTinh, nv.SoDienThoai, 
                nv.Email, nv.ViTriUngTuyen, nv.NamKinhNghiem, 
                nv.Anh, nv.DiaChi, nv.ChungChis, nv.KetQua);
            collection.InsertOne(nv);
        }
        public void Update(NhanVien nv)
        {
            var filter = Builders<NhanVien>.Filter.Eq(o => o.MaNV, nv.MaNV);
            var update = Builders<NhanVien>.Update
                .Set(o => o.CCCD, nv.CCCD)
                .Set(o => o.HoTen, nv.HoTen)
                .Set(o => o.NgaySinh, nv.NgaySinh)
                .Set(o => o.GioiTinh, nv.GioiTinh)
                .Set(o => o.SoDienThoai, nv.SoDienThoai)
                .Set(o => o.Email, nv.Email)
                .Set(o => o.ViTriUngTuyen, nv.ViTriUngTuyen)
                .Set(o => o.NamKinhNghiem, nv.NamKinhNghiem)
                .Set(o => o.Anh, nv.Anh)
                .Set(o => o.DiaChi.Duong, nv.DiaChi.Duong)
                .Set(o => o.DiaChi.Phuong_Xa, nv.DiaChi.Phuong_Xa)
                .Set(o => o.DiaChi.Quan_Huyen, nv.DiaChi.Quan_Huyen)
                .Set(o => o.DiaChi.Tinh_TP, nv.DiaChi.Tinh_TP)
                .Set(o => o.KetQua, nv.KetQua);
            collection.UpdateOne(filter, update);
        }
        
        public bool TimCC(string manv,string macc)
        {
            var filter = Builders<NhanVien>.Filter
                .Where(o => o.MaNV == manv && o.ChungChis
                .Any(i => i.MaCC == macc));
            var result = collection.Find(filter).FirstOrDefault();
            if (result != null)
                return false;
            return true;
        }
        public void InsertCC(string manv,ChungChi cc)
        {
            var filter = Builders<NhanVien>.Filter
                .Where(o => o.MaNV == manv);
            var update = Builders<NhanVien>.Update.Push(o => o.ChungChis,cc);
            collection.UpdateOne(filter, update);
        }
        public void DeleteCC(string manv,string macc) 
        {
            var filter = Builders<NhanVien>.Filter
                .Where(o => o.MaNV == manv);
            var update = Builders<NhanVien>.Update.
                PullFilter(o=>o.ChungChis,Builders<ChungChi>.
                Filter.Where(n=>n.MaCC==macc));
            collection.UpdateOne(filter, update);
        }
        public void UpdateCC(string manv,ChungChi cc)
        {
            var filter = Builders<NhanVien>.Filter
                .Where(o => o.MaNV == manv && o.ChungChis
                .Any(i => i.MaCC == cc.MaCC));
            var update = Builders<NhanVien>.Update
                .Set(o => o.ChungChis.FirstMatchingElement().TenCC, cc.TenCC)
                .Set(o => o.ChungChis.FirstMatchingElement().NgayCap, cc.NgayCap)
                .Set(o => o.ChungChis.FirstMatchingElement().DiaChi.Duong, cc.DiaChi.Duong)
                .Set(o => o.ChungChis.FirstMatchingElement().DiaChi.Phuong_Xa, cc.DiaChi.Phuong_Xa)
                .Set(o => o.ChungChis.FirstMatchingElement().DiaChi.Quan_Huyen, cc.DiaChi.Quan_Huyen)
                .Set(o => o.ChungChis.FirstMatchingElement().DiaChi.Tinh_TP, cc.DiaChi.Tinh_TP);

            collection.UpdateOne(filter, update);
        }
        public void Delete(string manv)
        {
            var filter = Builders<NhanVien>.Filter.Eq(o => o.MaNV, manv);
            collection.DeleteOne(filter);
        }
        public List<NhanVien> Sort()
        {
            var filter = Builders<NhanVien>.Filter.Empty;
            var sort = Builders<NhanVien>.Sort.Ascending(o => o.HoTen).Descending(o => o.CCCD);
            var results = collection.Find(filter).Sort(sort).ToList();
            return results;
        }
        public List<NhanVien> Find(string searchType, string value)
        {
            var results = collection.Find(e => e.HoTen.Contains(value)).ToList();
            switch (searchType)
            {
                case "HoTen":
                    results = collection.Find(e => e.HoTen.Contains(value)).ToList();
                    break;
                case "CCCD":
                    results = collection.Find(e => e.CCCD.Contains(value)).ToList();
                    break;
                case "SDT":
                    results = collection.Find(e => e.SoDienThoai.Contains(value)).ToList();
                    break;
                case "GT":
                    results = collection.Find(e => e.GioiTinh.Equals(value)).ToList();
                    break;
            }
            return results;

        }

        public List<NhanVien> FillKQ(string kq)
        {
            var filter = Builders<NhanVien>.Filter.Eq(o => o.KetQua,kq);
            var results = collection.Find(filter).ToList();
            return results;
        }
        public List<NhanVien> Fill_3NVNamKN()
        {
            var filter = Builders<NhanVien>.Filter.Empty;
            var sort = Builders<NhanVien>.Sort.Descending(o => o.NamKinhNghiem);
            var results = collection.Find(filter).Sort(sort).Limit(3).ToList();
            return results;
        }
        public List<NhanVien> Fill_VTUT_NamKN(string vtut,int namkn)
        {
            var filter = Builders<NhanVien>.Filter.Where(o => o.ViTriUngTuyen.Contains(vtut) && o.NamKinhNghiem > namkn);
            var results = collection.Find(filter).ToList();
            return results;
        }
        public List<NhanVien> Fill_NEHT_SortHT_CCCD(string ht)
        {
            var filter = Builders<NhanVien>.Filter.Ne(o=>o.HoTen,ht);
            var sort = Builders<NhanVien>.Sort
                .Ascending(o => o.HoTen)
                .Descending(o => o.CCCD);
            var results = collection.Find(filter).Sort(sort).ToList();
            return results;
        }
        public List<NhanVien> Fill_HoTenBD_SortSDT(string ht)
        {
            string value = "/^" + ht + "/i";
            var filter = Builders<NhanVien>.Filter.Regex(x=>x.HoTen,new BsonRegularExpression(value));
            var sort = Builders<NhanVien>.Sort.Ascending(o => o.SoDienThoai);
            var results = collection.Find(filter).Sort(sort).ToList();
            return results;
        }
        

    }
}
