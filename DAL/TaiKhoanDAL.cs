using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TaiKhoanDAL:ConnectDB
    {
        IMongoCollection<TaiKhoan> collection = GetCollection<TaiKhoan>("TaiKhoan");

        public TaiKhoan getTK(string tentk, string mk)
        {
            var filter = Builders<TaiKhoan>.Filter.Where(o=>o.TenTK == tentk && o.MatKhau == mk);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }

        public TaiKhoan getTK(string tentk)
        {
            var filter = Builders<TaiKhoan>.Filter.Eq(o => o.TenTK, tentk);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }

        public List<TaiKhoan> getTKs()
        {
            var filter = Builders<TaiKhoan>.Filter.Empty;
            var results = collection.Find(filter).ToList();
            return results;
        }
        public List<TaiKhoan> FindTKs(string FindType, string value)
        {
            var results = collection.Find(e => e.TenTK.Contains(value)).ToList();

            switch (FindType)
            {
                case "TenTK":
                    results = collection.Find(e => e.TenTK.Contains(value)).ToList();
                    break;
                case "Quyen":
                    results = collection.Find(e => e.Quyen.Equals(value)).ToList();
                    break;

            }

            return results;
        }
        public void Insert(TaiKhoan tk)
        {
            tk = new TaiKhoan(tk.TenTK, tk.MatKhau, tk.Quyen, tk.Anh);
            collection.InsertOne(tk);
        }
        public void Update(TaiKhoan tk)
        {
            var filter = Builders<TaiKhoan>.Filter.Eq(o => o.TenTK, tk.TenTK);
            var update = Builders<TaiKhoan>.Update.Set(o => o.MatKhau, tk.MatKhau).Set(o => o.Quyen, tk.Quyen).Set(o => o.Anh, tk.Anh);
            collection.UpdateOne(filter, update);
        }
        public void Update(string tentk, string matkhau, byte[] anh)
        {
            var filter = Builders<TaiKhoan>.Filter.Eq(o => o.TenTK, tentk);
            var update = Builders<TaiKhoan>.Update.Set(o => o.MatKhau, matkhau).Set(o => o.Anh, anh);
            collection.UpdateOne(filter, update);
        }
        public void Delete(string tentk)
        {
            var filter = Builders<TaiKhoan>.Filter.Eq(o => o.TenTK, tentk);
            collection.DeleteOne(filter);
        }
        public List<TaiKhoan> sortTKs()
        {
            var filter = Builders<TaiKhoan>.Filter.Empty;
            var sort = Builders<TaiKhoan>.Sort.Ascending(o => o.TenTK);
            var results = collection.Find(filter).Sort(sort).ToList();
            return results;
        }
    }
}
