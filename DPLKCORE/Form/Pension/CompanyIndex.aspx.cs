using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Class.Pension;
using DPLKCORE.Logic.Pension;

namespace DPLKCORE.Form.Pension
{
    public partial class CompanyIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayData();
            }
        }

        private void DisplayData()
        {
            List<CompanyModel> companies = GetCompanies();

            GridViewCompanyIndex.DataSource = companies;
            GridViewCompanyIndex.DataBind();
        }

        private List<CompanyModel> GetCompanies()
        {
            CompanyModel companyModel = new CompanyModel();
            return companyModel.GetCompanies();
        }

        protected void btnAddCompany_Click(object sender, EventArgs e)
        {
            Response.Redirect("Company.aspx");
        }

        protected void GridViewCompanyIndex_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewCompanyIndex.PageIndex = e.NewPageIndex;
            DisplayData();
        }
        /*   protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
           {
           }

           protected void OnGridDataBound(object sender, GridViewRowEventArgs e)
           {
           }*/
    }
}