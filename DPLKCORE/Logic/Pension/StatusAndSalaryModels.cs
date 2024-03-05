using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class StatusAndSalaryModels
    {
        public int? CertifNmbr { get; set; }
        public int? ClientNmbr { get; set; }

        public void GetGroupCompanyTxt(Database db, TextBox groupTxt, TextBox companyTxt)
        {
            string query = "select c.group_nmbr,company_nm from certificate c join group_info gi on c.group_nmbr = gi.group_nmbr join company co on co.client_nmbr = gi.client_nmbr where c.cer_nmbr=@cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertifNmbr);
                DataTable dt = db.ExecuteQuery();
                if (dt.Rows.Count > 0)
                {
                    groupTxt.Text = dt.Rows[0]["group_nmbr"].ToString();
                    companyTxt.Text = dt.Rows[0]["company_nm"].ToString();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadGvStatusHistory(Database db)
        {
            string query = "SELECT dbo.fnDisplayDate(EFCTV_DT) as EFCTV_DT,slry_amt FROM slry_history WHERE CER_NMBR = @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertifNmbr);
                DataTable result = db.ExecuteQuery();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadGvSalaryHistory(Database db)
        {
            string query = "SELECT dbo.fnDisplayDate(EFCTV_DT) as EFCTV_DT,STATUS_TYPE_NM FROM CER_STATUS CS JOIN STATUS_TYPE ST ON CS.statUS_TYPE_NMBR = ST.STATUS_TYPE_NMBR WHERE CER_NMBR = @cer_nmbr order by efctv_dt desc";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertifNmbr);
                DataTable result = db.ExecuteQuery();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
    }
}