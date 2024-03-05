using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using AjaxControlToolkit;

namespace DPLKCORE.Form.Pension.UserControl.GroupTabs
{
    public partial class GroupBillingStatus : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDDL();
                if (Request.QueryString["state"] == "Edit")
                {
                    btnBillingUpdate.Style.Remove("display");
                    btnBilling.Style.Add("display", "none");
                }
                else if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    btnBilling.Style.Remove("display");
                    btnBillingUpdate.Style.Add("display", "none");
                }
                LoadRecord();
            }
        }

        private void LoadRecord()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);

            BillingStatusModels billing = new BillingStatusModels();
            if (Request.QueryString["state"] == "Edit")
            {
                billing.GroupNmbr = Convert.ToInt32(Session["GroupIdDetail"]);
            }
            else if (Request.QueryString["state"] == "NewGroupInfo")
            {
                billing.GroupNmbr = Convert.ToInt32(Session["NewGroup"]);
            }
            
            try
            {
                db.Open();
                if (billing.LoadData(db))
                {
                    txtNextFeePeriode.Text = Convert.ToDateTime(billing.NextBillPrd).ToString("yyyy-MM-dd");
                    txtNextFeeGenerateDt.Text = Convert.ToDateTime(billing.NextBillDt).ToString("yyyy-MM-dd");
                    txtNextContributionPrd.Text = Convert.ToDateTime(billing.NextCntrbPrd).ToString("yyyy-MM-dd");
                    txtNextContributionDt.Text = Convert.ToDateTime(billing.NextCntrbDt).ToString("yyyy-MM-dd");
                    ddlFreq.SelectedIndex = ddlFreq.Items.IndexOf(ddlFreq.Items.FindByText(billing.a));
                    ddlConFrq.SelectedIndex = ddlConFrq.Items.IndexOf(ddlConFrq.Items.FindByText(billing.b));
                    ddlPslFreq.SelectedIndex = ddlPslFreq.Items.IndexOf(ddlPslFreq.Items.FindByText(billing.c));
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

        protected void btnBIllingUpdate_Click(object sender, EventArgs e)
        {
            if (InsertBill())
            {
                //go to the new tab
                TabContainer groupContainer = locateTabContainer(Page);
                if (groupContainer != null)
                {
                    if (groupContainer.ActiveTabIndex < groupContainer.Tabs.Count - 1)
                    {
                        groupContainer.ActiveTabIndex += 1;
                    }
                }
            }
        }

        protected void btnBilling_Click(object sender, EventArgs e)
        {
            if (InsertBill())
            {
                //go to the new tab
                TabContainer groupContainer = locateTabContainer(Page);
                if (groupContainer != null)
                {
                    if (groupContainer.ActiveTabIndex < groupContainer.Tabs.Count - 1)
                    {
                        groupContainer.ActiveTabIndex += 1;
                    }
                }
            }
        }

        private bool InsertBill()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);

            try
            {
                db.Open();
                db.BeginTransaction();
                BillingStatusModels bill = new BillingStatusModels();

                if (Request.QueryString["state"] == "Edit")
                {
                    bill.GroupNmbr = Convert.ToInt32(Session["GroupIdDetail"]);
                }
                else
                {
                    bill.GroupNmbr = Convert.ToInt32(Session["NewGroup"]);
                }
                
                bill.NextBillPrd = DateTime.Parse(txtNextFeePeriode.Text);
                bill.NextBillDt = DateTime.Parse(txtNextFeeGenerateDt.Text);
                bill.BillFreqNmbr = int.Parse(ddlFreq.SelectedValue);

                bill.NextCntrbPrd = DateTime.Parse(txtNextContributionPrd.Text);
                bill.NextCntrbDt = DateTime.Parse(txtNextContributionDt.Text);
                bill.CntrbFreqNmbr = int.Parse(ddlConFrq.SelectedValue);

                bill.NextPslPrd = txtNextPslPeriod.Text == "" ? (DateTime?)null : DateTime.Parse(txtNextPslPeriod.Text);
                bill.NextPslDt = txtNextPslDate.Text == "" ? (DateTime?)null : DateTime.Parse(txtNextPslDate.Text);
                bill.PslFreqNmbr = ddlPslFreq.SelectedValue == "" ? (int?)null : int.Parse(ddlPslFreq.SelectedValue);

                if (bill.GroupNmbr == null)
                {
                    return false;
                }

                if (bill.InsertBillingStatus(db))
                {
                    db.CommitTransaction();
                }
                else
                {
                    return false;
                }
                return true;
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


        private void DDLFreq()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.DDLFrequency(ddlFreq, db);
            db.Close();

            db.Open();
            ddlHelper.DDLFrequency(ddlConFrq, db);
            db.Close();

            db.Open();
            ddlHelper.DDLFrequency(ddlPslFreq, db);
            db.Close();
        }

        private void fillDDL()
        {
            DDLFreq();
        }

        

        
    }
}