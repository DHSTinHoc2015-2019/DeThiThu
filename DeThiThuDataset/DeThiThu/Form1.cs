using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DeThiThu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommandBuilder cb;
        OleDbCommand cmd;

        private void Form1_Load(object sender, EventArgs e)
        {
            KetNoi.Moketnoi();
            cmd = new OleDbCommand("select * from diem", KetNoi.con);
            ds.Clear();
            da.SelectCommand = cmd;
            da.Fill(ds, "diem");
            cb = new OleDbCommandBuilder(da);

            txtMaSo.DataBindings.Clear();
            txtMaSo.DataBindings.Add("Text", ds, "diem.masv");
            txtHoTen.DataBindings.Clear();
            txtHoTen.DataBindings.Add("Text", ds, "diem.hoten");
            dateNgaySinh.DataBindings.Clear();
            dateNgaySinh.DataBindings.Add("Text", ds, "diem.ngaysinh");
            txtDiemHS1.DataBindings.Clear();
            txtDiemHS1.DataBindings.Add("Text", ds, "diem.diemhs1");
            txtDiemHS2.DataBindings.Clear();
            txtDiemHS2.DataBindings.Add("Text", ds, "diem.diemhs2");

            txtDiemTB.Enabled = txtDiemThi.Enabled = false;
            txtHoTen.Text = CatKyTuTrong(txtHoTen.Text);
            txtDiemTB.Text = DiemTrungBinh(txtDiemHS1.Text.Trim(), txtDiemHS2.Text.Trim());
            Anhien(false);
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

        private string CatKyTuTrong(string chuoi)
        {
            string s = chuoi.Trim();
            int i = 0;
            while (i < s.Length - 1)
            {
                if (s[i] == ' ' && s[i + 1] == ' ') s = s.Remove(i + 1, 1);
                else i++;
            }
            return s;
        }

        private void Anhien(bool a)
        {
            txtMaSo.Enabled = txtHoTen.Enabled = dateNgaySinh.Enabled = txtDiemHS2.Enabled = 
                txtDiemHS1.Enabled = btnHuy.Enabled = btnLuu.Enabled = a;
            btnThem.Enabled = btnSua.Enabled = !a;
        }

        private void Xoatext()
        {
            txtDiemHS1.Text = txtDiemHS2.Text = txtDiemTB.Text = txtDiemThi.Text = txtHoTen.Text = txtMaSo.Text 
                = dateNgaySinh.Text = "";
        }

        private void btnDau_Click(object sender, EventArgs e)
        {
            BindingContext[ds, "diem"].Position = 0;
            txtHoTen.Text = CatKyTuTrong(txtHoTen.Text);
            txtDiemTB.Text = DiemTrungBinh(txtDiemHS1.Text.Trim(), txtDiemHS2.Text.Trim());
        }

        private void btnCuoi_Click(object sender, EventArgs e)
        {
            BindingContext[ds, "diem"].Position = BindingContext[ds, "diem"].Count - 1;
            txtHoTen.Text = CatKyTuTrong(txtHoTen.Text);
            txtDiemTB.Text = DiemTrungBinh(txtDiemHS1.Text.Trim(), txtDiemHS2.Text.Trim());
        }

        private void btnLui_Click(object sender, EventArgs e)
        {
            BindingContext[ds, "diem"].Position--;
            txtHoTen.Text = CatKyTuTrong(txtHoTen.Text);
            txtDiemTB.Text = DiemTrungBinh(txtDiemHS1.Text.Trim(), txtDiemHS2.Text.Trim());
        }

        private void btnTien_Click(object sender, EventArgs e)
        {
            BindingContext[ds, "diem"].Position++;
            txtHoTen.Text = CatKyTuTrong(txtHoTen.Text);
            txtDiemTB.Text = DiemTrungBinh(txtDiemHS1.Text.Trim(), txtDiemHS2.Text.Trim());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Anhien(true); btnSua.Enabled = true; btnXoa.Enabled = false;
            Xoatext();
            BindingContext[ds, "diem"].AddNew();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Anhien(true); btnSua.Enabled = true; btnXoa.Enabled = false; txtMaSo.Enabled = false;
            BindingContext[ds, "diem"].EndCurrentEdit();
            da.Update(ds, "diem");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa dữ liệu đã chọn?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (BindingContext[ds, "diem"].Position >= 0)
                {
                    BindingContext[ds, "diem"].RemoveAt(BindingContext[ds, "diem"].Position);
                    da.Update(ds, "diem");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Anhien(false); btnXoa.Enabled = true;
            BindingContext[ds, "diem"].EndCurrentEdit();
            da.Update(ds, "diem");
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Anhien(false); btnXoa.Enabled = true;
            BindingContext[ds, "diem"].CancelCurrentEdit();
            Form1_Load(sender, e);
        }

        int x = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblPhieuNhapDiem.Left += x;
            if (lblPhieuNhapDiem.Left <= 0 || lblPhieuNhapDiem.Right >= Width) x = -x;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
