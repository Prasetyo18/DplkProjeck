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
    public partial class InvestmentDirection : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnInvest.Click += ButtonClicked;
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

            if (ib == btnInvest)
            {
                InsertInvestment();
            }
        }
        protected void btnInvestAdd(object sender, EventArgs e)
        {
            InsertInvestment();
        }

        private void InsertInvestment()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                InvestmentDirectionModels c = new InvestmentDirectionModels();

                db.Open();
                db.BeginTransaction();

                DateTime nextChargeDate = DateTime.Now; 
                string changePaymentResponsbility = ""; 
                float billedPercent = 0.5F; 
                string chargeFrequency = ""; 
                float chargeRatePercent = 0.75F; 
                float chargeAmount = 100.0F; 
                float deducatedPatePercent = 0.25F; 

                c.InsertGroupInvestDir(db);

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