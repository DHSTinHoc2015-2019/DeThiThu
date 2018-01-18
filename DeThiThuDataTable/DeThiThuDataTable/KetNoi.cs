using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace DeThiThuDataTable
{
    class KetNoi
    {
        public static OleDbConnection con = new OleDbConnection();
        public static OleDbCommand cmd = new OleDbCommand();
        public static OleDbDataAdapter da;

        public static void MoKetNoi()
        {
            con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=qldiem.mdb";
            if (con.State == ConnectionState.Open) con.Close();
            con.Open();
        }

        public static void DongKetNoi()
        {
            con.Close();
        }

        public static DataTable table(string chuoiTruyVan)
        {
            DataTable dt = new DataTable();
            cmd.CommandText = chuoiTruyVan;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                MoKetNoi();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                DongKetNoi();
            }
            return dt;
        }

        public static bool CapNhat(string chuoiTruyVan)
        {
            cmd.CommandText = chuoiTruyVan;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                MoKetNoi();
                cmd.ExecuteNonQuery();
                DongKetNoi();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmd.Dispose();
                DongKetNoi();
            }
            return false;
        }
    }
}
