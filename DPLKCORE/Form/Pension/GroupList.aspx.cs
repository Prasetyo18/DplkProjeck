using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;

namespace DPLKCORE.Form.Pension
{
    public partial class GroupList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridView();
            }
        }

        private void LoadGridView()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                GroupInfoModels groupInfo = new GroupInfoModels();
                GridviewGroupList.DataSource = groupInfo.LoadData(db);
                GridviewGroupList.DataBind();

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }

        }

        protected void GridviewGroupList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridviewGroupList.PageIndex = e.NewPageIndex;
            LoadGridView();
        }

        protected void GridviewGroupList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int absoluteRowIndex = Math.Abs((GridviewGroupList.PageIndex * GridviewGroupList.PageSize) - rowIndex);

                GridViewRow row = GridviewGroupList.Rows[absoluteRowIndex];

                int companyId = Convert.ToInt32(row.Cells[6].Text);
                int groupId = Convert.ToInt32(row.Cells[1].Text);

                Session["groupIdDetail"] = groupId;
                Session["companyIdDetail"] = companyId;

                //go to detail page first
                Response.Redirect("Group.aspx?state=Edit");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Group.aspx?state=NewGroupInfo");
        }
    }
}