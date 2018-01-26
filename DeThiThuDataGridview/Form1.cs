using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeThiThuDataGridview
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            string cm = "SELECT masv, hoten, ngaysinh, diemhs1, diemhs2, ROUND((diemhs1 + 2 * diemhs2) / 3, 2) AS diemtb FROM diem";
            DataTable dt = KetNoi.Table(cm);
            dgvDanhSach.DataSource = dt;
            dgvDanhSach_Click(sender, e);
            txtDiemTB.Enabled = false;
            AnHien(true);
        }

        private void AnHien(bool k)
        {
            txtMaSo.Enabled = txtHoTen.Enabled = txtDiemHS1.Enabled = txtDiemHS2.Enabled = btnLuu.Enabled = 
                btnHuy.Enabled =dateNgaySinh.Enabled = !k;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = k;
        }

        private void dgvDanhSach_Click(object sender, EventArgs e)
        {
            int hang = dgvDanhSach.CurrentRow.Index;
            txtMaSo.Text = dgvDanhSach.Rows[hang].Cells[0].Value.ToString();
            txtHoTen.Text = CatKyTuTrong(dgvDanhSach.Rows[hang].Cells[1].Value.ToString());
            dateNgaySinh.Text = dgvDanhSach.Rows[hang].Cells[2].Value.ToString();
            txtDiemHS1.Text = dgvDanhSach.Rows[hang].Cells[3].Value.ToString();
            txtDiemHS2.Text = dgvDanhSach.Rows[hang].Cells[4].Value.ToString();
            txtDiemTB.Text = dgvDanhSach.Rows[hang].Cells[5].Value.ToString();
        }

        private string CatKyTuTrong(string s)
        {
            while (s.IndexOf("  ") != -1)
            {
                s = s.Remove(s.IndexOf("  "), 1);
            }
            return s.Trim();
        }

        private void XoaText()
        {
            txtMaSo.Text = txtHoTen.Text = txtDiemHS1.Text = txtDiemHS2.Text = txtDiemTB.Text = dateNgaySinh.Text = string.Empty;
        }

        bool flag;
        private void btnThem_Click(object sender, EventArgs e)
        {
            AnHien(false);
            XoaText();
            flag = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AnHien(false);
            flag = false;
            txtMaSo.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaSo.Text != "" && txtHoTen.Text != "" && txtDiemHS1.Text != "" && txtDiemHS2.Text != "")
            {
                if (flag)
                {
                    try
                    {
                        string chuoiTruyVan = string.Format("INSERT INTO diem (masv, hoten, ngaysinh, diemhs1, diemhs2)" + 
                            " VALUES ('{0}', '{1}', '{2}', {3}, {4})",txtMaSo.Text, txtHoTen.Text, dateNgaySinh.Text, 
                            txtDiemHS1.Text, txtDiemHS2.Text);
                        KetNoi.CapNhat(chuoiTruyVan);
                        MessageBox.Show("Thêm thành công");
                        Form1_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    

                }
                else
                {
                    try
                    {
                        string chuoiTruyVan = string.Format("UPDATE diem SET masv = '{0}', hoten = '{1}', ngaysinh = '{2}',"+
                            " diemhs1 = {3}, diemhs2 = {4} WHERE (masv = '{0}')",
                            txtMaSo.Text, txtHoTen.Text, dateNgaySinh.Text, txtDiemHS1.Text, txtDiemHS2.Text);
                        KetNoi.CapNhat(chuoiTruyVan);
                        MessageBox.Show("Sửa thành công");
                        Form1_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int hang = dgvDanhSach.CurrentRow.Index;
            if (hang >= 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu đang chọn", "Thông báo", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string chuoiTruyVan = string.Format("DELETE FROM diem WHERE (masv = '{0}')", txtMaSo.Text);
                    KetNoi.CapNhat(chuoiTruyVan);
                    MessageBox.Show("Xóa thành công");
                    Form1_Load(sender, e);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int x = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblPhieuNhapDiem.Left += x;
            if (lblPhieuNhapDiem.Left <= 0 || lblPhieuNhapDiem.Right > Width) x = -x;
        }


    }
}
