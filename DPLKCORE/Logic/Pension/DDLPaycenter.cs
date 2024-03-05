using DPLKCORE.Class.Pension;
using DPLKCORE.Class;
using DPLKCORE.Form.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;

namespace DPLKCORE.Logic.Pension
{
    public class DDLPaycenter
    {
        public  String ClientNmbr { get; set; }
        public  String CompanyNm { get; set; }

        public void LoadDataParamToDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLPaycenter m = new DDLPaycenter();
            List<DDLPaycenter> data = new List<DDLPaycenter>();

            data.AddRange(this.GetParamDDL(db));

            ddl.DataSource = data;
            ddl.DataValueField = "ClientNmbr";
            ddl.DataTextField = "CompanyNm";
            ddl.DataBind();
        }


        public List<DDLPaycenter> GetParamDDL(Database db)
        {
            List<DDLPaycenter> data = new List<DDLPaycenter>();

            String query = "EXEC  DDL_COMPANY_ON_SCR_PAYCENTER ";



            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLPaycenter m = new DDLPaycenter();


                    if (reader["client_nmbr"] != DBNull.Value)
                        m.ClientNmbr = reader["client_nmbr"].ToString().Trim();

                    if (reader["company_nm"] != DBNull.Value)
                        m.CompanyNm = reader["company_nm"].ToString().Trim();


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