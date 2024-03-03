using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        ErrorProvider ep = new ErrorProvider();
        TaiKhoanBLL tkbll = new TaiKhoanBLL();
        public void Message(string message, MyMessageBox.enmType type)
        {
            MyMessageBox frm = new MyMessageBox();
            frm.showMess(message, type);
        }
        private void btn_ShowPassword_Click(object sender, EventArgs e)
        {
            if (txt_Password.UseSystemPasswordChar == true)
            {
                txt_Password.UseSystemPasswordChar = false;
            }
            else
            {
                txt_Password.UseSystemPasswordChar = true;
            }
        }
        public bool isDangNhap(string TenTK,string MatKhau)
        {
            if (TenTK.Equals(""))
            {
                ep.SetError(txt_TenTK, "Tên tài khoản không để trống !");
                return false;
            }
            else
            {
                ep.Clear();
            }
            if (MatKhau.Equals(""))
            {
                ep.SetError(txt_Password, "Mật khẩu không để trống !");
                return false;
            }
            else
            {
                ep.Clear();
            }
            if (!tkbll.isTK(TenTK, MatKhau))
            {
                this.Message("Tài khoản không hợp lệ",MyMessageBox.enmType.Error); 
                return false;
            }
            return true;
        }
        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            string tentk = txt_TenTK.Text.Trim();
            string matkhau = txt_Password.Text.Trim();
            if (isDangNhap(tentk, matkhau))
            {
                this.Hide();
                Main frm = new Main();
                frm.LoadTK(tentk);
                frm.ShowDialog();
                this.Close();
            }
        }


        
    }
}
