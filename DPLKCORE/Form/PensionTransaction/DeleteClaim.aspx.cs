using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction
{
    public partial class DeleteClaim : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            BTN_DELETE.Click += BTN_GET_CLIENT_Click;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {


            }
        }


        protected void BTN_GET_CLIENT_Click(object sender, EventArgs e)
        {

            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                db.BeginTransaction();
                ClaimDeletionModel model = new ClaimDeletionModel();
                model.CertificateNumber = Convert.ToInt32(TXT_CERTIFICATE.Text);
/*                model.GetClient(db);
*/                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                //Response.Write($"An error occurred: {ex.Message}");
            }
            finally {
                db.Close();
            }
        }


        protected void BTN_DELETE_Click(object sender, EventArgs e)
        {
            try
            {
                Connection conn = new Connection();
                Database db = new Database(conn.ConnectionStringPension);
                ClaimDeletionModel model = new ClaimDeletionModel()
                {
                    CertificateNumber = Convert.ToInt32(TXT_CERTIFICATE.Text),

                    BatchId = Convert.ToInt32(TXT_BATCH_ID.Text)
                };

                model.DeleteClaim(db);
                Response.Write("Claim deleted successfully!");
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred: " + ex.Message);
            }
        }

    }
}