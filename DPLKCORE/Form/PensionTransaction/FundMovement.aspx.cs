using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction
{
    public partial class FundMovement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbTitle.Text = btnFundMovement.Text;
        }

        protected void btnFundMovement_Click(object sender, EventArgs e)
        {
            lbTitle.Text = btnFundMovement.Text;
            FundMovementTabs.ActiveTabIndex = 0;
        }

        protected void btnCertificateList_Click(object sender, EventArgs e)
        {
            lbTitle.Text = btnCertificateList.Text;
            FundMovementTabs.ActiveTabIndex = 1;
        }

        protected void btnGroupList_Click(object sender, EventArgs e)
        {
            lbTitle.Text = btnGroupList.Text;
            FundMovementTabs.ActiveTabIndex = 2;
        }
    }
}