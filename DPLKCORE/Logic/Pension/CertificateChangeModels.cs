using DPLKCORE.Class.Pension;
using DPLKCORE.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class CertificateChangeModels
    {
        public String ChargeTypeNmbr { get; set; }
        public String ChargeTypeNm { get; set; }

        public void LoadDDLCertificateChange(DropDownList ddl, Framework.Database db)
        {
            CertificateChangeModels m = new CertificateChangeModels();
            List<CertificateChangeModels> data = new List<CertificateChangeModels>();

            data.AddRange(this.GetParamDDL(db));

            ddl.DataSource = data;
            ddl.DataValueField = "ChargeTypeNmbr";
            ddl.DataTextField = "ChargeTypeNm";
            ddl.DataBind();
        }


        public List<CertificateChangeModels> GetParamDDL(Framework.Database db)
        {
            List<CertificateChangeModels> data = new List<CertificateChangeModels>();

            String query = "select charge_type_nmbr,charge_type_nm from charge_type order by 1 ";

            try
            {
                db.setQuery(query);


                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    CertificateChangeModels m = new CertificateChangeModels();


                    if (reader["charge_type_nmbr"] != DBNull.Value)
                        m.ChargeTypeNmbr = reader["charge_type_nmbr"].ToString().Trim();

                    if (reader["charge_type_nm"] != DBNull.Value)
                        m.ChargeTypeNm = reader["charge_type_nm"].ToString().Trim();


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