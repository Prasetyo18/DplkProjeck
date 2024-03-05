using DPLKCORE.Framework;
using DPLKCORE.Class;
using System;
using System.Web.UI.WebControls;
using DPLKCORE.Class.Pension;
using System.Collections.Generic;

namespace DPLKCORE.Form.Pension
{
    public partial class PayCenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                DisplayData();
            }
        }

        protected void btnAddPaycenter_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Form/Pension/PayCenterAdd.aspx");
        }

        private List<PaycenterModel> RetrieveData()
        {
            PaycenterModel paycenterModel = new PaycenterModel();
            List<PaycenterModel> allPaycenter = paycenterModel.GetPayCenters();

            return allPaycenter;
            
        }

        private void DisplayData()
        {
            GridViewPaycenter.DataSource = RetrieveData();
            GridViewPaycenter.DataBind();
        }

        //handle datagrid pagination
        protected void GridViewPaycenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPaycenter.PageIndex = e.NewPageIndex;
            DisplayData();
        }
    }
}
