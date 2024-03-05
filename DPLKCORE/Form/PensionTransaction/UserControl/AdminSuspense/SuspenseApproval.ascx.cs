using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction.UserControl.AdminSuspense
{
    public partial class SuspenseApproval : System.Web.UI.UserControl
    {
        private Database db;
        private Connection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);
            if (!IsPostBack)
            {
                FillGridUnapproved();
                FillGridApproved();
                FillGridRest();
            }
        }

        private void FillGridRest()
        {
            DGR_REST.DataSource = SuspenseApprovalModels.GetRestData(db);
            DGR_REST.DataBind();
        }

        private void FillGridApproved()
        {
            DGR_APPROVED.DataSource = SuspenseApprovalModels.GetApprovedData(db);
            DGR_APPROVED.DataBind();
        }

        private void FillGridUnapproved()
        {
            DGR_UNAPPROVED.DataSource = SuspenseApprovalModels.GetUnapprovedData(db);
            DGR_UNAPPROVED.DataBind();
        }

        protected void DGR_UNAPPROVED_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            int absoluteIndex;
            GridViewRow row;
            if (e.CommandName.ToLower() == "approved")
            {
                index = Convert.ToInt32(e.CommandArgument);
                absoluteIndex = Math.Abs((DGR_UNAPPROVED.PageIndex * DGR_UNAPPROVED.PageSize) - index);
                row = DGR_UNAPPROVED.Rows[absoluteIndex];

                DropDownList DDL_UP = (DropDownList)row.FindControl("DDL_UP");
                DropDownList DDL_ADDRESS = (DropDownList)row.FindControl("DDL_ADDRESS");
                TextBox TXT_PERIHAL = (TextBox)row.FindControl("TXT_PERIHAL");

                string pic = DDL_UP.SelectedItem.Text;
                string address = DDL_ADDRESS.SelectedItem.Text;
                string perihal = TXT_PERIHAL.Text;

                if (pic == "There isn't an active group PIC")
                {
                    pic = "";
                }

                if (address == "No address found")
                {
                    address = "";
                }

                SuspenseApprovalModels suspenseApproval = new SuspenseApprovalModels();
                suspenseApproval.pic = pic;
                suspenseApproval.address = address;
                suspenseApproval.perihal = perihal;
                suspenseApproval.SuspenseNmbr = long.Parse(row.Cells[1].Text);

                suspenseApproval.UpdateSuspense(db);


                suspenseApproval.StatusTypeNmbr = 140;
                suspenseApproval.SuspenseDesc1 = row.Cells[3].Text;

                suspenseApproval.InsertSuspense(db);

                FillGridApproved();
                FillGridUnapproved();
                FillGridRest();
            }
            else if (e.CommandName.ToLower() == "deleterow")
            {
                index = Convert.ToInt32(e.CommandArgument);
                absoluteIndex = Math.Abs((DGR_UNAPPROVED.PageIndex * DGR_UNAPPROVED.PageSize) - index);
                row = DGR_UNAPPROVED.Rows[absoluteIndex];

                SuspenseApprovalModels suspenseApproval = new SuspenseApprovalModels();
                suspenseApproval.SuspenseNmbr = long.Parse(row.Cells[1].Text);

                suspenseApproval.DeleteSuspenseHistory(db);
                suspenseApproval.DeleteSuspenseApproveHistory(db);

                FillGridApproved();
                FillGridUnapproved();
                FillGridRest();
            }
        }

        protected void DGR_UNAPPROVED_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_UNAPPROVED.PageIndex = e.NewPageIndex;
            FillGridUnapproved();
        }

        protected void DGR_APPROVED_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_APPROVED.PageIndex = e.NewPageIndex;
            FillGridApproved();
        }

        protected void DGR_REST_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_REST.PageIndex = e.NewPageIndex;
            FillGridRest();
        }

        protected void DGR_APPROVED_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            int absoluteIndex;
            GridViewRow row;
            if (e.CommandName.ToLower() == "updaterow")
            {
                index = Convert.ToInt32(e.CommandArgument);
                absoluteIndex = Math.Abs((DGR_APPROVED.PageIndex * DGR_APPROVED.PageSize) - index);
                row = DGR_APPROVED.Rows[absoluteIndex];

                TXT_SUSPNNO.Text = row.Cells[1].Text;
                TXT_SUSPNAMT.Text = row.Cells[2].Text;
                TXT_SUSPNDESC.Text = row.Cells[4].Text;
                TXT_GROUPNO.Text = row.Cells[7].Text;

            }
        }

        protected void BTN_EDITSUSPEN_Click(object sender, EventArgs e)
        {
            UpdateSuspenseHistory();

            FillGridApproved();
            FillGridUnapproved();
            FillGridRest();
        }

        private void UpdateSuspenseHistory()
        {
            SuspenseApprovalModels suspnAppr = new SuspenseApprovalModels();
            suspnAppr.GroupNmbr = int.Parse(TXT_GROUPNO.Text);
            suspnAppr.SuspenseAmmount = double.Parse(TXT_SUSPNAMT.Text);
            suspnAppr.SuspenseNmbr = long.Parse(TXT_SUSPNNO.Text);

            suspnAppr.UpdateSuspenseHistory(db);
        }

        protected void DGR_UNAPPROVED_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button BT_DELETE = (Button)e.Row.FindControl("BT_DELETE");
                DropDownList DDL_UP = (DropDownList)e.Row.FindControl("DDL_UP");
                DropDownList DDL_ADDRESS = (DropDownList)e.Row.FindControl("DDL_ADDRESS");

                DDLAdminSuspense.LoadDDLPIC(db, DDL_UP, e.Row.Cells[1].Text);
                if (DDL_UP.Items.Count == 0)
                {
                    List<DDLAdminSuspense> data = new List<DDLAdminSuspense>();
                    DDLAdminSuspense ddl = new DDLAdminSuspense();
                    ddl.KdPIC = "0";
                    ddl.NamaPIC = "There isn't an active group PIC";
                    data.Add(ddl);

                    DDL_UP.DataSource = data;
                    DDL_UP.DataBind();
                }

                DDLAdminSuspense.LoadDDLAddress(db, DDL_ADDRESS, e.Row.Cells[1].Text);
                if (DDL_ADDRESS.Items.Count == 0)
                {
                    List<DDLAdminSuspense> data = new List<DDLAdminSuspense>();
                    DDLAdminSuspense ddl = new DDLAdminSuspense();
                    ddl.Address = "No address found";
                    data.Add(ddl);
                    DDL_ADDRESS.DataSource = data;
                    DDL_ADDRESS.DataBind();
                }


            }
        }
    }
}