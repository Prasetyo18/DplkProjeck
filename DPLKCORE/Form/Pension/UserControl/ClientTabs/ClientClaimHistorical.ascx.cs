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
    public partial class ClientClaimHistorical : System.Web.UI.UserControl
    {
        private Connection conn;
        private Database db;
        private int ClientId;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (!IsPostBack)
            {
                //clear textbox in Detail section. can use controlHelper
                ClearDetailText();
                //setup the clientName and certif numbers
                Setup();
            }
        }

        public void Setup()
        {
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

            ddlCertifNmbr.SelectedIndex = 0;
            LoadResult();
        }

        protected void ddlCertifNmbr_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadResult();
        }

        public void LoadResult()
        {
            //set the group text and the company name
            GetCompanyAndGroup();
            //fill the gridview
            FillGvAvailableClaim();
            FillGvClaimHistorical();
        }

        private void FillGvClaimHistorical()
        {
            try
            {
                GvClaimHistorical.DataSource = null;
                GvClaimHistorical.DataBind();

                if (ddlCertifNmbr.Items.Count > 0)
                {
                    db.Open();
                    ClientClaimHistoricalModels claimHistory = new ClientClaimHistoricalModels();
                    claimHistory.CerNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                    DataTable dt = claimHistory.LoadGvClaim(db);
                    if (dt.Rows.Count > 0)
                    {
                        GvClaimHistorical.DataSource = dt;
                        GvClaimHistorical.DataBind();
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

        private void FillGvAvailableClaim()
        {
            try
            {
                
                GvAvailableClaim.DataSource = null;
                GvAvailableClaim.DataBind();

                if (ddlCertifNmbr.Items.Count > 0)
                {
                    db.Open();
                    ClientClaimHistoricalModels claimHistory = new ClientClaimHistoricalModels();
                    claimHistory.CerNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                    DataTable dt = claimHistory.LoadGvAvailableClaim(db);
                    if (dt.Rows.Count > 0)
                    {
                        GvAvailableClaim.DataSource = dt;
                        GvAvailableClaim.DataBind();
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

        private void GetCompanyAndGroup()
        {
            try
            {
                if (ddlCertifNmbr.Items.Count > 0)
                {
                    db.Open();
                    ClientClaimHistoricalModels claimHistory = new ClientClaimHistoricalModels();
                    claimHistory.CerNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                    claimHistory.GetGroupCompanyTxt(db, txtGroupNmbr, txtCompanyNm);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Close();
            }
        }

        private void ClearDetailText()
        {
            txtGrossAmt.Text = "";
            txtFeeAmt.Text = "";
            txtTaxAmt.Text = "";
            txtNetAmt.Text = "";
            txtCDV.Text = "";
            txtLumpsumAmt.Text = "";
            txtAccountNmbr.Text = "";
            txtAccountNm.Text = "";
            txtBankNm.Text = "";
            txtAnnuityAmt.Text = "";
            txtAccountNmbr2.Text = "";
            txtAccountNm2.Text = "";
            txtBankNm2.Text = "";
        }

        protected void GvClaimHistorical_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                db.Open();
                switch (e.CommandName)
                {
                    case "View":
                        //clear text
                        ClearDetailText();
                        GvDetailHistoryClaimStat.DataSource = null;
                        GvDetailHistoryClaimStat.DataBind();

                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        int absoluteIndex = Math.Abs((GvClaimHistorical.PageIndex * GvClaimHistorical.PageSize) - rowIndex);
                        GridViewRow row = GvClaimHistorical.Rows[absoluteIndex];

                        ClientClaimHistoricalModels claimHistory = new ClientClaimHistoricalModels();
                        claimHistory.CerNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                        claimHistory.TransactionDate = row.Cells[1].Text;
                        claimHistory.TransactionTypeNm = row.Cells[2].Text;
                        claimHistory.RegisterId = row.Cells[3].Text;
                        if (row.Cells[5].Text != "")
                        {
                            claimHistory.SequenceId = int.Parse(row.Cells[5].Text);
                        }
                        else
                        {
                            claimHistory.SequenceId = 0;
                        }

                        //fill GridView for Detail Claim
                        DataTable dt = claimHistory.LoadGvDetail(db);
                        if (dt.Rows.Count > 0)
                        {
                            GvDetailHistoryClaimStat.DataSource = dt;
                            GvDetailHistoryClaimStat.DataBind();
                        }

                        string sText;
                        sText = row.Cells[1].Text;
                        if (sText == "&nbsp;")
                        {
                            sText = "1900/01/01";
                        }

                        //fill textboxes in Detail Section
                        Dictionary<string, object> dt2 = claimHistory.LoadDetailSection(db);
                        if (dt2.Count > 0)
                        {
                            if (dt2["gross"].ToString() != "")
                            {
                                txtGrossAmt.Text = dt2["gross"].ToString();
                            }

                            if (dt2["fee"].ToString() != "")
                            {
                                txtFeeAmt.Text = dt2["fee"].ToString();
                            }

                            if (dt2["tax"].ToString() != "")
                            {
                                txtTaxAmt.Text = dt2["tax"].ToString();
                            }

                            if (dt2["net"].ToString() != "")
                            {
                                txtNetAmt.Text = dt2["net"].ToString();
                            }

                            if (dt2["flp"].ToString() != "")
                            {
                                txtFLP.Text = dt2["flp"].ToString();
                            }

                            if (dt2["cdv"].ToString() != "")
                            {
                                txtCDV.Text = dt2["cdv"].ToString();
                            }

                            if (dt2["ls_amt"].ToString() != "")
                            {
                                txtLumpsumAmt.Text = dt2["ls_amt"].ToString();
                            }

                            if (dt2["ls_actNo"].ToString() != "")
                            {
                                txtAccountNmbr.Text = dt2["ls_actNo"].ToString();
                            }

                            if (dt2["ls_actNm"].ToString() != "")
                            {
                                txtAccountNm.Text = dt2["ls_actNm"].ToString();
                            }

                            if (dt2["ls_bank"].ToString() != "")
                            {
                                txtBankNm.Text = dt2["ls_bank"].ToString();
                            }

                            if (dt2["an_amt"].ToString() != "")
                            {
                                txtAnnuityAmt.Text = dt2["an_amt"].ToString();
                            }

                            if (dt2["an_actNo"].ToString() != "")
                            {
                                txtAccountNmbr2.Text = dt2["an_actNo"].ToString();
                            }

                            if (dt2["an_actNm"].ToString() != "")
                            {
                                txtAccountNm2.Text = dt2["an_actNm"].ToString();
                            }

                            if (dt2["an_bank"].ToString() != "")
                            {
                                txtBankNm2.Text = dt2["an_bank"].ToString();
                            }

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
        }

        protected void GvClaimHistorical_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GvAvailableClaim_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GvClaimHistorical_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GvDetailHistoryClaimStat_PageIndexChanged(object sender, EventArgs e)
        {

        }
    }
}