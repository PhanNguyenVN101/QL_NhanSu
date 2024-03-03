using BLL;
using DevExpress.Utils.DirectXPaint.Svg;
using DevExpress.XtraReports.UI;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class TaoNhanVien : Form
    {
        public TaoNhanVien()
        {
            InitializeComponent();
        }
        NhanVienBLL nvbll = new NhanVienBLL();
        TaiKhoanBLL tkbll = new TaiKhoanBLL();
        Basis ba = new Basis();
        ErrorProvider ep = new ErrorProvider();
        List<ChungChi> listCC = new List<ChungChi>();
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
        public bool isThongTinCC_Insert(string macc, string tencc, DateTime NgayCap,string duong,
            string phuong_xa, string quan_huyen, string tinh_tp)
        {
            if (nvbll.isMaCC(dgv_CC,macc)>0)
            {
                ep.SetError(txt_MaCC, "Mã chứng chỉ đã có");
                return false;
            }
            else
                ep.Clear();
            if (macc.Equals(""))
            {
                ep.SetError(txt_MaCC, "Mã chứng chỉ không để trống");
                return false;
            }
            else
                ep.Clear();
            if (tencc.Equals(""))
            {
                ep.SetError(txt_MaCC, "Tên chứng chỉ không để trống");
                return false;
            }
            else
                ep.Clear();
            if (!nvbll.isNgayCap(NgayCap))
            {
                ep.SetError(dt_NgayCap, "Ngày cấp không hợp lệ");
                return false;
            }
            else
                ep.Clear();
            if (duong.Equals(""))
            {
                ep.SetError(txt_Duong_CC, "Đường không để trống");
                return false;
            }
            else
                ep.Clear();
            if (phuong_xa.Equals(""))
            {
                ep.SetError(txt_Phuong_XaCC, "Phường/Xã không để trống");
                return false;
            }
            else
                ep.Clear();
            if (quan_huyen.Equals(""))
            {
                ep.SetError(txt_Quan_Huyen_CC, "Quận/Huyện không để trống");
                return false;
            }
            else
                ep.Clear();
            if (tinh_tp.Equals(""))
            {
                ep.SetError(txt_Tinh_TPCC, "Tỉnh/Thành phố không để trống");
                return false;
            }
            else
                ep.Clear();
            return true;
        }
        public bool isThongTinCC_Update(string macc, string tencc, DateTime NgayCap, string duong,
            string phuong_xa, string quan_huyen, string tinh_tp)
        {

            if (macc.Equals(""))
            {
                ep.SetError(txt_MaCC, "Mã chứng chỉ không để trống");
                return false;
            }
            else
                ep.Clear();
            if (tencc.Equals(""))
            {
                ep.SetError(txt_MaCC, "Tên chứng chỉ không để trống");
                return false;
            }
            else
                ep.Clear();
            if (!nvbll.isNgayCap(NgayCap))
            {
                ep.SetError(dt_NgayCap, "Ngày cấp không hợp lệ");
                return false;
            }
            else
                ep.Clear();
            if (duong.Equals(""))
            {
                ep.SetError(txt_Duong_CC, "Đường không để trống");
                return false;
            }
            else
                ep.Clear();
            if (phuong_xa.Equals(""))
            {
                ep.SetError(txt_Phuong_XaCC, "Phường/Xã không để trống");
                return false;
            }
            else
                ep.Clear();
            if (quan_huyen.Equals(""))
            {
                ep.SetError(txt_Quan_Huyen_CC, "Quận/Huyện không để trống");
                return false;
            }
            else
                ep.Clear();
            if (tinh_tp.Equals(""))
            {
                ep.SetError(txt_Tinh_TPCC, "Tỉnh/Thành phố không để trống");
                return false;
            }
            else
                ep.Clear();
            return true;
        }
        public bool isThongTinNV(string cccd,string hoten, DateTime ngaysinh,
            string sdt,string email,string vtut,int namkn,string duong,
            string phuong_xa,string quan_huyen,string tinh_tp, DataGridView dgv)
        {
            if (!nvbll.isCCCD(cccd))
            {
                ep.SetError(txt_CCCD, "CCCD không hợp lệ");
                return false;
            }
            else
                ep.Clear();
            if (cccd.Equals(""))
            {
                ep.SetError(txt_CCCD, "CCCD không để trống");
                return false;
            }
            else
                ep.Clear();
            if (!nvbll.KT_TonTai_CCCD(cccd))
            {
                ep.SetError(txt_CCCD, "CCCD đã có");
                return false;
            }
            else
                ep.Clear() ;
            if (hoten.Equals(""))
            {
                ep.SetError(txt_HoTen, "Họ tên không để trống");
                return false;
            }
            else
                ep.Clear();
            if(!nvbll.isNgaySinh(ngaysinh))
            {
                ep.SetError(dt_NgaySinh, "Nhân viên từ 18 tuổi trở lên");
                return false;
            }
            else
                ep.Clear();
            if(!nvbll.isEmail(email))
            {
                ep.SetError(txt_Email, "Email không hợp lệ");
                return false;
            }
            else
                ep.Clear();
            if (email.Equals(""))
            {
                ep.SetError(txt_Email, "Email không để trống");
                return false;
            }
            else
                ep.Clear();
            if (!nvbll.isSDT(sdt))
            {
                ep.SetError(txt_SDT, "Số điện thoại không họp lệ");
                return false;

            }
            else
                ep.Clear();
            if (sdt.Equals(""))
            {
                ep.SetError(txt_SDT, "Số điện thoại không để trống");
                return false;
            }
            else
                ep.Clear();
            if (vtut.Equals(""))
            {
                ep.SetError(txt_SDT, "Vị trí ứng tuyển không để trống");
                return false;
            }
            else
                ep.Clear();
            if (namkn.Equals(""))
            {
                ep.SetError(txt_NamKN, "Năm kinh nghệm không để trống");
                return false;
            }
            else
                ep.Clear();
            if (!nvbll.isNamKN(namkn))
            {
                ep.SetError(txt_NamKN, "Năm kinh nghệm từ 1 đến 40 năm");
                return false;
            }
            else
                ep.Clear();
            if (duong.Equals(""))
            {
                ep.SetError(txt_DuongNV, "Đường không để trống");
                return false;

            }
            else
                ep.Clear();

            if (phuong_xa.Equals(""))
            {
                ep.SetError(txt_Phuong_XaNV, "Phường/Xã không để trống");
                return false;
            }
            else
                ep.Clear();
            if (quan_huyen.Equals(""))
            {
                ep.SetError(txt_Quan_HuyenNV, "Quận/Huyện không để trống");
                return false;
            }
            else
                ep.Clear();
            if (tinh_tp.Equals(""))
            {
                ep.SetError(txt_Phuong_XaNV, "Tỉnh/Thành phố không để trống");
                return false;
            }
            else
                ep.Clear();
            if (dgv.Rows.Count == 0)
            {
                this.Message("Danh sách chứng chỉ không để trống", MyMessageBox.enmType.Error);
                return false;
            }
            return true;
        }

        private void stBtn_ResetAll_Click(object sender, EventArgs e)
        {
            ba.clearTextBoxs(this.Controls);
            listCC.Clear();
            txt_CCCD.Focus();
            rdo_Nam.Checked = true;
            dgv_CC.Rows.Clear();
            
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_MaCC.Text = "";
            txt_TenCC.Text = "";
            dt_NgayCap.Value = DateTime.Now;
            txt_Duong_CC.Text = "";
            txt_Phuong_XaCC.Text = "";
            txt_Quan_Huyen_CC.Text = "";
            txt_Tinh_TPCC.Text = "";
            txt_MaCC.Focus();
        }

        private void tsBtn_Them_Click(object sender, EventArgs e)
        {
            string macc = txt_MaCC.Text.Trim();
            string tencc = txt_TenCC.Text.Trim();
            string duong = txt_Duong_CC.Text.Trim();
            string ph_xa = txt_Phuong_XaCC.Text.Trim();
            string q_hy = txt_Quan_Huyen_CC.Text.Trim();
            string t_tp = txt_Tinh_TPCC.Text.Trim();
            DateTime nc = dt_NgayCap.Value;
            DiaChi dc = nvbll.getDiaChi(duong,ph_xa,q_hy,t_tp);
            ChungChi cc = nvbll.getChungChi(macc, tencc, nc,dc);

            if (isThongTinCC_Insert(macc, tencc, nc, duong, ph_xa, q_hy, t_tp))
            {
                listCC.Add(cc);
                dgv_CC.Rows.Add(macc,tencc,nc.ToShortDateString());
            }
        }

        private void tsBtn_Xoa_Click(object sender, EventArgs e)
        {
            string macc = txt_MaCC.Text.Trim();
            if (macc.Equals(""))
            {
                ep.SetError(txt_MaCC, "Mã chứng chỉ không để trống !");
                return;
            }
            else
            {
                ep.Clear(); 
            }
            for(int i = 0; i < dgv_CC.Rows.Count; i++)
            {
                if (dgv_CC.Rows[i].Cells[0].Value.ToString() == macc)
                {
                    dgv_CC.Rows.RemoveAt(i); listCC.RemoveAt(i);  break;
                }
            }
        }

        private void tsBtn_Sua_Click(object sender, EventArgs e)
        {
            string macc = txt_MaCC.Text.Trim();
            string tencc = txt_TenCC.Text.Trim();
            string duong = txt_Duong_CC.Text.Trim();
            string ph_xa = txt_Phuong_XaCC.Text.Trim();
            string q_hy = txt_Quan_Huyen_CC.Text.Trim();
            string t_tp = txt_Tinh_TPCC.Text.Trim();
            DateTime nc = dt_NgayCap.Value;

            if (isThongTinCC_Update(macc, tencc, nc, duong, ph_xa, q_hy, t_tp))
            {
                int vt = 0;
                for (int i = 0; i < listCC.Count; i++)
                {
                    if (listCC[i].MaCC == macc)
                    {
                        vt = i; break;
                    }
                }
                dgv_CC.Rows[vt].Cells[0].Value = macc;
                dgv_CC.Rows[vt].Cells[1].Value = tencc;
                dgv_CC.Rows[vt].Cells[2].Value = nc.ToShortDateString();
                listCC[vt].TenCC = tencc;
                listCC[vt].NgayCap = ba.ConvertDateTime_Epoch(nc);
                listCC[vt].DiaChi.Duong = duong;
                listCC[vt].DiaChi.Phuong_Xa = ph_xa;
                listCC[vt].DiaChi.Quan_Huyen = q_hy;
                listCC[vt].DiaChi.Tinh_TP = t_tp;
            }
        }

        private void TaoNhanVien_Load(object sender, EventArgs e)
        {
            rdo_Nam.Checked = true;

            
        }
        public void TimNoiCap(string macc)
        {
            int vt = 0;
            for(int i=0;i<listCC.Count;i++)
            {
                if (listCC[i].MaCC== macc)
                {
                    vt = i; break;
                }
            }
            txt_Duong_CC.Text = listCC[vt].DiaChi.Duong;
            txt_Phuong_XaCC.Text = listCC[vt].DiaChi.Phuong_Xa;
            txt_Quan_Huyen_CC.Text = listCC[vt].DiaChi.Quan_Huyen;
            txt_Tinh_TPCC.Text = listCC[vt].DiaChi.Tinh_TP;
        }
        private void dgv_CC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgv_CC.Rows[e.RowIndex];
            txt_MaCC.Text = row.Cells[0].Value.ToString();
            txt_TenCC.Text = row.Cells[1].Value.ToString();
            dt_NgayCap.Text = row.Cells[2].Value.ToString();
            TimNoiCap(row.Cells[0].Value.ToString());
        }

        private void btn_ThemNV_Click(object sender, EventArgs e)
        {
            string cccd = txt_CCCD.Text.Trim();
            string hoten = txt_HoTen.Text.Trim();
            string gt = "";
            if(rdo_Nam.Checked)
            {
                gt = "Nam";
            }
            if (rdo_Nu.Checked)
            {
                gt = "Nữ";
            }
            string sdt = txt_SDT.Text.Trim();
            string email = txt_Email.Text.Trim();
            string vtut = txt_VTUT.Text.Trim();
            int namkn = int.Parse(txt_NamKN.Text.Trim());
            DateTime ngaysinh = dt_NgaySinh.Value;
            string dg = txt_DuongNV.Text.Trim();
            string ph_xa = txt_Phuong_XaNV.Text.Trim();
            string q_hy = txt_Quan_HuyenNV.Text.Trim();
            string t_tp = txt_Tinh_TPNV.Text.Trim();
            DiaChi dc = nvbll.getDiaChi(dg,ph_xa,q_hy,t_tp);
            byte[] anh = ba.ConvertImgToByte(pic_Img.Image);
            if(isThongTinNV(cccd, hoten, ngaysinh, sdt, email, vtut, namkn, dg, ph_xa, q_hy, t_tp,dgv_CC))
            {
                string manv = nvbll.TaoMaNV();
                nvbll.InsertNV(manv, cccd, hoten, ngaysinh, gt,sdt, email, vtut, namkn, anh, dc, listCC);
                this.Message("Success", MyMessageBox.enmType.Success);
            }
        }

        private void btn_UpImage_Click(object sender, EventArgs e)
        {
            ba.UpLoadImage(pic_Img);
        }

        private void txt_CCCD_TextChanged(object sender, EventArgs e)
        {
            lbl_LenCCCD.Text = txt_CCCD.Text.Count().ToString();
        }

        private void txt_SDT_TextChanged(object sender, EventArgs e)
        {
            lbl_LenSDT.Text = txt_SDT.Text.Count().ToString();
        }


        private void tsBtn_QuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main frm = new Main();
            frm.LoadTK(tsBtn_ThongTinTK.Text);
            frm.ShowDialog();
            this.Close();
        }

    }
}
