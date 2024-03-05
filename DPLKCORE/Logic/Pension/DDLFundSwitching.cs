using DPLKCORE.Class;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class DDLFundSwitching
    {
        //Group List Tab
        public int GroupNmbr { get; set; }

        //FundMovement Tab
        public int CerNmbr { get; set; }

        public string InvTypeNmbr { get; set; }
        public string InvTypeNm { get; set; }

        public string ModeNmbr { get; set; }
        public string ModeNm { get; set; }

        public string MoneyTypeNmbr { get; set; }
        public string MoneyTypeNm { get; set; }

        //Certificate List Tab
        public string EfctvDtVal { get; set; }
        public string EfctvDtText { get; set; }

        #region certificatelist
        //for certificate list
        public static List<DDLFundSwitching> GetTransactionDt(Database db)
        {
            List<DDLFundSwitching> output = new List<DDLFundSwitching>();
            string query = "select top 5 efctv_dt,dbo.fndisplaydate(efctv_dt) from fund_mvnt_est";
            //where processed_dt is null
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLFundSwitching obj = new DDLFundSwitching();
                        if (reader[1].ToString() != "")
                        {
                            obj.EfctvDtText = reader[1].ToString();
                        }
                        if (reader[0].ToString() != "")
                        {
                            obj.EfctvDtVal = reader[0].ToString();
                        }
                        output.Add(obj);
                    }
                }
                return output;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        public static void LoadTransactionDt(Database db, DropDownList ddl)
        {
            try
            {
                List<DDLFundSwitching> data = GetTransactionDt(db);

                foreach (var item in data)
                {
                    ddl.Items.Add(new ListItem(item.EfctvDtText, item.EfctvDtVal));
                }
                ddl.DataTextField = "EfctvDtText";
                ddl.DataValueField = "EfctvDtVal";
                ddl.DataBind();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public void LoadDDLMode(DropDownList ddl)
        {
            ddl.DataTextField = "InvTypeNm";

            ddl.Items.Clear();
            ddl.DataValueField = "InvTypeNmbr";
            ddl.Items.Add(new ListItem("Percentage", "0"));
            ddl.Items.Add(new ListItem("Ammount", "1"));
        }
        #endregion
        #region fundmovement
        //for fund movement

        public void LoadDDLDestFundMT(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetDestFundsMT(db);
            ddl.DataTextField = "InvTypeNm";
            ddl.DataValueField = "InvTypeNmbr";
            ddl.DataBind();
        }

        private object GetDestFundsMT(Database db)
        {
            List<DDLFundSwitching> output = new List<DDLFundSwitching>();
            string query = "select distinct ci.inv_type_nmbr,it.inv_type_nm from   cer_inv_drct ci join inv_type it on ci.inv_type_nmbr = it.inv_type_nmbr where it.inv_type_nmbr in (101,102,103)";
            try
            {
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLFundSwitching obj = new DDLFundSwitching();
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



        public void LoadDDLDestFund(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetDestFunds(db);
            ddl.DataTextField = "InvTypeNm";
            ddl.DataValueField = "InvTypeNmbr";
            ddl.DataBind();
        }

        private object GetDestFunds(Database db)
        {
            List<DDLFundSwitching> output = new List<DDLFundSwitching>();
            string query = "DDL_FUND_ON_SCR_FUNDSWITCHING_DST @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLFundSwitching obj = new DDLFundSwitching();
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

        public void LoadDDLSourceFund(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetSourceFunds(db);
            ddl.DataTextField = "InvTypeNm";
            ddl.DataValueField = "InvTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLFundSwitching> GetSourceFunds(Database db)
        {
            List<DDLFundSwitching> output = new List<DDLFundSwitching>();
            string query = "DDL_FUND_ON_SCR_FUNDSWITCHING_SRC @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLFundSwitching obj = new DDLFundSwitching();
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

        private List<DDLFundSwitching> GetMTDataGroup(Database db)
        {
            List<DDLFundSwitching> output = new List<DDLFundSwitching>();
            string query = "select distinct ci.money_type_nmbr,mt.money_type_nm from cer_inv ci	join money_type mt on ci.money_type_nmbr = mt.money_type_nmbr where mt.money_type_nmbr <> 102 ";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLFundSwitching obj = new DDLFundSwitching();
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

                throw new Exception(ex.Message);
            }
            return output;
        }

        public void LoadDDLMTGroup(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetMTDataGroup(db);
            ddl.DataTextField = "MoneyTypeNm";
            ddl.DataValueField = "MoneyTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLFundSwitching> GetMTData(Database db)
        {
            List<DDLFundSwitching> output = new List<DDLFundSwitching>();
            string query = "select distinct ci.money_type_nmbr,mt.money_type_nm from cer_inv ci	join money_type mt on ci.money_type_nmbr = mt.money_type_nmbr where cer_nmbr = @cer_nmbr ";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLFundSwitching obj = new DDLFundSwitching();
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

                throw new Exception(ex.Message);
            }
            return output;
        }

        public void LoadDDLMT(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetMTData(db);
            ddl.DataTextField = "MoneyTypeNm";
            ddl.DataValueField = "MoneyTypeNmbr";
            ddl.DataBind();
        }



        #endregion

        #region grouplist

        //for grouplist
        public void LoadDDLDestFundGroup(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetDestFundsGroup(db);
            ddl.DataTextField = "InvTypeNm";
            ddl.DataValueField = "InvTypeNmbr";
            ddl.DataBind();
        }

        private object GetDestFundsGroup(Database db)
        {
            List<DDLFundSwitching> output = new List<DDLFundSwitching>();
            string query = "DDL_FUND_ON_SCR_FUNDSWITCHING_GROUP_DST @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLFundSwitching obj = new DDLFundSwitching();
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

        public void LoadDDLSourceFundGroup(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetSourceFundsGroup(db);
            ddl.DataTextField = "InvTypeNm";
            ddl.DataValueField = "InvTypeNmbr";
            ddl.DataBind();
        }

        private List<DDLFundSwitching> GetSourceFundsGroup(Database db)
        {
            List<DDLFundSwitching> output = new List<DDLFundSwitching>();
            string query = "DDL_FUND_ON_SCR_FUNDSWITCHING_GROUP @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLFundSwitching obj = new DDLFundSwitching();
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
        #endregion
  
    }
}