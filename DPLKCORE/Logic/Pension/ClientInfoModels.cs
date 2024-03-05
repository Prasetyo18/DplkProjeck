using DPLKCORE.Class.Pension;
using DPLKCORE.Class;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DPLKCORE.Logic.Pension
{
    public class ClientInfoModels
    {
        public int ClientNmbr { get; set; }
        public string ClientNm { get; set; }
        public short? IdentityType { get; set; }
        public string IdentityNmbr { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDt { get; set; }
        public short? MaritalStatusNmbr { get; set; }
        public string MaidenNm { get; set; }
        public string EmailAddr { get; set; }
        public DateTime LastChangeDt { get; set; }
        public string DobPlace { get; set; }
        public int? ClientNmbrOpf { get; set; }
        public string Path { get; set; }
        public String IdentyType { get; set; }
        public String IdentityNm { get; set; }
        public String MaritialStatusNmbr { get; set; }
        public String MaritalStatusNm { get; set; }

        


        

        public void LoadDDLIndentity(DropDownList ddl, Framework.Database db)
        {
            ClientInfoModels m = new ClientInfoModels();
            List<ClientInfoModels> data = new List<ClientInfoModels>();

            data.AddRange(this.GetDDLIdentity(db));

            ddl.DataSource = data;
            ddl.DataValueField = "IdentyType";
            ddl.DataTextField = "IdentityNm";
            ddl.DataBind();
        }
        public List<ClientInfoModels> GetDDLIdentity(Framework.Database db)
        {
            List<ClientInfoModels> data = new List<ClientInfoModels>();

            String query = "EXEC DDL_PARAM 'id_type' ";



            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    ClientInfoModels m = new ClientInfoModels();


                    if (reader["identity_type"] != DBNull.Value)
                        m.IdentyType = reader["identity_type"].ToString().Trim();

                    if (reader["identity_nm"] != DBNull.Value)
                        m.IdentityNm = reader["identity_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }

        public void LoadDDLMarital(DropDownList ddl, Framework.Database db)
        {
            ClientInfoModels m = new ClientInfoModels();
            List<ClientInfoModels> data = new List<ClientInfoModels>();

            data.AddRange(this.GetDDLMarital(db));

            ddl.DataSource = data;
            ddl.DataValueField = "MaritialStatusNmbr";
            ddl.DataTextField = "MaritalStatusNm";
            ddl.DataBind();
        }

        public List<ClientInfoModels> GetDDLMarital(Framework.Database db)
        {
            List<ClientInfoModels> data = new List<ClientInfoModels>();

            String query = "EXEC DDL_PARAM  'marital_status_type' ";

            try
            {
                db.setQuery(query);
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    ClientInfoModels m = new ClientInfoModels();

                    if (reader["marital_status_nmbr"] != DBNull.Value)
                        m.MaritialStatusNmbr = reader["marital_status_nmbr"].ToString().Trim();

                    if (reader["marital_status_nm"] != DBNull.Value)
                        m.MaritalStatusNm = reader["marital_status_nm"].ToString().Trim();

                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }

        public DataTable LoadAllData(Database db)
        {
            string query = "SELECT top  1000 * FROM client order by client_nmbr desc";
            try
            {
                db.setQuery(query);
                DataTable output = db.ExecuteQuery();
                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Dictionary<string, object> PrepareFTP(Database db)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            string query = "select IN_DBFTP_IP, IN_DBFTP_PORT, IN_DBFTP_UID, IN_DBFTP_PWD from SPD_FTP";
            try
            {
                db.setQuery(query);
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

        public int InsertClient(Database x)
        {
            String query = "exec sp_client_i " +
                           "@clientnbr, @clientname, @identitytype, @identitynbr, @gender, " +
                           "@birthdate, @dob_place, @maritalstatus, @maidenname, @emailaddress, @path";

            try
            {
                x.setQuery(query);

                x.AddParameter("@clientnbr", this.ClientNmbr);
                x.AddParameter("@clientname", this.ClientNm);
                x.AddParameter("@identitytype", this.IdentityType);
                x.AddParameter("@identitynbr", this.IdentityNmbr);
                x.AddParameter("@gender", this.Gender);
                x.AddParameter("@birthdate", this.BirthDt);
                x.AddParameter("@dob_place", this.DobPlace);
                x.AddParameter("@maritalstatus", this.MaritalStatusNmbr);
                x.AddParameter("@maidenname", this.MaidenNm);
                x.AddParameter("@emailaddress", this.EmailAddr);
                x.AddParameter("@path", this.Path);

                int clientNmbr = 0;
                DataTable result = x.ExecuteQuery();
                if (result.Rows.Count > 0)
                {
                    clientNmbr += Convert.ToInt32(result.Rows[0]["client_nmbr"]);
                }                
                return clientNmbr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Dictionary<string, object> LoadById(Database db)
        {
            string query = "exec sp_client_r @client_nmbr";
            Dictionary<string, object> output = new Dictionary<string, object>();
            try
            {
                db.setQuery(query);
                db.AddParameter("@client_nmbr", this.ClientNmbr);
                DataTable result = db.ExecuteQuery();
                if (result.Rows.Count > 0)
                {
                    DataRow FirstRow = result.Rows[0];

                    foreach (DataColumn col in result.Columns)
                    {
                        output.Add(col.ColumnName, FirstRow[col]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return output;
        }
    }


}
