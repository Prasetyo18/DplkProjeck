using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.Pension
{
    public partial class CompanyEdit : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
           
                btnCompanyEdit.Click += ButtonClicked;
                btnCompanyDelete.Click += ButtonClicked;
            }
        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                //DisplayData();
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender; //casting

            if (ib == btnCompanyEdit)
                Response.Redirect("..../Company.aspx");
        }
    }
}