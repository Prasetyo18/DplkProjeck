using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DPLKCORE.Framework;

namespace DPLKCORE.Logic.Pension
{
    public class PaymentAfterApproveModels
    {
        public string CompanyName { get; set; }
        public int GroupNumber { get; set; }
        public int PaycenterNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int SequenceNumber { get; set; }
        public int Type { get; set; }
        public string TypeDescription { get; set; }
        public string Amount { get; set; }
        public DateTime ApprovalDate { get; set; }
    

 
        public List<PaymentAfterApproveModels> GetPaymentAfterApproveData()
        {
            List<PaymentAfterApproveModels> paymentData = new List<PaymentAfterApproveModels>();
            Connection con = new Connection();
            String cnstr = con.ConnectionStringPension;
            Database db = new Database(cnstr);

            String query = "SP_GETPAYMENT_AFTER_APPROVE";

            try
            {

                db.Open();
                db.setQuery(query);
                DataTable result = db.ExecuteQuery();

                foreach (DataRow reader in result.Rows)
                {
                    PaymentAfterApproveModels payment = new PaymentAfterApproveModels
                        {
                            CompanyName = reader["company_nm"].ToString(),
                            GroupNumber = Convert.ToInt32(reader["group_nmbr"]),
                            PaycenterNumber = Convert.ToInt32(reader["paycenter_nmbr"]),
                            EffectiveDate = Convert.ToDateTime(reader["efctv_dt"]),
                            SequenceNumber = Convert.ToInt32(reader["seq_nmbr"]),
                            Type = Convert.ToInt32(reader["type"]),
                            TypeDescription = reader["type_description"].ToString(),
                            Amount = reader["amt"].ToString(),
                            ApprovalDate = Convert.ToDateTime(reader["approval_dt"])
                        };

                        paymentData.Add(payment);
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

            return paymentData;
        }

    }
}
