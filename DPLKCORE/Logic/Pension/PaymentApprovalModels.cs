using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class PaymentApprovalModels
    {
        public int GroupNmbr { get; set; }
        public int SeqNmbr { get; set; }
        public int TransType { get; set; }
        public int PaycenterNmbr { get; set; }
        public int ApproveFlag { get; set; }
        public string UserName { get; set; }

        public int TransId { get; set; }
        public float Ammount { get; set; }


        public void CancelApproved(Database db)
        {
            string query = "update contrib_req set paid_dt = null where group_nmbr = @group_nmbr and cntrb_seq_nmbr = @seq_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@cntrb_seq_nmbr", this.SeqNmbr);
                db.ExecuteNonQuery();
                db.CommitTransaction();
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

        public DataTable ExecApprovalLayer(Database db)
        {
            string query = "exec usp_approval_layer " +
                                       "@trns_id, " +
                                       "@user_nm," +
                                       "@amt";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@trns_id",this.TransId);
                db.AddParameter("@user_nm", this.UserName);
                db.AddParameter("@amt", this.Ammount);
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

        public static bool VerifyUnitPrice(Database db)
        {
            string query = "select * from unit_price u join cycle s on u.efctv_dt = s.cycle_dt and u.approval_flg = 1";
            try
            {
                db.Open();
                db.setQuery(query);
                DataTable result = db.ExecuteQuery();
                if (result.Rows.Count <= 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return true;
        }

        public void ApprovePayment(Database db)
        {
            string query = "sp_approve_payment " +
                        "@group_nmbr," +
                        "@seq_nmbr," +
                        "@trnsType," +
                        "@paycenter_nmbr," +
                        "@approveFlg," +
                        "@user_nm";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@group_nmbr",this.GroupNmbr);
                db.AddParameter("@seq_nmbr", this.SeqNmbr);
                db.AddParameter("@trnsType", this.TransType);
                db.AddParameter("@paycenter_nmbr", this.PaycenterNmbr);
                db.AddParameter("@approveFlg", this.ApproveFlag);
                db.AddParameter("@user_nm", this.UserName);

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


        public static DataTable GetCanceledData(Database db)
        {
            string query = "EXEC SP_GETPAYMENT_AFTER_CANCEL";
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


        public static DataTable GetApprovedData(Database db)
        {
            string query = "EXEC SP_GETPAYMENT_AFTER_Approve";
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



        public static DataTable GetUnapproveData(Database db)
        {
            string query = "EXEC sp_get_payment_forApproval";
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