using DPLKCORE.Class.Pension;
using DPLKCORE.Class;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class BillContributionModels
    {
        public int ID { get; set; }
        public int Paycenter { get; set; }
        public DateTime Period { get; set; }
        public string Amount { get; set; }
        public string PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ReversalDate { get; set; }
        public string Comment { get; set; }
        public string PaycenterName { get; set; }
        public String ClientNmbr { get; set; }
        public String CompanyNm { get; set; }


        public void LoadDataParamToDDL(DropDownList ddl, DPLKCORE.Framework.Database db)
        {
            BillContributionModels m = new BillContributionModels();
            List<BillContributionModels> data = new List<BillContributionModels>();

            data.AddRange(this.GetDDLCompanyBilCon(db));

            ddl.DataSource = data;
            ddl.DataValueField = "ClientNmbr";
            ddl.DataTextField = "CompanyNm";
            ddl.DataBind();
        }


        public List<BillContributionModels> GetDDLCompanyBilCon(DPLKCORE.Framework.Database db)
        {
            List<BillContributionModels> data = new List<BillContributionModels>();

            String query = "exec DDL_COMPANY_ON_SCR_GROUP_NEW";



            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    BillContributionModels m = new BillContributionModels();


                    if (reader["client_nmbr"] != DBNull.Value)
                        m.ClientNmbr = reader["client_nmbr"].ToString().Trim();

                    if (reader["company_nm"] != DBNull.Value)
                        m.CompanyNm = reader["company_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }




    }

    public class UspFppPerkiraan
    {
        public int SeqNmbr { get; set; }
        public int GroupNmbr { get; set; }
        public int ModeOfTransaction { get; set; }
        public string UserNm { get; set; }
        public string ApprovalNm { get; set; }

        public void UspSendNotification(Database db)
        {
            string query = "usp_send_notification";
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

        public void ExecUspFppPerkiraan(Database db)
        {
            string query = "exec usp_fpp_perkiraan @seq_nmbr, @group_nmbr, @modeOfTransaction, @user_nm, @approval_nm";
            try
            {
                db.setQuery(query);
                db.AddParameter("@seq_nmbr", this.SeqNmbr);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@modeOfTransaction", this.ModeOfTransaction);
                db.AddParameter("@user_nm", this.UserNm);
                db.AddParameter("@approval_nm", this.ApprovalNm);

                db.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    public class PaymentHistory
    {
        public long SuspenseNumber { get; set; }
        public int GroupNumber { get; set; }
        public int PaycenterNumber { get; set; }
        public int PaymentTransactionNumber { get; set; }
        public int BillContributionNumber { get; set; }
        public int PaymentApprovalCode { get; set; }
        public DateTime PaidDate { get; set; }
        public int PaymentSequenceNumber { get; set; }
        public float SuspenseRestAmount { get; set; }
        public float PaidAmount { get; set; }
        public DateTime LastChangeDate { get; set; }
        public string Notes { get; set; }



        public class BillContributionScreenParameters
        {
            public int GroupNumber { get; set; }
            public int Mode { get; set; }
            public int SeqNmbr { get; set; }


            public int CheckTerminateNew(Database db)
            {
                int status;
                string query = "usp_check_terminate_NEW " +
                            "@mode," +
                            "@seq_nmbr," +
                            "@group_nmbr";
                try
                {
                    db.setQuery(query);
                    db.AddParameter("@mode", this.Mode);
                    db.AddParameter("@seq_nmbr", this.SeqNmbr);
                    db.AddParameter("@group_nmbr", this.GroupNumber);

                    status = Convert.ToInt32(db.ExecuteScalar());
                    return status;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }


            public DataTable GetBillContributionScreen(Database db)
            {
                string query = "EXEC sp_billcntrb_screen_new @group_nmbr, @mode";
                DataTable result;
                try
                {
                    db.setQuery(query);
                    db.AddParameter("@group_nmbr", this.GroupNumber);
                    db.AddParameter("@mode", this.Mode);
                    result = db.ExecuteQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return result;
            }

        }
        public class PaymentContributionModel
        {
            public int GroupNumber { get; set; }
            public int BillContributionNumber { get; set; }
            public int PaycenterNumber { get; set; }
            public int Trans { get; set; }
            public long SuspenseNumber { get; set; }
            public float SuspenseRestAmount { get; set; }
            public float PaidAmount { get; set; }
            public DateTime? ReceivedDate { get; set; }
            public short WaivedFlag { get; set; }
            public string Note { get; set; }

            public void ProcessPaymentContribution(Database db)
            {
                try
                {
                    db.setQuery("exec sp_payment_cntrb_process @group_nmbr, @billCntrb_seq_nmbr, @paycenter_nmbr, @trns_Type, " +
                        "@suspn_nmbr, @suspn_rest_amt, @paid_amt, @received_dt, @waived_flg, @note");

                    db.AddParameter("@group_nmbr", GroupNumber);
                    db.AddParameter("@billCntrb_seq_nmbr", BillContributionNumber);
                    db.AddParameter("@paycenter_nmbr", PaycenterNumber);
                    db.AddParameter("@trns_Type", Trans);
                    db.AddParameter("@suspn_nmbr", SuspenseNumber);
                    db.AddParameter("@suspn_rest_amt", SuspenseRestAmount);
                    db.AddParameter("@paid_amt", PaidAmount);
                    if (ReceivedDate == null)
                    {
                        db.AddParameter("@received_dt", DBNull.Value);
                    }
                    else
                    {
                        db.AddParameter("@received_dt", ReceivedDate);
                    }
                    
                    db.AddParameter("@waived_flg", WaivedFlag);

                    if (Note == null)
                    {
                        db.AddParameter("@note", DBNull.Value);
                    }
                    else
                    {
                        db.AddParameter("@note", Note);
                    }

                    db.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public class PaymentContributionCompletedModel
        {
            public int GroupNumber { get; set; }
            public int PaycenterNumber { get; set; }
            public int BillContributionSeqNumber { get; set; }
            public float PaidAmount { get; set; }
            public int TransType { get; set; }
            public string Comment { get; set; }

            public void ProcessPaymentContributionCompleted(Database db)
            {
                try
                {
                    db.setQuery("exec sp_payment_cntrb_completed @group_nmbr, @paycenter_nmbr, @billCntrb_seq_nmbr, @paid_amt, @trns_Type, @comment");

                    db.AddParameter("@group_nmbr", GroupNumber);
                    db.AddParameter("@paycenter_nmbr", PaycenterNumber);
                    db.AddParameter("@billCntrb_seq_nmbr", BillContributionSeqNumber);
                    db.AddParameter("@paid_amt", PaidAmount);
                    db.AddParameter("@trns_Type", TransType);
                    db.AddParameter("@comment", Comment);

                    db.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public class ReversalBillContributionModel
        {
            public int Mode { get; set; }
            public int GroupNumber { get; set; }
            public int SequenceNumber { get; set; }

            public void ReversalBillContribution(Database db)
            {
                try
                {
                    db.setQuery("exec sp_reversal_bill_cntrb_NEW @mode, @group_nmbr, @seq_nmbr");

                    db.AddParameter("@mode", Mode);
                    db.AddParameter("@group_nmbr", GroupNumber);
                    db.AddParameter("@seq_nmbr", SequenceNumber);

                    db.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}