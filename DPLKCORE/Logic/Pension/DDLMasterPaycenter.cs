using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Class;
using DPLKCORE.Class.Pension;
using System.Data;
using System.Data.SqlClient;

namespace DPLKCORE.Logic.Pension
{
    public class DDLMasterPaycenter
    {
        public int MasterPayCenterNumber { get; set; }
        public string MasterPayCenterName { get; set; }

        public void LoadDataParamToDDL(DropDownList ddl, Database db, int comCode)
        {
            List<DDLMasterPaycenter> data = new List<DDLMasterPaycenter>();
            data.AddRange(this.GetMasterPaycenter(db, comCode));

            ddl.DataSource = data;
            ddl.DataValueField = "MasterPayCenterNumber";
            ddl.DataTextField = "MasterPayCenterName";
            ddl.DataBind();
        }

        private List<DDLMasterPaycenter> GetMasterPaycenter(Database db, int companyCode)
        {
            List<DDLMasterPaycenter> output = new List<DDLMasterPaycenter>();
            //follow the previous app to populate masterpaycenter according to the companyName
            String query = "EXEC DDL_MASTER_PAYCENTER_ON_SCR_PAYCENTER @client_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@client_nmbr", companyCode);

                DataTable result = db.ExecuteQuery();
                foreach (DataRow item in result.Rows)
                {
                    DDLMasterPaycenter obj = new DDLMasterPaycenter
                    {
                        MasterPayCenterNumber = Convert.ToInt32(item["paycenter_nmbr"]),
                        MasterPayCenterName = item["paycenter_nm"].ToString().Trim()
                    };
                    output.Add(obj);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            finally
            {
                db.Close();
            }

            return output;
        }
    }
}