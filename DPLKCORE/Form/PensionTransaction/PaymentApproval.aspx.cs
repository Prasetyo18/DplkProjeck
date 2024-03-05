using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction
{
    public partial class PaymentApproval : System.Web.UI.Page
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
                fillGRID_UNAPPROVED();
                fillGRID_APPROVED();
                fillGRID_CANCELED();
            }
        }

        private void fillGRID_CANCELED()
        {
            DGR_CANCELED.DataSource = PaymentApprovalModels.GetCanceledData(db);
            DGR_CANCELED.DataBind();
        }

        private void fillGRID_APPROVED()
        {
            DGR_APPROVAL.DataSource = PaymentApprovalModels.GetApprovedData(db);
            DGR_APPROVAL.DataBind();
        }

        private void fillGRID_UNAPPROVED()
        {
            DGR_UNAPPROVED.DataSource = PaymentApprovalModels.GetUnapproveData(db);
            DGR_UNAPPROVED.DataBind();
        }

        protected void DGR_UNAPPROVED_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            int absoluteIndex;
            GridViewRow row;
            switch (e.CommandName.ToLower())
            {
                case "approvepayment":
                    index = Convert.ToInt32(e.CommandArgument);
                    absoluteIndex = Math.Abs((DGR_UNAPPROVED.PageIndex * DGR_UNAPPROVED.PageSize) - index);
                    row = DGR_UNAPPROVED.Rows[absoluteIndex];

                    PaymentApprovalModels paymentApprove = new PaymentApprovalModels();
                    paymentApprove.TransId = 100;
                    paymentApprove.UserName = Session["session.iduser"].ToString();
                    
                    paymentApprove.Ammount = float.Parse(row.Cells[7].Text.Replace(",",""));

                    paymentApprove.GroupNmbr = int.Parse(row.Cells[1].Text);
                    paymentApprove.SeqNmbr = int.Parse(row.Cells[4].Text);
                    paymentApprove.TransType = int.Parse(row.Cells[5].Text);
                    paymentApprove.PaycenterNmbr = int.Parse(row.Cells[2].Text);
                    paymentApprove.ApproveFlag= 1;

                    ExecuteApprovalLayer(paymentApprove);


                    if (PaymentApprovalModels.VerifyUnitPrice(db) == false)
                    {
                        DisplayMessage("Unit Price Not Available");
                        return;
                    }


                    ApprovePayment(paymentApprove);

                    fillGRID_UNAPPROVED();
                    fillGRID_APPROVED();
                    break;
                case "cancelpayment":
                    index = Convert.ToInt32(e.CommandArgument);
                    absoluteIndex = Math.Abs((DGR_UNAPPROVED.PageIndex * DGR_UNAPPROVED.PageSize) - index);
                    row = DGR_UNAPPROVED.Rows[absoluteIndex];

                    PaymentApprovalModels paymentCancel = new PaymentApprovalModels();
                    paymentCancel.GroupNmbr = int.Parse(row.Cells[1].Text);
                    paymentCancel.SeqNmbr = int.Parse(row.Cells[4].Text);
                    paymentCancel.TransType = int.Parse(row.Cells[5].Text);
                    paymentCancel.PaycenterNmbr = int.Parse(row.Cells[2].Text);
                    paymentCancel.ApproveFlag = 2;
                    paymentCancel.UserName = "";

                    CancelPayment(paymentCancel);

                    fillGRID_UNAPPROVED();
					fillGRID_CANCELED();

                    DisplayMessage("Payment has been canceled.");
                    break;
                default:
                    break;
            }
        }

        private void DisplayMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showModal('" + message + "');", true);
        }

        private void CancelPayment(PaymentApprovalModels obj)
        {
            //deletion is using the same sp with ApprovePayment. but approveflag = 2 and username = ""
            obj.ApprovePayment(db);
        }

        private void ExecuteApprovalLayer(PaymentApprovalModels obj)
        {
            DataTable approvalLayer = obj.ExecApprovalLayer(db);
            for (int i = 0; i < approvalLayer.Rows.Count; i++)
            {
                string message = approvalLayer.Rows[i]["msg"].ToString();
                DisplayMessage(message);
                return;
            }
        }

        private void ApprovePayment(PaymentApprovalModels obj)
        {
            obj.ApprovePayment(db);
        }

        protected void DGR_APPROVAL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            int absoluteIndex;
            GridViewRow row;
            switch (e.CommandName.ToLower())
            {
                case "cancel":
                    index = Convert.ToInt32(e.CommandArgument);
                    absoluteIndex = Math.Abs((DGR_APPROVAL.PageIndex * DGR_APPROVAL.PageSize) - index);
                    row = DGR_APPROVAL.Rows[absoluteIndex];

                    PaymentApprovalModels approvedCancel = new PaymentApprovalModels();
                    approvedCancel.GroupNmbr = int.Parse(row.Cells[1].Text);
                    approvedCancel.SeqNmbr = int.Parse(row.Cells[4].Text);

                    CancelApproved(approvedCancel);

                    fillGRID_UNAPPROVED();
                    fillGRID_APPROVED();
                    break;
                default:
                    break;
            }
        }

        private void CancelApproved(PaymentApprovalModels obj)
        {
            obj.CancelApproved(db);
        }

        protected void DGR_UNAPPROVED_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_UNAPPROVED.PageIndex = e.NewPageIndex;
            fillGRID_UNAPPROVED();
        }

        protected void DGR_APPROVAL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_APPROVAL.PageIndex = e.NewPageIndex;
            fillGRID_APPROVED();
        }

        

        protected void DGR_CANCELED_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_CANCELED.PageIndex = e.NewPageIndex;
            fillGRID_CANCELED();
        }
    }
}