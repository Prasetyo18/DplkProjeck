using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class ClientTransactionModels
    {
        public int? ClientNmbr { get; set; }
        public int? CertifNmbr { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? TransacSequenceNmbr { get; set; }

        public void GetGroupCompanyTxt(Database db, TextBox txtgroup, TextBox txtcompany)
        {
            string query = "select c.group_nmbr,company_nm from certificate c join group_info gi on c.group_nmbr = gi.group_nmbr join company co on co.client_nmbr = gi.client_nmbr where c.cer_nmbr=@cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertifNmbr);
                DataTable dt = db.ExecuteQuery();
                if (dt.Rows.Count>0)
                {
                    txtgroup.Text = dt.Rows[0]["group_nmbr"].ToString();
                    txtcompany.Text = dt.Rows[0]["company_nm"].ToString();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadTransacSummary(Database db)
        {
            string query = "EXEC DGR_CER_TRNS_SUMMARY @cer_nmbr, @start_dt, @end_dt";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertifNmbr);
                db.AddParameter("@start_dt", this.startTime);
                db.AddParameter("@end_dt", this.endTime);

                DataTable result = db.ExecuteQuery();
                return result; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadTransacDetail(Database db)
        {
            string query = "EXEC DGR_CER_TRNS_DETAIL @cer_nmbr, @trns_seq_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertifNmbr);
                db.AddParameter("@trns_seq_nmbr", this.TransacSequenceNmbr);

                DataTable result = db.ExecuteQuery();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}