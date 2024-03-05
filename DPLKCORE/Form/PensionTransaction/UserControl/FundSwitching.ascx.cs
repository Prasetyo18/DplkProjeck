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
    public partial class FundSwitching : System.Web.UI.UserControl
    {
        Database db;
        Connection conn;

        protected void Page_Init(Object sender, EventArgs e)
        {
            UCSearchPanel.BtnSearch.Click += OnButtonClick;
            UCSearchPanel.GvResult.PageIndexChanging += OnPageIndexChanging;
            UCSearchPanel.GvResult.RowCommand += OnRowCommand;

        }

        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                if (Session["searchResult"] != null)
                {
                    GridViewRow row = Session["searchResult"] as GridViewRow;
                    txtCerNmbr.Text = row.Cells[1].Text;
                    searchModal.Hide();
                }
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                searchModal.Show();
            }
        }

        protected void OnButtonClick(object sender, EventArgs e)
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

            Session["searchType"] = 2;
            Session["searchCaller"] = "TXT_ID";
            if (!IsPostBack)
            {
                //setup batch text. load with the latest batch. 
                fillBatch(false);
            }
        }

        #region setup
        protected void btnNewBatch_Click(object sender, EventArgs e)
        {
            fillBatch(true);
        }

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        

        protected void btnGo_Click(object sender, EventArgs e)
        {
            //fill all DDLs
            fillDDLS();
            //bind empty data to gridviewfund and gridviewestimation
            BindEmptyRecord();
            //fill all textboxes
            FillTextBoxes();
            //first save composition
            FirstSaveComposition();
            //fill the gridviewestimation
            FillGvEstimation();
        }

        protected void btnCalculation_Click(object sender, EventArgs e)
        {
            CalculateAmmount();
        }

        private void CalculateAmmount()
        {
            try
            {
                db.Open();
                FundSwitchingModels fundSwitch = new FundSwitchingModels();
                fundSwitch.CertificateNumber = int.Parse(txtCerNmbr.Text);
                fundSwitch.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                fundSwitch.InvestmentTypeNumber = int.Parse(ddlSourceFund.SelectedValue);
                fundSwitch.InvestmentTypeNumberDst = int.Parse(ddlDestinationFund.SelectedValue);
                fundSwitch.Mode = int.Parse(ddlSwitchingMethod.SelectedValue);
                fundSwitch.TransactionAmount = float.Parse(txtSwitchingValue.Text.Replace(",",""));

                txtAmtToProcess.Text = fundSwitch.GetCalculation(db);                
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

        private void FillGvEstimation()
        {
            try
            {
                db.Open();
                GvEst.DataSource = FundSwitchingModels.FillGvEstimation(db, int.Parse(txtCerNmbr.Text), txtTransactionDt.Text);
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



        private void FirstSaveComposition()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                for (int i = 0; i < GvFund.Rows.Count; i++)
                {
                    FundSwitchingModels fundSwitching = new FundSwitchingModels();
                    fundSwitching.CertificateNumber = int.Parse(txtCerNmbr.Text);
                    fundSwitching.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                    //fundSwitching.InvestmentTypeNumber = 0;
                    fundSwitching.CurrentAsset = float.Parse(GvFund.Rows[i].Cells[1].Text.Replace(",",""));
                    //fundSwitching.CompositionPercentage = 100;
                    //fundSwitching.TransactionAmount = 0;
                    fundSwitching.InvestmentTypeName = GvFund.Rows[i].Cells[0].Text.Replace(",","");
                    //fundSwitching.Mode = 0;
                    fundSwitching.FirstSaveComposition(db);
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

        #region fill textboxes
        private void FillTextBoxes()
        {
            FillTxtClient();
            FillTxtTransactionDt();
            FillGvFund();
        }

        private void FillGvFund()
        {
            try
            {
                db.Open();
                GvFund.DataSource = FundSwitchingModels.FillGvFund(db, int.Parse(txtCerNmbr.Text), txtTransactionDt.Text);
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
                txtTransactionDt.Text = Convert.ToDateTime(FundSwitchingModels.GetTransactionDt(db)).ToString("yyyy-MM-dd");
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

        private void FillTxtClient()
        {
            try
            {
                db.Open();
                txtClientNm.Text = FundSwitchingModels.GetClientNamebyID(db, int.Parse(txtCerNmbr.Text));
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
                GvFund.DataSource = FundSwitchingModels.BindEmptyGvFund(db);
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

        private void BindEmptyGvFund()
        {
            try
            {
                db.Open();
                GvEst.DataSource = FundSwitchingModels.BindEmptyGvEst(db);
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
        #endregion

        #region fill ddl
        private void fillDDLS()
        {
           //ddl source fund
            FillDDLSourceFund();

            //ddl dst fund
            FillDDLDestFund();

            //ddl method (not using query)
            FillDDLMethod();
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
                ddlHelper.CerNmbr = int.Parse(txtCerNmbr.Text);
                ddlHelper.LoadDDLDestFund(db, ddlDestinationFund);
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
                ddlHelper.CerNmbr = int.Parse(txtCerNmbr.Text);
                ddlHelper.LoadDDLSourceFund(db, ddlSourceFund);
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

        #region Save Insertion
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveProcess();
        }

        private void SaveProcess()
        {
            //insert
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
                FundSwitchingModels fundSwitching = new FundSwitchingModels();
                fundSwitching.CertificateNumber = int.Parse(txtCerNmbr.Text);
                fundSwitching.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                fundSwitching.InvestmentTypeNumber = 0;
                fundSwitching.CurrentAsset = 0;
                fundSwitching.CompositionPercentage = 100;
                fundSwitching.TransactionAmount = float.Parse(txtAmtToProcess.Text.Replace(",", ""));
                fundSwitching.InvestmentTypeName = ddlDestinationFund.SelectedItem.Text.Replace(",", "");
                fundSwitching.Mode = 1;

                fundSwitching.ManageFundMovementComponent(db);


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
                fundSwitching.CertificateNumber = int.Parse(txtCerNmbr.Text);
                fundSwitching.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                fundSwitching.InvestmentTypeNumber = 0;
                fundSwitching.CurrentAsset = 0;
                fundSwitching.CompositionPercentage = 100;
                fundSwitching.TransactionAmount = float.Parse(txtAmtToProcess.Text.Replace(",",""));
                fundSwitching.InvestmentTypeName = ddlSourceFund.SelectedItem.Text.Replace(",","");
                fundSwitching.Mode = 1;

                fundSwitching.ManageFundMovementComponent(db);


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
                GvEst.DataSource = FundSwitchingModels.FillGvEstimation(db, int.Parse(txtCerNmbr.Text), txtTransactionDt.Text);
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
                FundSwitchingModels fundSwitch = new FundSwitchingModels();
                fundSwitch.CertificateNumber = int.Parse(txtCerNmbr.Text);
                fundSwitch.EffectiveDate = DateTime.Parse(txtTransactionDt.Text);
                fundSwitch.InvestmentTypeNumber = int.Parse(ddlSourceFund.SelectedValue);
                fundSwitch.InvestmentTypeNumberDst = int.Parse(ddlDestinationFund.SelectedValue);
                fundSwitch.TransactionAmount = float.Parse(txtAmtToProcess.Text);
                fundSwitch.BatchId = int.Parse(txtBatchId.Text);

                fundSwitch.InsertFundMovementEstimation(db);

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