using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DeThiThu
{
    class KetNoi
    {
        public static OleDbConnection con = new OleDbConnection();

        public static void Moketnoi()
        {
            if (con.State == ConnectionState.Open) con.Close();
            con.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= qldiem.mdb";
            con.Open();
           // MessageBox.Show(con.State.ToString());
        }
    }
}
