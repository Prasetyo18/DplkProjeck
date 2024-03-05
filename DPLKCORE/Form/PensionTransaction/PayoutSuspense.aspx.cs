using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
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
    public partial class PayoutSuspense : System.Web.UI.Page
    {
        Database db;
        Connection conn;

        protected void Page_Init(object sender, EventArgs e)
        {
            UCSearchPanel.BtnSearch.Click += OnButtonClick;
            UCSearchPanel.GvResult.PageIndexChanging += OnPageIndexChanging;
            UCSearchPanel.GvResult.RowCommand += OnRowCommand;
        }

        private void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                if (Session["searchResult"] != null)
                {
                    GridViewRow row = Session["searchResult"] as GridViewRow;
                    if (Convert.ToInt32(Session["searchType"]) == 9)
                    {
                        TXT_REGIS.Text = row.Cells[1].Text;
                    }
                    if (Convert.ToInt32(Session["searchType"]) == 3)
                    {
                        TXT_BANKNM.Text = row.Cells[1].Text;
                    }
                    searchModal.Hide();
                }
            }
        }

        private void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                searchModal.Show();
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button ib = sender as Button;
            if (ib == UCSearchPanel.BtnSearch)
            {
                searchModal.Show();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                
                FillGrid();
                ClearControls();
                FillControls();
            }
        }

        private void ClearControls()
        {
            TXT_ACCTNM.Text = "";
            TXT_ACCTNO.Text = "";
            TXT_AMT.Text = "";
            TXT_BANKNM.Text = "";
            TXT_CHEQUE.Text = "0";
            TXT_DESC.Text = "";
            TXT_REGIS.Text = "";
            TXT_RETURINFO.Text = "";
            TXT_SUSPN.Text = "";
            TXT_SEQ.Text = "";
        }

        private void FillControls()
        {
            FillSequenceNmbr();
            FillDDLInvestment();
            
        }

        private void FillDDLInvestment()
        {
            DDLPayoutSuspense.LoadDDLInvest(db, DDL_INV);
        }

        private void FillSequenceNmbr()
        {
            TXT_SEQ.Text = DDLPayoutSuspense.GetLastestSeqNmbr(db);
        }

        private void FillGrid()
        {
            DGR_SUSPENSE_IDENTIFIED.DataSource = PayoutSuspenseModels.GetSuspenseRestData(db);
            DGR_SUSPENSE_IDENTIFIED.DataBind();
        }

        protected void DGR_SUSPENSE_IDENTIFIED_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index;
            int absoluteIndex;
            GridViewRow row;
            string message = "";
            switch (e.CommandName.ToLower())
            {
                case "retur":
                    index = Convert.ToInt32(e.CommandArgument);
                    absoluteIndex = Math.Abs((DGR_SUSPENSE_IDENTIFIED.PageIndex * DGR_SUSPENSE_IDENTIFIED.PageSize)-index);
                    row = DGR_SUSPENSE_IDENTIFIED.Rows[absoluteIndex];

                    PayoutSuspenseModels payoutSuspense = new PayoutSuspenseModels();
                    payoutSuspense.SuspenseNmbr = Convert.ToInt64(row.Cells[0].Text);
                    if (CheckPaymentHistory(payoutSuspense) == true)
                    {
                        message = "Suspense waiting for approval on contribution approval";
                        DisplayMessage(message);
                        return;
                    }

                    if(CheckReturInfo(payoutSuspense) == true){
                        message = "Suspense waiting for approval on retur approval";
                        DisplayMessage(message);
                        return;
                    }

                    ClearControls();
                    FillControls();

                    TXT_SUSPN.Text = row.Cells[0].Text;
				    TXT_AMT.Text = row.Cells[3].Text;
				    TXT_DESC.Text = row.Cells[4].Text;

                    break;
                default:
                    break;
            }
        }

        private bool CheckReturInfo(Logic.Pension.PayoutSuspenseModels obj)
        {
            DataTable result = obj.CheckReturInfo(db);
            if (result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void DisplayMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "showModal('" + message + "');", true);
        }

        private bool CheckPaymentHistory(PayoutSuspenseModels obj)
        {
            DataTable result = obj.CheckPaymentHistory(db);
            if (result.Rows.Count > 0)
            {
                return true;
            }
            return false;   
        }

        protected void DGR_SUSPENSE_IDENTIFIED_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_SUSPENSE_IDENTIFIED.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            Session["searchType"] = 9;
            UCSearchPanel.Setup();
            searchModal.Show();
        }

        protected void BT_SEARCH_BANK_Click(object sender, EventArgs e)
        {
            Session["searchType"] = 3;
            UCSearchPanel.Setup();
            searchModal.Show();
        }

        protected void BT_GO_Click(object sender, EventArgs e)
        {
            if (TXT_REGIS.Text == "")
            {
                return;
            }

            int len = TXT_REGIS.Text.Length;
            string regis = TXT_REGIS.Text.Substring(0, len - 4);
            string tfer = TXT_REGIS.Text.Substring(len - 3, 3);

            PayoutSuspenseModels payoutSuspense = new PayoutSuspenseModels();
            payoutSuspense.RegisId = regis;
            payoutSuspense.TransferType = tfer;

            TXT_ACCTNM.Text = "";
            TXT_ACCTNO.Text = "";
            TXT_BANKNM.Text = "";

            ReadSuspenseData(payoutSuspense);
        }

        private void ReadSuspenseData(PayoutSuspenseModels obj)
        {
            DataTable result = obj.ReadSuspense(db);
            if (result.Rows.Count > 0)
            {
                TXT_ACCTNM.Text = result.Rows[0]["Acc. Name"].ToString();
				TXT_ACCTNO.Text = result.Rows[0]["Acc. No"].ToString();
				TXT_BANKNM.Text = result.Rows[0]["Bank_nm"].ToString();
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            string sCer_nmbr = "";
            string sClient_nm = "";
            string seq = "";
            seq = TXT_SEQ.Text;
            if (TXT_REGIS.Text == "")
            {
                DisplayMessage("Please fill in the Data");
                return;
            }

            int len = TXT_REGIS.Text.Length;
            string regis = TXT_REGIS.Text.Substring(0, len - 4);

            if (DDL_INV.SelectedValue == "0")
            {
                DisplayMessage("Please Choose Fund");
                return;
            }

            PayoutSuspenseModels payoutSuspense = new PayoutSuspenseModels();
            payoutSuspense.RegisId = regis;

            DataTable certifClient = GetCertifClient(payoutSuspense);

            if (certifClient.Rows.Count > 0)
            {
                sCer_nmbr = certifClient.Rows[0]["cer_nmbr"].ToString();
                sClient_nm = certifClient.Rows[0]["cer_nmbr"].ToString();
            }

            payoutSuspense.SeqNmbr= int.Parse(TXT_SEQ.Text);
            payoutSuspense.CertifNmbr = int.Parse(sCer_nmbr);
            payoutSuspense.ClientNm = sClient_nm;
            payoutSuspense.BankNm = TXT_BANKNM.Text;
            payoutSuspense.AccNmbr = TXT_ACCTNO.Text;
            payoutSuspense.AccNm =TXT_ACCTNM.Text;
            payoutSuspense.Ammount =float.Parse(TXT_AMT.Text.Replace(",",""));
            payoutSuspense.CheckAmmount = float.Parse(TXT_CHEQUE.Text);
            payoutSuspense.TransferType = "Retur";
            payoutSuspense.Remarks = TXT_RETURINFO.Text;
            payoutSuspense.SuspenseNmbr = long.Parse(TXT_SUSPN.Text);
            payoutSuspense.ApprovalFlag = 0;
            payoutSuspense.PrepareBy = Session["session.iduser"].ToString();
            payoutSuspense.ApprovedBy = "";
            payoutSuspense.InvestTypeNmbr = int.Parse(DDL_INV.SelectedValue);

            AddPayoutSuspense(payoutSuspense);

            if (seq == "")
            {
                seq = PayoutSuspenseModels.GetLatestSeqNmbr(db);
            }
            //showup report viewer
            //Response.Write("<script language='javascript'>window.open('http://" +Dns.GetHostName()+ "/ReportServer?%2fPensionReport%2fAJTM_SUSPENSE_PAYOUT&rc:Toolbar=true&rc:Parameters=false&seq_nmbr=" +seq+ "','MenuAccess','status=no,scrollbars=yes,width=800,height=600');</script>");

            ClearControls();
            FillGrid();
            DisplayMessage("Data has been saved successfully.");

        }

        private void AddPayoutSuspense(Logic.Pension.PayoutSuspenseModels obj)
        {
            obj.AddPayoutSuspense(db);
        }

        private DataTable GetCertifClient(PayoutSuspenseModels obj)
        {
            DataTable result = obj.GetCertifClientData(db);
            return result;
        }



    }
}