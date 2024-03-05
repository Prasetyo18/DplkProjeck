using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class DDLPayoutSuspense
    {
        public string InvestTypeNmbr { get; set; }
        public string InvestTypeNm { get; set; }

        public static void LoadDDLInvest(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetInvestmentTypes(db);
            ddl.DataTextField = "InvestTypeNm";
            ddl.DataValueField = "InvestTypeNmbr";
            ddl.DataBind();
        }

        private static List<DDLPayoutSuspense> GetInvestmentTypes(Database db)
        {
            string query = "select inv_type_nmbr,inv_type_nm from inv_type order by inv_type_nm";
            List<DDLPayoutSuspense> output = new List<DDLPayoutSuspense>();
            try
            {
                db.Open();
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLPayoutSuspense obj = new DDLPayoutSuspense();
                        if (reader["inv_type_nmbr"] != null)
                        {
                            obj.InvestTypeNmbr = reader["inv_type_nmbr"].ToString();
                        }
                        if (reader["inv_type_nm"] != null)
                        {
                            obj.InvestTypeNm = reader["inv_type_nm"].ToString();
                        }
                        output.Add(obj);
                    }
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        public static string GetLastestSeqNmbr(Database db)
        {
            string output = "";
            string query = "select seq = coalesce(max(seq_nmbr),100) + 1 from retur_info";
            try
            {
                db.Open();
                db.setQuery(query);
                output = db.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return output;
        }
    }
}