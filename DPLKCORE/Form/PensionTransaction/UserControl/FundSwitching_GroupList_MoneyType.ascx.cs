using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction.UserControl
{
    public partial class FundSwitching_GroupList_MoneyType : System.Web.UI.UserControl
    {
        Database db;
        Connection conn;

        protected void Page_Init(object sender, EventArgs e)
        {
            UCSearchPanel.BtnSearch.Click += OnButtonClick;
            UCSearchPanel.GvResult.PageIndexChanging += OnPageIndexChanging;
            UCSearchPanel.GvResult.RowCommand += OnRowCommand;
        }

        #region handling search function
        private void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                if (Session["searchResult"] != null)
                {
                    GridViewRow row = Session["searchResult"] as GridViewRow;
                    txtGroupNmbr.Text = row.Cells[1].Text;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);
            Session["searchType"] = 4;
            Session["searchCaller"] = "TXT_ID";
            if (!IsPostBack)
            {
                fillBatch(false);
            }
        }

        #region setup
        private void fillBatch(bool createNew)
        {
            try
            {
                db.Open();
                if (createNew == true)
                {
                    db.BeginTransaction();
                    FundSwitchingModels.UpdateLastBatchId(db);
                    db.CommitTransaction();
                }
                txtBatchId.Text = FundSwitchingModels.GetLatestBatchID(db);
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

        protected void btnNewBatch_Click(object sender, EventArgs e)
        {
            fillBatch(true);
        }
        #endregion

        protected void btnGo_Click(object sender, EventArgs e)
        {
            fillDDLS();
            //bind empty data to gridviewfund and gridviewestimation
            BindEmptyRecord();
            //fill all textboxes
            FillTextBoxes();
            //first save composition
            FirstSaveComposition();
        }

        #region first save composition
        private void FirstSaveComposition()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                for (int i = 0; i < GvFund.Rows.Count; i++)
                {
                    FSGroupListModels fsGroup = new FSGroupListModels();
                    fsGroup.GroupNmbr = int.Parse(txtGroupNmbr.Text);
                    fsGroup.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                    //fundSwitching.InvestmentTypeNumber = 0;
                    //fsGroup.MoneyTypeNmbr = 0
                    fsGroup.CurrentAsset = float.Parse(GvFund.Rows[i].Cells[2].Text.Replace(",", ""));
                    //fundSwitching.CompositionPercentage = 100;
                    //fundSwitching.TransactionAmount = 0;
                    fsGroup.InvestmentTypeName = GvFund.Rows[i].Cells[0].Text.Replace(",", "");
                    fsGroup.MoneyTypeNm = GvFund.Rows[i].Cells[1].Text.Replace(",", "");
                    //fundSwitching.Mode = 0;
                    fsGroup.FirstSaveCompositionMT(db);
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
        #endregion

        #region fill text boxes
        private void FillTextBoxes()
        {
            FillTxtCompany();
            FillTxtTransactionDt();
            FillGvFund();
        }

        private void FillGvFund()
        {
            try
            {
                db.Open();
                GvFund.DataSource = FSGroupListModels.FillGvFundMT(db, int.Parse(txtGroupNmbr.Text));
                GvFund.DataBind();
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

        private void FillTxtTransactionDt()
        {
            try
            {
                db.Open();
                txtTransactionDt.Text = Convert.ToDateTime(FSGroupListModels.GetTransactionDt(db)).ToString("yyyy-MM-dd");
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

        private void FillTxtCompany()
        {
            try
            {
                db.Open();
                txtCompanyNm.Text = FSGroupListModels.GetCompanyNmById(db, int.Parse(txtGroupNmbr.Text));
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
        #endregion

        #region bind empty record
        private void BindEmptyRecord()
        {
            BindEmptyGvFund();
            BindEmptyGvEst();
        }

        private void BindEmptyGvEst()
        {
            try
            {
                db.Open();
                GvEst.DataSource = FundSwitchingModels.BindEmptyGvEstMT(db);
                GvEst.DataBind();
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

        private void BindEmptyGvFund()
        {
            try
            {
                db.Open();
                GvFund.DataSource = FundSwitchingModels.BindEmptyGvFundMT(db);
                GvFund.DataBind();
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

        #endregion  

        #region fill DDLs
        private void fillDDLS()
        {
            //ddl source fund
            FillDDLSourceFund();
            FillDDLSourceMT();

            //ddl dst fund
            FillDDLDestFund();
            FIllDDLDestMT();

            //ddl method (not using query)
            FillDDLMethod();
        }

        private void FIllDDLDestMT()
        {
            try
            {
                db.Open();
                DDLFundSwitching ddlHelper = new DDLFundSwitching();
                ddlHelper.LoadDDLMTGroup(db, ddlDestinationMT);
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
                ddlHelper.LoadDDLMTGroup(db, ddlSourceMT);
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

        private void FillDDLMethod()
        {
            try
            {
                DDLFundSwitching ddlHelper = new DDLFundSwitching();
                ddlHelper.LoadDDLMode(ddlSwitchingMethod);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void FillDDLDestFund()
        {
            try
            {
                db.Open();
                DDLFundSwitching ddlHelper = new DDLFundSwitching();
                ddlHelper.GroupNmbr = int.Parse(txtGroupNmbr.Text);
                ddlHelper.LoadDDLDestFundGroup(db, ddlDestinationFund);
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
                ddlHelper.GroupNmbr = int.Parse(txtGroupNmbr.Text);
                ddlHelper.LoadDDLSourceFundGroup(db, ddlSourceFund);
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
        #endregion  

        #region calculate ammount
        protected void btnCalculation_Click(object sender, EventArgs e)
        {
            CalculateAmmount();
        }

        private void CalculateAmmount()
        {
            try
            {
                db.Open();
                FSGroupListModels fsGroup = new FSGroupListModels();
                fsGroup.GroupNmbr = int.Parse(txtGroupNmbr.Text);
                fsGroup.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                fsGroup.InvestmentTypeNumber = int.Parse(ddlSourceFund.SelectedValue);
                fsGroup.MoneyTypeNmbr = int.Parse(ddlSourceMT.SelectedValue);
                fsGroup.InvestmentTypeNumberDst = int.Parse(ddlDestinationFund.SelectedValue);
                fsGroup.Mode = int.Parse(ddlSwitchingMethod.SelectedValue);
                fsGroup.TransactionAmount = float.Parse(txtSwitchingValue.Text.Replace(",", ""));

                txtAmtToProcess.Text = fsGroup.GetCalculationMT(db);
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
        #endregion

        #region insertion and save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            InsertData();

            //load gvestimation
            LoadEstimationResult();

            //update for current source existing fund
            UpdateCurrentSourceFund();

            //update for current destination existing fund
            UpdateCurrentDestinationFund();

            //popup with message data has been saved

            txtAmtToProcess.Text = "";
            txtSwitchingValue.Text = "";
        }

        private void UpdateCurrentDestinationFund()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                FSGroupListModels fsGroup = new FSGroupListModels();
                fsGroup.GroupNmbr = int.Parse(txtGroupNmbr.Text);
                fsGroup.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                fsGroup.InvestmentTypeNumber = 0;
                fsGroup.MoneyTypeNmbr = 0;
                fsGroup.CurrentAsset = 0;
                fsGroup.CompositionPercentage = 100;
                fsGroup.TransactionAmount = float.Parse(txtAmtToProcess.Text.Replace(",", ""));
                fsGroup.InvestmentTypeName = ddlDestinationFund.SelectedItem.Text.Replace(",", "");
                fsGroup.MoneyTypeNm = ddlDestinationMT.SelectedItem.Text.Replace(",", "");
                fsGroup.Mode = 1;

                fsGroup.ManageFundMovementComponentMT(db);


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
                FSGroupListModels fsGroup = new FSGroupListModels();
                fsGroup.GroupNmbr = int.Parse(txtGroupNmbr.Text);
                fsGroup.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                fsGroup.InvestmentTypeNumber = 0;
                fsGroup.MoneyTypeNmbr = 0;
                fsGroup.CurrentAsset = 0;
                fsGroup.CompositionPercentage = 100;
                fsGroup.TransactionAmount = float.Parse(txtAmtToProcess.Text.Replace(",", ""));
                fsGroup.InvestmentTypeName = ddlSourceFund.SelectedItem.Text.Replace(",", "");
                fsGroup.MoneyTypeNm = ddlSourceMT.SelectedItem.Text.Replace(",", "");
                fsGroup.Mode = 1;

                fsGroup.ManageFundMovementComponentMT(db);


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
                GvEst.DataSource = FSGroupListModels.FillGvEstimationMT(db, int.Parse(txtGroupNmbr.Text), txtTransactionDt.Text);
                GvEst.DataBind();
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
                FSGroupListModels fsGroup = new FSGroupListModels();
                fsGroup.GroupNmbr = int.Parse(txtGroupNmbr.Text);
                fsGroup.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                fsGroup.InvestmentTypeNumber = int.Parse(ddlSourceFund.SelectedItem.Text);
                fsGroup.InvestmentTypeNumberDst = int.Parse(ddlDestinationFund.SelectedItem.Text);
                fsGroup.MoneyTypeNmbr = int.Parse(ddlDestinationMT.SelectedItem.Text);
                fsGroup.TransactionAmount = float.Parse(txtAmtToProcess.Text);
                fsGroup.BatchId = int.Parse(txtBatchId.Text);

                fsGroup.InsertFundMovementEstimationMT(db);

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
        #endregion        
    }
}