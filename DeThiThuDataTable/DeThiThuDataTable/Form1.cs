using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
namespace DeThiThuDataTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        int hangHienTai = -1;

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDiemTB.Enabled = false;
            AnHien(false);
            Xoatext();
            if (TongSoHangDuLieu() > 0) hangHienTai = 0;
            GiaTriHang(hangHienTai);
            
        }

        private void Xoatext()
        {
            txtDiemHS1.Text = txtDiemHS2.Text = txtDiemTB.Text = txtHoTen.Text = txtMaSo.Text = dateNgaySinh.Text = "";
        }

        private void AnHien(bool a)
        {
            txtMaSo.Enabled = txtHoTen.Enabled = dateNgaySinh.Enabled = txtDiemHS2.Enabled = txtDiemHS1.Enabled = 
                btnHuy.Enabled = btnLuu.Enabled = a;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnDau.Enabled = btnCuoi.Enabled = btnLui.Enabled =
                btnTien.Enabled = !a;
        }

        private string DiemTrungBinh(string diemhs1, string diemhs2)
        {
            try
            {
                int diem1 = Convert.ToInt32(diemhs1);
                int diem2 = Convert.ToInt32(diemhs2);
                return ((diem1 + 2 * diem2) / 3).ToString();
            }
            catch (Exception)
            {

            }
            return "";
        }

        private string CatKyTuTrong(string ten)
        {
            return Regex.Replace(ten, @"\s+", " ").Trim();
        }

        private void GiaTriHang(int h)
        {
            if (h >= 0)
            {
                DataTable dt = KetNoi.table("Select * from diem");
                txtMaSo.Text = dt.Rows[h][0].ToString();
                string ten = dt.Rows[h][1].ToString();
                txtHoTen.Text = CatKyTuTrong(ten);
                dateNgaySinh.Text = dt.Rows[h][2].ToString();
                txtDiemHS1.Text = dt.Rows[h][3].ToString();
                txtDiemHS2.Text = dt.Rows[h][4].ToString();
                txtDiemHS1.Text = DiemTrungBinh(txtDiemHS1.Text, txtDiemHS2.Text);
                txtDiemTB.Text = DiemTrungBinh(txtDiemHS1.Text, txtDiemHS2.Text);
            }
            else
            {
                MessageBox.Show("Dữ liệu rỗng");
                Xoatext();
            }
        }
        
        private int TongSoHangDuLieu()
        {
            return KetNoi.table("Select * from diem").Rows.Count;
        }  

        private void btnDau_Click(object sender, EventArgs e)
        {
            if (TongSoHangDuLieu() > 0)
            {
                hangHienTai = 0;
                GiaTriHang(hangHienTai);
            }
        }

        private void btnCuoi_Click(object sender, EventArgs e)
        {
            if (TongSoHangDuLieu() > 0)
            {
                hangHienTai = TongSoHangDuLieu() - 1;
                GiaTriHang(hangHienTai);
            }
        }

        private void btnLui_Click(object sender, EventArgs e)
        {
            hangHienTai--;
            if (hangHienTai >= 0 && TongSoHangDuLieu() > 0) GiaTriHang(hangHienTai);
            else hangHienTai = 0;
        }

        private void btnTien_Click(object sender, EventArgs e)
        {
            hangHienTai++;
            if (hangHienTai < TongSoHangDuLieu()) GiaTriHang(hangHienTai);
            else hangHienTai = TongSoHangDuLieu() - 1;
        }

        bool flag = true;
        private void btnThem_Click(object sender, EventArgs e)
        {
            Xoatext(); AnHien(true);
            flag = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AnHien(true);
            flag = false;
            txtMaSo.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (hangHienTai >= 0)
            {
                string truyVan = string.Format("DELETE FROM diem WHERE (masv = '{0}')", txtMaSo.Text.Trim());
                if (KetNoi.CapNhat(truyVan))
                {
                    MessageBox.Show("Xóa thành công");
                    hangHienTai--;
                    GiaTriHang(hangHienTai);
                }
                else MessageBox.Show("Xóa thất bại");
            }
            else MessageBox.Show("Không còn dữ liệu để xóa");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (GanGiaTri())
            {
                if (flag)
                {
                    string truyVan = string.Format("INSERT INTO diem (masv, hoten, ngaysinh, diemhs1, diemhs2) VALUES"
                        + "('{0}', '{1}', '{2}', {3}, {4})", ma, hoTen, ngaySinh, diemHS1, diemHS2);
                    if(KetNoi.CapNhat(truyVan))
                    {
                        hangHienTai = TongSoHangDuLieu() - 1;
                        GiaTriHang(hangHienTai);
                        AnHien(false);
                        MessageBox.Show("Thêm thành công");
                    }
                    else MessageBox.Show("Thêm thất bại");
                }
                else
                {
                    string truyVan = string.Format("UPDATE diem SET  masv = '{0}', hoten = '{1}', ngaysinh = '{2}'," + 
                        " diemhs1 = {3}, diemhs2 = {4} WHERE (masv = '{0}')", ma, hoTen, ngaySinh, diemHS1, diemHS2);
                    if(KetNoi.CapNhat(truyVan))
                    {
                        GiaTriHang(hangHienTai);
                        AnHien(false);
                        MessageBox.Show("Sửa thành công");
                    }
                    else MessageBox.Show("Sửa thất bại");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            GiaTriHang(hangHienTai);
            AnHien(false);
        }

        string ma, hoTen, ngaySinh;
        int diemHS1, diemHS2;
        private bool GanGiaTri()
        {
            if (txtMaSo.Text != "" && txtHoTen.Text != "" && txtDiemHS1.Text != "" && txtDiemHS2.Text != "")
            {
                try
                {
                    ma = txtMaSo.Text;
                    hoTen = CatKyTuTrong(txtHoTen.Text);
                    ngaySinh = dateNgaySinh.Text;
                    diemHS1 = Convert.ToInt32(txtDiemHS1.Text.Trim());
                    diemHS2 = Convert.ToInt32(txtDiemHS2.Text.Trim());
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Nhập dữ liệu không đúng");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Nhập dữ liệu không đúng");
                return false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int x = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblPhieuNhapDiem.Left += x;
            if (lblPhieuNhapDiem.Left <= 0 || lblPhieuNhapDiem.Right >= Width) x = -x;
        }
    }
}
