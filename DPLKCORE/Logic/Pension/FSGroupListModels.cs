using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class FSGroupListModels
    {

        public int GroupNmbr { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int InvestmentTypeNumber { get; set; }
        public int InvestmentTypeNumberDst { get; set; }
        public int MoneyTypeNmbr { get; set; }
        public string MoneyTypeNm { get; set; }
        public float CurrentAsset { get; set; }
        public float CompositionPercentage { get; set; }
        public float TransactionAmount { get; set; }
        public string InvestmentTypeName { get; set; }
        public int Mode { get; set; }
        public string FundSource { get; set; }
        public string FundDestination { get; set; }
        public int BatchId { get; set; }
        public string UserID { get; set; }

        public void ManageFundMovementComponentMT(Database db)
        {
            String query = "exec usp_money_mvmnt_comp @group_nmbr, @efctv_dt, @inv_type_nmbr, @money_type_nmbr, @current_asset, @composition_pct, @trns_amt, @inv_type_nm, @money_type_nm, @mode";

            try
            {
                db.setQuery(query);

                db.AddParameter("@group_nmbr", GroupNmbr);
                db.AddParameter("@efctv_dt", EffectiveDate);
                db.AddParameter("@inv_type_nmbr", InvestmentTypeNumber);
                db.AddParameter("@money_type_nmbr", MoneyTypeNmbr);
                db.AddParameter("@current_asset", CurrentAsset);
                db.AddParameter("@composition_pct", CompositionPercentage);
                db.AddParameter("@trns_amt", TransactionAmount);
                db.AddParameter("@inv_type_nm", InvestmentTypeName);
                db.AddParameter("@money_type_nm", MoneyTypeNm);
                db.AddParameter("@mode", Mode);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ManageFundMovementComponent(Database db)
        {
            String query = "exec usp_fund_mvmnt_comp @group_nmbr, @efctv_dt, @inv_type_nmbr, @current_asset, @composition_pct, @trns_amt, @inv_type_nm, @mode";

            try
            {
                db.setQuery(query);

                db.AddParameter("@group_nmbr", GroupNmbr);
                db.AddParameter("@efctv_dt", EffectiveDate);
                db.AddParameter("@inv_type_nmbr", InvestmentTypeNumber);
                db.AddParameter("@current_asset", CurrentAsset);
                db.AddParameter("@composition_pct", CompositionPercentage);
                db.AddParameter("@trns_amt", TransactionAmount);
                db.AddParameter("@inv_type_nm", InvestmentTypeName);
                db.AddParameter("@mode", Mode);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable FillGvEstimationMT(Database db, int groupNmbr, string TransactionDt)
        {
            string query = "EXEC GRID_ESTIMATION_ON_SCR_FUNDSWITHCING_GRP_MONEY_TYPE @group_nmbr, @efctv_dt";
            DataTable result;
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);
                db.AddParameter("@efctv_dt", TransactionDt);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return result;
        }

        public static DataTable FillGvEstimation(Database db, int groupNmbr, string TransactionDt)
        {
            string query = "EXEC GRID_ESTIMATION_ON_SCR_FUNDSWITHCING_GRP @group_nmbr, @efctv_dt";
            DataTable result;
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);
                db.AddParameter("@efctv_dt", TransactionDt);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return result;
        }

        public void InsertFundMovementEstimationMT(Database db)
        {
            String query = "sp_money_mvnt_est_i_Grp @group_nmbr, @efctv_dt, @inv_Type_src, @inv_Type_dst, @money_type, @gross_amt, @doc_recv_dt, @batchId";

            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@efctv_dt", this.EffectiveDate);
                db.AddParameter("@inv_Type_src", this.InvestmentTypeNumber);
                db.AddParameter("@inv_Type_dst", this.InvestmentTypeNumberDst);
                db.AddParameter("@money_type", this.MoneyTypeNmbr);
                db.AddParameter("@gross_amt", this.TransactionAmount);
                db.AddParameter("@doc_recv_dt", this.EffectiveDate);
                db.AddParameter("@batchId", this.BatchId);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public void InsertFundMovementEstimation(Database db)
        {
            String query = "exec sp_fund_mvnt_est_i_Grp @group_nmbr, @efctv_dt, @inv_Type_src, @inv_Type_dst, @gross_amt, @doc_recv_dt, @batchId";

            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@efctv_dt", this.EffectiveDate);
                db.AddParameter("@inv_Type_src", this.InvestmentTypeNumber);
                db.AddParameter("@inv_Type_dst", this.InvestmentTypeNumberDst);
                db.AddParameter("@gross_amt", this.TransactionAmount);
                db.AddParameter("@doc_recv_dt", this.EffectiveDate);
                db.AddParameter("@batchId", this.BatchId);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public string GetCalculationMT(Database db)
        {
            string query = "usp_money_asset_group @group_nmbr,@efctv_dt," +
                    "@money_type_nmbr,@inv_type_nmbr,@inv_type_nmbr_dst,@calc_method," +
                    "@amt";
            string output = "";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@efctv_dt", this.EffectiveDate);
                db.AddParameter("@money_type_nmbr", this.MoneyTypeNmbr);
                db.AddParameter("@inv_type_nmbr", this.InvestmentTypeNumber);
                db.AddParameter("@inv_type_nmbr_dst", this.InvestmentTypeNumberDst);
                db.AddParameter("@calc_method", this.Mode);
                db.AddParameter("@amt", this.TransactionAmount);

                output = Convert.ToString(db.ExecuteScalar());

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return output;
        }

        public string GetCalculation(Database db)
        {
            string query = "usp_fund_asset_group @group_nmbr,@efctv_dt," +
                    "@inv_type_nmbr,@inv_type_nmbr_dst,@calc_method," +
                    "@amt";
            string output = "";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@efctv_dt", this.EffectiveDate);
                db.AddParameter("@inv_type_nmbr", this.InvestmentTypeNumber);
                db.AddParameter("@inv_type_nmbr_dst", this.InvestmentTypeNumberDst);
                db.AddParameter("@calc_method", this.Mode);
                db.AddParameter("@amt", this.TransactionAmount);

                output = Convert.ToString(db.ExecuteScalar());

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return output;
        }

        public static DataTable BindEmptyGvFund(Database db)
        {
            DataTable result;
            string query = "CREATE TABLE #TEMPGRID(inv_type_nm varchar(100),acct_val float) " +
                  "select * from #TEMPGRID " +
                  "Drop table #TempGRID";
            try
            {
                db.setQuery(query);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return result;
        }

        public static DataTable BindEmptyGvEst(Database db)
        {
            DataTable result;
            string query = "CREATE TABLE #TEMPGRID(inv_type_nm varchar(100),acct_val float) " +
                  "select * from #TEMPGRID " +
                  "Drop table #TempGRID";
            try
            {
                db.setQuery(query);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return result;
        }

        public static DataTable FillGvFundMT(Database db, int groupNmbr)
        {
            DataTable output;
            string query = "usp_calc_asset_group_money_type @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);
                output = db.ExecuteQuery();
            }
            catch (Exception)
            {

                throw;
            }

            return output;
        }

        public static DataTable FillGvFund(Database db, int groupNmbr)
        {
            DataTable output;
            string query = "usp_calc_asset_group @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);
                output = db.ExecuteQuery();
            }
            catch (Exception)
            {

                throw;
            }

            return output;
        }

        public static string GetTransactionDt(Database db)
        {
            string query = "GET_CYCLE_DT";
            string output = "";
            try
            {
                db.setQuery(query);
                output = Convert.ToString(db.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return output;
        }

        public static string GetCompanyNmById(Database db, int groupNmbr)
        {
            string output = "";
            string query = "select company_nm from group_info g join company co on co.client_nmbr = g.client_nmbr where group_nmbr =  @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        output = reader[0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return output;
        }

        public void FirstSaveCompositionMT(Database db)
        {
            string query = "usp_money_mvmnt_comp_grp " +
                        "@group_nmbr," +
                        "@efctv_dt," +
                        "@inv_type_nmbr," +//inv type 
                        "@money_type_nmbr," +
                        "@current_asset," +
                        "@composition_pct," + //composition pct
                        "@trns_amt," + //trns amt
                        "@inv_type_nm," +
                        "@money_type_nm," +
                        "@mode"; //mode
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@efctv_dt", this.EffectiveDate);
                db.AddParameter("@inv_type_nmbr", 0);
                db.AddParameter("@money_type_nmbr", 0);
                db.AddParameter("@current_asset", this.CurrentAsset);
                db.AddParameter("@composition_pct", 100);
                db.AddParameter("@trns_amt", 0);
                db.AddParameter("@inv_type_nm", this.InvestmentTypeName);
                db.AddParameter("@money_type_nm", this.MoneyTypeNm);
                db.AddParameter("@mode", 0);

                db.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void FirstSaveComposition(Database db)
        {
            string query = "usp_fund_mvmnt_comp_grp " +
                        "@group_nmbr," +
                        "@efctv_dt," +
                        "@inv_type_nmbr," +//inv type nmbr
                        "@current_asset," +
                        "@composition_pct," + //composition pct
                        "@trns_amt," + //trns amt
                        "@inv_type_nm," +
                        "@mode"; //mode
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@efctv_dt", this.EffectiveDate);
                db.AddParameter("@inv_type_nmbr", 0);
                db.AddParameter("@current_asset", this.CurrentAsset);
                db.AddParameter("@composition_pct", 100);
                db.AddParameter("@trns_amt", 0);
                db.AddParameter("@inv_type_nm", this.InvestmentTypeName);
                db.AddParameter("@mode", 0);

                db.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}