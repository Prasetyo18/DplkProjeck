using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class ClientFundModels
    {
        public int? CerNmbr { get; set; }
        public DateTime? AsOfDate { get; set; }
        public short GroupByType { get; set; }


        public DataTable ReadFund(Database db)
        {
            string query = "EXEC SP_FUND_INFO_R @cer_nmbr, @as_of_dt, @group_by_type";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CerNmbr);
                db.AddParameter("@as_of_dt", this.AsOfDate);
                db.AddParameter("@group_by_type", this.GroupByType);

                DataTable dt = db.ExecuteQuery();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}   