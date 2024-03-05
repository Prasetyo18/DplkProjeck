using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using AjaxControlToolkit;
using System.Data;

namespace DPLKCORE.Form.Pension.UserControl.GroupTabs
{
    public partial class GroupCharge : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDDL();
                fillGrid();

                ddlChargeRate.Visible = false;
                ddlChargeRate.Enabled = false;

                if (Request.QueryString["state"] == "NewGroupInfo")
                {

                }
                else if (Request.QueryString["state"] == "Edit")
                {

                }
            }
        }

        private void fillGrid()
        {
            try
            {
                Connection conn = new Connection();
                Database db = new Database(conn.ConnectionStringPension);
                GroupChargeModels gc = new GroupChargeModels();
                gc.GroupNmbr = Convert.ToInt32(Session["groupIdDetail"]);
                gc.ChargeTypeNmbr = int.Parse(ddlChargeType.SelectedValue);

                GV_SUMMARY.DataSource =  gc.GetAllData(db);
                GV_SUMMARY.DataBind();

                GV_NEWDATA.DataSource = gc.GetSummaryData(db);
                GV_NEWDATA.DataBind();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void InsertGroupCharge()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                //check the value before insert
                //validate();

                GroupChargeModels gc = new GroupChargeModels();
                if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    gc.GroupNmbr = Convert.ToInt32(Session["newGroup"]);
                }
                else if (Request.QueryString["state"] == "Edit")
                {
                    gc.GroupNmbr = Convert.ToInt32(Session["groupIdDetail"]);
                }
                
                gc.ChargeTypeNmbr = int.Parse(ddlChargeType.SelectedValue);
                gc.ChargeEfctvDt = DateTime.Parse(txtEffecDt.Text);
                if (txtNextChgDt.Text != String.Empty)
                {
                    gc.NextChargeDt = DateTime.Parse(txtNextChgDt.Text);
                }

                if (txtChrgTerDt.Text != String.Empty)
                {
                    gc.ChargeTrmntnDt = DateTime.Parse(txtChrgTerDt.Text);
                }
                gc.PayRspnNmbr = int.Parse(ddlPaymentRes.SelectedValue);
                gc.FreqTypeNmbr = int.Parse(ddlFreq.SelectedValue);

                if (ckChargeRate.Checked)
                {
                    gc.chargeRate = float.Parse(ddlChargeRate.SelectedValue);
                }
                else
                {
                    gc.chargeRate = float.Parse(txtChargeRate.Text);
                }

                gc.maxCnt = short.Parse(txtMaxFreqYear.Text);
                gc.freeCnt = short.Parse(txtMaxFeeFreqPerYear.Text);
                gc.chargeAmount = float.Parse(txtChargeAmmout.Text);
                gc.billPercent = float.Parse(txtBilPer.Text);
                gc.deductPercent = float.Parse(txtDeductPer.Text);
                gc.maxChargeAmmount = float.Parse(txtMaxChargeAmt.Text);

                if (gc.GroupNmbr != 0)
                {
                    DataTable data = gc.InsertGroupCharge(db);
                    if (data.Rows.Count > 0)
                    {
                        //insert group rt
                        gc.InsertGroupChargeRt(db);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        protected void btnGroupCharge_Click(object sender, EventArgs e)
        {
            //insert group charge
            InsertGroupCharge();

            //move to next tab if in state new group info
            if (Request.QueryString["state"] == "NewGroupInfo")
            {
                TabContainer groupContainer = locateTabContainer(Page);
                if (groupContainer != null)
                {
                    if (groupContainer.ActiveTabIndex < groupContainer.Tabs.Count - 1)
                    {
                        groupContainer.ActiveTabIndex += 1;
                    }
                }
            }
            fillGrid();
        }

        private TabContainer locateTabContainer(Control parent)
        {
            foreach (Control item in parent.Controls)
            {
                if (item is TabContainer)
                {
                    return (TabContainer)item;
                }

                if (item.HasControls())
                {
                    TabContainer foundContainer = locateTabContainer(item);
                    if (foundContainer != null)
                    {
                        return foundContainer;
                    }
                }
            }
            return null;
        }


        private void fillDDL()
        {
            LoadDDLChargeType();
            LoadDDLPaymentRes();
            LoadDDLFrequency();
        }

        private void LoadDDLChargeRate()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.DDLChargeRate(ddlChargeRate, db);
            db.Close();
        }

        private void LoadDDLFrequency()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.DDLFrequency(ddlFreq, db);
            db.Close();
        }

        private void LoadDDLChargeType()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.DDLChargeType(ddlChargeType, db);
            db.Close();
        }

        private void LoadDDLPaymentRes()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.DDLPaymentRes(ddlPaymentRes, db);
            db.Close();
        }

        protected void ckChargeRate_CheckedChanged(object sender, EventArgs e)
        {
            if (ckChargeRate.Checked)
            {
                txtChargeRate.Enabled = false;
                txtChargeRate.Visible = false;
                ddlChargeRate.Visible = true;
                ddlChargeRate.Enabled = true;
                LoadDDLChargeRate();
            }
            else
            {
                txtChargeRate.Enabled = true;
                txtChargeRate.Visible = true;
                ddlChargeRate.Visible = false;
                ddlChargeRate.Enabled = false;
                ddlChargeRate.Items.Clear();
            }
        }

        protected void btnGroupChargeUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void ddlChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGrid();
            txtChargeRate.Enabled = true;
            txtChargeRate.Visible = true;
            ddlChargeRate.Visible = false;
            ddlChargeRate.Enabled = false;
            ddlChargeRate.Items.Clear();
            ckChargeRate.Checked = false;
        }


    }
}