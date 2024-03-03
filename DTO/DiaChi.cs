using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DiaChi
    {
        public string Duong { get;set; }
        public string Phuong_Xa { get; set; }
        public string Quan_Huyen { get;set; }
        public string Tinh_TP { get;set; }
    }
}
