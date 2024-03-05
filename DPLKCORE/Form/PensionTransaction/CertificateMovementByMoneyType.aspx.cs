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
    public partial class CertificateMovementByMoneyType : System.Web.UI.Page
    {
        Connection conn;
        Database db;
        protected void Page_Init(object sender, EventArgs e)
        {
            UCSearchPanel.BtnSearch.Click += OnButtonClick;
            UCSearchPanel.GvResult.PageIndexChanging += OnPageIndexChanging;
            UCSearchPanel.GvResult.RowCommand += OnRowCommand;
        }

        private void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView gv = (GridView)sender;

            if (gv == UCSearchPanel.GvResult)
            {
                if (Session["searchResult"] != null)
                {
                    GridViewRow row = Session["searchResult"] as GridViewRow;
                    TxtCertificate.Text = row.Cells[1].Text;
                    searchModal.Hide();
                }
            }
        }

        private void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;

            if (gv == UCSearchPanel.GvResult)
            {
                searchModal.Show();
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn == UCSearchPanel.BtnSearch)
            {
                searchModal.Show();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);
            if (Session.Count == 0)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            Session["searchType"] = 2;
            Session["searchCaller"] = "TXT_CER_NMBR";
            if (!IsPostBack)
            {
                FillDDLCompany();
                FillTransactionDate();
            }
        }

        private void FillTransactionDate()
        {
            TXT_EFCTV_DT.Text = DDLCertificateMovement.LoadTransactionDate(db);
        }

        private void FillDDLCompany()
        {
            DDLCertificateMovement ddlHelper = new DDLCertificateMovement();
            ddlHelper.LoadCompanyDDL(db, DDL_COMPANY_NEW);
        }

        protected void BTN_GO_Click(object sender, EventArgs e)
        {
            //setup based on certificate 
            CertificateMovementModels certifMovement = new CertificateMovementModels();
            certifMovement.CertificateNumber = int.Parse(TxtCertificate.Text);
            certifMovement.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);

            DataTable ClientData = certifMovement.GetDataClient(db);
            if (ClientData.Rows.Count > 0)
            {
                LBL_CLIENT_NM.Text = ClientData.Rows[0]["client_nm"].ToString();
                LBL_COMPANY_OLD.Text = ClientData.Rows[0]["company_nm"].ToString();
                TXT_GROUP_OLD.Text = ClientData.Rows[0]["group_nmbr"].ToString();
            }

            //fill grid DGR_INV_SRC
            certifMovement.GroupNumber = int.Parse(TXT_GROUP_OLD.Text);
            certifMovement.Mode = 1;
            DGR_INV_SRC.DataSource = certifMovement.GetInvestmentDirectFunds(db);
            DGR_INV_SRC.DataBind();
            BTN_SAVE.Enabled = false;

            //fill DGR_INV_DST 
            certifMovement.GroupNumber = 0;
            certifMovement.Mode = 2;
            DGR_INV_DST.DataSource = certifMovement.GetInvestmentDirectFunds(db);
            DGR_INV_DST.DataBind();

            //fill DGR_FUND
            certifMovement.GroupByType = 3;
            DGR_FUND.DataSource = certifMovement.GetFundInfoMT(db);
            DGR_FUND.DataBind();
        }

        protected void BT_VALIDATE_Click(object sender, EventArgs e)
        {
            double EE = 0;
            double ER = 0;
            double TU = 0;
            double FT = 0;

            for (int i = 0; i < DGR_INV_DST.Rows.Count; i++)
            {
                TextBox TXTEE = (TextBox)DGR_INV_DST.Rows[i].Cells[2].FindControl("TXT_EE");
                TextBox TXTER = (TextBox)DGR_INV_DST.Rows[i].Cells[3].FindControl("TXT_ER");
                TextBox TXTTU = (TextBox)DGR_INV_DST.Rows[i].Cells[4].FindControl("TXT_TU");
                TextBox TXTFT = (TextBox)DGR_INV_DST.Rows[i].Cells[5].FindControl("TXT_FT");
                EE = EE + double.Parse(TXTEE.Text);
                ER = ER + double.Parse(TXTER.Text);
                TU = TU + double.Parse(TXTTU.Text);
                FT = FT + double.Parse(TXTFT.Text);
            }

            if (EE != 100)
            {
                //GlobalTools.popMessage(this, "Employee PCT does not equal 100 % ");
                return;
            }
            if (ER != 100)
            {
                //GlobalTools.popMessage(this, "Employer PCT does not equal 100 % ");
                return;
            }
            if (TU != 100)
            {
                //GlobalTools.popMessage(this, "Top Up PCT does not equal 100 % ");
                return;
            }
            if (FT != 100)
            {
                //GlobalTools.popMessage(this, "Fund Transfer PCT does not equal 100 % ");
                return;
            }
            BTN_SAVE.Enabled = true;
            //GlobalTools.popMessage(this, "Percentage Valid");
            return;
        }

        protected void BTN_SAVE_Click(object sender, EventArgs e)
        {
            if (DDL_COMPANY_NEW.SelectedItem.Text != TXT_GROUP_OLD.Text)
            {
                CertificateMovementModels certifMovement = new CertificateMovementModels();
                certifMovement.CertificateNumber = int.Parse(TxtCertificate.Text);
                certifMovement.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                certifMovement.OldGroupNumber = int.Parse(TXT_GROUP_OLD.Text);
                certifMovement.NewGroupNumber = int.Parse(DDL_COMPANY_NEW.SelectedValue);

                DataTable result = certifMovement.MoveCertificate(db);
                string message = result.Rows[0]["mesage"].ToString();

                if (result.Rows.Count > 0)
                {
                    for (int i = 0; i < DGR_INV_DST.Rows.Count; i++)
                    {
                        TextBox TXTEE = (TextBox)DGR_INV_DST.Rows[i].Cells[2].FindControl("TXT_EE");
                        TextBox TXTER = (TextBox)DGR_INV_DST.Rows[i].Cells[3].FindControl("TXT_ER");
                        TextBox TXTTU = (TextBox)DGR_INV_DST.Rows[i].Cells[4].FindControl("TXT_TU");
                        TextBox TXTFT = (TextBox)DGR_INV_DST.Rows[i].Cells[5].FindControl("TXT_FT");

                        CertificateInfoModels certifInfo = new CertificateInfoModels();
                        certifInfo.CertificateNumber = int.Parse(TxtCertificate.Text);
                        certifInfo.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                        certifInfo.InvTypeNmbr = int.Parse(DGR_INV_DST.Rows[i].Cells[6].Text);

                        certifInfo.MoneyTypeNmbr = 101;
                        certifInfo.Percentage = float.Parse(TXTEE.Text);
                        certifInfo.InsertCertifInvDrct(db);

                        certifInfo.MoneyTypeNmbr = 102;
                        certifInfo.Percentage = float.Parse(TXTER.Text);
                        certifInfo.InsertCertifInvDrct(db);

                        certifInfo.MoneyTypeNmbr = 103;
                        certifInfo.Percentage = float.Parse(TXTTU.Text);
                        certifInfo.InsertCertifInvDrct(db);

                        certifInfo.MoneyTypeNmbr = 104;
                        certifInfo.Percentage = float.Parse(TXTFT.Text);
                        certifInfo.InsertCertifInvDrct(db);
                    }

                    CertificateMovementModels certifMovement2 = new CertificateMovementModels();
                    certifMovement2.CertificateNumber = int.Parse(TxtCertificate.Text);
                    certifMovement2.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);

                    //fill grid DGR_INV_SRC
                    certifMovement2.Mode = 1;
                    certifMovement2.GroupNumber = int.Parse(TXT_GROUP_OLD.Text);  
                    DGR_INV_SRC.DataSource = certifMovement2.GetInvestmentDirectFunds(db);
                    DGR_INV_SRC.DataBind();

                    //fill DGR_FUND
                    certifMovement2.GroupByType = 3;
                    DGR_FUND.DataSource = certifMovement2.GetFundInfoMT(db);
                    DGR_FUND.DataBind();

                    FillDDLSwitch();
                    FillBatch(false);
                    FirstSaveComposition();
                }
            }
        }

        private void FirstSaveComposition()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                for (int i = 0; i < DGR_FUND.Rows.Count; i++)
                {
                    FundSwitchingModels fundSwitching = new FundSwitchingModels();
                    fundSwitching.CertificateNumber = int.Parse(TxtCertificate.Text);
                    fundSwitching.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                    //fundSwitching.InvestmentTypeNumber = 0;
                    //fundSwitching.MoneyTypeNmbr = 0
                    fundSwitching.CurrentAsset = float.Parse(DGR_FUND.Rows[i].Cells[2].Text.Replace(",", ""));
                    //fundSwitching.CompositionPercentage = 100;
                    //fundSwitching.TransactionAmount = 0;
                    fundSwitching.InvestmentTypeName = DGR_FUND.Rows[i].Cells[0].Text.Replace(",", "");
                    fundSwitching.MoneyTypeNm = DGR_FUND.Rows[i].Cells[1].Text.Replace(",", "");
                    //fundSwitching.Mode = 0;
                    fundSwitching.FirstSaveCompositionMT(db);
                }
                db.CommitTransaction();
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

        private void FillBatch(bool isNew)
        {
            try
            {
                db.Open();
                if (isNew == true)
                {
                    db.BeginTransaction();
                    FundSwitchingModels.UpdateLastBatchId(db);
                    db.CommitTransaction();
                }
                TXT_BATCHID.Text = FundSwitchingModels.GetLatestBatchID(db);
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        private void FillDDLSwitch()
        {
            //ddl source fund
            FillDDLSourceFund();
            FillDDLSourceMT();

            //ddl dst fund
            FillDDLDestFund();
            FillDDLDestMT();

            //ddl method (not using query)
            FillDDLMethod();
        }

        private void FillDDLMethod()
        {
            try
            {
                DDLFundSwitching ddlHelper = new DDLFundSwitching();
                ddlHelper.LoadDDLMode(DDL_METHOD);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void FillDDLDestMT()
        {
            try
            {
                db.Open();
                DDLFundSwitching ddlHelper = new DDLFundSwitching();
                ddlHelper.CerNmbr = int.Parse(TxtCertificate.Text);
                ddlHelper.LoadDDLMT(db, DDL_DST_MT);
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


        private void FillDDLDestFund()
        {
            try
            {
                db.Open();
                DDLFundSwitching ddlHelper = new DDLFundSwitching();
                ddlHelper.CerNmbr = int.Parse(TxtCertificate.Text);
                ddlHelper.LoadDDLDestFund(db, DDL_DST_FUND);
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

        private void FillDDLSourceMT()
        {
            try
            {
                db.Open();
                DDLFundSwitching ddlHelper = new DDLFundSwitching();
                ddlHelper.CerNmbr = int.Parse(TxtCertificate.Text);
                ddlHelper.LoadDDLMT(db, DDL_SRC_MT);
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

        private void FillDDLSourceFund()
        {
            try
            {
                db.Open();
                DDLFundSwitching ddlHelper = new DDLFundSwitching();
                ddlHelper.CerNmbr = int.Parse(TxtCertificate.Text);
                ddlHelper.LoadDDLSourceFund(db, DDL_SRC_FUND);
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


        protected void BTN_SEARCH_Click(object sender, EventArgs e)
        {

        }

        protected void DDL_COMPANY_NEW_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTN_GO_Click(sender, e);
            FillGridviewInvDst();
        }

        private void FillGridviewInvDst()
        {
            CertificateMovementModels certifMovement = new CertificateMovementModels();
            certifMovement.CertificateNumber = int.Parse(TxtCertificate.Text);
            certifMovement.GroupNumber = int.Parse(DDL_COMPANY_NEW.SelectedValue);
            certifMovement.Mode = 2;

            DGR_INV_DST.DataSource = certifMovement.GetInvestmentDirectFunds(db);
            DGR_INV_DST.DataBind();

            for (int i = 0; i < DGR_INV_DST.Rows.Count; i++)
            {
                TextBox TXTEE = (TextBox)DGR_INV_DST.Rows[i].Cells[2].FindControl("TXT_EE");
                TextBox TXTER = (TextBox)DGR_INV_DST.Rows[i].Cells[3].FindControl("TXT_ER");
                TextBox TXTTU = (TextBox)DGR_INV_DST.Rows[i].Cells[4].FindControl("TXT_TU");
                TextBox TXTFT = (TextBox)DGR_INV_DST.Rows[i].Cells[5].FindControl("TXT_FT");
                TXTEE.Text = "0";
                TXTER.Text = "0";
                TXTTU.Text = "0";
                TXTFT.Text = "0";
            }
            BTN_SAVE.Enabled = false;
        }

        protected void DGR_INV_SRC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR_INV_SRC.PageIndex = e.NewPageIndex;
            BTN_GO_Click(sender, e);
        }

        protected void BTN_NEW_Click(object sender, EventArgs e)
        {
            FillBatch(true);
        }

        protected void BTN_CALC_Click(object sender, EventArgs e)
        {
            CalculateAmmount();
        }

        private void CalculateAmmount()
        {
            try
            {
                db.Open();
                FundSwitchingModels fundSwitch = new FundSwitchingModels();
                fundSwitch.CertificateNumber = int.Parse(TxtCertificate.Text);
                fundSwitch.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                fundSwitch.MoneyTypeNmbr = int.Parse(DDL_SRC_MT.SelectedValue);
                fundSwitch.InvestmentTypeNumber = int.Parse(DDL_SRC_FUND.SelectedValue);
                fundSwitch.InvestmentTypeNumberDst = int.Parse(DDL_DST_FUND.SelectedValue);
                fundSwitch.Mode = int.Parse(DDL_METHOD.SelectedValue);
                fundSwitch.TransactionAmount = float.Parse(TXT_SWITCH_VALUE.Text.Replace(",",""));

                TXT_SWITCH_AMT.Text = fundSwitch.GetCalculationMT(db);
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

        protected void BTN_SAVESWITHC_Click(object sender, EventArgs e)
        {
            SaveProcess();
        }

        private void SaveProcess()
        {
            if (DDL_SRC_MT.SelectedItem.Text != DDL_DST_MT.SelectedItem.Text)
            {
                //popup Source and destination money type were different
                return;
            }

            InsertData();

            //load gvestimation
            LoadEstimationResult();

            //update for current source existing fund
            UpdateCurrentSourceFund();

            //update for current destination existing fund
            UpdateCurrentDestinationFund();

            //popup with message data has been saved

            TXT_SWITCH_AMT.Text = "";
            TXT_SWITCH_VALUE.Text = "";
        }

        private void UpdateCurrentDestinationFund()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                FundSwitchingModels fundSwitching = new FundSwitchingModels();
                fundSwitching.CertificateNumber = int.Parse(TxtCertificate.Text);
                fundSwitching.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                fundSwitching.InvestmentTypeNumber = 0;
                fundSwitching.MoneyTypeNmbr = 0;
                fundSwitching.CurrentAsset = 0;
                fundSwitching.CompositionPercentage = 100;
                fundSwitching.TransactionAmount = float.Parse(TXT_SWITCH_AMT.Text.Replace(",", ""));
                fundSwitching.InvestmentTypeName = DDL_DST_FUND.SelectedItem.Text.Replace(",", "");
                fundSwitching.MoneyTypeNm = DDL_DST_MT.SelectedItem.Text.Replace(",", "");
                fundSwitching.Mode = 1;

                fundSwitching.ManageFundMovementComponentMT(db);


                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        private void UpdateCurrentSourceFund()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                FundSwitchingModels fundSwitching = new FundSwitchingModels();
                fundSwitching.CertificateNumber = int.Parse(TxtCertificate.Text);
                fundSwitching.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                fundSwitching.InvestmentTypeNumber = 0;
                fundSwitching.MoneyTypeNmbr = 0;
                fundSwitching.CurrentAsset = 0;
                fundSwitching.CompositionPercentage = 100;
                fundSwitching.TransactionAmount = float.Parse(TXT_SWITCH_AMT.Text.Replace(",", ""));
                fundSwitching.InvestmentTypeName = DDL_SRC_FUND.SelectedItem.Text.Replace(",", "");
                fundSwitching.MoneyTypeNm = DDL_SRC_MT.SelectedItem.Text.Replace(",", "");
                fundSwitching.Mode = 1;

                fundSwitching.ManageFundMovementComponentMT(db);


                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        private void LoadEstimationResult()
        {
            try
            {
                db.Open();
                DGR_ESTIMATION.DataSource = FundSwitchingModels.FillGvEstimation(db, int.Parse(TxtCertificate.Text), TXT_EFCTV_DT.Text);
                DGR_ESTIMATION.DataBind();
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

        private void InsertData()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                FundSwitchingModels fundSwitch = new FundSwitchingModels();
                fundSwitch.CertificateNumber = int.Parse(TxtCertificate.Text);
                fundSwitch.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                fundSwitch.InvestmentTypeNumber = int.Parse(DDL_SRC_FUND.SelectedValue);
                fundSwitch.InvestmentTypeNumberDst = int.Parse(DDL_DST_FUND.SelectedValue);
                fundSwitch.MoneyTypeNmbr = int.Parse(DDL_SRC_MT.SelectedValue);
                fundSwitch.TransactionAmount = float.Parse(TXT_SWITCH_AMT.Text);
                fundSwitch.BatchId = int.Parse(TXT_BATCHID.Text);

                fundSwitch.InsertFundMovementEstimationMT(db);

                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }



        protected void DGR_ESTIMATION_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex;
            int absoluteRow;
            GridViewRow row;
            if (e.CommandName.ToLower() == "apprv")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
                absoluteRow = Math.Abs((DGR_ESTIMATION.PageIndex * DGR_ESTIMATION.PageSize) - rowIndex);
                row = DGR_ESTIMATION.Rows[absoluteRow];

                string sUID = Session["session.iduser"].ToString();
                //query sp_fund_mvmnt_apprv_delete
                FundSwitchingModels fs = new FundSwitchingModels();
                fs.CertificateNumber = int.Parse(TxtCertificate.Text);
                fs.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                fs.FundSource = row.Cells[0].Text;
                fs.FundDestination = row.Cells[1].Text;
                fs.Mode = 1;
                fs.BatchId = int.Parse(TXT_BATCHID.Text);
                fs.FundMovementApprovalDelete(db);

                //usp_fpp_perkiraan mode 11
                FPP fpp = new FPP();
                fpp.SeqNmbr = int.Parse(TXT_BATCHID.Text);
                fpp.GroupNmbr = int.Parse(TxtCertificate.Text);
                fpp.ModeOfTransaction = 11;
                fpp.UserNm = sUID;
                fpp.ApprovalNm = "";
                fpp.UpdateFppPerkiraan(db);

                //usp_fpp_perkiraan mode 12
                fpp.ModeOfTransaction = 12;
                fpp.UpdateFppPerkiraan(db);

                //usp_fpp_perkiraan mode 13
                fpp.ModeOfTransaction = 13;
                fpp.UpdateFppPerkiraan(db);

                DGR_ESTIMATION.DataSource = FundSwitchingModels.FillGvEstimationMT(db, int.Parse(TxtCertificate.Text), TXT_EFCTV_DT.Text);
                DGR_ESTIMATION.DataBind();

                //show report viewer


            }
            else if (e.CommandName.ToLower() == "cancel")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
                absoluteRow = Math.Abs((DGR_ESTIMATION.PageIndex * DGR_ESTIMATION.PageSize) - rowIndex);
                row = DGR_ESTIMATION.Rows[absoluteRow];

                string sUID = Session["session.iduser"].ToString();
                //query sp_fund_mvmnt_apprv_delete
                FundSwitchingModels fs = new FundSwitchingModels();
                fs.CertificateNumber = int.Parse(TxtCertificate.Text);
                fs.EffectiveDate = DateTime.Parse(TXT_EFCTV_DT.Text);
                fs.FundSource = row.Cells[0].Text;
                fs.FundDestination = row.Cells[1].Text;
                fs.Mode = 2;
                fs.BatchId = int.Parse(TXT_BATCHID.Text);
                fs.FundMovementApprovalDelete(db);

                DGR_ESTIMATION.DataSource = FundSwitchingModels.FillGvEstimationMT(db, int.Parse(TxtCertificate.Text), TXT_EFCTV_DT.Text);
                DGR_ESTIMATION.DataBind();
            }
        }
    }
}