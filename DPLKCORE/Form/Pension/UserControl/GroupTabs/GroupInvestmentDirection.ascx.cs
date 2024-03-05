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
    public partial class GroupInvestmentDirection : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDDL();
                if (Request.QueryString["state"] == "Edit")
                {
                    btnInvestUpdate.Style.Remove("display");
                    btnInvest.Style.Add("display", "none");
                    LoadData();
                }
                else if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    btnInvestUpdate.Style.Add("display", "none");
                    btnInvest.Style.Remove("display");
                }
            }
        }

        private void LoadData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                InvestmentDirectionModels invDir = new InvestmentDirectionModels();
                Dictionary<string, object> result = invDir.LoadData(db, Convert.ToInt32(Session["groupIdDetail"]));

                if (result.Count > 0)
                {
                    if (result["inv_type_nm"].ToString() != string.Empty)
                    {
                        ddlInvestOption.SelectedIndex = ddlInvestOption.Items.IndexOf(ddlInvestOption.Items.FindByText(result["inv_type_nm"].ToString()));
                    }

                    if (result["pay_rspn_nm"].ToString() != string.Empty)
                    {
                        ddlPaymentRes.SelectedIndex = ddlPaymentRes.Items.IndexOf(ddlPaymentRes.Items.FindByText(result["pay_rspn_nm"].ToString()));
                    }

                    if (result["freq_type_nm"].ToString() != string.Empty)
                    {
                        ddlChargeFreq.SelectedIndex = ddlChargeFreq.Items.IndexOf(ddlChargeFreq.Items.FindByText(result["freq_type_nm"].ToString()));
                    }

                    if (result["next_charge_dt"].ToString() != string.Empty)
                    {
                        txtNextChargeDt.Text = Convert.ToDateTime(result["next_charge_dt"]).ToString("yyyy-MM-dd");
                    }

                    if (result["bill_pct"].ToString() != string.Empty)
                    {
                        txtBilled.Text = result["bill_pct"].ToString();
                    }

                    if (result["deduct_pct"].ToString() != string.Empty)
                    {
                        txtDeducated.Text = result["deduct_pct"].ToString();
                    }

                    if (result["charge_rt"].ToString() != string.Empty)
                    {
                        txtChargeRate.Text = result["charge_rt"].ToString();
                    }

                    if (result["charge_amt"].ToString() != string.Empty)
                    {
                        txtChargeAmt.Text = result["charge_amt"].ToString();
                    }

                    if (result["max_percent"].ToString() != string.Empty)
                    {
                        txtMaxInvChsdPerc.Text = result["max_percent"].ToString();
                    }

                    if (result["min_percent"].ToString() != string.Empty)
                    {
                        txtMinInvChsdPerc.Text = result["min_percent"].ToString();
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

        protected void btnInvest_Click(object sender, EventArgs e)
        {
            if (InsertInvestDirection())
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
            
        }

        protected void btnInvestUpdate_Click(object sender, EventArgs e)
        {
            if (InsertInvestDirection())
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
        }

        private bool InsertInvestDirection()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                db.BeginTransaction();
                InvestmentDirectionModels invDir = new InvestmentDirectionModels();
                if (Request.QueryString["state"] == "Edit")
                {
                    invDir.GroupNmbr = Convert.ToInt32(Session["groupIdDetail"]);
                }

                else if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    invDir.GroupNmbr = Convert.ToInt32(Session["newGroup"]);
                }

                invDir.InvTypeNmbr = short.Parse(ddlInvestOption.SelectedValue);
                invDir.MaxPercent = float.Parse(txtMaxInvChsdPerc.Text);
                invDir.MinPercent = float.Parse(txtMinInvChsdPerc.Text);
                invDir.ChargeTypeNmbr = 160;

                if (txtNextChargeDt.Text == String.Empty)
                {
                    invDir.ChargeEffectiveDt = DateTime.Now;
                }
                else
                {
                    invDir.ChargeEffectiveDt = DateTime.Parse(txtNextChargeDt.Text);
                }

                if (txtNextChargeDt.Text == String.Empty)
                {
                    invDir.NextChargeDt = DateTime.Now;
                }
                else
                {
                    invDir.NextChargeDt = DateTime.Parse(txtNextChargeDt.Text);
                }
                
                invDir.PaymentResNmbr = int.Parse(ddlPaymentRes.SelectedValue);
                invDir.FreqTypeNmbr = int.Parse(ddlChargeFreq.SelectedValue);
                invDir.BillPct = float.Parse(txtBilled.Text);
                invDir.DeducPct = float.Parse(txtDeducated.Text);
                invDir.ChargeAmmount = float.Parse(txtChargeAmt.Text);
                invDir.ChargeRate = float.Parse(txtChargeRate.Text);

                invDir.InsertAll(db);

                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                return false;
            }
            finally
            {
                db.Close();
            }

            return true;
        }


        private void LoadDDLChargeFreq()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            ddlHelper.DDLFrequency(ddlChargeFreq, db);
            db.Close();
        }

        private void LoadDDLInvestOption()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            ddlHelper.DDLInvestOpt(ddlInvestOption, db);
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

        private void fillDDL()
        {
            LoadDDLPaymentRes();
            LoadDDLInvestOption();
            LoadDDLChargeFreq();
        }

        

        
    }
}