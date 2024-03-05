using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.Pension
{
    public partial class BillingStatus : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnBilling.Click += ButtonClicked;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {

                //LoadBusinessLinesToDDL();
                //LoadMoneySourceTypesToDDL();
            }
        }
        private void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;

            if (ib == btnBilling)
            {
                InsertBilling();
            }
        }
        protected void btnInsertBilling(object sender, EventArgs e)
        {
            InsertBilling();
        }


        private void InsertBilling()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                BillingStatusModels c = new BillingStatusModels();

                db.Open();
                db.BeginTransaction();

                //propertinya 

                c.InsertBillingStatus(db);

                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
            }
            finally
            {
                db.Close();
            }
        }

    }
}