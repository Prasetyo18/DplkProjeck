using DPLKCORE.Framework;
using DPLKCORE.Form.Pension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DPLKCORE.Class.Pension
{
    public class PaycenterModel
    {
        public int ClientNmbr { get; set; }
        public int PaycenterNmbr { get; set; }
        public string PaycenterNm { get; set; }
        public int? MasterPaycenterNmbr { get; set; }
        public string ContactPerson { get; set; }
        public DateTime LastChangeDt { get; set; }


        public void InsertPayCenter(Database db)
        {
            String query = "exec sp_paycenter_i @client_nmbr, @paycenter_nm, @master_paycenter_nmbr, @contact_person";

            try
            {
                db.setQuery(query);

                db.AddParameter("@client_nmbr", this.ClientNmbr);
                db.AddParameter("@paycenter_nm", this.PaycenterNm); 
                db.AddParameter("@master_paycenter_nmbr", this.MasterPaycenterNmbr ?? (object)DBNull.Value);
                db.AddParameter("@contact_person", this.ContactPerson ?? (object)DBNull.Value);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PaycenterModel> GetPayCenters()
        {
            List<PaycenterModel> paycenters = new List<PaycenterModel>();
            Connection con = new Connection();
            String cnstr = con.ConnectionStringPension;
            Database db = new Database(cnstr);

            String query = "SELECT * FROM Paycenter";

            try
            {
                db.Open();
                db.setQuery(query);

                DataTable result = db.ExecuteQuery();

                foreach (DataRow row in result.Rows)
                {
                    PaycenterModel paycenter = new PaycenterModel
                    {
                        ClientNmbr = Convert.ToInt32(row["client_nmbr"]),
                        PaycenterNmbr = Convert.ToInt32(row["paycenter_nmbr"]),
                        PaycenterNm = row["paycenter_nm"].ToString(),
                        MasterPaycenterNmbr = row["master_paycenter_nmbr"] != DBNull.Value ? (int?)Convert.ToInt32(row["master_paycenter_nmbr"]) : null,
                        ContactPerson = row["contact_person"].ToString(),
                        LastChangeDt = Convert.ToDateTime(row["last_change_dt"])
                    };

                    paycenters.Add(paycenter);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }

            return paycenters;
        }
    }
}


