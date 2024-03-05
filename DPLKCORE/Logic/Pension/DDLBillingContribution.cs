using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class DDLBillingContribution
    {
        public string ModeNmbr { get; set; }
        public string ModeNm { get; set; }

        public string GroupNmbr { get; set; }
        public string GroupNm { get; set; }

        public string CompanyNmbr { get; set; }
        public string CompanyNm { get; set; }

        public string PaycenterNmbr { get; set; }


        public DataTable LoadDgSuspense(Database db)
        {
            string query = "EXEC sp_grp_suspense_r_NEW @group_nmbr, @paycenter_nmbr";
            DataTable result;
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr",this.GroupNmbr);
                db.AddParameter("@paycenter_nmbr", this.PaycenterNmbr);

                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public void LoadDDLMode(Database db, DropDownList ddl)
        {
            List<DDLBillingContribution> data = GetModes(db);
            ddl.DataSource = data;
            ddl.DataTextField = "ModeNm";
            ddl.DataValueField = "ModeNmbr";
            ddl.DataBind();
        }

        public List<DDLBillingContribution> GetModes(Database db)
        {
            List<DDLBillingContribution> output = new List<DDLBillingContribution>();
            string query = "select '','' union select '0','Bill' union select '1','Contribution' union select '2','Sporadic Rollover / Top up'";
            try
            {
                db.setQuery(query);
                DbDataReader result = db.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DDLBillingContribution obj = new DDLBillingContribution();

                        if (result[0] != DBNull.Value)
                        {
                            obj.ModeNmbr = result[0].ToString();
                        }
                        if (result[1] != DBNull.Value)
                        {
                            obj.ModeNm = result[1].ToString();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return output;
        }

        public void GetDDLGroup(Database db, DropDownList ddl)
        {

        }

        public void LoadDDLCompany(Database db, DropDownList ddl)
        {
            List<DDLBillingContribution> data = GetCompanies(db);
            ddl.DataSource = data;
            ddl.DataTextField = "CompanyNm";
            ddl.DataValueField = "CompanyNmbr";
            ddl.DataBind();
        }

        public List<DDLBillingContribution> GetCompanies(Database db)
        {
            List<DDLBillingContribution> output = new List<DDLBillingContribution>();
            string query = "EXEC DDL_COMPANY_ON_SCR_GROUP_NEW";
            try
            {
                db.setQuery(query);
                DbDataReader result = db.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DDLBillingContribution obj = new DDLBillingContribution();

                        if (result[0] != DBNull.Value)
                        {
                            obj.CompanyNmbr = result[0].ToString();
                        }
                        if (result[1] != DBNull.Value)
                        {
                            obj.CompanyNm = result[1].ToString();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return output;
        }

        public void LoadDDLGroup(Database db, DropDownList ddl, string CompanyNmbr)
        {
            List<DDLBillingContribution> data = GetGroups(db, CompanyNmbr);
            ddl.DataSource = data;
            ddl.DataTextField = "GroupNm";
            ddl.DataValueField = "GroupNmbr";
            ddl.DataBind();
        }

        private List<DDLBillingContribution> GetGroups(Database db, string CompanyNmbr)
        {
            List<DDLBillingContribution> output = new List<DDLBillingContribution>();
            string query = "EXEC DDL_GROUP_ON_SCR_TRANSACTIONBILLCONTRIBUTION_NEW @company_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@company_nmbr", CompanyNmbr);
                DbDataReader result = db.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        DDLBillingContribution obj = new DDLBillingContribution();

                        if (result[0] != DBNull.Value)
                        {
                            obj.GroupNmbr = result[0].ToString();
                        }
                        if (result[1] != DBNull.Value)
                        {
                            obj.GroupNm = result[1].ToString();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return output;
        }
    }
}