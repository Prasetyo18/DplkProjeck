using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;


namespace DPLKCORE.Form.PensionTransaction
{
    public partial class PayoutSuspenseApproval : System.Web.UI.Page
    {
        Connection conn;
        Database db;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        private void FillGrid()
        {
            DGR_SUSPENSE_APPROVAL.DataSource = PayoutSuspenseApprovalModels.GetPendingPayoutSuspenseApproval(db);
            DGR_SUSPENSE_APPROVAL.DataBind();
        }

        protected void DGR_SUSPENSE_APPROVAL_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            DGR_SUSPENSE_APPROVAL.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void DGR_SUSPENSE_APPROVAL_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index;
            int absoluteIndex;
            GridViewRow row;
            switch (e.CommandName.ToLower())
            {
                case "approvedata":
                    index = Convert.ToInt32(e.CommandArgument);
                    absoluteIndex = Math.Abs((DGR_SUSPENSE_APPROVAL.PageIndex * DGR_SUSPENSE_APPROVAL.PageSize) - index);
                    row = DGR_SUSPENSE_APPROVAL.Rows[absoluteIndex];

                    PayoutSuspenseApprovalModels approveObj = new PayoutSuspenseApprovalModels();
                    approveObj.UserName = Session["session.iduser"].ToString();
                    approveObj.SeqNumber = int.Parse(row.Cells[1].Text);
                    UpdateReturInfo(approveObj);

                    approveObj.ClaimRequestId = row.Cells[2].Text;
                    DataTable result = ValidateClaimRegister(approveObj);
                    if (result.Rows.Count > 0)
                    {
                        UpdateClaimTrackRegister(approveObj);
                    }
                    approveObj.GrossAmmount = float.Parse(row.Cells[9].Text.Replace(",",""));
                    approveObj.SuspenseNumber = long.Parse(row.Cells[12].Text);
                    UpdateSuspenseHistory(approveObj);

                    DisplayReport();

                    DisplayMessage("Data has been saved.");
                    FillGrid();
                    break;
                case "deletedata":
                    index = Convert.ToInt32(e.CommandArgument);
                    absoluteIndex = Math.Abs((DGR_SUSPENSE_APPROVAL.PageIndex * DGR_SUSPENSE_APPROVAL.PageSize) - index);
                    row = DGR_SUSPENSE_APPROVAL.Rows[absoluteIndex];

                    PayoutSuspenseApprovalModels deleteObj = new PayoutSuspenseApprovalModels();
                    deleteObj.SeqNumber = long.Parse(row.Cells[1].Text);
                    deleteObj.GroupNmbr = int.Parse(row.Cells[3].Text);

                    DeleteReturInfo(deleteObj);
                    DeleteFppTable(deleteObj);

                    DisplayMessage("Data has been deleted.");

                    FillGrid();
                    break;
                default:

                    break;
            }
        }

        private void DisplayMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showModal('" + message + "');", true);
        }

        private void DisplayReport()
        {
            //Response.Write("<script language='javascript'>window.open('http://" +Dns.GetHostName()+ "/ReportServer?%2fPensionReport%2fAJTM_SUSPENSE_PAYOUT&rc:Toolbar=true&rc:Parameters=false&seq_nmbr=" +e.Item.Cells[1].Text+ "','MenuAccess','status=no,scrollbars=yes,width=800,height=600');</script>");
        }

        private void UpdateSuspenseHistory(PayoutSuspenseApprovalModels obj)
        {
            obj.UpdateSuspenseHistory(db);
        }

        private void UpdateClaimTrackRegister(PayoutSuspenseApprovalModels obj)
        {
            obj.UpdateClaimTrackRegister(db);
        }

        private DataTable ValidateClaimRegister(PayoutSuspenseApprovalModels obj)
        {
            DataTable result = obj.ValidateClaimRegister(db);
            return result;
        }

        private void UpdateReturInfo(PayoutSuspenseApprovalModels obj)
        {
            obj.UpdateReturInfo(db);
        }

        private void DeleteFppTable(PayoutSuspenseApprovalModels obj)
        {
            obj.DeleteFPPTable(db);
        }

        private void DeleteReturInfo(PayoutSuspenseApprovalModels obj)
        {
            obj.DeleteReturInfo(db);
        }



    }
}
