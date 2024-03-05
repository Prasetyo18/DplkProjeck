using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class DDLCertificateMovement
    {
        public string GroupNmbr { get; set; }
        public string CompanyName { get; set; }

        public string CycleDate { get; set; }

        public static string LoadTransactionDate(Database db)
        {
            string output = "";
            string query = "select dbo.fndisplaydate(cycle_dt) as cycle_dt from cycle";
            try
            {
                db.Open();
                db.setQuery(query);
                output = db.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return output;
        }


        public void LoadCompanyDDL(Database db, DropDownList ddl)
        {
            List<DDLCertificateMovement> data = GetCompanies(db);
            ddl.DataTextField = "CompanyName";
            ddl.DataValueField = "GroupNmbr";
            ddl.DataSource = data;
            ddl.DataBind();
        }

        public List<DDLCertificateMovement> GetCompanies(Database db)
        {
            List<DDLCertificateMovement> output = new List<DDLCertificateMovement>();
            string query = "select group_nmbr, company_nm from Group_info gi join company co on gi.client_nmbr = co.client_nmbr order by co.company_nm asc";
            try
            {
                db.Open();
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLCertificateMovement obj = new DDLCertificateMovement();
                        if (reader["group_nmbr"] != null)
                        {
                            obj.GroupNmbr = reader["group_nmbr"].ToString();
                        }

                        if (reader["company_nm"] != null)
                        {
                            obj.CompanyName = reader["company_nm"].ToString();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return output;
        }
    }
}