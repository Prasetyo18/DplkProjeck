using DPLKCORE.Class.Pension;
using DPLKCORE.Class;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DPLKCORE.Logic.Pension
{
    public class DDLClient
    {
        //Certificate Info Tab
        public string CertificateNmbr { get; set; }

        public string GroupNmbr { get; set; }
        public string CompanyNm { get; set; }

        public string PaycenterNmbr { get; set; }
        public string PaycenterNm { get; set; }

        public string PaymentTypeNmbr { get; set; }
        public string PaymentTypeNm { get; set; }

        public string CitizenshipNmbr { get; set; }
        public string CitizenshipNm { get; set; }

        public string PremiumTypeNmbr { get; set; }
        public string PremiumTypeNm { get; set; }

        public string RiderTypeNmbr { get; set; }
        public string RiderTypeNm { get; set; }

        public string PlanTypeNmbr { get; set; }
        public string PlanTypeNm { get; set; }

        public string AgentNmbr { get; set; }
        public string AgentNm { get; set; }

        public string CommisionTypeNmbr { get; set; }
        public string CommisionTypeNm { get; set; }

        public string MCPNmbr { get; set; }
        public string MCPNm { get; set; }

        public string FundsrcNmbr { get; set; }
        public string FundsrcNm { get; set; }

        public string StatusTypeNmbr { get; set; }
        public string StatusTypeNm { get; set; }

        public string InvTypeNmbr { get; set; }
        public string InvTypeNm { get; set; }

        public string MoneyTypeNmbr { get; set; }
        public string MoneyTypeNm { get; set; }


        public void LoadClientNm(Database db, TextBox textbox ,int ClientNmbr)
        {
            textbox.Text = GetClientNm(db, ClientNmbr);
        }

        private string GetClientNm(Database db, int ClientNmbr)
        {
            string output;
            string query = "EXEC TXT_CLIENTNAME_ON_SCR_CERTIFICATEINFO @client_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@client_nmbr", ClientNmbr);
                output = Convert.ToString(db.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return output;
        }


        public void LoadDDLMoneyType(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetMoneyTypeDDL(db);
            ddl.DataSource = data;
            ddl.DataTextField = "MoneyTypeNm";
            ddl.DataValueField = "MoneyTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetMoneyTypeDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'money_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["money_type_nmbr"] != DBNull.Value)
                        {
                            obj.MoneyTypeNmbr = reader["money_type_nmbr"].ToString().Trim();
                        }
                        if (reader["money_type_nm"] != DBNull.Value)
                        {
                            obj.MoneyTypeNm = reader["money_type_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }

        public void LoadInvestmentName(Database db, DropDownList ddl, string GroupNmbr)
        {
            List<DDLClient> data = GetInvNameDDL(db, GroupNmbr);
            ddl.DataSource = data;
            ddl.DataTextField = "InvTypeNm";
            ddl.DataValueField = "InvTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetInvNameDDL(Database db, string GroupNmbr)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_INVESTMENTNAME_ON_SCR_CERTIFICATEINFO @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", GroupNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["inv_type_nmbr"] != DBNull.Value)
                        {
                            obj.InvTypeNmbr = reader["inv_type_nmbr"].ToString().Trim();
                        }
                        if (reader["inv_type_nm"] != DBNull.Value)
                        {
                            obj.InvTypeNm = reader["inv_type_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }

        public void LoadRetirementAge(Database db, TextBox txtRetireAge, string GroupNmbr)
        {
            txtRetireAge.Text = GetRetireAge(db, GroupNmbr);
        }

        private string GetRetireAge(Database db, string GroupNmbr)
        {
            string output;
            string query = "EXEC TXT_RETIREMENT_AGE_ON_SCR_CERTIFICATEINFO @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", GroupNmbr);
                output = Convert.ToString(db.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }

        public void LoadDDLStatus(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetStatusDDL(db);
            ddl.DataSource = data;
            ddl.DataTextField = "StatusTypeNm";
            ddl.DataValueField = "StatusTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetStatusDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'status_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["status_type_nmbr"] != DBNull.Value)
                        {
                            obj.StatusTypeNmbr = reader["status_type_nmbr"].ToString().Trim();
                        }
                        if (reader["status_type_nm"] != DBNull.Value)
                        {
                            obj.StatusTypeNm = reader["status_type_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }


        public void LoadDDLFund(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetFundDDL(db);
            ddl.DataSource = data;
            ddl.DataTextField = "FundsrcNm";
            ddl.DataValueField = "FundsrcNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetFundDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'fund_src_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["fund_src_nmbr"] != DBNull.Value)
                        {
                            obj.FundsrcNmbr = reader["fund_src_nmbr"].ToString().Trim();
                        }
                        if (reader["fund_src_nm"] != DBNull.Value)
                        {
                            obj.FundsrcNm = reader["fund_src_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }

        public void LoadDDLMCP(Database db, DropDownList ddl, string GroupNmbr)
        {
            List<DDLClient> data = GetMCPDDL(db, GroupNmbr);
            ddl.DataSource = data;
            ddl.DataTextField = "MCPNm";
            ddl.DataValueField = "MCPNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetMCPDDL(Database db, string GroupNmbr)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_MBRCLSPLAN_ON_SCR_CERTIFICATEINFO @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", GroupNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["mcp_nmbr"] != DBNull.Value)
                        {
                            obj.MCPNmbr = reader["mcp_nmbr"].ToString().Trim();
                        }
                        if (reader["mcp_nm"] != DBNull.Value)
                        {
                            obj.MCPNm = reader["mcp_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }


        public void LoadDDLKomisi(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetDDLKomisi(db);
            ddl.DataSource = data;
            ddl.DataTextField = "CommisionTypeNm";
            ddl.DataValueField = "CommisionTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetDDLKomisi(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'commision_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["commision_type_nmbr"] != DBNull.Value)
                        {
                            obj.CommisionTypeNmbr = reader["commision_type_nmbr"].ToString().Trim();
                        }
                        if (reader["commision_type_nm"] != DBNull.Value)
                        {
                            obj.CommisionTypeNm = reader["commision_type_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }

        public void LoadDDLAgen(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetAgentDDL(db);
            ddl.DataSource = data;
            ddl.DataTextField = "AgentNm";
            ddl.DataValueField = "AgentNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetAgentDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'agent_master'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["agent_nmbr"] != DBNull.Value)
                        {
                            obj.AgentNmbr = reader["agent_nmbr"].ToString().Trim();
                        }
                        if (reader["agent_nm"] != DBNull.Value)
                        {
                            obj.AgentNm = reader["agent_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }


        public void LoadDDLJenisPlan(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetJenisPlanDDL(db);
            ddl.DataSource = data;
            ddl.DataTextField = "PlanTypeNm";
            ddl.DataValueField = "PlanTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetJenisPlanDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PLAN_RIDER " +
                "@jenis_premi, @jenis_rider";
            try
            {
                db.setQuery(query);
                db.AddParameter("@jenis_premi", this.PremiumTypeNmbr);
                db.AddParameter("@jenis_rider", this.RiderTypeNmbr);

                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["plan_type_nmbr"] != DBNull.Value)
                        {
                            obj.PlanTypeNmbr = reader["plan_type_nmbr"].ToString().Trim();
                        }
                        if (reader["plan_type_nm"] != DBNull.Value)
                        {
                            obj.PlanTypeNm = reader["plan_type_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }


        public void LoadDDLJenisRider(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetJenisRider(db);
            ddl.DataSource = data;
            ddl.DataTextField = "RiderTypeNm";
            ddl.DataValueField = "RiderTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetJenisRider(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'rider_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["rider_type_nmbr"] != DBNull.Value)
                        {
                            obj.RiderTypeNmbr = reader["rider_type_nmbr"].ToString().Trim();
                        }
                        if (reader["rider_type_nm"] != DBNull.Value)
                        {
                            obj.RiderTypeNm = reader["rider_type_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }



        public void LoadDDLJenisPremi(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetJenisPremiDDL(db);
            ddl.DataSource = data;
            ddl.DataTextField = "PremiumTypeNm";
            ddl.DataValueField = "PremiumTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetJenisPremiDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'premium_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["premium_type_nmbr"] != DBNull.Value)
                        {
                            obj.PremiumTypeNmbr = reader["premium_type_nmbr"].ToString().Trim();
                        }
                        if (reader["premium_type_nm"] != DBNull.Value)
                        {
                            obj.PremiumTypeNm = reader["premium_type_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }


        public void LoadDDLCitizenship(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetCitizenshipDDL(db);
            ddl.DataSource = data;
            ddl.DataTextField = "CitizenshipNm";
            ddl.DataValueField = "CitizenshipNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetCitizenshipDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'citizenship'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["citizenship_cd"] != DBNull.Value)
                        {
                            obj.CitizenshipNmbr = reader["citizenship_cd"].ToString().Trim();
                        }
                        if (reader["citizenship_nm"] != DBNull.Value)
                        {
                            obj.CitizenshipNm = reader["citizenship_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }



        public void LoadDDLPayMethod(Database db, DropDownList ddl)
        {
            List<DDLClient> data = GetPayMethodDDL(db);
            ddl.DataSource = data;
            ddl.DataTextField = "PaymentTypeNm";
            ddl.DataValueField = "PaymentTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetPayMethodDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PARAM 'payment_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["payment_type_nmbr"] != DBNull.Value)
                        {
                            obj.PaymentTypeNmbr = reader["payment_type_nmbr"].ToString().Trim();
                        }
                        if (reader["payment_nm"] != DBNull.Value)
                        {
                            obj.PaymentTypeNm = reader["payment_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }


        public void LoadDDLPaycenter(Database db, DropDownList ddl, string groupNmbrParam)
        {
            List<DDLClient> data = GetPaycenterDDL(db, groupNmbrParam);
            ddl.DataSource = data;
            ddl.DataTextField = "PaycenterNm";
            ddl.DataValueField = "PaycenterNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetPaycenterDDL(Database db, string groupNmbrParam)
        {
            List<DDLClient> output = new List<DDLClient>();
            string query = "EXEC DDL_PAYCENTER_ON_SCR_CERTIFICATEINFO @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbrParam);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["paycenter_nmbr"] != DBNull.Value)
                        {
                            obj.PaycenterNmbr = reader["paycenter_nmbr"].ToString().Trim();
                        }
                        if (reader["paycenter_nm"] != DBNull.Value)
                        {
                            obj.PaycenterNm = reader["paycenter_nm"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }


        public void LoadDDLGroup(Database db, DropDownList ddl)
        {
            DDLClient m = new DDLClient();
            List<DDLClient> data = new List<DDLClient>();
            data.AddRange(this.GetGroupDDL(db));
            ddl.DataSource = data;
            ddl.DataTextField = "CompanyNm";
            ddl.DataValueField = "GroupNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetGroupDDL(Database db)
        {
            List<DDLClient> output = new List<DDLClient>();
            try
            {
                string query = "EXEC DDL_GROUP_ON_SCR_CERTIFICATEINFO";
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["group_nmbr"] != DBNull.Value)
                        {
                            obj.GroupNmbr = reader["group_nmbr"].ToString().Trim();
                        }
                        if (reader["company_nm"] != DBNull.Value)
                        {
                            obj.CompanyNm = String.Format("{0} - {1}", reader["company_nm"].ToString().Trim(), reader["group_nmbr"].ToString().Trim());
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }


        public void LoadDDLCertif(Database db, DropDownList ddl, int ClientNmbr)
        {
            List<DDLClient> data = GetCertifInfoDDL(db, ClientNmbr);
            ddl.DataSource = data;
            ddl.DataTextField = "CertificateNmbr";
            ddl.DataValueField = "CertificateNmbr";
            ddl.DataBind();
        }

        private List<DDLClient> GetCertifInfoDDL(Database db, int ClientNmbr)
        {
            List<DDLClient> output = new List<DDLClient>();
            try
            {
                string query = "EXEC DDL_CERTIFICATE_ON_SCR_CERTIFICATEINFO @client_nmbr";
                db.setQuery(query);
                db.AddParameter("@client_nmbr", ClientNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLClient obj = new DDLClient();
                        if (reader["cer_nmbr"] != DBNull.Value)
                        {
                            obj.CertificateNmbr = reader["cer_nmbr"].ToString().Trim();
                        }
                        output.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }



        
    }
}