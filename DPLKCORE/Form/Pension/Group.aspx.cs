using System;
using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Logic.Pension;

namespace DPLKCORE.Form.Pension
{
    public partial class Group : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnGroup.Click += ButtonClicked;
            btnUpdateGroupInfo.Click += ButtonClicked;
            btnCancel.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {          

            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                fillDDL();
                ddlProductType.SelectedIndex = 0;
                ProductSetup();

                if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    //activate insert button
                    btnGroup.Enabled = true;
                    btnGroup.Visible = true;

                    txtOldGrbNmbr.Style.Add("display", "none");
                    lbOldGroup.Style.Add("display", "none");

                    //make sure ddlcompany enabled
                    ddlCompany.Enabled = true;

                    //disabled btn update
                    btnUpdateGroupInfo.Enabled = false;
                    btnUpdateGroupInfo.Style.Add("display", "none");

                    txtGroupNumber.Text = "new";
                    txtGroupNumber.Enabled = false;
                    txtOldGrbNmbr.Text = null;
                    txtOldGrbNmbr.Enabled = false;
                    HideTabs();
                }
                else if (Request.QueryString["state"] == "Edit")
                {
                    //hide insert button
                    btnGroup.Enabled = false;
                    btnGroup.Style.Add("display", "none");

                    txtOldGrbNmbr.Enabled = false;

                    txtGroupNumber.Enabled = false;

                    ddlCompany.Enabled = false;

                    //activate update button
                    btnUpdateGroupInfo.Enabled = true;
                    btnUpdateGroupInfo.Style.Remove("display");

                    txtGroupNumber.Text = Session["groupIdDetail"].ToString();
                    ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(Session["companyIdDetail"].ToString()));
                    LoadRecord();
                }
            }
        }

        private void LoadRecord()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                
                GroupInfoModels groupInfo = new GroupInfoModels();
                Dictionary<string, object> recordResult = groupInfo.LoadByGroupId(db, int.Parse(txtGroupNumber.Text));

                if (recordResult.Count > 0)
                {
                    //access and load data if the value is not null
                    if (recordResult["client_nmbr"].ToString() != string.Empty)
                    {
                        ddlCompany.Text = recordResult["client_nmbr"].ToString();
                    }
                
                    if (recordResult["product_type"].ToString() != string.Empty)
                    {
                        ddlProductType.SelectedValue = recordResult["product_type"].ToString();
                    }
              
                    if (recordResult["allow_with_nmbr"].ToString() != string.Empty)
                    {
                        ddlAllow.SelectedValue = recordResult["allow_with_nmbr"].ToString();
                    }
              
                    if (recordResult["with_src_type_nmbr"].ToString() != string.Empty)
                    {
                        ddlWdSrc.SelectedValue = recordResult["with_src_type_nmbr"].ToString();
                    }
                
                    if (recordResult["psl_payment_freq"].ToString() != string.Empty)
                    {
                        ddlPslFreq.SelectedValue = recordResult["psl_payment_freq"].ToString();
                    }
                 
                    if (recordResult["premium_mtd_type"].ToString() != string.Empty)
                    {
                        ddlPremium.SelectedValue = recordResult["premium_mtd_type"].ToString();
                    }
               
                    if (recordResult["maturity_type_nmbr"].ToString() != string.Empty)
                    {
                        ddlMaturityTypeNmbr.SelectedValue = recordResult["maturity_type_nmbr"].ToString();
                    }
                 
                    if (recordResult["accbal_freq_nmbr"].ToString() != string.Empty)
                    {
                        ddlAccBalFreq.SelectedValue = recordResult["accbal_freq_nmbr"].ToString();
                    }
                   
                    if (recordResult["psl_type"].ToString() != string.Empty)
                    {
                        ddlPlsType.SelectedValue = recordResult["psl_type"].ToString();
                    }
                 
                    if (recordResult["support_uu1992"].ToString() != string.Empty)
                    {
                        ddlUU.SelectedValue = recordResult["support_uu1992"].ToString();
                    }
                  
                    if (recordResult["ind_grp_cd"].ToString() != string.Empty)
                    {
                        ddlGroupt.SelectedValue = recordResult["ind_grp_cd"].ToString();
                    }
                 
                    if (recordResult["incl_contrib_flg"].ToString() != string.Empty)
                    {
                        ddlIncContriFlg.SelectedValue = recordResult["incl_contrib_flg"].ToString();
                    }
                
                    if (recordResult["have_psl"].ToString() != string.Empty)
                    {
                        ddlPsl.SelectedValue = recordResult["have_psl"].ToString();
                    }
                  
                    if (recordResult["pooled_flg"].ToString() != string.Empty)
                    {
                        ddlAlcFund.SelectedValue = recordResult["pooled_flg"].ToString();
                    }
                    
                    if (recordResult["affiliatedto"].ToString() != string.Empty)
                    {
                        ddlAffliatedTo.SelectedValue = recordResult["affiliatedto"].ToString();
                    }
                
                    if (recordResult["va_currency_nmbr"].ToString() != string.Empty)
                    {
                        ddlCry.SelectedValue = recordResult["va_currency_nmbr"].ToString();
                    }
               
                    if (recordResult["va_dplk_nmbr"].ToString() != string.Empty)
                    {
                        ddlOperation.SelectedValue = recordResult["va_dplk_nmbr"].ToString();
                    }
               
                    if (recordResult["commision_type"].ToString() != string.Empty)
                    {
                        ddlComision.SelectedValue = recordResult["commision_type"].ToString();
                    }
             
                    if (recordResult["agent_nmbr"].ToString() != string.Empty)
                    {
                        ddlAgentNmbr.SelectedValue = recordResult["agent_nmbr"].ToString();
                    }
             
                    txtSpakRecDt.Text = Convert.ToDateTime(recordResult["spak_recv_dt"]).ToString("yyyy-MM-dd");
           
                    txtBackdatedEfectiv.Text = Convert.ToDateTime(recordResult["backdated_efctv_dt"]).ToString("yyyy-MM-dd");
                    
                    txteftdt.Text = Convert.ToDateTime(recordResult["efctv_dt"]).ToString("yyyy-MM-dd");
           
                    txtMaturityNm.Text = recordResult["maturity_val"].ToString();

                    txtMinAnnurity.Text = recordResult["min_annuity_prct"].ToString();

                    txtMinAnnurityAmt.Text = recordResult["min_annuity_amt"].ToString().Replace(",", "");

                    txtAnualMax.Text = recordResult["anl_max_with_freq"].ToString();
                   
                    txtMinWithAmt.Text = recordResult["min_with_amt"].ToString().Replace(",", "");
                 
                    txtMaxWithdrawalPer.Text = recordResult["max_with_prct"].ToString();

                    txtMinYrforwith.Text = recordResult["min_yr_for_with"].ToString();

                    txtNormalRetire.Text = recordResult["normal_retire_age"].ToString();

                    txterlyRetire.Text = recordResult["early_retire_age"].ToString();

                    txtOldGrbNmbr.Text = recordResult["old_grp_nmbr"].ToString();

                    txtClaimPrc.Text = recordResult["claim_process_day"].ToString();
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

        //hide tabs except the first 
        private void HideTabs()
        {
            MemberClassPlan.Visible = false;
            GroupCharge.Visible = false;
            InvestmentDirection.Visible = false;
            BillingStatus.Visible = false;
            Benefit.Visible = false;
            Pic.Visible = false;
        }

        private void ShowTabs()
        {
            MemberClassPlan.Visible = true;
            GroupCharge.Visible = true;
            InvestmentDirection.Visible = true;
            BillingStatus.Visible = true;
            Benefit.Visible = true;
            Pic.Visible = true;
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;


            if (ib == btnGroup)
            {
                InsertGroup();
            }

            if (ib == btnUpdateGroupInfo)
            {
                UpdateGroup();
            }

            if (ib == btnCancel)
            {
                BackToList();
            }
        }

        private void BackToList()
        {
            Response.Redirect("GroupList.aspx");
        }

        private void UpdateGroup()
        {

            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                db.BeginTransaction();
                GroupInfoModels groupInfo = new GroupInfoModels();
                groupInfo.GroupNmbr = int.Parse(txtGroupNumber.Text);
                groupInfo.ProductType = short.Parse(ddlProductType.SelectedValue);
                groupInfo.EfctvDt = DateTime.Parse(txteftdt.Text);
                groupInfo.ClientNmbr = int.Parse(ddlCompany.SelectedValue);
                groupInfo.IndGrpCd = short.Parse(ddlGroupt.SelectedValue);
                groupInfo.AllowWithNmbr = short.Parse(ddlAllow.SelectedValue);
                groupInfo.MinYrForWith = int.Parse(txtMinYrforwith.Text);
                groupInfo.MinAnnuityPrct = double.Parse(txtMinAnnurity.Text);
                groupInfo.MinAnnuityAmt = double.Parse(txtMinAnnurityAmt.Text.Replace(",",""));
                groupInfo.AnlMaxWithFreq = int.Parse(txtAnualMax.Text);
                groupInfo.MinWithAmt = double.Parse(txtMinWithAmt.Text.Replace(",",""));
                groupInfo.MaxWithPrct = double.Parse(txtMaxWithdrawalPer.Text);
                groupInfo.EarlyRetireAge = int.Parse(txterlyRetire.Text);
                groupInfo.NormalRetireAge = int.Parse(txtNormalRetire.Text);
                groupInfo.WithSrcTypeNmbr = short.Parse(ddlWdSrc.SelectedValue);
                groupInfo.InclContribFlg = short.Parse(ddlIncContriFlg.SelectedValue);
                groupInfo.AffiliatedTo = int.Parse(ddlAffliatedTo.SelectedValue);
                groupInfo.PremiumMtdType = short.Parse(ddlPremium.SelectedValue);
                groupInfo.MaturityTypeNmbr = short.Parse(ddlMaturityTypeNmbr.SelectedValue);
                groupInfo.MaturityVal = txtMaturityNm.Text;

                if (ddlPslFreq.SelectedValue == "")
                {
                    groupInfo.PslPaymentFreq = null;
                }
                else
                {
                    groupInfo.PslPaymentFreq = int.Parse(ddlPslFreq.SelectedValue);
                }
                groupInfo.BackdatedEfctvDt = DateTime.Parse(txtBackdatedEfectiv.Text);
                groupInfo.AccbalFreqNmbr = short.Parse(ddlAccBalFreq.SelectedValue);
                groupInfo.SupportUu1992 = short.Parse(ddlUU.SelectedValue);
                groupInfo.SpakRecvDt = DateTime.Parse(txtSpakRecDt.Text);
                if (ddlPsl.SelectedValue == "")
                {
                    groupInfo.HavePsl = null;
                }
                else
                {
                    groupInfo.HavePsl = short.Parse(ddlPsl.SelectedValue);
                }

                if (ddlPlsType.SelectedValue == "")
                {
                    groupInfo.PslType = null;
                }
                else
                {
                    groupInfo.PslType = short.Parse(ddlPlsType.SelectedValue);
                }


                if (ddlAlcFund.SelectedValue == "")
                {
                    groupInfo.PooledFlg = null;
                }
                else
                {
                    groupInfo.PooledFlg = int.Parse(ddlAlcFund.SelectedValue);
                }
                groupInfo.ClaimProcessDay = int.Parse(txtClaimPrc.Text);
                groupInfo.VaCurrencyNmbr = ddlCry.SelectedValue;
                groupInfo.VaDplkNmbr = ddlOperation.SelectedValue;
                groupInfo.CommisionType = int.Parse(ddlComision.SelectedValue);
                groupInfo.AgentNmbr = int.Parse(ddlAgentNmbr.SelectedValue);

                if (groupInfo.UpdateGroupInfo(db))
                {
                    db.CommitTransaction();
                    GroupTabs.ActiveTabIndex += 1;
                }
               
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

            //pop up group has been updated 
        }

        private void InsertGroup()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                GroupInfoModels c = new GroupInfoModels();

                db.Open();
                db.BeginTransaction();
                
                c.ProductType = short.Parse(ddlProductType.SelectedValue);

                c.EfctvDt = DateTime.Parse(txteftdt.Text);

                c.ClientNmbr = int.Parse(ddlCompany.SelectedValue);

                c.IndGrpCd = short.Parse(ddlGroupt.SelectedValue);

                c.AllowWithNmbr = int.Parse(ddlAllow.SelectedValue);

                c.WithYear = int.Parse(txtMinYrforwith.Text);

                c.MinAnnuityPrct = double.Parse(txtMinAnnurity.Text);

                c.MinAnnuityAmt = double.Parse(txtMinAnnurityAmt.Text);

                c.AnlMaxWithFreq = int.Parse(txtAnualMax.Text);

                c.MinWithAmt = double.Parse(txtMinWithAmt.Text);

                c.MaxWithPrct = double.Parse(txtMaxWithdrawalPer.Text);

                c.MinYrForWith = int.Parse(txtMinYrforwith.Text);

                c.EarlyRetireAge = int.Parse(txterlyRetire.Text);

                c.NormalRetireAge = int.Parse(txtNormalRetire.Text);

                c.WithSrcTypeNmbr = short.Parse(ddlWdSrc.SelectedValue);


                c.InclContribFlg = short.Parse(ddlIncContriFlg.SelectedValue);


                c.AffiliatedTo = short.Parse(ddlAffliatedTo.SelectedValue);

                c.PremiumMtdType = short.Parse(ddlPremium.SelectedValue);

                c.MaturityTypeNmbr = short.Parse(ddlMaturityTypeNmbr.SelectedValue);

                c.MaturityVal = txtMaturityNm.Text;

                c.PslPaymentFreq = 100;

                c.BackdatedEfctvDt = DateTime.Parse(txtBackdatedEfectiv.Text);


                c.AccbalFreqNmbr = short.Parse(ddlAccBalFreq.SelectedValue);

                c.SupportUu1992 = short.Parse(ddlUU.SelectedValue);

                c.SpakRecvDt = DateTime.Parse(txtSpakRecDt.Text);

                c.OldGrpNmbr = txtOldGrbNmbr.Text;

                c.ClaimProcessDay = int.Parse(txtClaimPrc.Text);

                c.VaCurrencyNmbr = ddlCry.SelectedValue;

                c.CommisionType = short.Parse(ddlComision.SelectedValue);

                c.AgentNmbr = int.Parse(ddlAgentNmbr.SelectedValue);       
            
                Session["newGroup"] = c.InsertGroupInfo(db);
                db.CommitTransaction();

                txtGroupNumber.Text = Session["newGroup"].ToString();
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
            }
            finally
            {
                db.Close();
            }

            ShowTabs();
            GroupTabs.ActiveTabIndex +=1;
        }

        private void fillDDL()
        {
            DDLCompanyNm();
            DDLProductType();
            DDLAffiliate();
            DDLPremium();
            DDLFrequency();
            DDLAgent();
            DDLMatType();
            DDLCommision();
            DDLAllowType();
            DDLWdSource();
            DDLCurrency();
            DDLOperation();
        }

        private void DDLWdSource()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.SourceType(ddlWdSrc, db);
            db.Close();
        }
        private void DDLCompanyNm()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.CompanyDll(ddlCompany, db);
            db.Close();
        }
        private void DDLProductType()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.ProductDDL(ddlProductType, db);
            db.Close();
        }
        private void DDLAffiliate()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.AffiliateDDL(ddlAffliatedTo, db);
            db.Close();
        }
        private void DDLPremium()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.PremiumDDL(ddlPremium, db);
            db.Close();
        }

        private void DDLPslFreq()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.FrequencyDDL(ddlPslFreq, db);
            db.Close();
        }

        private void DDLFrequency()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.FrequencyDDL(ddlAccBalFreq, db);
            db.Close();
        }
        private void DDLAgent()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.AgentDDL(ddlAgentNmbr, db);
            db.Close();
        }
        private void DDLCommision()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.CommisonDLL(ddlComision, db);
            db.Close();
        }
        private void DDLAllowType()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.AllowDDL(ddlAllow, db);
            db.Close();
        }
        private void DDLCurrency()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.Currency(ddlCry, db);
            db.Close();
        }
        private void DDLOperation()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.Operation(ddlOperation, db);
            db.Close();
        }

        private void DDLMatType()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.MattypeDDL(ddlMaturityTypeNmbr, db);
            db.Close();
            
        }

        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductSetup();
        }

        private void ProductSetup()
        {
            Connection con = new Connection();
            Database db = new Database(con.ConnectionStringPension);
            ProductSetupModels productSetup = new ProductSetupModels();
            productSetup.productTypeNmbr = int.Parse(ddlProductType.SelectedValue);
            
            try
            {
                db.Open();
                var productSetupList = productSetup.SetupProduct(db);
                if (productSetupList.Count > 0)
                {
                    foreach (var item in productSetupList)
                    {
                        switch (item.setupTypeNmbr)
                        {
                            case 2:
                                txtMinAnnurity.Text = item.defltValue;
                                break;
                            case 3:
                                txtMinAnnurityAmt.Text = item.defltValue;
                                break;
                            case 4:
                                txtAnualMax.Text = item.defltValue;
                                break;
                            case 5:
                                txtMinWithAmt.Text = item.defltValue;
                                break;
                            case 6:
                                txtMaxWithdrawalPer.Text = item.defltValue;
                                break;
                            case 7:
                                txtMinYrforwith.Text = item.defltValue;
                                break;
                            case 8:
                                txtNormalRetire.Text = item.defltValue;
                                txterlyRetire.Text = item.defltValue;
                                break;
                            case 16:
                                ddlGroupt.SelectedIndex = ddlGroupt.Items.IndexOf(ddlGroupt.Items.FindByText(item.defltValue));
                                break;
                            case 17:
                                ddlIncContriFlg.SelectedIndex = ddlIncContriFlg.Items.IndexOf(ddlIncContriFlg.Items.FindByText(item.defltValue));
                                break;
                            case 18:
                                ddlAlcFund.SelectedIndex = ddlAlcFund.Items.IndexOf(ddlAlcFund.Items.FindByText(item.defltValue));
                                break;
                            case 19:
                                ddlAccBalFreq.SelectedIndex = ddlAccBalFreq.Items.IndexOf(ddlAccBalFreq.Items.FindByText(item.defltValue));
                                break;
                            case 20:
                                ddlMaturityTypeNmbr.SelectedIndex = ddlMaturityTypeNmbr.Items.IndexOf(ddlMaturityTypeNmbr.Items.FindByText(item.defltValue));
                                break;
                            case 21:
                                txtMaturityNm.Text = item.defltValue;
                                break;
                        }
                    }

                    txtClaimPrc.Text = "14";
                    DateTime cycleDate = GetCycleDate();
                    String cycleDateString = cycleDate.ToString("yyyy-MM-dd");
                    txtBackdatedEfectiv.Text = cycleDateString;
                    txteftdt.Text = cycleDateString;
                    txtSpakRecDt.Text = cycleDateString;
                }
                else
                {
                    //nothing happens if selected product type is "PPUKP"
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        private void ClearTextBox(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = String.Empty;
                }
                if (control.Controls.Count > 0)
                {
                    ClearTextBox(control);  
                }
            }
        }

        private void ClearSetup()
        {

            ClearTextBox(this);
            DDLCompanyNm();
            DDLProductType();
            DDLAffiliate();
            DDLPremium();
            DDLFrequency();
            DDLAgent();
            DDLCommision();
            DDLAllowType();
            DDLWdSource();
            DDLCurrency();
            DDLOperation();

        }

        private DateTime GetCycleDate()
        {
            Connection conn = new Connection();
            string constring = conn.ConnectionStringPension;
            Database db = new Database(constring);
            db.Open();
            string query = "select cycle_dt from cycle";
            db.setQuery(query);
            DateTime output = Convert.ToDateTime(db.ExecuteScalar());

            return output;
        }

        protected void GroupTabs_ActiveTabChanged(object sender, EventArgs e)
        {
            
        }
    }
}