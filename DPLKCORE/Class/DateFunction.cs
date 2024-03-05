using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Class
{
    public class DateFunction {
        public Int32 Bulan { get; set; }
        public String NamaBulan { get; set; }
        public Int32 Tahun { get; set; }

        public static String GetNamaBulan(int angka) {
            String namaBulan = "";
            List<String> listBulan = new List<String>();

            listBulan.Add("Januari");
            listBulan.Add("Februari");
            listBulan.Add("Maret");
            listBulan.Add("April");
            listBulan.Add("Mei");
            listBulan.Add("Juni");
            listBulan.Add("Juli");
            listBulan.Add("Agustus");
            listBulan.Add("September");
            listBulan.Add("Oktober");
            listBulan.Add("November");
            listBulan.Add("Desember");

            if(angka!=0)
            {
                namaBulan = listBulan[angka - 1];
            }
            return namaBulan;
        }

        public static void PopulateBulan(DropDownList ddl) {
            List<DateFunction> data = new List<DateFunction>();

            for (int i = 1; i <= 12; i++) {
                DateFunction df = new DateFunction();
                df.Bulan = i;
                df.NamaBulan = GetNamaBulan(i);

                data.Add(df);
            }

            ddl.DataSource = data;
            ddl.DataTextField = "NamaBulan";
            ddl.DataValueField = "Bulan";
            ddl.DataBind();
        }

        public static void PopulateTahun(DropDownList ddl)
        {
            List<DateFunction> data = new List<DateFunction>();
            int year = DateTime.Now.Year;
            for (int i = year - 5; i <= year + 1; i++)
            {
                DateFunction df = new DateFunction();
                df.Tahun = i;

                data.Add(df);
            }

            ddl.DataSource = data;
            ddl.DataTextField = "Tahun";
            ddl.DataValueField = "Tahun";
            ddl.DataBind();
        }
    }
}