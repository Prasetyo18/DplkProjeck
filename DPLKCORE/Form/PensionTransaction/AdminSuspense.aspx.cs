using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;

namespace DPLKCORE.Form.PensionTransaction
{
    public partial class AdminSuspense : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count == 0)
                    Response.Redirect("~/Login.aspx");

                if (!IsPostBack)
                {

                }
            }
        }


    }
}
