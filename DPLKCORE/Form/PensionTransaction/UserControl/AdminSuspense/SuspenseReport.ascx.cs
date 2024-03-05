using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction.UserControl.AdminSuspense
{
    public partial class SuspenseReport : System.Web.UI.UserControl
    {
        Database db;
        Connection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (!IsPostBack)
            {
                fillGridRest();
            }
        }

        private void fillGridRest()
        {
            SuspenseReportModels suspenseReport = new SuspenseReportModels();
            suspenseReport.SuspenseNmbr = TXT_SUSPNNO.Text;
            suspenseReport.GroupNmbr = TXT_GROUPNO.Text;

            DGR_REST.DataSource = suspenseReport.GetSuspenseResetData(db);
            DGR_REST.DataBind();
        }

        protected void BTN_SEARCH_Click(object sender, EventArgs e)
        {
            fillGridRest();
        }

        protected void DGR_REST_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_REST.PageIndex = e.NewPageIndex;
            fillGridRest();
        }

        protected void DGR_REST_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "viewreport")
            {
                //open report viewer in new window.
                //Response.Write("<script language='javascript'>window.open('http://" +Dns.GetHostName()+ "/ReportServer?%2fPensionReport%2fAJTM-KUITANSI&rc:Toolbar=true&rc:Parameters=false&suspn_nmbr=" +e.Item.Cells[0].Text + "','MenuAccess','status=no,scrollbars=yes,width=800,height=600');</script>");
            }
        }
    }
}