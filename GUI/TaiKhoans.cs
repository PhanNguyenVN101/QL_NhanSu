using BLL;
using GUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class TaiKhoans : Form
    {
        public TaiKhoans()
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
            tsBtn_ThongTinTK.Text = tentk;
            var stream = new MemoryStream(tkbll.getTK(tentk).Anh);
            tsBtn_ThongTinTK.Image = Image.FromStream(stream);
        }
        public void LoadData()
        {
           
            tkbll.ShowAllTK(dgv_TK);
            lbl_Total.Text = dgv_TK.RowCount.ToString();
        }
        public void LoadCboQuyen()
        {
            string[] quyen = { "Admin", "User" };
            foreach (string c in quyen)
            {
                cbo_Quyen.Items.Add(c);
            }
            cbo_Quyen.SelectedIndex = 0;
        }
        public void LoadCboLoaiTK()
        {
            string[] strings = { "Tên tài khoản", "Quyền" };
            foreach(string c in strings)
            {
                cbo_LoaiTK.Items.Add(c);
            }
            cbo_LoaiTK.SelectedIndex = 0;
        }
        public bool isTaiKhoan(string TenTK, string MatKhau)
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
            if(!tkbll.isTK(TenTK))
            {
                this.Message("Tên tài khoản đã có", MyMessageBox.enmType.Error);
                return false;
            }

            if (MatKhau.Equals(""))
            {
                ep.SetError(txt_MatKhau, "Mật khẩu không để trống !");
                return false;
            }
            else
            {
                ep.Clear();
            }
            return true;
        }
        public bool isTaiKhoan_Update(string TenTK, string MatKhau)
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
            if (tkbll.isTK(TenTK))
            {
                this.Message("Không tìm thấy tài khoản này", MyMessageBox.enmType.Error);
                return false;
            }

            if (MatKhau.Equals(""))
            {
                ep.SetError(txt_MatKhau, "Mật khẩu không để trống !");
                return false;
            }
            else
            {
                ep.Clear();
            }
            return true;
        }
        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            LoadCboLoaiTK();
            LoadCboQuyen();
            LoadData();
        }

        private void dgv_TK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgv_TK.Rows[e.RowIndex];
            txt_TenTK.Text = row.Cells[1].Value.ToString();
            txt_MatKhau.Text = row.Cells[2].Value.ToString();
            cbo_Quyen.SelectedItem = row.Cells[3].Value.ToString();
            var data = (byte[])(row.Cells[0].Value);
            var stream = new MemoryStream(data);
            pic_Img.Image = Image.FromStream(stream);
        }

        

        private void btn_UpImage_Click(object sender, EventArgs e)
        {
            ba.UpLoadImage(pic_Img);
        }


        private void tsBtn_Them_Click(object sender, EventArgs e)
        {
            string tentk = txt_TenTK.Text.Trim();
            string mk = txt_MatKhau.Text.Trim();
            string quyen = cbo_Quyen.SelectedItem.ToString();
            byte[] img = ba.ConvertImgToByte(pic_Img.Image);
            if (isTaiKhoan(tentk, mk))
            {
                tkbll.InsertTK(tentk, mk, quyen, img);
                this.Message("Success", MyMessageBox.enmType.Success);
                LoadData();
            }
        }

        private void tsBtn_Sua_Click(object sender, EventArgs e)
        {
            string tentk = txt_TenTK.Text.Trim();
            string mk = txt_MatKhau.Text.Trim();
            string quyen = cbo_Quyen.SelectedItem.ToString();
            byte[] img = ba.ConvertImgToByte(pic_Img.Image);
            if (isTaiKhoan_Update(tentk, mk))
            {

                tkbll.UpdateTK(tentk, mk, quyen, img);
                this.Message("Success", MyMessageBox.enmType.Success);
                LoadData();
            }
        }

        private void tsBtn_QuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main frm = new Main();
            frm.LoadTK(tsBtn_ThongTinTK.Text);
            frm.ShowDialog();
            this.Close();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            ba.clearTextBoxs(this.Controls);
            cbo_LoaiTK.SelectedIndex = 0;
            cbo_Quyen.SelectedIndex = 0;
            pic_Img.Image = Resources.icons8_user_96;
            txt_TenTK.Focus();
            LoadData();
        }

        private void tsBtn_Xoa_Click(object sender, EventArgs e)
        {
            string tentk = txt_TenTK.Text.Trim();
            if (tentk.Equals(""))
            {
                ep.SetError(txt_TenTK, "Tên tài khoản không để trống !");
                return;
            }
            else
            {
                ep.Clear();
            }
            if (tentk.Equals(tsBtn_ThongTinTK.Text))
            {
                this.Message("Tài khoản đang hoạt động", MyMessageBox.enmType.Error);
            }
            else
            {
                if(dgv_TK.RowCount<2)
                {
                    this.Message("Không thể xóa hết tài khoản", MyMessageBox.enmType.Error);
                }else
                    tkbll.DeleteTK(tentk);
            }
            LoadData();
        }

        private void tsBtn_SapXep_Click(object sender, EventArgs e)
        {
            tkbll.SortTKs(dgv_TK);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string searchType = "",value="";
            if (cbo_LoaiTK.SelectedItem.Equals("Quyền"))
            {
                searchType = "Quyen";
                value = cbo_Quyen.SelectedItem.ToString();
            }
            else
            {
                searchType = "TenTK";
                value = txt_TenTK.Text.Trim();
            }
            if (value.Equals(""))
                this.Message("Tìm kiếm tên không để trống", MyMessageBox.enmType.Error);
            else
                tkbll.FindTKs(dgv_TK, searchType, value);
        }
    }
}
