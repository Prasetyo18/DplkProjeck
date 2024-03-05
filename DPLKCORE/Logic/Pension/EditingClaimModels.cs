using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class EditingClaimModels
    {
        public string register { get; set; }

        public int tfer_type_nmbr { get; set; }
        public int trns_seq_nmbr { get; set; }
        public int cer_nmbr { get; set; }
        public string client_nm { get; set; }
        public string company_nm { get; set; }
        public string tfer_type_nm { get; set; }
        public double? tfer_amt { get; set; }
        public string bank_central_nm { get; set; }
        public string bank_addr { get; set; }
        public string acct_nmbr { get; set; }
        public string acct_nm { get; set; }
        public string kode_bank { get; set; }
        public int batch_id { get; set; }


        public bool ValidateClaim(Database db)
        {
            string query = "select client_nm from client cl join certificate c on cl.client_nmbr = c.client_nmbr where cer_nmbr = @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.cer_nmbr);
                DataTable dt = db.ExecuteQuery();
                if (dt.Rows.Count > 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool DeleteClaim(Database db)
        {
            string query = "exec usp_delete_claim @cer_nmbr,@batch_id";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.cer_nmbr);
                db.AddParameter("@batch_id", this.batch_id);
                db.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditClaim(Database db)
        {
            String query = "EXEC dbo.usp_editing_claim_u @cer_nmbr, @trns_seq_nmbr, @tfer_type_nmbr, @Bank_update, @acct_nmbr, @acct_nm";

            try
            {
                db.setQuery(query);

                db.AddParameter("@cer_nmbr", this.cer_nmbr);
                db.AddParameter("@trns_seq_nmbr", this.trns_seq_nmbr);
                db.AddParameter("@tfer_type_nmbr", this.tfer_type_nmbr);
                db.AddParameter("@Bank_update", this.bank_central_nm);
                db.AddParameter("@acct_nmbr", this.acct_nmbr);
                db.AddParameter("@acct_nm", this.acct_nm);

                db.ExecuteQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getEditClaim(Database db)
        {
            string query = "Exec DGR_ON_SCR_EDITING_CLAIM";
            try
            {
                db.setQuery(query);
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
