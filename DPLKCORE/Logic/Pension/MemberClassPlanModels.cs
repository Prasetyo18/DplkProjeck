using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace DPLKCORE.Logic.Pension
{
    public class MemberClassPlanModels
    {
        public int GroupNmbr { get; set; }
        public int McpNmbr { get; set; }
        public float CntrbAmtER { get; set; }
        public float CntrbRtER { get; set; }
        public float CntrbAmtEE { get; set; }
        public float CntrbRtEE { get; set; }
        public float CntrbAmtTU { get; set; }
        public float CntrbRtTU { get; set; }
        public float CntrbAmtFT { get; set; }
        public float CntrbRtFT { get; set; }

        public int getMCPType(Database db, int groupNmbr)
        {
            int output = 0;
            string query = "select distinct mcp_nmbr from mbr_cls_plan where group_nmbr = @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);
                output += Convert.ToInt32(db.ExecuteScalar());
                return output;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadData(Database db)
        {
            string query = "sp_mbr_cls_plan_r @group_nmbr, @mcp_type";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@mcp_type", this.McpNmbr);
                DataTable result = db.ExecuteQuery();

                return result;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertMemberClassPlan(Database db)
        {
            try
            {
                String query = "exec sp_mbr_cls_plan_i " +
                               "@group_nmbr, @mcp_nmbr, " +
                               "@cntrb_amt_ER, @cntrb_rt_ER, " +
                               "@cntrb_amt_EE, @cntrb_rt_EE, " +
                               "@cntrb_amt_TU, @cntrb_rt_TU, " +
                               "@cntrb_amt_FT, @cntrb_rt_FT";

                db.setQuery(query);

                db.AddParameter("@group_nmbr", this.GroupNmbr);
                db.AddParameter("@mcp_nmbr", this.McpNmbr);
                db.AddParameter("@cntrb_amt_ER", this.CntrbAmtER);
                db.AddParameter("@cntrb_rt_ER", this.CntrbRtER);
                db.AddParameter("@cntrb_amt_EE", this.CntrbAmtEE);
                db.AddParameter("@cntrb_rt_EE", this.CntrbRtEE);
                db.AddParameter("@cntrb_amt_TU", this.CntrbAmtTU);
                db.AddParameter("@cntrb_rt_TU", this.CntrbRtTU);
                db.AddParameter("@cntrb_amt_FT", this.CntrbAmtFT);
                db.AddParameter("@cntrb_rt_FT", this.CntrbRtFT);

                db.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
