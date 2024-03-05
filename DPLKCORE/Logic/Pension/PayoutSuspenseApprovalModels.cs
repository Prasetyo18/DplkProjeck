using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DPLKCORE.Logic.Pension
{
    public class PayoutSuspenseApprovalModels
    {
        public long SeqNumber { get; set; }
        public int GroupNmbr { get; set; }
        public string UserName { get; set; }
        public string ClaimRequestId { get; set; }
        public float GrossAmmount { get; set; }
        public long SuspenseNumber { get; set; }



        public string RegisterID { get; set; }
        public string CertificateNumber { get; set; }
        public string ClientName { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public decimal CheckAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Remarks { get; set; }
        
        public string PreparedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }


        public void UpdateReturInfo(Database db)
        {
            string query = "update retur_info set " + 
										   "approval_dt = (select cycle_dt from cycle), " +
										   "approved_by = @username, " + 
										   "approval_flg = 1 " +
										   "where seq_nmbr = @seq_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@username", this.UserName);
                db.AddParameter("@seq_nmbr", this.SeqNumber);
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

        public DataTable ValidateClaimRegister(Database db)
        {
            string query = "select cr_id from claim_register where cr_id = @cr_id";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@cr_id", this.ClaimRequestId);
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

        public void UpdateClaimTrackRegister(Database db)
        {
            string query = "insert claim_register_track select @cr_id,13, @username, getdate()";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@cr_id", this.ClaimRequestId);
                db.AddParameter("@username", this.UserName);
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

        public void UpdateSuspenseHistory(Database db)
        {
            string query = "update suspense_hst set suspn_use_amt = suspn_use_amt + @gross_amt where suspn_nmbr = @suspn_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@gross_amt", this.GrossAmmount);
                db.AddParameter("@suspn_nmbr", this.SuspenseNumber);
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


        public static DataTable GetPendingPayoutSuspenseApproval(Database db)
        {
            string query = "select seq_nmbr," +
                    "regisid," +
                    "cer_nmbr," +
                    "client_nm," +
                    "bank_nm = coalesce(bank_nm,'')," +
                    "bank_address = coalesce(bank_address,'')," +
                    "acct_nmbr," +
                    "acct_nm," +
                    "amount," +
                    "check_amt," +
                    "net_amt = amount - check_amt," +
                    "Remarks," +
                    "Suspense_nmbr," +
                    "Prepare_by," +
                    "approval_dt " +
                    "from retur_info " +
                    "where approval_dt is null " +
                    "order by retur_dt asc ";
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

        public void DeleteFPPTable(Database db)
        {
            string query = "delete retur_info where seq_nmbr = @seq_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@seq_nmbr", this.SeqNumber);
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

        public void DeleteReturInfo(Database db)
        {
            string query = "delete tbl_fppflp_perkiraan where seq_nmbr = @seq_nmbr and mode_trans in(14,15) and grp = @group_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@seq_nmbr", this.SeqNumber);
                db.AddParameter("@group_nmbr",this.GroupNmbr);

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

        
    }
}
