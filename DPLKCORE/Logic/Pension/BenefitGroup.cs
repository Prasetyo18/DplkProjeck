using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class BenefitGroup
    {
        public int? GroupNmbr { get; set; }
        public int? McpTypeNmbr { get; set; }
        public int? BeneTypeNmbr { get; set; }
        public int? SiCalcTypeNmbr { get; set; }
        public double? DefaultSumInsured { get; set; }
        public double? MaxSumInsured { get; set; }
        public bool? CoiDiscontFlg { get; set; }
        public double? CoiDiscontValue { get; set; }
        public bool? CoiLoadingFlg { get; set; }
        public double? CoiLoadingValue { get; set; }
        public int? MaxEntryAge { get; set; }
        public int? MaxCovAge { get; set; }
        public int? CoiTypeNmbr { get; set; }
        public DateTime? ChangeDt { get; set; }
        public int? SubTrnsNmbr { get; set; }


        public Dictionary<string, object> LoadData(Database db, int groupNmbr)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            string query = "exec DGR_group_benefit_r @group_nmbr";
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

                
                //DbDataReader reader = db.ExecuteReader();
                //if (reader.HasRows)
                //{
                //    while (reader.Read())
                //    {
                //        for (int i = 0; i < reader.FieldCount; i++)
                //        {
                //            output.Add(reader.GetName(i), reader.GetValue(i));
                //        }
                //    }
                //}
                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertOrUpdateGroupBenefit(Database x)
        {
            String query = "exec usp_group_benefit_i " +
                           "@group_nmbr, @mcp_type_nmbr, @bene_type_nmbr, " +
                           "@si_calc_type_nmbr, @default_sum_insured, @max_sum_insured, " +
                           "@coi_discont_flg, @coi_discont_value, @coi_loading_flg, " +
                           "@coi_loading_value, @max_Entry_age, @max_cov_age, " +
                           "@coi_type_nmbr, @change_dt, @sub_trns_nmbr";
            try
            {
                x.setQuery(query);

                x.AddParameter("@group_nmbr", this.GroupNmbr);
                x.AddParameter("@mcp_type_nmbr", this.McpTypeNmbr);
                x.AddParameter("@bene_type_nmbr", this.BeneTypeNmbr);
                x.AddParameter("@si_calc_type_nmbr", this.SiCalcTypeNmbr);
                x.AddParameter("@default_sum_insured", this.DefaultSumInsured);
                x.AddParameter("@max_sum_insured", this.MaxSumInsured);
                x.AddParameter("@coi_discont_flg", this.CoiDiscontFlg);
                x.AddParameter("@coi_discont_value", this.CoiDiscontValue);
                x.AddParameter("@coi_loading_flg", this.CoiLoadingFlg);
                x.AddParameter("@coi_loading_value", this.CoiLoadingValue);
                x.AddParameter("@max_Entry_age", this.MaxEntryAge);
                x.AddParameter("@max_cov_age", this.MaxCovAge);
                x.AddParameter("@coi_type_nmbr", this.CoiTypeNmbr);
                x.AddParameter("@change_dt", this.ChangeDt);
                x.AddParameter("@sub_trns_nmbr", this.SubTrnsNmbr);

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