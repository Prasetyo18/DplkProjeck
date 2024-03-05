using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace DPLKCORE.Logic.Pension
{
    public class FundSwitchingRequestFundApprovalModels
    {
        public int ClaimSequence { get; set; }
        public DateTime TransactionEffectiveDate { get; set; }
        public int CertificateNumber { get; set; }
        public string ClientName { get; set; }
        public string TransactionType { get; set; }
        public string UserId { get; set; }
        public int BatchId { get; set; }

        public int typeOfOutput { get; set; }
        public float MM { get; set; }
        public float FIX { get; set; }
        public float SHM { get; set; }
        public float PHKT { get; set; }
        public float PDSI { get; set; }
        public float kondur { get; set; }
        public float emoi { get; set; }
        public float VICO { get; set; }
        public float STAR { get; set; }
        public float CNOOC { get; set; }
        public float PREMIER { get; set; }
        public float ENI { get; set; }
        public float CHEVRON { get; set; }
        public float MANDIRI { get; set; }
        public float magma { get; set; }
        public float PETROCINA { get; set; }
        public float NONFUND { get; set; }
        public float PPUKP_PU { get; set; }
        public float SITAMPAN { get; set; }
        public float PPUKP_PTII { get; set; }
        

        public List<NotificationModel> GetFundSwitchingNotifications(Database db)
        {
            NotificationProcessor notificationProcessor = new NotificationProcessor();
            return notificationProcessor.GetNotifications(db);
        }

        public void Update_Fund_Movement_Est(Database db)
        {
            string query = "update pension.dbo.fund_mvnt_est set approve_flg = 1, processed_dt = GETDATE(), processed_by = @user_id " +
                                            "where cer_nmbr = @cer_nmbr " +
                                            "and batchid = @batch_id  ";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.AddParameter("user_id", this.UserId);
                db.AddParameter("cer_nmbr", this.CertificateNumber);
                db.AddParameter("batch_id", this.BatchId);

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

        public static DataTable Get_CM_Swithcing_approve_amount(Database db, string parameters)
        {
            DataTable result;
            string query = "exec pension.dbo.sp_CM_Swithcing_approve_amount 2, " + parameters;
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

        public static DataTable Get_CM_Swithcing_approve_amount(Database db)
        {
            DataTable result;
            string query = "exec pension.dbo.sp_CM_Swithcing_approve_amount 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
            try
            {
                db.Open();
                db.setQuery(query);
                result = db.ExecuteQuery();

            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                db.Close();
            }
            return result;
        }

    }

    public class NotificationModel
    {
        public string NotificationMessage { get; set; }
        public float Amount { get; set; }
        public string Email { get; set; }

        public static List<NotificationModel> ConvertFromDataTable(DataTable dataTable)
        {
            List<NotificationModel> notifications = new List<NotificationModel>();

            foreach (DataRow row in dataTable.Rows)
            {
                NotificationModel notification = new NotificationModel
                {
                    NotificationMessage = Convert.ToString(row["c1"]),
                    Amount = Convert.ToSingle(row["Amt"]),
                    Email = Convert.ToString(row["email"])
                };

                notifications.Add(notification);
            }

            return notifications;
        }
    }

    public class NotificationProcessor
    {
        public List<NotificationModel> GetNotifications(Database db)
        {
            List<NotificationModel> notifications = new List<NotificationModel>();

            string query = "EXEC dbo.usp_send_notification";
            try
            {
                db.setQuery(query);
                DataTable dataTable = db.ExecuteQuery();

                if (dataTable.Rows.Count > 0)
                {
                    notifications = NotificationModel.ConvertFromDataTable(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return notifications;
        }
    }
}
