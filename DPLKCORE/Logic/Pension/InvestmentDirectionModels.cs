using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DPLKCORE.Framework;
using System.Data.Common;
using System.Data;

namespace DPLKCORE.Logic.Pension
{
    public class InvestmentDirectionModels
    {
        //for sp 1
        public int GroupNmbr { get; set; }
        public short InvTypeNmbr { get; set; }
        public float MaxPercent { get; set; }
        public float MinPercent { get; set; }
        public DateTime LastChangeDt { get; set; }

        // for second sp
        public int ChargeTypeNmbr { get; set; }
        public DateTime ChargeEffectiveDt { get; set; }
        public DateTime NextChargeDt { get; set; }
        public int PaymentResNmbr { get; set; }
        public int FreqTypeNmbr { get; set; }
        public float BillPct { get; set; }
        public float DeducPct { get; set; }
        public float ChargeAmmount { get; set; }
        public float ChargeRate { get; set; }



        public Dictionary<string,object> LoadData(Database db, int groupNmbr)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            string query = "sp_group_charge_inv_r @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);
                DataTable dt = db.ExecuteQuery();

                if (dt.Rows.Count > 0)
                {
                    DataRow firstRow = dt.Rows[0];

                    foreach (DataColumn col in dt.Columns)
                    {
                        output.Add(col.ColumnName, firstRow[col]);
                    } 
                }
                
                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertAll(Database db)
        {
            if (InsertGroupChargeInv(db) && InsertGroupInvestDir(db))
            {
                return true;   
            }
            return false;
        }

        public bool InsertGroupChargeInv(Database db)
        {
            string query = "sp_group_charge_inv_i " +
                           "@group_nmbr, @charge_type_nmbr, @charge_efctv_dt, " +
                           "@next_charge_dt, @pay_rspn_nmbr, @freq_type_nmbr, " +
                           "@inv_type_nmbr, @bill_pct, @deduct_pct, @charge_amt, " +
                           "@charge_rt";
            try
            {
                db.setQuery(query);

                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@charge_type_nmbr", this.ChargeTypeNmbr);
                db.AddParameter("@charge_efctv_dt", this.ChargeEffectiveDt);
                db.AddParameter("@next_charge_dt", this.NextChargeDt);
                db.AddParameter("@pay_rspn_nmbr", this.PaymentResNmbr);
                db.AddParameter("@freq_type_nmbr", this.FreqTypeNmbr);
                db.AddParameter("@inv_type_nmbr", this.InvTypeNmbr);
                db.AddParameter("@bill_pct", this.BillPct);
                db.AddParameter("@deduct_pct", this.DeducPct);
                db.AddParameter("@charge_amt", this.ChargeAmmount);
                db.AddParameter("@charge_rt", this.ChargeRate);

                db.ExecuteNonQuery();
                return true;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool InsertGroupInvestDir(Database x)
        {
            String query = "exec sp_grp_inv_drct_i " +
                           "@group_nmbr, @inv_type_nmbr, @max_percent, " +
                           "@min_percent";

            try
            {
                x.setQuery(query);

                x.AddParameter("@group_nmbr", this.GroupNmbr);
                x.AddParameter("@inv_type_nmbr", this.InvTypeNmbr);
                x.AddParameter("@max_percent", this.MaxPercent);
                x.AddParameter("@min_percent", this.MinPercent);

                x.ExecuteNonQuery();

                InsertGroupChargeInv(x);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }


}