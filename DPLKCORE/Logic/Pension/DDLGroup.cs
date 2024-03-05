using DPLKCORE.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using System.Data;
using System.Data.Common;

namespace DPLKCORE.Logic.Pension
{
    public class DDLGroup
    {
        //GROUP INFO TAB
        public String WithSrcTypeNmbr { get; set; }
        public String WithSrcTypeNm { get; set; }
        public String CurrencyTypeNmbr { get; set; }
        public String CurrencyTypeNm { get; set; }
        public String DplkNmbr { get; set; }
        public String DplkNm { get; set; }
        public Int32 ClientNmbr { get; set; }
        public String CompanyNm { get; set; }
        public String ProductTypeNmbr { get; set; }
        public String ProductTypeNm { get; set; }
        public String AllowWithNmbr { get; set; }
        public String AllowWithNm { get; set; }
        public String PremiumMtdType { get; set; }
        public String PremiumMtdNm { get; set; }
        public String CommisionType { get; set; }
        public String CommisionNm { get; set; }
        public String AgentNmbr { get; set; }
        public String AgentNm { get; set; }
        public String FreqTypeNmbr { get; set; }
        public String FreqTypeNm { get; set; }
        public String GroupNmbr { get; set; }
        public String DisplayText { get; set; }
        public String Mattype { get; set; }
        public String MattypeNm { get; set; }


        //MEMBER CLASS PLAN TAB
        public String McpNmbr { get; set; }
        public String McpNm { get; set; }

        //GROUP CHARGE TAB
        public String ChargeTypeNm { get; set; }
        public String ChargeTypeNmbr { get; set; }
        public String PaymentResNmbr { get; set; }
        public String PaymentResNm { get; set; }
        public String FrequencyNm { get; set; }
        public String FrequencyNmbr { get; set; }
        public String ChargeRateTypeNmbr { get; set; }
        public String ChargeRateTypeNm { get; set; }
         
        //GROUP INVESTMENT DIRECTION TAB
        public String InvestOptNm { get; set; }
        public String InvestOptNmbr { get; set; }

        //BENEFITS TAB
        public String BenefitTypeNm { get; set; }
        public String BenefitTypeNmbr { get; set; }
        public String SumCalcTypeNm { get; set; }
        public String SumCalcTypeNmbr { get; set; }
        public String OptionTypeNmbr { get; set; }
        public String OptionTypeNm { get; set; }
        public String CoiTypeNmbr { get; set; }
        public String CoiTypeNm { get; set; }
        public String AdditionalBeneNmbr { get; set; }
        public String AdditionalBeneNm { get; set; }


        public List<DDLGroup> GetMCPGroupNmbr(Database db, int groupNmbr)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "EXEC DDL_MBRCLSPLAN_ON_SCR_CERTIFICATEINFO @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);

                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["mcp_nmbr"] != DBNull.Value)
                    {
                        obj.McpNmbr = reader["mcp_nmbr"].ToString().Trim();
                    }
                    if (reader["mcp_nm"] != DBNull.Value)
                    {
                        obj.McpNm = reader["mcp_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }

        public bool DDLMcpGroupNmbr(DropDownList ddl, Database db, int groupNmbr)
        {

            List<DDLGroup> data = GetMCPGroupNmbr(db, groupNmbr);
            if (data.Count > 0)
            {
                ddl.DataSource = data;
                ddl.DataValueField = "McpNmbr";
                ddl.DataTextField = "McpNm";
                ddl.DataBind();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public List<DDLGroup> GetAdditionalBenefits(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "select sub_trns_nmbr,sub_trns_nm from trns_type where trns_type_nmbr = 400 order by trns_type_nmbr asc";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["sub_trns_nmbr"] != DBNull.Value)
                    {
                        obj.AdditionalBeneNmbr = reader["sub_trns_nmbr"].ToString().Trim();
                    }
                    if (reader["sub_trns_nm"] != DBNull.Value)
                    {
                        obj.AdditionalBeneNm = reader["sub_trns_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }


        public void DDLAdditionalBenefit(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetAdditionalBenefits(db);
            ddl.DataSource = data;
            ddl.DataValueField = "AdditionalBeneNmbr";
            ddl.DataTextField = "AdditionalBeneNm";
            ddl.DataBind();
        }


        public List<DDLGroup> GetCoiRates(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "EXEC DDL_PARAM 'COI_TYPE'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["coi_type_nmbr"] != DBNull.Value)
                    {
                        obj.CoiTypeNmbr = reader["coi_type_nmbr"].ToString().Trim();
                    }
                    if (reader["coi_type_nm"] != DBNull.Value)
                    {
                        obj.CoiTypeNm = reader["coi_type_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }


        public void DDLCOIRate(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetCoiRates(db);
            ddl.DataSource = data;
            ddl.DataValueField = "CoiTypeNmbr";
            ddl.DataTextField = "CoiTypeNm";
            ddl.DataBind();
        }



        public List<DDLGroup> GetOptions(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "EXEC DDL_PARAM 'OPTION_TYPE'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["option_type_nmbr"] != DBNull.Value)
                    {
                        obj.OptionTypeNmbr = reader["option_type_nmbr"].ToString().Trim();
                    }
                    if (reader["option_type_nm"] != DBNull.Value)
                    {
                        obj.OptionTypeNm = reader["option_type_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }
        

        public void DDLLoadOption(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetOptions(db);
            ddl.DataSource = data;
            ddl.DataValueField = "OptionTypeNmbr";
            ddl.DataTextField = "OptionTypeNm";
            ddl.DataBind();
        }


        public List<DDLGroup> GetSumCalcType(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "EXEC DDL_PARAM 'si_calc_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["si_calc_type_nmbr"] != DBNull.Value)
                    {
                        obj.SumCalcTypeNmbr = reader["si_calc_type_nmbr"].ToString().Trim();
                    }
                    if (reader["si_calc_type_nm"] != DBNull.Value)
                    {
                        obj.SumCalcTypeNm = reader["si_calc_type_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }

        public void DDLSumMethod(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetSumCalcType(db);
            ddl.DataSource = data;
            ddl.DataValueField = "SumCalcTypeNmbr";
            ddl.DataTextField = "SumCalcTypeNm";
            ddl.DataBind();
        }


        public List<DDLGroup> GetBenefits(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "EXEC DDL_PARAM 'bene_type'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["bene_type_nmbr"] != DBNull.Value)
                    {
                        obj.BenefitTypeNmbr = reader["bene_type_nmbr"].ToString().Trim();
                    }
                    if (reader["bene_type_nm"] != DBNull.Value)
                    {
                        obj.BenefitTypeNm = reader["bene_type_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }

        public void DDLBenefit(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetBenefits(db);
            ddl.DataSource = data;
            ddl.DataValueField = "BenefitTypeNmbr";
            ddl.DataTextField = "BenefitTypeNm";
            ddl.DataBind();
        }


        public List<DDLGroup> GetInvestOption(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "EXEC DDL_PARAM 'INV_TYPE'";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["inv_type_nmbr"] != DBNull.Value)
                    {
                        obj.InvestOptNmbr = reader["inv_type_nmbr"].ToString().Trim();
                    }
                    if (reader["inv_type_nm"] != DBNull.Value)
                    {
                        obj.InvestOptNm = reader["inv_type_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }

        public void DDLInvestOpt(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetInvestOption(db);
            ddl.DataSource = data;
            ddl.DataTextField = "InvestOptNm";
            ddl.DataValueField = "InvestOptNmbr";
            ddl.DataBind();
        }

        public List<DDLGroup> GetChargeRates(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "select coi_type_nmbr as rate_type,coi_type_nm  as rate_nm from coi_type " +
                                "union " +
                                "select rate_type_nmbr as rate_type,rate_type_nm as rate_nm from rate_table_type ";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["rate_type"] != DBNull.Value)
                    {
                        obj.ChargeRateTypeNmbr = reader["rate_type"].ToString().Trim();
                    }
                    if (reader["rate_nm"] != DBNull.Value)
                    {
                        obj.ChargeRateTypeNm = reader["rate_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }

        public void DDLChargeRate(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetChargeRates(db);
            ddl.DataSource = data;
            ddl.DataTextField = "ChargeRateTypeNm";
            ddl.DataValueField = "ChargeRateTypeNmbr";
            ddl.DataBind();
        }

        public List<DDLGroup> GetFrequencies(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "SELECT * FROM frequency_type";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["freq_type_nmbr"] != DBNull.Value)
                    {
                        obj.FrequencyNmbr = reader["freq_type_nmbr"].ToString().Trim();
                    }
                    if (reader["freq_type_nm"] != DBNull.Value)
                    {
                        obj.FrequencyNm = reader["freq_type_nm"].ToString().Trim();
                    }

                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return output;
        }

        public void DDLFrequency(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetFrequencies(db);
            ddl.DataSource = data;
            ddl.DataTextField = "FrequencyNm";
            ddl.DataValueField = "FrequencyNmbr";
            ddl.DataBind();
        }

        public List<DDLGroup> GetPaymentRes(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "SELECT * FROM PAY_RSPN_TYPE";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["pay_rspn_nmbr"] != DBNull.Value)
                    {
                        obj.PaymentResNmbr = reader["pay_rspn_nmbr"].ToString().Trim();
                    }
                    if (reader["pay_rspn_nm"] != DBNull.Value)
                    {
                        obj.PaymentResNm = reader["pay_rspn_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;

        }

        public void DDLPaymentRes(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetPaymentRes(db);
            ddl.DataSource = data;
            ddl.DataValueField = "PaymentResNmbr";
            ddl.DataTextField = "PaymentResNm";
            ddl.DataBind();
        }

        public List<DDLGroup> GetChargeType(Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "SELECT * FROM CHARGE_TYPE";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["charge_type_nmbr"] != DBNull.Value)
                    {
                        obj.ChargeTypeNmbr = reader["charge_type_nmbr"].ToString().Trim();
                    }
                    if (reader["charge_type_nm"] != DBNull.Value)
                    {
                        obj.ChargeTypeNm = reader["charge_type_nm"].ToString().Trim();
                    }
                    output.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }

        public void DDLChargeType(DropDownList ddl, Database db)
        {
            List<DDLGroup> data = GetChargeType(db);
            ddl.DataSource = data;
            ddl.DataTextField = "ChargeTypeNm";
            ddl.DataValueField = "ChargeTypeNmbr";
            ddl.DataBind();
        }

        public List<DDLGroup> GetMCPType(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> output = new List<DDLGroup>();
            string query = "EXEC DDL_PARAM 'MCP_TYPE'";
            try
            {
                db.setQuery(query);
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["mcp_nmbr"] != DBNull.Value)
                    {
                        obj.McpNmbr = reader["mcp_nmbr"].ToString().Trim();
                    }
                    if (reader["mcp_nm"] != DBNull.Value)
                    {
                        obj.McpNm = reader["mcp_nm"].ToString().Trim();
                    }

                    output.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            return output;
        }

        public void DDLMcpType(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = this.GetMCPType(db);

            ddl.DataSource = data;
            ddl.DataTextField = "McpNm";
            ddl.DataValueField = "McpNmbr";
            ddl.DataBind();
        }



        public List<DDLGroup> GetAffiliate(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "SELECT group_nmbr, company_nm + ' - ' + cast(group_nmbr as varchar(10)) AS DisplayText FROM group_info g JOIN company co ON g.client_nmbr = co.client_nmbr ORDER BY company_nm ASC ";
            try
            {
                db.setQuery(query);

                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();

                    if (reader["group_nmbr"] != DBNull.Value)
                        m.GroupNmbr = reader["group_nmbr"].ToString().Trim();

                    if (reader["DisplayText"] != DBNull.Value)
                        m.DisplayText = reader["DisplayText"].ToString().Trim();

                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void AffiliateDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetAffiliate(db));

            ddl.DataSource = data;
            ddl.DataValueField = "GroupNmbr";
            ddl.DataTextField = "DisplayText";
            ddl.DataBind();
        }
        public List<DDLGroup> GetFrequency(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "EXEC DDL_PARAM 'FREQUENCY_TYPE' ";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["freq_type_nmbr"] != DBNull.Value)
                        m.FreqTypeNmbr = reader["freq_type_nmbr"].ToString().Trim();

                    if (reader["freq_type_nm"] != DBNull.Value)
                        m.FreqTypeNm = reader["freq_type_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void FrequencyDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetFrequency(db));

            ddl.DataSource = data;
            ddl.DataValueField = "FreqTypeNmbr";
            ddl.DataTextField = "FreqTypeNm";
            ddl.DataBind();
        }
        public List<DDLGroup> GetAgent(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "select agent_nmbr, agent_nm from agent_master";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["agent_nmbr"] != DBNull.Value)
                        m.AgentNmbr = reader["agent_nmbr"].ToString().Trim();

                    if (reader["agent_nm"] != DBNull.Value)
                        m.AgentNm = reader["agent_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void AgentDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetAgent(db));

            ddl.DataSource = data;
            ddl.DataValueField = "AgentNmbr";
            ddl.DataTextField = "AgentNm";
            ddl.DataBind();
        }


        public List<DDLGroup> GetMattype(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();
            string query = "EXEC DDL_PARAM 'MATURITY_TYPE'";
            try
            {
                db.setQuery(query);
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup obj = new DDLGroup();
                    if (reader["maturity_type_nmbr"] != DBNull.Value)
                    {
                        obj.Mattype = reader["maturity_type_nmbr"].ToString().Trim();
                    }

                    if (reader["maturity_type_nm"] != DBNull.Value)
                    {
                        obj.MattypeNm = reader["maturity_type_nm"].ToString().Trim();
                    }

                    data.Add(obj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }

        public void MattypeDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetMattype(db));

            ddl.DataSource = data;
            ddl.DataValueField = "Mattype";
            ddl.DataTextField = "MattypeNm";
            ddl.DataBind();
        }


        public List<DDLGroup> GetCommision(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "EXEC DDL_PARAM 'COMMISSION_TYPE' ";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["commision_type"] != DBNull.Value)
                        m.CommisionType = reader["commision_type"].ToString().Trim();

                    if (reader["commision_nm"] != DBNull.Value)
                        m.CommisionNm = reader["commision_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void CommisonDLL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetCommision(db));

            ddl.DataSource = data;
            ddl.DataValueField = "CommisionType";
            ddl.DataTextField = "CommisionNm";
            ddl.DataBind();
        }
        public List<DDLGroup> GetPremium(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "EXEC DDL_PARAM 'PREMIUM_METHOD' ";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["premium_mtd_type"] != DBNull.Value)
                        m.PremiumMtdType = reader["premium_mtd_type"].ToString().Trim();

                    if (reader["premium_mtd_nm"] != DBNull.Value)
                        m.PremiumMtdNm = reader["premium_mtd_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void PremiumDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetPremium(db));

            ddl.DataSource = data;
            ddl.DataValueField = "PremiumMtdType";
            ddl.DataTextField = "PremiumMtdNm";
            ddl.DataBind();
        }
        public List<DDLGroup> GetAllowType(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "EXEC DDL_PARAM 'ALLOW_WITH_TYPE' ";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["allow_with_nmbr"] != DBNull.Value)
                        m.AllowWithNmbr = reader["allow_with_nmbr"].ToString().Trim();

                    if (reader["allow_with_nm"] != DBNull.Value)
                        m.AllowWithNm = reader["allow_with_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void AllowDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetAllowType(db));

            ddl.DataSource = data;
            ddl.DataValueField = "AllowWithNmbr";
            ddl.DataTextField = "AllowWithNm";
            ddl.DataBind();
        }
        public List<DDLGroup> GetProduct(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "EXEC DDL_PARAM 'PRODUCT_TYPE' ";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["product_type_nmbr"] != DBNull.Value)
                        m.ProductTypeNmbr = reader["product_type_nmbr"].ToString().Trim();

                    if (reader["product_type_nm"] != DBNull.Value)
                        m.ProductTypeNm = reader["product_type_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void ProductDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetProduct(db));

            ddl.DataSource = data;
            ddl.DataValueField = "ProductTypeNmbr";
            ddl.DataTextField = "ProductTypeNm";
            ddl.DataBind();
        }
        public void SourceType(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetParamDDLSourceType(db));

            ddl.DataSource = data;
            ddl.DataValueField = "WithSrcTypeNmbr";
            ddl.DataTextField = "WithSrcTypeNm";
            ddl.DataBind();
        }
        public List<DDLGroup> GetParamDDLSourceType(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "EXEC DDL_PARAM 'WITH_SOURCE_TYPE' ";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["with_src_type_nmbr"] != DBNull.Value)
                        m.WithSrcTypeNmbr = reader["with_src_type_nmbr"].ToString().Trim();

                    if (reader["with_src_type_nm"] != DBNull.Value)
                        m.WithSrcTypeNm = reader["with_src_type_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void Currency(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetParamDDLCurrency(db));

            ddl.DataSource = data;
            ddl.DataValueField = "CurrencyTypeNmbr";
            ddl.DataTextField = "CurrencyTypeNm";
            ddl.DataBind();
        }
        public List<DDLGroup> GetParamDDLCurrency(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "EXEC DDL_PARAM 'Currency_TYPE' ";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["currency_type_nmbr"] != DBNull.Value)
                        m.CurrencyTypeNmbr = reader["currency_type_nmbr"].ToString().Trim();

                    if (reader["currency_type_nm"] != DBNull.Value)
                        m.CurrencyTypeNm = reader["currency_type_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public void Operation(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetOperation(db));

            ddl.DataSource = data;
            ddl.DataValueField = "DplkNmbr";
            ddl.DataTextField = "DplkNm";
            ddl.DataBind();
        }
        public List<DDLGroup> GetOperation(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "SELECT v.dplk_nmbr, r.dplk_nm FROM va_dplk_type v JOIN dplk_rollover r ON v.dplk_nmbr = r.dplk_nmbr ";



            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["dplk_nmbr"] != DBNull.Value)
                        m.DplkNmbr = reader["dplk_nmbr"].ToString().Trim();

                    if (reader["dplk_nm"] != DBNull.Value)
                        m.DplkNm = reader["dplk_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
        public List<DDLGroup> GetCompany(DPLKCORE.Framework.Database db)
        {
            List<DDLGroup> data = new List<DDLGroup>();

            String query = "EXEC DDL_COMPANY_ON_SCR_GROUP";
            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLGroup m = new DDLGroup();


                    if (reader["client_nmbr"] != DBNull.Value)
                        m.ClientNmbr = Convert.ToInt32(reader["client_nmbr"]);

                    if (reader["company_nm"] != DBNull.Value)
                        m.CompanyNm = reader["company_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data.OrderBy(x => x.CompanyNm).ToList();
        }
        public void CompanyDll(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            DDLGroup m = new DDLGroup();
            List<DDLGroup> data = new List<DDLGroup>();

            data.AddRange(this.GetCompany(db));

            ddl.DataSource = data;
            ddl.DataValueField = "ClientNmbr";
            ddl.DataTextField = "CompanyNm";
            ddl.DataBind();
        }



    }


}