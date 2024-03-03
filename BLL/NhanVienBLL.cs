using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class NhanVienBLL
    {
        NhanVienDAL nvdal = new NhanVienDAL();
        Basis ba = new Basis();
        public bool isNgaySinh(DateTime date)
        {
            DateTime nowdate = DateTime.Now;
            var result = nowdate.Year - date.Year;
            if (result < 18)
                return false;
            return true;
        }
        public bool isEmail(string mail)
        {
            if (string.IsNullOrEmpty(mail))
                return false;
            string strRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(strRegex);
            return regex.IsMatch(mail);

        }
        public bool isSDT(string phone)
        {
            if (phone == null)
                return false;
            if (phone.Length < 10 || phone.Length > 10)
                return false;
            char c = phone[0];
            if (!c.Equals('0'))
                return false;
            return true;

        }
        public bool isCCCD(string idNum)
        {
            if (idNum == null)
                return false;
            if (idNum.Length < 12 || idNum.Length > 12)
                return false;
            char c = idNum[0];
            if (!c.Equals('0'))
                return false;
            return true;

        }
        public bool KT_TonTai_CCCD(string cccd)
        {
            if (nvdal.getNV_CCCD(cccd) != null)
            {
                return false;
            }
            return true;
        }
        public string TaoMaNV()
        {
            string MaNV = "";
            for (int i = 1; i <= 100000; i++)
            {
                MaNV = "NV" + i;
                if (nvdal.getNV(MaNV)==null)
                {
                    break;
                }
            }
            return MaNV;

        }
        public bool isNgayCap(DateTime date)
        {
            DateTime nowdate = DateTime.Now;
            if (date > nowdate)
                return false;
            return true;
        }
        public bool isNamKN(int nam)
        {
            if(nam > 40 || nam < 1)
            {
                return false;
            }
            return true;
        }
        public void ShowAllNV(DataGridView dgv)
        {
            dgv.Rows.Clear();
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nvdal.getNVs();
            if (nvs.Count > 0)
            {
                for (int i = 0; i < nvs.Count; i++)
                {
                    dgv.Rows.Add(nvs[i].Anh, nvs[i].MaNV, nvs[i].HoTen, nvs[i].CCCD, 
                        ba.ConvertEpoch_DateTime(nvs[i].NgaySinh).ToShortDateString(), nvs[i].GioiTinh, nvs[i].SoDienThoai,
                        nvs[i].ViTriUngTuyen, nvs[i].KetQua);
                }
            }
        }
        public void SortNV(DataGridView dgv)
        {
            dgv.Rows.Clear();
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nvdal.Sort();
            if (nvs.Count > 0)
            {
                for (int i = 0; i < nvs.Count; i++)
                {
                    dgv.Rows.Add(nvs[i].Anh, nvs[i].MaNV, nvs[i].HoTen, nvs[i].CCCD,
                        ba.ConvertEpoch_DateTime(nvs[i].NgaySinh).ToShortDateString(), nvs[i].GioiTinh, nvs[i].SoDienThoai,
                        nvs[i].ViTriUngTuyen, nvs[i].KetQua);
                }
            }
        }
        //public NhanVien(string maNV, string cCCD, string hoTen, long ngaySinh,
        //    string gioiTinh, string soDienThoai, string email, string viTriUngTuyen,
        //    int namKinhNghiem, byte[] anh, DiaChi diaChi, List<ChungChi> chungChis, string ketQua)
        public void InsertNV(string manv,string cccd,string hoten,DateTime dt,
            string gt,string sdt,string email,string vtut,int namkn, byte[] anh,
            DiaChi dc,List<ChungChi> ccs)
        {
            NhanVien nv = new NhanVien();
            nv.MaNV = manv;
            nv.CCCD = cccd;
            nv.HoTen = hoten;
            nv.NgaySinh = ba.ConvertDateTime_Epoch(dt.Date);
            nv.GioiTinh = gt;
            nv.SoDienThoai = sdt;
            nv.Email = email;
            nv.ViTriUngTuyen = vtut;
            nv.NamKinhNghiem = namkn;
            nv.Anh = anh;
            nv.DiaChi = dc;
            nv.ChungChis = ccs;
            nv.KetQua = "Đang duyệt";
            nvdal.Insert(nv);
        }
        public DiaChi getDiaChi(string Duong, string phuong_xa,string quan_huyen, string tinh_tp)
        {
            DiaChi dc = new DiaChi();
            dc.Duong = Duong;
            dc.Phuong_Xa = phuong_xa;
            dc.Quan_Huyen = quan_huyen;
            dc.Tinh_TP = tinh_tp;
            return dc;
        }
        public ChungChi getChungChi(string macc, string tencc,DateTime dt,DiaChi dc)
        {
            ChungChi cc = new ChungChi();
            cc.MaCC = macc;
            cc.TenCC = tencc;
            cc.NgayCap = ba.ConvertDateTime_Epoch(dt.Date);
            cc.DiaChi = dc;
            return cc;
        }
        public void UpdateNV(string manv, string cccd, string hoten, DateTime dt,
            string gt, string sdt, string email, string vtut, int namkn, byte[] anh,
            DiaChi dc, string kq)
        {
            NhanVien nv = new NhanVien();
            nv.MaNV = manv;
            nv.CCCD = cccd;
            nv.HoTen = hoten;
            nv.NgaySinh = ba.ConvertDateTime_Epoch(dt);
            nv.GioiTinh = gt;
            nv.SoDienThoai = sdt;
            nv.Email = email;
            nv.ViTriUngTuyen = vtut;
            nv.NamKinhNghiem = namkn;
            nv.Anh = anh;
            nv.DiaChi = dc;
            nv.KetQua = kq;
            nvdal.Update(nv);
        }
        
        public void insertCC(string manv,string macc,string tencc,DateTime dt,DiaChi dc)
        {
            ChungChi cc = new ChungChi();
            cc.MaCC = macc;
            cc.TenCC = tencc;
            cc.NgayCap = ba.ConvertDateTime_Epoch(dt);
            cc.DiaChi = dc;
            nvdal.InsertCC(manv, cc);
        }
        public void updateCC(string manv, string macc, string tencc, DateTime dt, DiaChi dc)
        {
            ChungChi cc = new ChungChi();
            cc.MaCC = macc;
            cc.TenCC = tencc;
            cc.NgayCap = ba.ConvertDateTime_Epoch(dt);
            cc.DiaChi = dc;
            nvdal.UpdateCC(manv, cc);
        }
        public void DeleteCC(string manv,string macc)
        {
            nvdal.DeleteCC(manv,macc);
        }
        public void DeleteNV(string manv)
        {
            nvdal.Delete(manv);
        }
        public void FindNV(string searchType,string value,DataGridView dgv)
        {
            dgv.Rows.Clear();
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nvdal.Find(searchType,value);
            if (nvs.Count > 0)
            {
                for (int i = 0; i < nvs.Count; i++)
                {
                    dgv.Rows.Add(nvs[i].Anh, nvs[i].MaNV, nvs[i].HoTen, nvs[i].CCCD,
                        nvs[i].NgaySinh, nvs[i].GioiTinh, nvs[i].SoDienThoai,
                        nvs[i].ViTriUngTuyen, nvs[i].KetQua);
                }
            }
        }
        public int isMaCC(DataGridView dgv,string macc)
        {
            int dem = 0;
            foreach(DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value.ToString() == macc)
                {
                    dem += 1;
                }
            }
            return dem;
        }
        public bool isMaCC(string manv,string macc)
        {
            return nvdal.TimCC(manv, macc);
        }
        public NhanVien XuatNV(string manv)
        {
            return nvdal.getNV(manv);
        }
        public void FillKQ(string kq,DataGridView dgv)
        {
            dgv.Rows.Clear();
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nvdal.FillKQ(kq);
            if (nvs.Count > 0)
            {
                for (int i = 0; i < nvs.Count; i++)
                {
                    dgv.Rows.Add(nvs[i].Anh, nvs[i].MaNV, nvs[i].HoTen, nvs[i].CCCD,
                        nvs[i].NgaySinh, nvs[i].GioiTinh, nvs[i].SoDienThoai,
                        nvs[i].ViTriUngTuyen, nvs[i].KetQua);
                }
            }
        }
        public void Fill_3NVNamKN(DataGridView dgv)
        {
            dgv.Rows.Clear();
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nvdal.Fill_3NVNamKN();
            if (nvs.Count > 0)
            {
                for (int i = 0; i < nvs.Count; i++)
                {
                    dgv.Rows.Add(nvs[i].Anh, nvs[i].MaNV, nvs[i].HoTen, nvs[i].CCCD,
                        nvs[i].NgaySinh, nvs[i].GioiTinh, nvs[i].SoDienThoai,
                        nvs[i].ViTriUngTuyen, nvs[i].KetQua);
                }
            }
        }
        public void Fill_VTUT_NamKN(DataGridView dgv,string vtut,int namkn)
        {
            dgv.Rows.Clear();
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nvdal.Fill_VTUT_NamKN(vtut, namkn);
            if (nvs.Count > 0)
            {
                for (int i = 0; i < nvs.Count; i++)
                {
                    dgv.Rows.Add(nvs[i].Anh, nvs[i].MaNV, nvs[i].HoTen, nvs[i].CCCD,
                        nvs[i].NgaySinh, nvs[i].GioiTinh, nvs[i].SoDienThoai,
                        nvs[i].ViTriUngTuyen, nvs[i].KetQua);
                }
            }
        }
        public void Fill_NEHT_SortHT_CCCD(DataGridView dgv, string hoten)
        {
            dgv.Rows.Clear();
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nvdal.Fill_NEHT_SortHT_CCCD(hoten);
            if (nvs.Count > 0)
            {
                for (int i = 0; i < nvs.Count; i++)
                {
                    dgv.Rows.Add(nvs[i].Anh, nvs[i].MaNV, nvs[i].HoTen, nvs[i].CCCD,
                        nvs[i].NgaySinh, nvs[i].GioiTinh, nvs[i].SoDienThoai,
                        nvs[i].ViTriUngTuyen, nvs[i].KetQua);
                }
            }
        }
        public void Fill_HoTenBD_SortSDT(DataGridView dgv, string hoten)
        {
            dgv.Rows.Clear();
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nvdal.Fill_HoTenBD_SortSDT(hoten);
            if (nvs.Count > 0)
            {
                for (int i = 0; i < nvs.Count; i++)
                {
                    dgv.Rows.Add(nvs[i].Anh, nvs[i].MaNV, nvs[i].HoTen, nvs[i].CCCD,
                        nvs[i].NgaySinh, nvs[i].GioiTinh, nvs[i].SoDienThoai,
                        nvs[i].ViTriUngTuyen, nvs[i].KetQua);
                }
            }
        }
    }
}
