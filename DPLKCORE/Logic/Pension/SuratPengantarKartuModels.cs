using DPLKCORE.Class;
using DPLKCORE.Form.Pension;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DPLKCORE.Logic.Pension
{
    public class CerNumber
    {
        public string CompanyName { get; set; }

    }
    public class SuratPengantarKartuModels
    {
        public string Kepada { get; set; }
        public string Up { get; set; }
        public string Ttd { get; set; }
        public string Dibuat { get; set; }
        public string Diusulkan { get; set; }
        public string Hostname { get; set; }
    }
    public class SuratPengatarGet
    {
        public int GroupInfo { get; set; }
        public string ClientNmbr { get; set; }
        public string CompanyName { get; set; }

        public CerNumber GetCerNumber(Database db)
        {
            try
            {
                string query = "SELECT company_nm FROM group_info g " +
                               "LEFT JOIN company co ON co.client_nmbr = g.client_nmbr " +
                               "WHERE group_nmbr = @group_nmbr";

                db.setQuery(query);
                db.AddParameter("@group_nmbr", GroupInfo);

                object result = db.ExecuteScalar();
                string companyName = result != null ? result.ToString() : string.Empty;

                return new CerNumber { CompanyName = companyName };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


    public class SuratPengantarKartuResult
    {
        public string Number { get; set; }
    }

    public class SuratPengantarKartuProcessor
    {
        public SuratPengantarKartuResult GenerateSuratPengantarKartu(Database db, SuratPengantarKartuModels data)
        {
            try
            {
                db.setQuery("exec sp_surat_pengantar_kartu " +
                            "@kepada, @up, @ttd, @dibuat, @diusulkan, @hostname" +
                            "  @client_nmbr ");

                db.AddParameter("@kepada", data.Kepada);
                db.AddParameter("@up", data.Up);
                db.AddParameter("@ttd", data.Ttd);
                db.AddParameter("@dibuat", data.Dibuat);
                db.AddParameter("@diusulkan", data.Diusulkan);
                db.AddParameter("@hostname", data.Hostname);

                SuratPengantarKartuResult result = new SuratPengantarKartuResult();

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Number = reader["Number"].ToString();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GenerateSuratPengantarKartu", ex);
            }
        }
    }


    public class DDLSuratPengantar
    {
        public string Address { get; set; }

        public void LoadDataParamToDDL(DropDownList ddl, DPLKCORE.Framework.Database db, string clientNmbr)
        {
            DDLSuratPengantar m = new DDLSuratPengantar();
            List<DDLSuratPengantar> data = new List<DDLSuratPengantar>();

            data.AddRange(this.GetParamDDL(db, clientNmbr));

            ddl.DataSource = data;
            ddl.DataValueField = "Address";
            ddl.DataTextField = "Address";
            ddl.DataBind();
        }

        public List<DDLSuratPengantar> GetParamDDL(DPLKCORE.Framework.Database db, string clientNmbr)
        {
            List<DDLSuratPengantar> data = new List<DDLSuratPengantar>();

            string query = "SELECT ISNULL(ca.address1, '') + ' ' + ISNULL(ca.address2, '') + ' ' + " +
                           "ISNULL(ca.address3, '') + ' ' + ISNULL(ca.city, '') + ' ' + ISNULL(ca.postal_cd, '') AS Address " +
                           "FROM group_info g " +
                           "LEFT JOIN company co ON co.client_nmbr = g.client_nmbr " +
                           "LEFT JOIN client_address ca ON ca.client_nmbr = co.client_nmbr " +
                           "WHERE group_nmbr = @client_nmbr " +
                           "UNION " +
                           "SELECT UPPER(ISNULL(address1, '') + ISNULL(address2, '') + " +
                           "ISNULL(address3, '') + ' ' + ISNULL(city, '') + ' ' + ISNULL(postal_cd, '')) AS Address " +
                           "FROM branch_office bo " +
                           "LEFT JOIN company com ON com.client_nmbr = bo.client_nmbr " +
                           "LEFT JOIN group_info gi ON gi.client_nmbr = bo.client_nmbr " +
                           "WHERE group_nmbr = @client_nmbr;";

            try
            {
                db.setQuery(query);
                db.AddParameter("@client_nmbr", clientNmbr);

                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    DDLSuratPengantar m = new DDLSuratPengantar();

                    if (reader["Address"] != DBNull.Value)
                        m.Address = reader["Address"].ToString().Trim();

                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }
    }

}