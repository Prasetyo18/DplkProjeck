using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DPLKCORE.Framework;
using System.Data.Common;

namespace DPLKCORE.Logic.Pension
{
    public class GroupInfoModels
    {
        //??properties
        public int GroupNmbr { get; set; }
        public int? CommisionFlg { get; set; }
        public DateTime? CreateDt { get; set; }
        public DateTime LastChangeDt { get; set; }


        //44 properties
        public int? ProductType { get; set; }
        public DateTime? TerminationDt { get; set; }
        public DateTime? CaseCloseDt { get; set; }
        public DateTime EfctvDt { get; set; }
        public int ClientNmbr { get; set; }
        public short IndGrpCd { get; set; }
        public int AllowWithNmbr { get; set; }
        public int WithYear { get; set; }
        public double? MinAnnuityPrct { get; set; }
        public double? MinAnnuityAmt { get; set; }
        public int? AnlMaxWithFreq { get; set; }
        public double? MinWithAmt { get; set; }
        public double? MaxWithPrct { get; set; }
        public int? MinYrForWith { get; set; }
        public int? EarlyRetireAge { get; set; }
        public int? NormalRetireAge { get; set; }
        public double? MinCntrbAmt { get; set; }
        public double? MaxCntrbAmt { get; set; }
        public short? WithSrcTypeNmbr { get; set; }
        public short? InclContribFlg { get; set; }
        public int? AffiliatedTo { get; set; }
        public short? PremiumMtdType { get; set; }
        public short? MaturityTypeNmbr { get; set; }
        public string MaturityVal { get; set; }
        public int? MailAddrOpt { get; set; }
        public int? BillPayctrOpt { get; set; }
        public int? PslPaymentFreq { get; set; }
        public DateTime? BackdatedEfctvDt { get; set; }
        public short? AccbalFreqNmbr { get; set; }
        public short? SupportUu1992 { get; set; }
        public DateTime? CompletedDt { get; set; }
        public DateTime? AccbalLstprnDt { get; set; }
        public int? ProrateFeeFlg { get; set; }
        public DateTime? MppTerminationDt { get; set; }
        public DateTime? SpakRecvDt { get; set; }
        public short? HavePsl { get; set; }
        public short? PslType { get; set; }
        public int? PooledFlg { get; set; }
        public string OldGrpNmbr { get; set; }
        public int? ClaimProcessDay { get; set; }
        public string VaCurrencyNmbr { get; set; }
        public string VaDplkNmbr { get; set; }
        public int? CommisionType { get; set; }
        public int? AgentNmbr { get; set; }


        public bool UpdateGroupInfo(Database db)
        {
            string query = "exec sp_group_info_u " +
                           "@group_nmbr, @product_type, @termination_dt, " +
                           "@case_close_dt, @efctv_dt, @client_nmbr, " +
                           "@ind_grp_cd, @allow_with_nmbr, @with_year, " +
                           "@min_annuity_prct, @min_annuity_amt, @anl_max_with_freq, " +
                           "@min_with_amt, @max_with_prct, @min_yr_for_with, " +
                           "@early_retire_age, @normal_retire_age, @min_cntrb_amt, " +
                           "@max_cntrb_amt, @with_src_type_nmbr, @incl_contrib_flg, " +
                           "@affiliatedto, @premium_mtd_type, @maturity_type_nmbr, " +
                           "@maturity_val, @mail_addr_opt, @bill_payctr_opt, " +
                           "@psl_payment_freq, @backdated_efctv_dt, @accbal_freq_nmbr, " +
                           "@support_uu1992, @completed_dt, @accbal_lstPrn_dt, " +
                           "@prorate_fee_flg, @mpp_termination_dt, @spak_recv_dt, " +
                           "@have_psl, @psl_type, @pooled_flg, " +
                           "@old_grp_nmbr, @claim_process_day, @va_currency, " +
                           "@va_partner, @commision_type, @agent_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@product_type", this.ProductType);
                db.AddParameter("@termination_dt", DBNull.Value);
                db.AddParameter("@case_close_dt", DBNull.Value);
                db.AddParameter("@efctv_dt", this.EfctvDt);
                db.AddParameter("@client_nmbr", this.ClientNmbr);
                db.AddParameter("@ind_grp_cd", this.IndGrpCd);
                db.AddParameter("@allow_with_nmbr", this.AllowWithNmbr);
                db.AddParameter("@with_year", this.MinYrForWith);
                db.AddParameter("@min_annuity_prct", this.MinAnnuityPrct);
                db.AddParameter("@min_annuity_amt", this.MinAnnuityAmt);
                db.AddParameter("@anl_max_with_freq", this.AnlMaxWithFreq);
                db.AddParameter("@min_with_amt", this.MinWithAmt);
                db.AddParameter("@max_with_prct", this.MaxWithPrct);
                db.AddParameter("@min_yr_for_with", this.MinYrForWith);
                db.AddParameter("@early_retire_age", this.EarlyRetireAge);
                db.AddParameter("@normal_retire_age", this.NormalRetireAge);
                db.AddParameter("@min_cntrb_amt", DBNull.Value);
                db.AddParameter("@max_cntrb_amt", DBNull.Value);
                db.AddParameter("@with_src_type_nmbr", this.WithSrcTypeNmbr);
                db.AddParameter("@incl_contrib_flg", this.InclContribFlg);
                db.AddParameter("@affiliatedto", this.AffiliatedTo);
                db.AddParameter("@premium_mtd_type", this.PremiumMtdType);
                db.AddParameter("@maturity_type_nmbr", this.MaturityTypeNmbr);
                db.AddParameter("@maturity_val", this.MaturityVal);
                db.AddParameter("@mail_addr_opt", DBNull.Value);
                db.AddParameter("@bill_payctr_opt", DBNull.Value);
                if (this.PslPaymentFreq == null)
                {
                    db.AddParameter("@psl_payment_freq", DBNull.Value);
                }
                else
                {
                    db.AddParameter("@psl_payment_freq", this.PslPaymentFreq);
                }
                db.AddParameter("@backdated_efctv_dt", this.BackdatedEfctvDt);
                db.AddParameter("@accbal_freq_nmbr", this.AccbalFreqNmbr);
                db.AddParameter("@support_uu1992", this.SupportUu1992);
                db.AddParameter("@completed_dt", DBNull.Value);
                db.AddParameter("@accbal_lstPrn_dt", DBNull.Value);
                db.AddParameter("@prorate_fee_flg", DBNull.Value);
                db.AddParameter("@mpp_termination_dt", DBNull.Value);
                db.AddParameter("@spak_recv_dt", this.SpakRecvDt);

                if (this.HavePsl == null)
                {
                    db.AddParameter("@have_psl", DBNull.Value);
                }
                else
                {
                    db.AddParameter("@have_psl", this.HavePsl);
                }
                

                if (this.PslType == null)
                {
                    db.AddParameter("@psl_type", DBNull.Value);
                }
                else
                {
                    db.AddParameter("@psl_type", this.PslType);
                }

                if (this.PooledFlg == null)
                {
                    db.AddParameter("@pooled_flg", DBNull.Value);
                }
                else
                {
                    db.AddParameter("@pooled_flg", this.PooledFlg);
                }

                db.AddParameter("@old_grp_nmbr", DBNull.Value);
                db.AddParameter("@claim_process_day", this.ClaimProcessDay);
                db.AddParameter("@va_currency", this.VaCurrencyNmbr);
                db.AddParameter("@va_partner", this.VaDplkNmbr);
                db.AddParameter("@commision_type", this.CommisionType);
                db.AddParameter("@agent_nmbr", this.AgentNmbr);

                db.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Dictionary<string, object> LoadByGroupId(Database db, int groupId)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            string query = "SP_GROUP_INFO_R @group_id";
            db.setQuery(query);
            db.AddParameter("@group_id", groupId);

            DataTable dt = db.ExecuteQuery();
            if (dt.Rows.Count > 0)
            {
                DataRow firstRow = dt.Rows[0];

                    foreach (DataColumn col in dt.Columns)
                    {
                        output.Add(col.ColumnName, firstRow[col]);
                    } 
            }
            //DbDataReader reader = db.ExecuteReader();
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        for (int i = 0; i < reader.FieldCount; i++)
            //        {
            //            output.Add(reader.GetName(i), reader.GetValue(i));
            //        }
            //    }
            //}
            return output;

        }


        public DataTable LoadData(Database db)
        {
            string query = "SELECT * FROM group_info order by group_nmbr desc;";
            db.setQuery(query);
            DataTable output = db.ExecuteQuery();
            return output;
        }

        public int InsertGroupInfo(DPLKCORE.Framework.Database x)
        {

            //44 parameters. ok
            String query = "exec sp_group_info_i " +
                           "@product_type, @termination_dt, @case_close_dt, " +
                           "@efctv_dt, @client_nmbr, @ind_grp_cd, " +
                           "@allow_with_nmbr, @with_year, @min_annuity_prct, " +
                           "@min_annuity_amt, @anl_max_with_freq, @min_with_amt, " +
                           "@max_with_prct, @min_yr_for_with, @early_retire_age, " +
                           "@normal_retire_age, @min_cntrb_amt, @max_cntrb_amt, " +
                           "@with_src_type_nmbr, @incl_contrib_flg, @affiliatedTo, " +
                           "@premium_mtd_type, @maturity_type_nmbr, @maturity_val, " +
                           "@mail_addr_opt, @bill_payctr_opt, @psl_payment_freq, " +
                           "@backdated_efctv_dt, @accbal_freq_nmbr, @Support_UU1992, " +
                           "@completed_dt, @accbal_lstprn_dt, @prorate_fee_flg, " +
                           "@mpp_termination_dt, @SPAK_recv_dt, @have_psl, @psl_type, " +
                           "@pooled_flg, @old_grp_nmbr, @claim_process_day, @va_currency, " +
                           "@va_partner, @commision_type, @agent_nmbr";
            try
            {
                x.setQuery(query);
                
                //44 input
                x.AddParameter("@product_type", this.ProductType);
                x.AddParameter("@termination_dt", DBNull.Value);
                x.AddParameter("@case_close_dt", DBNull.Value);
                x.AddParameter("@efctv_dt", this.EfctvDt);
                x.AddParameter("@client_nmbr", this.ClientNmbr);
                x.AddParameter("@ind_grp_cd", this.IndGrpCd);
                x.AddParameter("@allow_with_nmbr", this.AllowWithNmbr);
                x.AddParameter("@with_year", this.WithYear);
                x.AddParameter("@min_annuity_prct", this.MinAnnuityPrct);
                x.AddParameter("@min_annuity_amt", this.MinAnnuityAmt);
                x.AddParameter("@anl_max_with_freq", this.AnlMaxWithFreq);
                x.AddParameter("@min_with_amt", this.MinWithAmt);
                x.AddParameter("@max_with_prct", this.MaxWithPrct);
                x.AddParameter("@min_yr_for_with", this.MinYrForWith);
                x.AddParameter("@early_retire_age", this.EarlyRetireAge);
                x.AddParameter("@normal_retire_age", this.NormalRetireAge);
                x.AddParameter("@min_cntrb_amt", DBNull.Value);
                x.AddParameter("@max_cntrb_amt", DBNull.Value);
                x.AddParameter("@with_src_type_nmbr", this.WithSrcTypeNmbr);
                x.AddParameter("@incl_contrib_flg", this.InclContribFlg);
                x.AddParameter("@affiliatedTo", this.AffiliatedTo);
                x.AddParameter("@premium_mtd_type", this.PremiumMtdType);
                x.AddParameter("@maturity_type_nmbr", this.MaturityTypeNmbr);
                x.AddParameter("@maturity_val", this.MaturityVal);
                x.AddParameter("@mail_addr_opt", DBNull.Value);
                x.AddParameter("@bill_payctr_opt", DBNull.Value);
                x.AddParameter("@psl_payment_freq", this.PslPaymentFreq);
                x.AddParameter("@backdated_efctv_dt", this.BackdatedEfctvDt);
                x.AddParameter("@accbal_freq_nmbr", this.AccbalFreqNmbr);
                x.AddParameter("@Support_UU1992", this.SupportUu1992);
                x.AddParameter("@completed_dt", DBNull.Value);
                x.AddParameter("@accbal_lstprn_dt", DBNull.Value);
                x.AddParameter("@prorate_fee_flg", DBNull.Value);
                x.AddParameter("@mpp_termination_dt", DBNull.Value);
                x.AddParameter("@SPAK_recv_dt", this.SpakRecvDt);
                x.AddParameter("@have_psl", DBNull.Value);
                x.AddParameter("@psl_type", DBNull.Value);
                x.AddParameter("@pooled_flg", DBNull.Value);
                x.AddParameter("@old_grp_nmbr", DBNull.Value);
                x.AddParameter("@claim_process_day", this.ClaimProcessDay);
                x.AddParameter("@va_currency", this.VaCurrencyNmbr);
                x.AddParameter("@va_partner", DBNull.Value);
                x.AddParameter("@commision_type", this.CommisionType);
                x.AddParameter("@agent_nmbr", this.AgentNmbr);

                SqlParameter outputParameter = new SqlParameter("@generated_id", DbType.Int32);
                outputParameter.Direction = ParameterDirection.Output;
                x.AddOutputParam(outputParameter);
                
                //result hold the new groupId
                var result = Convert.ToInt32(x.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }


    
}