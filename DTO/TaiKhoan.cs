using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DTO
{
    public class TaiKhoan
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string TenTK { get; set; }
        public string MatKhau { get;set; }
        public string Quyen { get;set; }
        public byte[] Anh { get; set; }

        public TaiKhoan(string tenTK, string matKhau, string quyen, byte[] anh)
        {
            TenTK = tenTK;
            MatKhau = matKhau;
            Quyen = quyen;
            Anh = anh;
        }

        public TaiKhoan()
        {
            TenTK = "";
            MatKhau = "";
            Quyen = "";
            Anh = null;
        }
    }
}
