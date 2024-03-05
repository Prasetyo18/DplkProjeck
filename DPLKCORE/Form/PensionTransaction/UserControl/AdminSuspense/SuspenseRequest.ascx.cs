using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction.UserControl.AdminSuspense
{
    public partial class SuspenseRequest : System.Web.UI.UserControl
    {
        private Database db;
        private Connection conn;
        private bool IsNew;
        private string iGroup_nmbr;
        private string nSuspense_nmbr;

        private void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (!IsPostBack)
            {
                FillDDL();
                FillGrid();
                IsNew = true;
                TXT_SUSPNNO.Text = "0";

            }
        }

        private void FillGrid()
        {
            GVRequest.DataSource = AdminSuspenseModels.LoadSuspenseRequestData(db);
            GVRequest.DataBind();
        }

        private void FillDDL()
        {
            DDLAdminSuspense.LoadDDLGroup(db, DDL_GROUP);
            DDLAdminSuspense.LoadDDLSuspenseType(db, DDL_SUSPNTYPE);
            DDLAdminSuspense.LoadDDLPaycenter(db, DDL_PAYCENTER, DDL_GROUP.SelectedValue);
        }

        protected void GVRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVRequest.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            int absoluteIndex;
            GridViewRow row;
            if (e.CommandName.ToLower() == "editrow")
            {
                index = Convert.ToInt32(e.CommandArgument);
                absoluteIndex = Math.Abs((GVRequest.PageIndex * GVRequest.PageSize) - index);
                row = GVRequest.Rows[absoluteIndex];

                TXT_SUSPNNO.Text = row.Cells[1].Text;
                TXT_SUSPNAMT.Text = string.Format(CultureInfo.CurrentCulture, "{0:C}", row.Cells[2].Text.ToString());
                TXT_SUSPNDESC.Text = row.Cells[3].Text;
                DDL_SUSPNTYPE.SelectedIndex = DDL_SUSPNTYPE.Items.IndexOf(DDL_SUSPNTYPE.Items.FindByText(row.Cells[4].Text));
                TXT_RECEIVEDT.Text = Convert.ToDateTime(row.Cells[5].Text).ToString("yyyy-MM-dd");
                DDL_GROUP.SelectedIndex = DDL_GROUP.Items.IndexOf(DDL_GROUP.Items.FindByText(row.Cells[7].Text+" - "+ row.Cells[6].Text));
                TxtRef_Bank.Text = row.Cells[9].Text;
                DDL_PAYCENTER.SelectedIndex = DDL_PAYCENTER.Items.IndexOf(DDL_PAYCENTER.Items.FindByText(row.Cells[8].Text));
                IsNew = false;
            }
        }

        protected void DDL_GROUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLAdminSuspense.LoadDDLPaycenter(db, DDL_PAYCENTER, DDL_GROUP.SelectedValue);
        }

        protected void BTN_SUBMIT_Click(object sender, EventArgs e)
        {
            if (TXT_RECEIVEDT.Text != "")
            {
                // Cek Value date //
                try
                {
                    DateTime date = DateTime.Parse(TXT_RECEIVEDT.Text);
                }
                catch (Exception err)
                {
                    string sMsg = "";
                    if (err.Message == "String was not recognized as a valid DateTime.")
                    {
                        sMsg = "Wrong format on field Received Date. Use YYYY/MM/DD ";

                    }

                    //popup the sMsg
                    return;
                }

                AdminSuspenseModels adminSuspense = new AdminSuspenseModels();
                adminSuspense.SuspnNumber = long.Parse(TXT_SUSPNNO.Text);
                adminSuspense.GroupNumber = int.Parse(DDL_GROUP.SelectedValue);
                adminSuspense.PaycenterNmbr = int.Parse(DDL_PAYCENTER.SelectedValue);
                adminSuspense.SuspenseAmount = float.Parse(TXT_SUSPNAMT.Text.Replace(",", ""));
                adminSuspense.SuspenseUseAmount = 0;
                adminSuspense.FinApproveCd = 0;
                adminSuspense.AdmApproveCd = 0;
                adminSuspense.SuspenseDescription = TXT_SUSPNDESC.Text;
                adminSuspense.SuspenseDescription2 = "";
                adminSuspense.Remark = "";
                adminSuspense.SuspenseType = DDL_SUSPNTYPE.SelectedValue;
                adminSuspense.ReceivedDate = DateTime.Parse(TXT_RECEIVEDT.Text);
                adminSuspense.UseStatusCd = 0;
                adminSuspense.WaivedFlag = 0;
                adminSuspense.RcptVoucher = 0;
                adminSuspense.ReferenceBank = TxtRef_Bank.Text;
                adminSuspense.SubmitSuspense(db);

                FillGrid();

            }

            iGroup_nmbr = "0";
            nSuspense_nmbr = "0";

            if (DDL_GROUP.SelectedValue != "0")
            {
                iGroup_nmbr = DDL_GROUP.SelectedValue;
            }

        }
    }
}