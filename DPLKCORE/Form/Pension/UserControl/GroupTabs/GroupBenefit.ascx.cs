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
    public partial class GroupBenefit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDDL();
                if (Request.QueryString["state"] == "Edit")
                {
                    LoadData();
                    btnUpdateBenefit.Style.Remove("display");
                    btnSaveBenefit.Style.Add("display", "none");
                }
                else if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    btnSaveBenefit.Style.Remove("display");
                    btnUpdateBenefit.Style.Add("display", "none");
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
                BenefitGroup benefit = new BenefitGroup();
                Dictionary<string,object> result = benefit.LoadData(db, Convert.ToInt32(Session["GroupIdDetail"]));


                if (result.Count > 0)
                {
                    if (result["mcp_nm"].ToString() != string.Empty)
                    {
                        ddlMCPType.SelectedIndex = ddlMCPType.Items.IndexOf(ddlMCPType.Items.FindByText(result["mcp_nm"].ToString()));
                    }

                    if (result["bene_type_nmbr"].ToString() != string.Empty)
                    {
                        ddlBenefitNm.SelectedIndex = ddlBenefitNm.Items.IndexOf(ddlBenefitNm.Items.FindByText(result["bene_type_nmbr"].ToString()));
                    }

                    if (result["si_calc_type_nm"].ToString() != string.Empty)
                    {
                        ddlSumMethod.SelectedIndex = ddlSumMethod.Items.IndexOf(ddlSumMethod.Items.FindByText(result["si_calc_type_nm"].ToString()));
                    }

                    if (result["default_sum_insured"].ToString() != string.Empty)
                    {
                        txtSumAmmount.Text = result["default_sum_insured"].ToString();
                    }

                    if (result["max_sum_insured"].ToString() != string.Empty)
                    {
                        txtMaxAmmount.Text = result["max_sum_insured"].ToString();
                    }

                    if (result["coi_discont_flg"].ToString() != string.Empty)
                    {
                        ddlDiscCoi.SelectedIndex = ddlDiscCoi.Items.IndexOf(ddlDiscCoi.Items.FindByText(result["coi_discont_flg"].ToString()));
                    }

                    if (result["coi_discont_value"].ToString() != string.Empty)
                    {
                        txtDiscPct.Text = result["coi_discont_value"].ToString();
                    }

                    if (result["coi_loading_flg"].ToString() != string.Empty)
                    {
                        ddlLoadCoi.SelectedIndex = ddlLoadCoi.Items.IndexOf(ddlLoadCoi.Items.FindByText(result["coi_loading_flg"].ToString()));
                    }

                    if (result["coi_loading_value"].ToString() != string.Empty)
                    {
                        txtLoadPct.Text = result["coi_loading_value"].ToString();
                    }

                    if (result["rate_type_nm"].ToString() != string.Empty)
                    {
                        ddlCoiRate.SelectedIndex = ddlCoiRate.Items.IndexOf(ddlCoiRate.Items.FindByText(result["rate_type_nm"].ToString()));
                    }

                    if (result["efctv_dt"].ToString() != string.Empty)
                    {
                        txtEffectiveDt.Text = Convert.ToDateTime(result["efctv_dt"].ToString()).ToString("yyyy-MM-dd");
                    }

                    if (result["max_entry_age"].ToString() != string.Empty)
                    {
                        txtMaxEntryAge.Text = result["max_entry_age"].ToString();
                    }

                    if (result["max_cov_age"].ToString() != string.Empty)
                    {
                        txtMaxCovAge.Text = result["max_cov_age"].ToString();
                    }

                    if (result["sub_trns_nm"].ToString() != string.Empty)
                    {
                        ddlClaimTypeAddBenefit.SelectedIndex = ddlClaimTypeAddBenefit.Items.IndexOf(ddlClaimTypeAddBenefit.Items.FindByText(result["sub_trns_nm"].ToString()));
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


        //insert and update use the same SP. the SP will handle the if-exist validation. 
        private bool InsertGroupBenefit()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                BenefitGroup benefit = new BenefitGroup();
                db.Open();
                db.BeginTransaction();
                if (Request.QueryString["state"] == "Edit")
                {
                    benefit.GroupNmbr = Convert.ToInt32(Session["GroupIdDetail"]);
                }
                else
                {
                    benefit.GroupNmbr = Convert.ToInt32(Session["NewGroup"]);
                }
                
                benefit.McpTypeNmbr = int.Parse(ddlMCPType.SelectedValue);
                benefit.BeneTypeNmbr = int.Parse(ddlBenefitNm.SelectedValue);
                benefit.SiCalcTypeNmbr = int.Parse(ddlSumMethod.SelectedValue);
                benefit.DefaultSumInsured = double.Parse(txtSumAmmount.Text);
                benefit.MaxSumInsured = double.Parse(txtMaxAmmount.Text);
                benefit.CoiDiscontFlg = int.Parse(ddlDiscCoi.SelectedValue) == 0 ? false : true;
                benefit.CoiDiscontValue = double.Parse(txtDiscPct.Text);
                benefit.CoiLoadingFlg = int.Parse(ddlLoadCoi.SelectedValue) == 0 ? false : true;
                benefit.CoiLoadingValue = double.Parse(txtLoadPct.Text);
                benefit.MaxEntryAge = int.Parse(txtMaxEntryAge.Text);
                benefit.MaxCovAge = int.Parse(txtMaxCovAge.Text);
                benefit.CoiTypeNmbr = int.Parse(ddlCoiRate.SelectedValue);
                benefit.ChangeDt = DateTime.Parse(txtEffectiveDt.Text);
                benefit.SubTrnsNmbr = int.Parse(ddlClaimTypeAddBenefit.SelectedValue);

                if (benefit.InsertOrUpdateGroupBenefit(db))
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
                return false;
            }
            finally
            {
                db.Close();
            }
        }


        protected void btnUpdateBenefit_Click(object sender, EventArgs e)
        {
            if (InsertGroupBenefit())
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

        protected void btnSaveBenefit_Click(object sender, EventArgs e)
        {
            if (InsertGroupBenefit())
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

        private void LoadAdditionalBenefit()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            ddlHelper.DDLAdditionalBenefit(ddlClaimTypeAddBenefit, db);
            db.Close();
        }

        private void LoadCOIRate()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            ddlHelper.DDLCOIRate(ddlCoiRate, db);
            db.Close();
        }

        private void LoadCOI()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            ddlHelper.DDLLoadOption(ddlLoadCoi, db);
            db.Close();
        }

        private void LoadDDLDiscountCOI()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            ddlHelper.DDLLoadOption(ddlDiscCoi, db);
            db.Close();
        }

        private void LoadDDLSumMethod()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            ddlHelper.DDLSumMethod(ddlSumMethod, db);
            db.Close();
        }

        private void LoadDDLBenefitName()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            ddlHelper.DDLBenefit(ddlBenefitNm, db);
            db.Close();
        }

        private void LoadDDLMcpType()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();
            db.Open();
            if (Request.QueryString["state"] == "Edit")
            {
               ddlHelper.DDLMcpGroupNmbr(ddlMCPType, db, Convert.ToInt32(Session["GroupIdDetail"]));
            }
            else
            {
                ddlHelper.DDLMcpGroupNmbr(ddlMCPType, db, Convert.ToInt32(Session["NewGroup"]));
            }
            
            db.Close();
        }

        public void fillDDL()
        {
            LoadDDLMcpType();
            LoadDDLBenefitName();
            LoadDDLSumMethod();
            LoadDDLDiscountCOI();
            LoadCOI();
            LoadCOIRate();
            LoadAdditionalBenefit();
        }

        

        
    }
}