using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DPLKCORE.Logic.Pension
{
    public class BillingStatusModels
    {
        public int GroupNmbr { get; set; }
        public DateTime? NextBillPrd { get; set; }
        public DateTime? NextBillDt { get; set; }
        public int? BillFreqNmbr { get; set; }
        public DateTime? NextCntrbPrd { get; set; }
        public DateTime? NextCntrbDt { get; set; }
        public int? CntrbFreqNmbr { get; set; }
        public DateTime LastChangeDt { get; set; }
        public int? PslFreqNmbr { get; set; }
        public DateTime? NextPslPrd { get; set; }
        public DateTime? NextPslDt { get; set; }
        public short? AutoPrintFlg { get; set; }
        public short? MateraiFlg { get; set; }
        public DateTime? NextPrintDt { get; set; }

        //return value from sp_bill_status_read
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }


        public bool LoadData(Database db)
        {
            string query = "exec sp_bill_status_r @group_nmbr";
            db.setQuery(query);
            db.AddParameter("@group_nmbr", this.GroupNmbr);
            DataTable dt = db.ExecuteQuery();
            if (dt.Rows.Count > 0)
            {
                DataRow firstRow = dt.Rows[0];
                this.NextBillPrd = Convert.ToDateTime(firstRow["next_bill_prd"]);
                this.NextBillDt = Convert.ToDateTime(firstRow["next_bill_dt"]);
                this.NextCntrbPrd = Convert.ToDateTime(firstRow["next_cntrb_prd"]);
                this.NextCntrbDt = Convert.ToDateTime(firstRow["next_cntrb_dt"]);
                if (dt.Rows[0]["next_PSL_dt"].ToString() != "")
                {
                    this.NextPslDt = Convert.ToDateTime(firstRow["next_PSL_dt"]);
                }
                else
                {
                    this.NextPslDt = null;
                }

                if (dt.Rows[0]["next_PSL_prd"].ToString() != "")
                {
                    this.NextPslPrd = Convert.ToDateTime(firstRow["next_PSL_prd"]);
                }
                else
                {
                    this.NextPslPrd = null;
                }
                
                if (this.NextBillDt != null)
                {
                    this.a = firstRow["a"].ToString().Trim();
                }
                if (this.NextCntrbDt != null)
                {
                    this.b = firstRow["b"].ToString().Trim();
                }
                if (this.NextPslDt != null)
                {
                    this.c = firstRow["c"].ToString().Trim();
                }
                return true;
            }
            return false;
            
        }

        public bool InsertBillingStatus(Database x)
        {
            String query = "exec sp_bill_status_i " +
                           "@group_nmbr, @next_bill_prd, @next_bill_dt, @bill_freq_nmbr, " +
                           "@next_cntrb_prd, @next_cntrb_dt, @cntrb_freq_nmbr, @next_psl_prd, " +
                           "@next_psl_dt, @psl_freq_nmbr, @auto_print_flg, @materai_flg, @next_print_dt";

            try
            {
                x.setQuery(query);

                x.AddParameter("@group_nmbr",this.GroupNmbr);
                x.AddParameter("@next_bill_prd", this.NextBillPrd);
                x.AddParameter("@next_bill_dt", this.NextBillDt);
                x.AddParameter("@bill_freq_nmbr", this.BillFreqNmbr);
                x.AddParameter("@next_cntrb_prd", this.NextCntrbPrd);
                x.AddParameter("@next_cntrb_dt", this.NextCntrbDt);
                x.AddParameter("@cntrb_freq_nmbr", this.CntrbFreqNmbr);
                //handle if null
                if (this.NextPslPrd != null)
                {
                    x.AddParameter("@next_psl_prd", this.NextPslPrd);
                }
                else
                {
                    x.AddParameter("@next_psl_prd", DBNull.Value);
                }

                if (this.NextPslDt != null)
                {
                    x.AddParameter("@next_psl_dt", this.NextPslDt);
                }
                else
                {
                    x.AddParameter("@next_psl_dt", DBNull.Value);
                }

                if (true)
                {
                    x.AddParameter("@psl_freq_nmbr", this.PslFreqNmbr);
                }
                else
                {
                    x.AddParameter("@psl_freq_nmbr", DBNull.Value);
                }
                
                x.AddParameter("@auto_print_flg", DBNull.Value);
                x.AddParameter("@materai_flg", DBNull.Value);
                x.AddParameter("@next_print_dt", DBNull.Value);

                x.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

    }


}