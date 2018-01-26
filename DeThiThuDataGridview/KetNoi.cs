using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace DeThiThuDataGridview
{
    class KetNoi
    {
        public static OleDbConnection con = new OleDbConnection();
        public static OleDbDataAdapter da;
        public static OleDbCommand cmd;

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

        public static DataTable Table(string chuoiTruyVan)
        {
            MoKetNoi();
            DataTable dt = new DataTable();
            cmd = new OleDbCommand(chuoiTruyVan, con);
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            DongKetNoi();
            return dt;
        }

        public static void CapNhat(string chuoiTruyVan)
        {
            MoKetNoi();
            cmd = new OleDbCommand(chuoiTruyVan, con);
            cmd.ExecuteNonQuery();
            DongKetNoi();
        }
    }
}
