using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class ClientClaimHistoricalModels
    {
        public int? ClientNmber { get; set; }
        public int? CerNmbr { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionTypeNm { get; set; }
        public string RegisterId { get; set; }
        public int? SequenceId { get; set; }


        public void GetGroupCompanyTxt(Database db, TextBox txtgroup, TextBox txtcompany)
        {
            string query = "select c.group_nmbr,company_nm from certificate c join group_info gi on c.group_nmbr = gi.group_nmbr join company co on co.client_nmbr = gi.client_nmbr where c.cer_nmbr=@cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                DataTable dt = db.ExecuteQuery();
                if (dt.Rows.Count > 0)
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


        public DataTable LoadGvAvailableClaim(Database db)
        {
            string query = "exec DGR_CLAIM_AVAILABLE_ON_CERTIFICATE_CLAIM_HISTORY @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                DataTable result = db.ExecuteQuery();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadGvClaim(Database db)
        {
            string query = "exec DGR_CLAIM_ON_CERTIFICATE_CLAIM_HISTORY @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                DataTable result = db.ExecuteQuery();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Dictionary<string, object> LoadDetailSection(Database db)
        {
            string query = "exec usp_Certificate_Claim_detail @cer_nmbr, @trns_dt, @trns_type_nm, @register_id, @seq_id";
            Dictionary<string, object> output = new Dictionary<string, object>();
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                db.AddParameter("@trns_dt", this.TransactionDate);
                db.AddParameter("@trns_type_nm", this.TransactionTypeNm);
                db.AddParameter("@register_id", this.RegisterId);
                db.AddParameter("@seq_id", this.SequenceId);
                DataTable result = db.ExecuteQuery();

                DataRow firstRow = result.Rows[0];

                foreach (DataColumn col in result.Columns)
                {
                    output.Add(col.ColumnName, firstRow[col]);
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadGvDetail(Database db)
        {
            string query = "exec DGR_HISTORICAL_CLAIM_STATUS_ON_CERTIFICATE_CLAIM_HISTORY @register_id";
            try
            {
                db.setQuery(query);
                db.AddParameter("@register_id", this.RegisterId);
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