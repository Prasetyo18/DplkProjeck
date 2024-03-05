using DPLKCORE;
using DPLKCORE.Framework;
using iTextSharp.text.pdf.qrcode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class CertificateMovementModels
    {
        public int CertificateNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int InvestmentTypeNumber { get; set; }
        public int DestinationInvestmentTypeNumber { get; set; }
        public int CalculationMethod { get; set; }
        public float Amount { get; set; }
        public int OldGroupNumber { get; set; }
        public int GroupNumber { get; set; }
        public int NewGroupNumber { get; set; }
        public string Message { get; set; }
        public int Mode { get; set; }
        public float CurrentAsset { get; set; }
        public float CompositionPercentage { get; set; }
        public float TransactionAmount { get; set; }
        public string InvestmentTypeName { get; set; }
        public string InvestmentTypeSource { get; set; }
        public string InvestmentTypeDestination { get; set; }
        public float GrossAmount { get; set; }
        public DateTime DocumentReceiveDate { get; set; }
        public int? BatchId { get; set; }
        public string FundSource { get; set; }
        public string FundDestination { get; set; }
        public int? GroupByType { get; set; }


        public DataTable GetDataClient(Database db)
        {
            string query = "select client_nm,gi.group_nmbr,co.company_nm " +
                "from client cl join certificate c on c.client_nmbr = cl.client_nmbr " +
                "join group_info gi on gi.group_nmbr = c.group_nmbr " +
                "join company co on co.client_nmbr = gi.client_nmbr " +
                "where c.cer_nmbr = @cer_nmbr";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return result;
        }




        public void ProcessFundMovementApprovalOrDeletion(Database db)
        {
            String query = "exec sp_fund_mvmnt_apprv_delete " +
                           "@cer_nmbr, @efctv_dt, @fund_src, @fund_dst, @mode, @batchid";

            try
            {
                db.setQuery(query);

                db.AddParameter("@cer_nmbr", CertificateNumber);
                db.AddParameter("@efctv_dt", EffectiveDate);
                db.AddParameter("@fund_src", FundSource);
                db.AddParameter("@fund_dst", FundDestination);
                db.AddParameter("@mode", Mode);
                db.AddParameter("@batchid", BatchId);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CertificateMovementModels> GetEstimationOnFundsSwitching(Database db)
        {
            String query = "exec GRID_ESTIMATION_ON_SCR_FUNDSWITHCING " +
                           "@cer_nmbr, @efctv_dt";

            try
            {
                db.setQuery(query);

                db.AddParameter("@cer_nmbr", CertificateNumber);
                db.AddParameter("@efctv_dt", EffectiveDate);

                DataTable resultTable = db.ExecuteQuery();

                List<CertificateMovementModels> estimations = new List<CertificateMovementModels>();

                foreach (DataRow row in resultTable.Rows)
                {
                    CertificateMovementModels estimation = new CertificateMovementModels
                    {
                        InvestmentTypeSource = row["inv_type_src"].ToString(),
                        InvestmentTypeDestination = row["inv_type_dst"].ToString(),
                        /*                        Amount = Convert.GetTypeCode(row["amount"])
                        */
                    };

                    estimations.Add(estimation);
                }

                return estimations;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void InsertFundMovementEstimation(Database db)
        {
            String query = "exec sp_fund_mvnt_est_i " +
                           "@cer_nmbr, @efctv_dt, @inv_Type_src, @inv_Type_dst, " +
                           "@gross_amt, @doc_recv_dt, @batchId";

            try
            {
                db.setQuery(query);

                db.AddParameter("@cer_nmbr", CertificateNumber);
                db.AddParameter("@efctv_dt", EffectiveDate);
                db.AddParameter("@inv_Type_src", InvestmentTypeSource);
                db.AddParameter("@inv_Type_dst", InvestmentTypeDestination);
                db.AddParameter("@gross_amt", GrossAmount);
                db.AddParameter("@doc_recv_dt", DocumentReceiveDate);
                db.AddParameter("@batchId", BatchId);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void ManageFundMovementComposition(Database db)
        {
            String query = "exec usp_fund_mvmnt_comp " +
                           "@cer_nmbr, @efctv_dt, @inv_type_nmbr, " +
                           "@current_asset, @composition_pct, @trns_amt, " +
                           "@inv_type_nm, @mode";

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

        public DataTable GetFundInfoMT(Database db)
        {
            DataTable result;
            string query = "EXEC SP_FUND_INFO_R @cer_nmbr, @as_of_dt, @groupby_type";
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                db.AddParameter("@as_of_dt", this.EffectiveDate);
                db.AddParameter("@groupby_type", this.GroupByType);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return result;
        }

        public DataTable GetFundInfo(Database db)
        {
            DataTable result;
            string query = "EXEC SP_FUND_INFO_R @cer_nmbr, @as_of_dt, @groupby_type";
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                db.AddParameter("@as_of_dt", this.EffectiveDate);
                db.AddParameter("@groupby_type", this.GroupByType);
                result = db.ExecuteQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return result;
        }

        public DataTable GetInvestmentDirectFunds(Database db)
        {
            DataTable resultTable = new DataTable();

            String query = "exec usp_cer_mvmnt_inv_drct " +
                           "@cer_nmbr, @group_nmbr, @mode";

            try
            {
                db.Open();
                db.setQuery(query);

                db.AddParameter("@cer_nmbr", CertificateNumber);
                db.AddParameter("@group_nmbr", GroupNumber);
                db.AddParameter("@mode", Mode);

                resultTable = db.ExecuteQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }

            return resultTable;
        }

        public float UpdateFundAsset(Database db)
        {
            float result = 0;

            String query = "exec usp_fund_asset " +
                           "@cer_nmbr, @efctv_dt, @inv_type_nmbr, @inv_type_nmbr_dst, @calc_method, @amt";

            try
            {
                db.setQuery(query);

                db.AddParameter("@cer_nmbr", CertificateNumber);
                db.AddParameter("@efctv_dt", EffectiveDate);
                db.AddParameter("@inv_type_nmbr", InvestmentTypeNumber);
                db.AddParameter("@inv_type_nmbr_dst", DestinationInvestmentTypeNumber);
                db.AddParameter("@calc_method", CalculationMethod);
                db.AddParameter("@amt", Amount);

                SqlDataReader reader = db.ExecuteReader();

                if (reader.Read())
                {
                    result = Convert.ToSingle(reader["result"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }


        public DataTable MoveCertificate(Database db)
        {
            String query = "exec sp_cer_movemnt @cer_nmbr, @efctv_dt, @old_grp, @new_grp";
            DataTable result;
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);

                db.AddParameter("@cer_nmbr", CertificateNumber);
                db.AddParameter("@efctv_dt", EffectiveDate);
                db.AddParameter("@old_grp", OldGroupNumber);
                db.AddParameter("@new_grp", NewGroupNumber);

                result = db.ExecuteQuery();
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
            return result;

        }


    }
}