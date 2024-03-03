using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace DTO
{
    public class NhanVien
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string MaNV { get; set; }
        public string CCCD { get; set; }
        public string HoTen {get; set; }
        public long NgaySinh { get;set; }
        public string GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string ViTriUngTuyen { get; set; }
        public int NamKinhNghiem { get;set; }
        public byte[] Anh { get;set; }
        public DiaChi DiaChi { get; set; }
        public List<ChungChi> ChungChis { get; set;}
        public string KetQua { get; set; }

        public NhanVien(string maNV, string cCCD, string hoTen, long ngaySinh, 
            string gioiTinh, string soDienThoai, string email, string viTriUngTuyen, 
            int namKinhNghiem, byte[] anh, DiaChi diaChi, List<ChungChi> chungChis, string ketQua)
        {
            MaNV = maNV;
            CCCD = cCCD;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            SoDienThoai = soDienThoai;
            Email = email;
            ViTriUngTuyen = viTriUngTuyen;
            NamKinhNghiem = namKinhNghiem;
            Anh = anh;
            DiaChi = diaChi;
            ChungChis = chungChis;
            KetQua = ketQua;
        }

        public NhanVien()
        {
            MaNV = "";
            CCCD = "";
            HoTen = "";
            NgaySinh = 0;
            GioiTinh = "";
            SoDienThoai = "";
            Email = "";
            ViTriUngTuyen = "";
            NamKinhNghiem = 0;
            Anh = null;
            DiaChi = null;
            ChungChis = null;
            KetQua = "";
        }
    }
}
