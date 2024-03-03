using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ChangeInfoAcc : Form
    {
        public ChangeInfoAcc()
        {
            InitializeComponent();
        }
        Basis ba = new Basis();
        TaiKhoanBLL tkbll = new TaiKhoanBLL();
        ErrorProvider ep = new ErrorProvider();
        public void Message(string message, MyMessageBox.enmType type)
        {
            MyMessageBox frm = new MyMessageBox();
            frm.showMess(message, type);
        }

        public void LoadTK(string tentk)
        {
            lbl_TenTK.Text = tentk;
            var stream = new MemoryStream(tkbll.getTK(tentk).Anh);
            txt_Password.Text = tkbll.getTK(tentk).MatKhau;
            pic_Img.Image = Image.FromStream(stream);
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
        public bool isInfo(string MatKhau)
        {
            
            if (MatKhau.Equals(""))
            {
                ep.SetError(txt_Password, "Mật khẩu không để trống !");
                return false;
            }
            else
            {
                ep.Clear();
            }
            return true;
        }
        private void btn_UpdateInfoUser_Click(object sender, EventArgs e)
        {
            string tentk = lbl_TenTK.Text;
            string mk = txt_Password.Text.Trim();
            byte[] img = ba.ConvertImgToByte(pic_Img.Image);
            if(isInfo(mk))
            {
                tkbll.UpdateTK(tentk, mk, img);
                this.Message("Success", MyMessageBox.enmType.Success);
            }
        }

        private void btn_UpImage_Click(object sender, EventArgs e)
        {
            ba.UpLoadImage(pic_Img);
        }

        private void tsBtn_QuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main frm = new Main();
            frm.LoadTK(lbl_TenTK.Text);
            frm.ShowDialog();
            this.Close();
        }
    }
}
