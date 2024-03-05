using DPLKCORE.Framework;
using iTextSharp.text.pdf.qrcode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class FundSwitchingModels
{
    public int CertificateNumber { get; set; }
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


    public string GetCalculationMT(Database db)
    {
        string query = "usp_money_asset @cer_nmbr, @efctv_dt," +
                "@money_type_nmbr, @inv_type_nmbr, @inv_type_nmbr_dst," +
                "@calc_method," +
                "@amt";
        string output = "";
        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", this.CertificateNumber);
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
        string query = "usp_fund_asset @cer_nmbr,@efctv_dt," +
                "@inv_type_nmbr,@inv_type_nmbr_dst,@calc_method," +
                "@amt";
        string output = "";
        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", this.CertificateNumber);
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

    public static DataTable FillGvEstimationMT(Database db, int CerNmbr, string TransactionDt)
    {
        string query = "EXEC GRID_ESTIMATION_ON_SCR_FUNDSWITHCING_MONEY_TYPE @cer_nmbr, @efctv_dt";
        DataTable result;
        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", CerNmbr);
            db.AddParameter("@efctv_dt", TransactionDt);
            result = db.ExecuteQuery();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

        return result;
    }

    public static DataTable FillGvEstimation(Database db, int CerNmbr, string TransactionDt)
    {
        string query = "EXEC GRID_ESTIMATION_ON_SCR_FUNDSWITHCING @cer_nmbr, @efctv_dt";
        DataTable result;
        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", CerNmbr);
            db.AddParameter("@efctv_dt", TransactionDt);
            result = db.ExecuteQuery();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

        return result;
    }

    public void FirstSaveCompositionMT(Database db)
    {
        string query = "usp_money_mvmnt_comp @cer_nmbr," +
					"@efctv_dt," +
					"@inv_type_nmbr," +
					"@money_type_nmbr," +
					"@current_asset," +
					"@composition_pct," +
					"@trns_amt," +
					"@inv_type_nm," +
					"@money_type_nm," +
					"@mode"; //mode
        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", this.CertificateNumber);
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
        string query = "usp_fund_mvmnt_comp "+
                    "@cer_nmbr," +
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
            db.AddParameter("@cer_nmbr", this.CertificateNumber );
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

    public static DataTable FillGvFundMT(Database db, int CerNmbr, string TransactionDt)
    {
        DataTable output;
        string query = "EXEC SP_FUND_INFO_R @cer_nmbr, @as_of_dt, 3";
        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", CerNmbr);
            db.AddParameter("@as_of_dt", TransactionDt);
            output = db.ExecuteQuery();
        }
        catch (Exception)
        {

            throw;
        }

        return output;
    }

    public static DataTable FillGvFund(Database db, int CerNmbr, string TransactionDt)
    {
        DataTable output;
        string query = "EXEC SP_FUND_INFO_R @cer_nmbr, @as_of_dt, 5";
        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", CerNmbr);
            db.AddParameter("@as_of_dt", TransactionDt);
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


    public static string GetClientNamebyID(Database db, int cerNmbr)
    {
        string output = "";
        string query = "EXEC TXT_CERTIFICATE_GROUP_NAME_ON_SCR_JOINACCOUNT @cer_nmbr";
        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", cerNmbr);
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

    public static DataTable BindEmptyGvEstMT(Database db)
    {
        DataTable result;
        string query = "CREATE TABLE #TEMPGRID(inv_type_scr varchar(100),inv_type_dst varchar(100)" +
                ",money_type_nmbr varchar(100),amount float) " +
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
        string query = "CREATE TABLE #TEMPGRID(inv_type_scr varchar(100),inv_type_dst varchar(100),amount float) " +
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

    public static DataTable BindEmptyGvFundMT(Database db)
    {
        DataTable result;
        string query = "CREATE TABLE #TEMPGRID(inv_type_nm varchar(100), money_type_nm varchar(100), acct_val float) " +
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

    public static string GetLatestBatchID(Database db){
        string query = "select dbo.F_counter_number(1200)";
        string output;
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

    public static void UpdateLastBatchId(Database db)
    {
        string query = "update counter set last_nmbr = last_nmbr + 1 where counter_nmbr = 1200";
        try
        {
            db.setQuery(query);
            db.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public void CheckSwitchingProcess(Database db, FundSwitchingModels model)
    {
        string query = "exec usp_check_switching_process " +
                       "@cer_nmbr, @efctv_dt, @batchid, @uid, @mode";

        try
        {
            db.setQuery(query);

            db.AddParameter("@cer_nmbr", model.CertificateNumber);
            db.AddParameter("@efctv_dt", model.EffectiveDate);
            db.AddParameter("@batchid", model.BatchId);
            db.AddParameter("@uid", model.UserID);
            db.AddParameter("@mode", model.Mode);

            db.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public void FundMovementApprovalDelete(Database db)
    {
        string query = "EXEC dbo.sp_fund_mvmnt_apprv_delete " +
                       "@cer_nmbr, @efctv_dt, @fund_src, @fund_dst, @mode, @batchid";

        try
        {
            db.Open();
            db.BeginTransaction();

            db.setQuery(query);
            db.AddParameter("@cer_nmbr", CertificateNumber);
            db.AddParameter("@efctv_dt", EffectiveDate);
            db.AddParameter("@fund_src", FundSource);
            db.AddParameter("@fund_dst", FundDestination);
            db.AddParameter("@mode", Mode);
            db.AddParameter("@batchid", BatchId);
            db.ExecuteNonQuery();

            db.CommitTransaction();
        }
        catch (Exception ex)
        {
            db.RollbackTransaction();
            throw new Exception(ex.Message);
        }
        finally
        {
            db.Close();
        }
    }



    public void ExecuteTransactionHistory(Database db)
    {
        String query = "exec sp_fm_trnshst_i @cer_nmbr, @efctv_dt, @batch_id";

        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", CertificateNumber);
            db.AddParameter("@efctv_dt", EffectiveDate);
            db.AddParameter("@batch_id", BatchId);

            db.ExecuteQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void GetApprovedFundsSwitchingGrid(Database db)
    {
        String query = "exec GRID_APROVED_ON_SCR_FUNDSWITCHING";

        try
        {
            db.setQuery(query);

            var result = db.ExecuteQuery();


            /*     foreach (var row in result.Rows)
                 {

                     int cerNmbr = (int)row["cer_nmbr"];
                     DateTime efctvDt = (DateTime)row["Efctv_dt"];
                     string companyNm = row["company_nm"].ToString(); 
                     double acctVal = Convert.ToDouble(row["Acct_val"]);
                     int batchId = (int)row["batch_ID"];
                     int approveFlg = (int)row["approve_flg"];
                     string status = (approveFlg == 1) ? "Ready To Process" : (approveFlg == 10) ? "Request Fund From Cash Management" : "Unknown Status";

                 }*/
        }
        catch (Exception ex)
        {
            // Handle the exception as needed
            throw new Exception(ex.Message);
        }

    }
    public DataTable GetFundMovementGrid(Database db)
    {
        String query = "exec sp_fund_mvmnt_grd @cer_nmbr, @efctv_dt, @mode, @batch_id";

        try
        {
            db.Open();
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", CertificateNumber);
            db.AddParameter("@efctv_dt", EffectiveDate);
            db.AddParameter("@mode", Mode);
            db.AddParameter("@batch_id", BatchId);

            DataTable result = db.ExecuteQuery();

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            db.Close();
        }

    }

    public void InsertFundMovementEstimationMT(Database db)
    {
        String query = "exec sp_money_mvnt_est_i @cer_nmbr, @efctv_dt, @inv_Type_src, @inv_Type_dst, @money_type, @gross_amt, @doc_recv_dt, @batchId";

        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", this.CertificateNumber);
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
        String query = "exec sp_fund_mvnt_est_i @cer_nmbr, @efctv_dt, @inv_Type_src, @inv_Type_dst, @gross_amt, @doc_recv_dt, @batchId";

        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", this.CertificateNumber);
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
    public void GetCertificateGroupName(Database db)
    {
        String query = "exec TXT_CERTIFICATE_GROUP_NAME_ON_SCR_JOINACCOUNT @cer_nmbr";

        try
        {
            db.setQuery(query);
            db.AddParameter("@cer_nmbr", CertificateNumber);

            var result = db.ExecuteQuery();


            /*       foreach (var row in result.Rows)
                   {
                       string clientName = row["client_nm"].ToString();
                       string companyName = row["company_nm"].ToString();

                   }*/
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }


    }
    public void ManageFundMovementComponentMT(Database db)
    {
        String query = "exec usp_money_mvmnt_comp @cer_nmbr, @efctv_dt, @inv_type_nmbr, @money_type_nmbr,  @current_asset, @composition_pct, @trns_amt, @inv_type_nm, @money_type_nm, @mode";

        try
        {
            db.setQuery(query);

            db.AddParameter("@cer_nmbr", CertificateNumber);
            db.AddParameter("@efctv_dt", EffectiveDate);
            db.AddParameter("@inv_type_nmbr", InvestmentTypeNumber);
            db.AddParameter("@money_type_nmbr", this.MoneyTypeNmbr);
            db.AddParameter("@current_asset", CurrentAsset);
            db.AddParameter("@composition_pct", CompositionPercentage);
            db.AddParameter("@trns_amt", TransactionAmount);
            db.AddParameter("@inv_type_nm", InvestmentTypeName);
            db.AddParameter("@money_type_nm", this.MoneyTypeNm);
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
        String query = "exec usp_fund_mvmnt_comp @cer_nmbr, @efctv_dt, @inv_type_nmbr, @current_asset, @composition_pct, @trns_amt, @inv_type_nm, @mode";

        try
        {
            db.setQuery(query);

            db.AddParameter("@cer_nmbr", CertificateNumber);
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

    public List<FundSwitchingResult> GetFundEstimationResults(Database db)
    {
        List<FundSwitchingResult> results = new List<FundSwitchingResult>();

        String query = "exec GRID_ESTIMATION_ON_SCR_FUNDSWITHCING @cer_nmbr, @efctv_dt";

        try
        {
            db.setQuery(query);

            db.AddParameter("@cer_nmbr", CertificateNumber);
            db.AddParameter("@efctv_dt", EffectiveDate);

            var reader = db.ExecuteReader();

            while (reader.Read())
            {
                FundSwitchingResult result = new FundSwitchingResult
                {
                    InvestmentTypeSource = reader["inv_type_src"].ToString(),
                    InvestmentTypeDestination = reader["inv_type_dst"].ToString(),
                    Amount = Convert.ToDecimal(reader["amount"])
                };

                results.Add(result);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return results;
    }
}

public class FundSwitchingResult
{
    public string InvestmentTypeSource { get; set; }
    public string InvestmentTypeDestination { get; set; }
    public decimal Amount { get; set; }
}

public class FundSwitchingByMoneyType
{
    public int CertificateNumber { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string InvestmentTypeSource { get; set; }
    public string InvestmentTypeDestination { get; set; }
    public int MoneyTypeNumber { get; set; }
    public decimal Amount { get; set; }

    public void GetEstimationFunds(Database db)
    {
        try
        {
            db.setQuery("exec GRID_ESTIMATION_ON_SCR_FUNDSWITHCING_MONEY_TYPE @cer_nmbr, @efctv_dt");

            db.AddParameter("@cer_nmbr", CertificateNumber);
            db.AddParameter("@efctv_dt", EffectiveDate);

            db.ExecuteNonQuery();

            //while (db.DataReader.Read())
            //{
            //    // Map the results to the model properties
            //    InvestmentTypeSource = db.DataReader["inv_type_src"].ToString();
            //    InvestmentTypeDestination = db.DataReader["inv_type_dst"].ToString();
            //    MoneyTypeNumber = Convert.ToInt32(db.DataReader["money_type_nmbr"]);
            //    Amount = Convert.ToDecimal(db.DataReader["amount"]);
            //}

            //db.DataReader.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public class CertificateGroupNameModel
    {
        public int CertificateNumber { get; set; }
        public string ClientName { get; set; }
        public string CompanyName { get; set; }

        public void GetCertificateGroupName(Database db)
        {

            db.setQuery("exec TXT_CERTIFICATE_GROUP_NAME_ON_SCR_JOINACCOUNT @cer_nmbr");
            db.AddParameter("@cer_nmbr", CertificateNumber);

            using (var reader = db.ExecuteReader())
            {
                if (reader.Read())
                {
                    ClientName = reader["client_nm"].ToString();
                    CompanyName = reader["company_nm"].ToString();
                }
            }
        }
    }
}
