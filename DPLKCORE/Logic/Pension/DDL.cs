using DPLKCORE.Class.Pension;
using DPLKCORE.Class;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace DPLKCORE.Class.Pension
{
    public class DDL 
    {
        public String MnySrcType { get; set; }
        public String MnySrcNm { get; set; }


        public void LoadDataParamToDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDL m = new DDL();
            List<DDL> data = new List<DDL>();

            data.AddRange(this.GetParamDDL(db));

            ddl.DataSource = data;
            ddl.DataValueField = "MnySrcType";
            ddl.DataTextField = "MnySrcNm";
            ddl.DataBind();
        }


        public List<DDL> GetParamDDL(DPLKCORE.Framework.Database db)
        {
            List<DDL> data = new List<DDL>();

            String query = "EXEC DDL_PARAM 'mny_src' ";



            try
            {
                db.setQuery(query);

        
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDL m = new DDL();


                    if (reader["Mny_src_type"] != DBNull.Value)
                        m.MnySrcType = reader["Mny_src_type"].ToString().Trim();

                    if (reader["Mny_src_nm"] != DBNull.Value)
                        m.MnySrcNm = reader["Mny_src_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }







    }
}
