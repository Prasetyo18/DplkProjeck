using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DPLKCORE.Framework;

namespace DPLKCORE.Framework
{
    public class Connection
    {
        protected String query;
        protected Database db;
        protected DatabaseOledb dbExcel;
        private static string mDefaultConnStr = null;

        public String ConnectionStringDBJiwa = ConfigurationManager.ConnectionStrings["connDBJiwa"].ToString();
        public String ConnectionStringPension = ConfigurationManager.ConnectionStrings["connPension"].ToString();


        
        public static String ConnectionExcel
        {
            get
            {
                return "provider=microsoft.ace.oledb.12.0; data source={0}; extended properties=\"Excel 12.0;HDR=NO;IMEX=1\";";
            }
        }

        protected void InitDBExcel(String filePath)
        {
            if (dbExcel == null || dbExcel.Conn.State == System.Data.ConnectionState.Closed)
            {
                dbExcel = new DatabaseOledb(String.Format(ConnectionExcel, filePath));
            }
        }

        public static SqlConnection GetDBConnection()
        {
            return new SqlConnection(mDefaultConnStr);
        }


        public DataTable isiTabel(string q)
        {
            SqlCommand kueri = new SqlCommand();
            SqlConnection koneksi = new SqlConnection();
            SqlDataAdapter ad = new SqlDataAdapter();
            System.Data.DataTable tabel = new System.Data.DataTable();

            kueri.CommandText = q;
            kueri.Connection = GetDBConnection();
            ad.SelectCommand = kueri;
            ad.Fill(tabel);
            ad.Dispose();

            return tabel;
        }
    }
}