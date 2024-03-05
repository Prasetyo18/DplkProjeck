using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class SuspenseReportModels
    {
        public string SuspenseNmbr { get; set; }
        public string GroupNmbr { get; set; }

        public DataTable GetSuspenseResetData(Database db)
        {
            string query = "EXEC GRID_SUSPN_REPORT @suspense_nmbr, @group_nmbr";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@suspense_nmbr", this.SuspenseNmbr);
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
    }
}