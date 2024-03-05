using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DPLKCORE.Framework;
using System.Data.SqlClient;
using System.Data;

namespace DPLKCORE.Logic.Pension
{
    public class GroupChargeModels
    {
        //nullabel sp params
        
        public DateTime? ChargeTrmntnDt { get; set; }
        public DateTime? NextChargeDt { get; set; }
        public DateTime LastChangeDt { get; set; }

        //required sp params
        public int GroupNmbr { get; set; }
        public int ChargeTypeNmbr { get; set; }
        public DateTime ChargeEfctvDt { get; set; }
        public int PayRspnNmbr { get; set; }
        public int FreqTypeNmbr { get; set; }

        //required sp params (for sp_grp_charge_rt_i)
        public short maxCnt { get; set; }
        public short freeCnt { get; set; }
        public float chargeAmount { get; set; }
        public float billPercent { get; set; }
        public float deductPercent { get; set; }
        public int sequence { get; set; }
        public float maxChargeAmmount { get; set; }
        public float chargeRate { get; set; }
        public short begPeriod { get; set; }
        public short endPeriod { get; set; }

        public DataTable GetSummaryData(Database db)
        {
            DataTable result;
            string query = "SELECT " +
                "gc.main_chrg_seq_nmbr , " +
                "charge_trmntn_dt = convert(varchar(50),gc.charge_trmntn_dt,106), " +
                "next_charge_dt = convert(varchar(50),gc.next_charge_dt,106), " +
                "ct.charge_type_nm, " +
                "p.pay_rspn_nm, " +
                "ft.freq_type_nm, " +
                /*"gct.charge_rt, " +*/
                "case ct.charge_type_nmbr " +
                "when 210 then (select coi_type_nm from coi_type where coi_type_nmbr = gct.charge_rt) " +
                /*"else cast(gct.charge_rt as varchar(100)) end charge_rt," +	*/
                "else " +
                "case when gct.charge_rt > 100 then (select rate_type_nm from rate_table_type where rate_type_nmbr = gct.charge_rt) " +
                "else  cast(gct.charge_rt as varchar(10)) end " +
                "end as charge_rt, " +
                "charge_amt = convert(varchar(100),convert(money,gct.charge_amt),1), " +
                "gct.bill_pct, " +
                "gct.deduct_pct, " +
                "gct.max_cnt, " +
                "gct.free_cnt, " +
                "charge_efctv_dt = convert(varchar(50),gc.charge_efctv_dt,106), " +
                "max_charge_amt = convert(varchar(100),convert(money,gct.max_charge_amt),1) " +
                "FROM Group_Charge gc  " +
                "inner join Charge_type ct on gc.charge_type_nmbr = ct.charge_type_nmbr " +
                "left join pay_rspn_type p on gc.pay_rspn_nmbr = p.pay_rspn_nmbr  " +
                "left join Frequency_type ft on gc.freq_type_nmbr = ft.freq_type_nmbr " +
                "left join grp_charge_rt gct on gc.group_nmbr = gct.group_nmbr and gc.charge_type_nmbr = gct.charge_type_nmbr and gc.main_chrg_seq_nmbr = gct.main_chrg_seq_nmbr " +
                "WHERE  " +
                "gc.group_nmbr = @group_nmbr " +
                "and gc.charge_trmntn_dt is null " +
                "order by " +
                "gc.main_chrg_seq_nmbr desc,gc.charge_efctv_dt, " +
                "ct.charge_type_nm ";
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
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

        public DataTable GetAllData(Database db)
        {
            DataTable result;
            string query = "SELECT " +
                                "gc.main_chrg_seq_nmbr , " +
                                "charge_trmntn_dt = convert(varchar(50),gc.charge_trmntn_dt,106), " +
                                "next_charge_dt = convert(varchar(50),gc.next_charge_dt,106), " +
                                "ct.charge_type_nm, " +
                                "p.pay_rspn_nm, " +
                                "ft.freq_type_nm, " +
                /*"gct.charge_rt, " +*/
                                "case ct.charge_type_nmbr " +
                                "when 210 then (select coi_type_nm from coi_type where coi_type_nmbr = gct.charge_rt) " +
                /*"else cast(gct.charge_rt as varchar(100)) end charge_rt," +	*/
                                    "else " +
                                        "case when gct.charge_rt > 100 then (select rate_type_nm from rate_table_type where rate_type_nmbr = gct.charge_rt) " +
                                        "else  cast(gct.charge_rt as varchar(10)) end " +
                                    "end as charge_rt, " +
                                "charge_amt = convert(varchar(100),convert(money,gct.charge_amt),1), " +
                                "gct.bill_pct, " +
                                "gct.deduct_pct, " +
                                "gct.max_cnt, " +
                                "gct.free_cnt, " +
                                "charge_efctv_dt = convert(varchar(50),gc.charge_efctv_dt,106), " +
                                "max_charge_amt = convert(varchar(100),convert(money,gct.max_charge_amt),1) " +
                                "FROM Group_Charge gc  " +
                                "inner join Charge_type ct on gc.charge_type_nmbr = ct.charge_type_nmbr " +
                                "left join pay_rspn_type p on gc.pay_rspn_nmbr = p.pay_rspn_nmbr  " +
                                "left join Frequency_type ft on gc.freq_type_nmbr = ft.freq_type_nmbr " +
                                "left join grp_charge_rt gct on gc.group_nmbr = gct.group_nmbr and gc.charge_type_nmbr = gct.charge_type_nmbr and gc.								main_chrg_seq_nmbr = gct.main_chrg_seq_nmbr " +
                                "WHERE  " +
                                "gc.group_nmbr = @group_nmbr " +
                                "and gc.charge_type_nmbr = @charge_type_nmbr " +
                                "order by " +
                                "gc.main_chrg_seq_nmbr desc,gc.charge_efctv_dt, " +
                                "ct.charge_type_nm ";
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@charge_type_nmbr", this.ChargeTypeNmbr);
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

        public void InsertGroupChargeRt(Database db)
        {

            string query = "exec sp_grp_charge_rt_i " +
                "@groupnbr, @chargetype, @begprd," +
                "@endprd,@maxcnt,@freecnt," +
                "@chargert, @chargeamt, @billPct," +
                "@deductPct, @sequence, @max_charge_amt ";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@groupnbr", this.GroupNmbr);
                db.AddParameter("@chargetype", this.ChargeTypeNmbr);
                db.AddParameter("@begprd", 0);
                db.AddParameter("@endprd", 0);
                db.AddParameter("@maxcnt", this.maxCnt);
                db.AddParameter("@freeCnt", this.freeCnt);
                db.AddParameter("@chargert", this.chargeRate);
                db.AddParameter("@chargeamt", this.chargeAmount);
                db.AddParameter("@billPct", this.billPercent);
                db.AddParameter("@deductPct", this.deductPercent);
                db.AddParameter("@sequence", this.sequence);
                db.AddParameter("@max_charge_amt", this.maxChargeAmmount);

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

        public DataTable InsertGroupCharge(DPLKCORE.Framework.Database x)
        {
            String query = "exec sp_group_charge_i @groupnbr, @chargetype, @efctv, @termination, @next, @freqtype, @payRspn";

            try
            {
                x.Open();
                x.BeginTransaction();
                x.setQuery(query);

                x.AddParameter("@groupnbr", this.GroupNmbr);
                x.AddParameter("@chargetype", this.ChargeTypeNmbr);
                x.AddParameter("@efctv", this.ChargeEfctvDt);
                if (this.ChargeTrmntnDt == null)
                {
                    x.AddParameter("@termination", DBNull.Value);
                }
                else
                {
                    x.AddParameter("@termination", this.ChargeTrmntnDt);
                }

                if (this.NextChargeDt == null)
                {
                    x.AddParameter("@next", DBNull.Value);
                }
                else
                {
                    x.AddParameter("@next", this.NextChargeDt);
                }
                
                x.AddParameter("@freqtype", this.FreqTypeNmbr);
                x.AddParameter("@payRspn", this.PayRspnNmbr);

                DataTable dt = x.ExecuteQuery();
                this.sequence = Convert.ToInt32(dt.Rows[0]["seq_nmbr"]);
                var dataExist = dt.Rows[0]["data_exist"];
                var error = dt.Rows[0]["return_value"];

                x.CommitTransaction();
                return dt;
            }
            catch (Exception ex)
            {
                x.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                x.Close();
            }
        }

    }
}