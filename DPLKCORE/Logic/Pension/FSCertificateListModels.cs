using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class FSCertificateListModels
    {
        public DateTime? EfctvDt { get; set; }
        public string CerNmbr { get; set; }
        public int BatchId { get; set; }
        public string UID { get; set; }
        public int Mode { get; set; }


        public void CheckSwitchingProcess(Database db)
        {
            string query = "usp_check_switching_process " +
                            "@cer_nmbr, " +
                            "@efctv_dt, " +
                            "@batchid, " +
                            "@uid, " +
                            "@mode";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                db.AddParameter("@efctv_dt", this.EfctvDt);
                db.AddParameter("@batchid", this.BatchId);
                db.AddParameter("@uid", this.UID);
                db.AddParameter("@mode", this.Mode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable GetAllProcessData(Database db)
        {
            DataTable result;
            string query = "select * from unit_price where efctv_dt = (select cycle_dt from cycle) and approval_flg = 1";
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

        public static DataTable LoadSummaryData(Database db, string TransactionDt)
        {
            DataTable result;
            string query = "sp_fund_mvmnt_grd @cer_nmbr, @efctv_dt, @mode, @batch_id";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", 0);
                db.AddParameter("@efctv_dt", DateTime.Parse(TransactionDt));
                db.AddParameter("@mode", 3);
                db.AddParameter("@batch_id", DBNull.Value);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return result;
        }

        public static DataTable LoadProcessData(Database db)
        {
            DataTable result;
            string query = "GRID_APROVED_ON_SCR_FUNDSWITCHING";
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

        public static DataTable LoadProcessData(Database db, string TransactionDt)
        {
            DataTable result;
            string query = "sp_fund_mvmnt_grd @cer_nmbr, @efctv_dt, @mode, @batch_id";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", 0);
                db.AddParameter("@efctv_dt", DateTime.Parse(TransactionDt));
                db.AddParameter("@mode", 6);
                db.AddParameter("@batch_id", DBNull.Value);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return result;
        }

        public static DataTable LoadListData(Database db, string TransactionDt)
        {
            DataTable result;
            string query = "sp_fund_mvmnt_grd @cer_nmbr, @efctv_dt, @mode, @batch_id";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", 0);
                db.AddParameter("@efctv_dt", DateTime.Parse(TransactionDt));
                db.AddParameter("@mode", 4);
                db.AddParameter("@batch_id", DBNull.Value);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return result;
        }




    }

    public class FPP
    {

        public int SeqNmbr { get; set; }
        public int GroupNmbr { get; set; }
        public int ModeOfTransaction { get; set; }
        public string UserNm { get; set; }
        public string ApprovalNm { get; set; }

        public void UpdateFppPerkiraan(Database db)
        {
            string query = "usp_fpp_perkiraan " +
                        "@seq_nmbr," +
                        "@group_nmbr," +
                        "@modeOfTransaction," +
                        "@user_nm,@approval_nm";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@seq_nmbr", this.SeqNmbr);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@modeOfTransaction", this.ModeOfTransaction);
                db.AddParameter("@user_nm", this.UserNm);
                if (this.ApprovalNm == "")
                {
                    db.AddParameter("@approval_nm", DBNull.Value);
                }
                else 
                {
                    db.AddParameter("@approval_nm", this.ApprovalNm);
                }
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


    public class FMTransactionHistory
    {
        public int CerNmbr { get; set; }
        public DateTime? EfctvDt { get; set; }
        public int BatchId { get; set; }

        public void InserFMTransactionHistorySwitch(Database db)
        {
            string query = "sp_fm_trnshst_i_switch " +
                        "@cer_nmbr," +
                        "@efctv_dt," +
                        "@batch_id";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                db.AddParameter("@efctv_dt", this.EfctvDt);
                db.AddParameter("@batch_id", this.BatchId);

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

        public void InsertFmTransactionHistory(Database db)
        {
            string query = "sp_fm_trnshst_i " +
                        "@cer_nmbr," +
                        "@efctv_dt," +
                        "@batch_id";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                db.AddParameter("@efctv_dt", this.EfctvDt);
                db.AddParameter("@batch_id", this.BatchId);

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