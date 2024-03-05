using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class PayoutSuspenseModels
    {
        public long SuspenseNmbr { get; set; }

        public string RegisId { get; set; }
        public string TransferType { get; set; }

        public int SeqNmbr { get; set; }
        public int CertifNmbr { get; set; }
        public string ClientNm { get; set; }
        public string BankNm { get; set; }
        public string AccNmbr { get; set; }
        public string AccNm { get; set; }
        public float Ammount { get; set; }
        public float CheckAmmount { get; set; }
        public string Remarks { get; set; }
        public int ApprovalFlag { get; set; }
        public string PrepareBy { get; set; }
        public string ApprovedBy { get; set; }
        public int InvestTypeNmbr { get; set; }


        public static string GetLatestSeqNmbr(Database db)
        {
            string query = "select seq = max(seq_nmbr) from retur_info";
            string output = "";
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

        public void AddPayoutSuspense(Database db)
        {
            string query = "exec usp_retur_info_iu " +
                               "@seq_nmbr," +
                               "@regisid," +
                               "@cer_nmbr," +
                               "@client_nm," +
                               "@bank_nm_i," +
                               "@acct_nmbr," +
                               "@acct_nm," +
                               "@amount," +
                               "@check_amt," +
                               "@trans_type," +
                               "@remarks," +
                               "@suspense_nmbr," +
                               "@approval_flg," +
                               "@prepare_by," +
                               "@approved_by," +
                               "@inv_type_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@seq_nmbr", this.SeqNmbr);
                db.AddParameter("@regisid", this.RegisId);
                db.AddParameter("@cer_nmbr", this.CertifNmbr);
                db.AddParameter("@client_nm", this.ClientNm);
                db.AddParameter("@bank_nm_i", this.BankNm);
                db.AddParameter("@acct_nmbr", this.AccNmbr);
                db.AddParameter("@acct_nm", this.AccNm);
                db.AddParameter("@amount", this.Ammount);
                db.AddParameter("@check_amt", this.CheckAmmount);
                db.AddParameter("@trans_type", this.TransferType);
                db.AddParameter("@remarks", this.Remarks);
                db.AddParameter("@suspense_nmbr", this.SuspenseNmbr);
                db.AddParameter("@approval_flg", this.ApprovalFlag);
                db.AddParameter("@prepare_by", this.PrepareBy);
                db.AddParameter("@approved_by", this.ApprovedBy);
                db.AddParameter("@inv_type_nmbr", this.InvestTypeNmbr);
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

        public DataTable GetCertifClientData(Database db)
        {
            string query = "select cr.cer_nmbr,client_nm " +
                               "from claim_register cr join " +
                               "certificate c on cr.cer_nmbr = c.cer_nmbr " +
                                " join client cl on c.client_nmbr = cl.client_nmbr " +
                                " where cr.cr_id = @regis";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@regis", this.RegisId);
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

        public DataTable ReadSuspense(Database db)
        {
            string query = "exec usp_suspense_payout_r " +
							   "@regisid," +
							   "@tfer_type";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@regisid", this.RegisId);
                db.AddParameter("@tfer_type",this.TransferType);
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


        public DataTable CheckReturInfo(Database db)
        {
            string query = "select * from retur_info where suspense_nmbr = @suspn_nmbr and approval_dt is null";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@suspn_nmbr", this.SuspenseNmbr);
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

        public DataTable CheckPaymentHistory(Database db)
        {
            string query = "select * from payment_hst where suspn_nmbr = @suspn_nmbr and payment_apprv_cd = 0";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@suspn_nmbr", this.SuspenseNmbr);
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


        public static DataTable GetSuspenseRestData(Database db)
        {
            string query = "EXEC GRID_SUSPNREST_ON_SCR_SUSPENSEAPPROVED";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
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


    }
}


