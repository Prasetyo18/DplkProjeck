using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.Pension.UserControl.ClientTabs
{
    public partial class ClientTransaction : System.Web.UI.UserControl
    {
        private Database db;
        private Connection conn;
        private int ClientId;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);
            if (!IsPostBack)
            {
                Setup();
            }
        }

        public void Setup()
        {
            //get certifs, and client name
            try
            {
                db.Open();
                if (Request.QueryString["state"] == "Edit")
                {
                    ClientId = Convert.ToInt32(Session["ClientIdDetail"]);
                }
                else if (Request.QueryString["state"] == "NewClient")
                {
                    ClientId = Convert.ToInt32(Session["newClientId"]);
                }
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadClientNm(db, txtClientNm, ClientId);
                ddlHelper.LoadDDLCertif(db, ddlCertifNmbr, ClientId);
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

        protected void GvTransacSummary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GvTransacDetail.DataSource = null;
                GvTransacDetail.DataBind();
                db.Open();
                switch (e.CommandName)
                {
                    case "Detail":
                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        int absoluteIndex = Math.Abs((GvTransacSummary.PageIndex * GvTransacSummary.PageSize) - rowIndex);
                        GridViewRow row = GvTransacSummary.Rows[absoluteIndex];

                        
                        ClientTransactionModels transac = new ClientTransactionModels();
                        transac.CertifNmbr = Convert.ToInt32(row.Cells[4].Text);
                        transac.TransacSequenceNmbr = Convert.ToInt32(row.Cells[2].Text);

                        DataTable dt = transac.LoadTransacDetail(db);
                        if (dt.Rows.Count > 0 )
                        {
                            GvTransacDetail.DataSource = dt;
                            GvTransacDetail.DataBind();
                        }
                        break;
                    default:
                        break;
                }
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

        protected void GvTransacSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvTransacSummary.PageIndex = e.NewPageIndex;
            LoadTransacSummary();
        }

        protected void GvTransacDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvTransacDetail.PageIndex = e.NewPageIndex;
            LoadTransacDetail();
        }

        private void LoadTransacDetail()
        {
            throw new NotImplementedException();
        }

        private void LoadTransacSummary()
        {
            GvTransacSummary.DataSource = null;
            GvTransacSummary.DataBind();

            GvTransacDetail.DataSource = null;
            GvTransacDetail.DataBind();
            try
            {
                if (ddlCertifNmbr.Items.Count > 0)
                {
                    db.Open();
                    ClientTransactionModels transac = new ClientTransactionModels();
                    transac.CertifNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                    transac.startTime = DateTime.Parse(txtTransacDtStart.Text);
                    transac.endTime = DateTime.Parse(txtTransacDtEnd.Text);
                    DataTable dt = transac.LoadTransacSummary(db);
                    if (dt.Rows.Count > 0)
                    {
                        GvTransacSummary.DataSource = dt;
                        GvTransacSummary.DataBind();
                    }
                }

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


        protected void btnGo_Click(object sender, EventArgs e)
        {
            //get company and group 
            try
            {
                if (ddlCertifNmbr.Items.Count > 0)
                {
                    db.Open();
                    ClientTransactionModels transac = new ClientTransactionModels();
                    transac.CertifNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                    transac.GetGroupCompanyTxt(db, txtGroupNmbr, txtCompanyNm);
                }

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Load the summary gridview
            LoadTransacSummary();
        }
    }
}