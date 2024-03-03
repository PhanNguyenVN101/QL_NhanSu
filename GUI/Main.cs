using BLL;
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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        TaiKhoanBLL tkbll = new TaiKhoanBLL();
        NhanVienBLL nvbll = new NhanVienBLL();
        Basis ba = new Basis();
        public void Message(string message, MyMessageBox.enmType type)
        {
            MyMessageBox frm = new MyMessageBox();
            frm.showMess(message, type);
        }
        public void LoadCboSearch()
        {
            string[] cbos = { "Họ tên", "CCCD","Số điện thoại", "Giới tính" };
            foreach(string s in cbos) 
            {
                cbo_LoaiTK.Items.Add(s);
            }
            cbo_LoaiTK.SelectedIndex = 0;
        }
        public void LoadCboGT()
        {
            string[] cbos = { "Nam", "Nữ"};
            foreach (string s in cbos)
            {
                cbo_GT.Items.Add(s);
            }
            cbo_GT.SelectedIndex = 0;
        }
        public void LoadTK(string tentk)
        {
            tsBtn_DoiThongTinTK.Text = tentk;
            var stream = new MemoryStream(tkbll.getTK(tentk).Anh);
            tsBtn_DoiThongTinTK.Image = Image.FromStream(stream);
        }
        public void LoadData()
        {
            nvbll.ShowAllNV(dgv_NV);
            lbl_Total.Text = dgv_NV.RowCount.ToString();
        }
        private void tsBtn_DoiThongTinTK_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangeInfoAcc frm = new ChangeInfoAcc();
            frm.LoadTK(tsBtn_DoiThongTinTK.Text);
            frm.ShowDialog();
            this.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCboSearch();
            LoadCboGT();
            if (tkbll.getQuyen(tsBtn_DoiThongTinTK.Text) == "Admin")
            {
                tàiKhoảnToolStripMenuItem.Enabled = true;
            }else
                tàiKhoảnToolStripMenuItem.Enabled = false;

        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            TaiKhoans frm = new TaiKhoans();
            frm.LoadTK(tsBtn_DoiThongTinTK.Text );
            frm.ShowDialog();
            this.Close();
        }

       

        private void tsBtn_DangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap frm = new DangNhap();
            frm.ShowDialog();
            this.Close();
        }

        

        private void tsBtn_Them_Click(object sender, EventArgs e)
        {
            this.Hide();
            TaoNhanVien frm = new TaoNhanVien();
            frm.LoadTK(tsBtn_DoiThongTinTK.Text);
            frm.ShowDialog();
            this.Close();
        }

        private void tsBtn_Xoa_Click(object sender, EventArgs e)
        {
            if(dgv_NV.RowCount > 0)
            {
                int index = dgv_NV.CurrentRow.Index;
                string manv = dgv_NV.Rows[index].Cells[1].Value.ToString();
                nvbll.DeleteNV(manv);
                this.Message("Success", MyMessageBox.enmType.Success);
                LoadData();
            }
            else
            {
                this.Message("Chưa có nhân viên", MyMessageBox.enmType.Error);
            }
           
        }

        private void tsBtn_SapXep_Click(object sender, EventArgs e)
        {
            nvbll.SortNV(dgv_NV);
            
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_Search.Text = "";
            cbo_LoaiTK.SelectedIndex = 0;
            cbo_GT.SelectedIndex = 0;
            LoadData();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string search = txt_Search.Text.Trim();
            string searchType = "";
            if (cbo_LoaiTK.SelectedIndex == 3)
            {
                searchType = "GT";
                search = cbo_GT.SelectedItem.ToString();
                nvbll.FindNV(searchType, search, dgv_NV);
            }
            if (!search.Equals(""))
            {
                if(cbo_LoaiTK.SelectedIndex==0)
                {
                    searchType = "HoTen";
                    
                }
                if (cbo_LoaiTK.SelectedIndex == 1)
                {
                    searchType = "CCCD";
                }
                if (cbo_LoaiTK.SelectedIndex == 2)
                {
                    searchType = "SDT";
                }
                
                nvbll.FindNV(searchType, search, dgv_NV);
            }else
            {
                this.Message("Tìm kiếm không để trống", MyMessageBox.enmType.Error);
            }
        }

        private void tsBtn_Sua_Click(object sender, EventArgs e)
        {
            if (dgv_NV.RowCount > 0)
            {
                int index = dgv_NV.CurrentRow.Index;
                string manv = dgv_NV.Rows[index].Cells[1].Value.ToString();
                this.Hide();
                UpdateNV frm = new UpdateNV();
                frm.LoadTK(tsBtn_DoiThongTinTK.Text);
                frm.LoadMaNV(manv);
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                this.Message("Chưa có nhân viên", MyMessageBox.enmType.Error);
            }
        }

        private void tsBtn_Loc_Click(object sender, EventArgs e)
        {
            this.Hide();
            Fill_NV frm = new Fill_NV();
            frm.LoadTK(tsBtn_DoiThongTinTK.Text);
            frm.ShowDialog();
            this.Close();
        }
    }
}
