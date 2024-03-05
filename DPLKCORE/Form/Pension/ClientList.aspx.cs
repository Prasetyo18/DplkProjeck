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
    public partial class ClientListing : System.Web.UI.Page
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
                ClientInfoModels clientInfo = new ClientInfoModels();
                GridviewClientList.DataSource = clientInfo.LoadAllData(db);
                GridviewClientList.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Client.aspx?state=NewClient");
        }

        protected void GridviewClientList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditClientRow")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    int absoluteIndex = Math.Abs((GridviewClientList.PageIndex * GridviewClientList.PageSize) - rowIndex);

                    GridViewRow row = GridviewClientList.Rows[absoluteIndex];
                    int clientId = Convert.ToInt32(row.Cells[1].Text);
                    string clientName = row.Cells[2].Text;

                    Session["ClientIdDetail"] = clientId;
                    Session["ClientNameDetail"] = clientName;

                    Response.Redirect("Client.aspx?state=Edit",false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        protected void GridviewClientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridviewClientList.PageIndex = e.NewPageIndex;
            LoadGridView();
        }
    }
}