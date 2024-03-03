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
    public partial class Fill_NV : Form
    {
        public Fill_NV()
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
        public void LoadTK(string tentk)
        {
            tsBtn_ThongTinTK.Text = tentk;
            var stream = new MemoryStream(tkbll.getTK(tentk).Anh);
            tsBtn_ThongTinTK.Image = Image.FromStream(stream);
        }
      
      
        public void LoadCboKetQua()
        {
            string[] kq = { "Đang duyệt", "Đậu", "Rớt" };
            foreach (string i in kq)
            {
                cbo_KQ.Items.Add(i);
            }
            cbo_KQ.SelectedIndex = 0;
        }
        public void LoadCbo_LoaiLoc()
        {
            string[] strings =
            {
                "Kết quả",
                "Xuất ra 3 nhân viên có năm kinh nghiệm nhiều nhất",
                "Có vị trí ứng tuyển = chuỗi bất kì và năm kinh nghiệm >...",
                "Có họ tên bắt đầu bằng ... rồi sắp xếp tăng số điện thoại",
                "Có họ tên không bằng ...,sắp xếp tăng họ tên ,giảm CCCD"
            };
            foreach (string s in strings)
            {
                cbo_Loai.Items.Add(s);
            }
            cbo_Loai.SelectedIndex = 0;
        }
        public void LoadData()
        {
            nvbll.ShowAllNV(dgv_NV);
        }
        private void Fill_NV_Load(object sender, EventArgs e)
        {
            LoadCbo_LoaiLoc();
            LoadCboKetQua();
            LoadData();
        }

        private void btn_Fill_Click(object sender, EventArgs e)
        {
            if(cbo_Loai.SelectedIndex == 0)
            {
                nvbll.FillKQ(cbo_KQ.SelectedItem.ToString(), dgv_NV);
            }
            if (cbo_Loai.SelectedIndex == 1)
            {
                nvbll.Fill_3NVNamKN(dgv_NV);
            }
            if (cbo_Loai.SelectedIndex == 2)
            {
                nvbll.Fill_VTUT_NamKN(dgv_NV,txt_TimKiem.Text.Trim(),int.Parse(txt_SoNamKN.Text.Trim()));
            }
            if (cbo_Loai.SelectedIndex == 3)
            {
                nvbll.Fill_HoTenBD_SortSDT(dgv_NV, txt_TimKiem.Text.Trim());
            }
            if (cbo_Loai.SelectedIndex == 4)
            {
                nvbll.Fill_NEHT_SortHT_CCCD(dgv_NV, txt_TimKiem.Text.Trim());
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
            cbo_KQ.SelectedIndex = 0;
            cbo_Loai.SelectedIndex = 0;
            ba.clearTextBoxs(this.Controls);
            LoadData();
        }
    }
}
