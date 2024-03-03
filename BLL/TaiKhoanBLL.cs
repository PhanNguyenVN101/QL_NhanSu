using DAL;
using DTO;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class TaiKhoanBLL
    {
        TaiKhoanDAL tkdal = new TaiKhoanDAL();
        Basis ba = new Basis();
        

        public bool isTK(string tentk, string mk)
        {
            if(tkdal.getTK(tentk, mk)==null)
                return false;
            return true;
        }
        public void InsertTK(string tentk,string mk,string quyen, byte[] anh)
        {
            TaiKhoan tk = new TaiKhoan();
            tk.TenTK = tentk;
            tk.MatKhau = mk;
            tk.Quyen = quyen;
            tk.Anh = anh;
            tkdal.Insert(tk);
        }
        public void UpdateTK(string tentk, string mk, string quyen, byte[] anh)
        {
            TaiKhoan tk = new TaiKhoan();
            tk.TenTK = tentk;
            tk.MatKhau = mk;
            tk.Quyen = quyen;
            tk.Anh = anh;
            tkdal.Update(tk);
        }
        public void UpdateTK(string tentk, string mk, byte[] anh)
        {
            tkdal.Update(tentk,mk,anh);
        }
        public void DeleteTK(string tentk)
        {
            tkdal.Delete(tentk);
        }
        public TaiKhoan getTK(string tentk)
        {
            return tkdal.getTK(tentk);
        }
        public bool isTK(string tentk)
        {
            if (tkdal.getTK(tentk) != null)
                return false;
            return true;

        }
        public void ShowAllTK(DataGridView dgv)
        {
            dgv.Rows.Clear();
            List<TaiKhoan> tks = new List<TaiKhoan>();
            tks = tkdal.getTKs();
            if (tks.Count > 0)
            {
                for(int i = 0; i < tks.Count; i++)
                {
                    dgv.Rows.Add(tks[i].Anh, tks[i].TenTK, tks[i].MatKhau, tks[i].Quyen);
                }
            }
        }
        public void FindTKs(DataGridView dgv,string searchType,string value)
        {
            dgv.Rows.Clear();
            List<TaiKhoan> tks = new List<TaiKhoan>();
            tks = tkdal.FindTKs(searchType,value);
            if (tks.Count > 0)
            {
                for (int i = 0; i < tks.Count; i++)
                {
                    dgv.Rows.Add(tks[i].Anh, tks[i].TenTK, tks[i].MatKhau, tks[i].Quyen);
                }
            }
        }
        public string getQuyen(string tentk)
        {
            return tkdal.getTK(tentk).Quyen;
        }
        public void SortTKs(DataGridView dgv)
        {
            dgv.Rows.Clear();
            List<TaiKhoan> tks = new List<TaiKhoan>();
            tks = tkdal.sortTKs();
            if (tks.Count > 0)
            {
                for (int i = 0; i < tks.Count; i++)
                {
                    dgv.Rows.Add(tks[i].Anh, tks[i].TenTK, tks[i].MatKhau, tks[i].Quyen);
                }
            }
        }
    }
}
