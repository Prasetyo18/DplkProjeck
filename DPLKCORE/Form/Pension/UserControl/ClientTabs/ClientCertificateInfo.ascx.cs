using DPLKCORE.Framework;
using DPLKCORE.Framework.Helper;
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
    public partial class ClientCertificateInfo : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCertifAdd.Click += ButtonClicked;
            btnAddInvDrct.Click += ButtonClicked;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDDL();
                //btnAddInvDrct.Style.Add("display", "none");
                if (Request.QueryString["state"] == "NewClient")
                {
                    btnCertifAdd.Style.Remove("display");
                    btnCertifUpdate.Style.Add("display", "none");

                    btnAddInvDrct.Style.Add("display", "none");
                }
                else if (Request.QueryString["state"] == "Edit")
                {
                    btnCertifUpdate.Style.Remove("display");
                    btnAddInvDrct.Style.Remove("display");
                    LoadData();
                }
            }
        }

        #region Load Data and DDL



        private void fillDDL()
        {
            //load DDL
            LoadCertifDDL();
            ddlCerInfo.SelectedIndex = 0;

            LoadGroupDDL();
            ddlGroup.SelectedIndex = 0;
            LoadGvInvestDirect(int.Parse(ddlCerInfo.SelectedValue));

            LoadPaycenterDDL(ddlGroup.SelectedValue);
            LoadPaymentMethodDDL();
            LoadCitizenshipDDL();
            LoadJenisPremiDDL();
            LoadJenisRiderDDL();
            LoadJenisPlanDDL();
            LoadAgentDDL();
            LoadJenisKomisiDDL();

            LoadMCPDDL(ddlGroup.SelectedValue);
            ddlMmbrClasPlan.SelectedIndex = 0;

            LoadFundDDL();
            LoadStatusDDL();
            LoadClientNm();
            LoadRetirementAge(ddlGroup.SelectedValue);
            LoadInvestmentName(ddlGroup.SelectedValue);
            LoadInvMoneyTypeDDL();
        }

        protected void GvCertifMCP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvCertifMCP.PageIndex = e.NewPageIndex;
            LoadGvMCP(int.Parse(ddlGroup.SelectedValue), int.Parse(ddlMmbrClasPlan.SelectedValue), int.Parse(ddlCerInfo.SelectedValue));
        }

        protected void ddlMmbrClasPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGvMCP(int.Parse(ddlGroup.SelectedValue), int.Parse(ddlMmbrClasPlan.SelectedValue), int.Parse(ddlCerInfo.SelectedValue));
        }

        private void LoadGvMCP(int GroupNmbr, int MCPNmbr, int CerNmbr)
        {
            GvCertifMCP.DataSource = null;
            GvCertifMCP.DataBind();

            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                CertificateInfoModels certif = new CertificateInfoModels();
                DataTable data = certif.LoadGvMCP(db, GroupNmbr, MCPNmbr, CerNmbr);
                if (data.Rows.Count > 0)
                {
                    GvCertifMCP.DataSource = data;
                    GvCertifMCP.DataBind();
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

        protected void GvSumInsured_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int absoluteRowIndex = Math.Abs((GvSumInsured.PageIndex * GvSumInsured.PageSize) - rowIndex);
                GridViewRow row = GvSumInsured.Rows[absoluteRowIndex];

                Button btEdit = (Button)row.Cells[0].FindControl("BT_EDIT");
                Button btSave = (Button)row.Cells[3].FindControl("BT_SAVE");
                TextBox txtSIAmmount = (TextBox)row.Cells[2].FindControl("TXT_SI_AMT");


                switch (e.CommandName)
                {
                    case "BT_EDIT":
                        btSave.Enabled = true;
                        btSave.Visible = true;
                        txtSIAmmount.Enabled = true;
                        btEdit.Enabled = false;
                        break;
                    case "BT_SAVE":
                        CertificateInfoModels certif = new CertificateInfoModels();
                        certif.CertificateNumber = int.Parse(ddlCerInfo.SelectedValue);
                        certif.SumInsuredEffectiveDate = DateTime.Parse(txtSumInsuredEfctvDt.Text);
                        certif.SumInsured = float.Parse(txtSIAmmount.Text);
                        certif.BeneTypeNm = row.Cells[1].Text;

                        if (certif.InsertCertifSumInsured(db))
                        {
                            btSave.Enabled = false;
                            btSave.Visible = true;
                            txtSIAmmount.Enabled = false;
                            btEdit.Enabled = true;
                        }

                        LoadGvSumInsured(int.Parse(ddlCerInfo.SelectedValue), txtSumInsuredEfctvDt.Text);
                        break;
                    default:
                        break;
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

        private void LoadGvSumInsured(int CerNmbr, string SumInsuredDt)
        {
            GvSumInsured.DataSource = null;
            GvSumInsured.DataBind();
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                CertificateInfoModels certif = new CertificateInfoModels();
                DataTable data = certif.LoadGvSumInsured(db, CerNmbr, SumInsuredDt);
                if (data.Rows.Count > 0)
                {
                    GvSumInsured.DataSource = data;
                    GvSumInsured.DataBind();
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

        protected void GvSumInsured_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvSumInsured.PageIndex = e.NewPageIndex;
            LoadGvSumInsured(int.Parse(ddlCerInfo.SelectedValue), txtSumInsuredEfctvDt.Text);
        }

        private void ClearControl()
        {
            ControlHelper.ClearTextBox(this);
            ddlApp.SelectedIndex = 0;
            btnAddInvDrct.Style.Add("display", "none");
        }

        protected void ddlCerInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlCerInfo.SelectedValue) == 0)
            {
                ClearControl();
                return;
            }
            LoadData();
            LoadGvInvestDirect(int.Parse(ddlCerInfo.SelectedValue));
            LoadGvMCP(int.Parse(ddlGroup.SelectedValue), int.Parse(ddlMmbrClasPlan.SelectedValue), int.Parse(ddlCerInfo.SelectedValue));
        }

        private void LoadGvInvestDirect(int CerNmbr)
        {
            GvInvDrct.DataSource = null;
            GvInvDrct.DataBind();
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                CertificateInfoModels certif = new CertificateInfoModels();
                DataTable data = certif.LoadGvInvestDirection(db, CerNmbr);
                if (data.Rows.Count > 0)
                {
                    GvInvDrct.DataSource = data;
                    GvInvDrct.DataBind();
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

        protected void GvInvDrct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadGvInvestDirect(int.Parse(ddlCerInfo.SelectedValue));
        }


        public void LoadClientNm()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                if (Request.QueryString["state"] == "Edit")
                {
                    ddlHelper.LoadClientNm(db, txtClientName, Convert.ToInt32(Session["ClientIdDetail"]));
                }
                else if (Request.QueryString["state"] == "NewClient")
                {
                    ddlHelper.LoadClientNm(db, txtClientName, Convert.ToInt32(Session["newClientId"]));
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

        private void LoadInvMoneyTypeDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLMoneyType(db, ddlMoneytp);
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

        private void LoadInvestmentName(string GroupNmbr)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadInvestmentName(db, ddlIvstNm, GroupNmbr);
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

        private void LoadRetirementAge(string GroupNmbr)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadRetirementAge(db, txtRetireAge, GroupNmbr);
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

        private void LoadStatusDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLStatus(db, ddlStatus);
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

        private void LoadFundDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLFund(db, ddlFunds);
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

        private void LoadMCPDDL(string GroupNmbr)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLMCP(db, ddlMmbrClasPlan, GroupNmbr);
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

        private void LoadJenisKomisiDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLKomisi(db, ddlNamaKomisi);
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

        private void LoadAgentDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLAgen(db, ddlNamaAgen);
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

        private void LoadJenisPlanDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.PremiumTypeNmbr = ddlJenisPremi.SelectedValue;
                ddlHelper.RiderTypeNmbr = ddlJenisRider.SelectedValue;
                ddlHelper.LoadDDLJenisPlan(db, ddlJenisPlan);
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

        private void LoadJenisRiderDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLJenisRider(db, ddlJenisRider);
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

        private void LoadJenisPremiDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLJenisPremi(db, ddlJenisPremi);
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

        private void LoadCitizenshipDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLCitizenship(db, ddlCityz);
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

        private void LoadPaymentMethodDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLPayMethod(db, ddlPayCenterMethod);
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

        private void LoadPaycenterDDL(string GroupNmbr)
        {
            ddlPayC.Items.Clear();

            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLPaycenter(db, ddlPayC, GroupNmbr);
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

        private void LoadGroupDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLGroup(db, ddlGroup);
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

        private void LoadCertifDDL()
        {
            //if (Request.QueryString["state"] == "NewClient")
            //{
            //    ddlCerInfo.Items.Add(new ListItem("New", "0"));
            //}
            if (Request.QueryString["state"] == "Edit")
            {
                Connection conn = new Connection();
                Database db = new Database(conn.ConnectionStringPension);
                try
                {
                    db.Open();
                    DDLClient ddlHelper = new DDLClient();
                    ddlHelper.LoadDDLCertif(db, ddlCerInfo, Convert.ToInt32(Session["ClientIdDetail"]));
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
            ddlCerInfo.Items.Add(new ListItem("New", "0"));
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPaycenterDDL(ddlGroup.SelectedValue);
            LoadMCPDDL(ddlGroup.SelectedValue);
            LoadRetirementAge(ddlGroup.SelectedValue);
            LoadInvestmentName(ddlGroup.SelectedValue);
        }

        protected void ddlJenisPremi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadJenisPlanDDL();
        }

        protected void ddlJenisRider_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadJenisPlanDDL();
        }

        #endregion

        private void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;
            if (ib == btnCertifAdd)
            {
                InsertCertif();
                ClientFundInfo fund = ControlHelper.FindControlRecursive<ClientFundInfo>(this.Page, "UCFundInfo");
                StatusAndSalary stat = ControlHelper.FindControlRecursive<StatusAndSalary>(this.Page, "UCStatAndSalary");
                ClientTransaction transac = ControlHelper.FindControlRecursive<ClientTransaction>(this.Page, "UCTransaction");
                ClientClaimHistorical claimHistorical = ControlHelper.FindControlRecursive<ClientClaimHistorical>(this.Page, "UCClaimHistorical");
                if (fund != null)
                {
                    fund.LoadCertifDDL();
                }
                if (stat != null)
                {
                    stat.Setup();
                }
                if (transac != null)
                {
                    transac.Setup();
                }
                if (claimHistorical != null)
                {
                    claimHistorical.Setup();
                    claimHistorical.LoadResult();
                }
            }
            if (ib == btnAddInvDrct)
            {
                CheckInvDrct();
            }
        }



        #region insert, load record, update

        private void LoadData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                CertificateInfoModels certif = new CertificateInfoModels();
                certif.CertificateNumber = int.Parse(ddlCerInfo.SelectedValue);
                Dictionary<string, object> data = certif.LoadDataById(db);

                if (data.Count > 0)
                {
                    if (data["old_cer_nmbr"].ToString() != "")
                    {
                        txtOldClient.Text = data["old_cer_nmbr"].ToString();
                    }
                    if (data["client_nm"].ToString() != "")
                    {
                        txtClientName.Text = data["client_nm"].ToString();
                    }
                    if (data["group_nmbr"].ToString() != "")
                    {
                        ddlGroup.SelectedValue = data["group_nmbr"].ToString();
                    }
                    if (data["plan_type_nmbr"].ToString() != "")
                    {
                        ddlJenisPlan.SelectedValue = data["plan_type_nmbr"].ToString();
                    }

                    //fill plan type 
                    LoadPaycenterDDL(ddlGroup.SelectedValue);
                    if (data["paycenter_nm"].ToString() != "")
                    {
                        ddlPayC.SelectedIndex = ddlPayC.Items.IndexOf(ddlPayC.Items.FindByText(data["paycenter_nm"].ToString()));
                    }

                    //fill invDirct type
                    LoadInvestmentName(ddlGroup.SelectedValue);

                    if (data["employee_nmbr"].ToString() != "")
                    {
                        txtEmpoyeCode.Text = data["employee_nmbr"].ToString();
                    }
                    if (data["employment_dt"].ToString() != "")
                    {
                        txtEmployedDT.Text = Convert.ToDateTime(data["employment_dt"]).ToString("yyyy-MM-dd");
                    }
                    if (data["retirement_age"].ToString() != "")
                    {
                        txtRetireAge.Text = data["retirement_age"].ToString();
                    }
                    if (data["payment_type_nm"].ToString() != "")
                    {
                        ddlPayCenterMethod.SelectedIndex = ddlPayCenterMethod.Items.IndexOf(ddlPayCenterMethod.Items.FindByText(data["payment_type_nm"].ToString()));
                    }
                    if (data["citizenship_cd"].ToString() != "")
                    {
                        ddlCityz.SelectedIndex = ddlCityz.Items.IndexOf(ddlCityz.Items.FindByText(data["citizenship_cd"].ToString()));
                    }
                    if (data["tax_id_nmbr"].ToString() != "")
                    {
                        txtTax.Text = data["tax_id_nmbr"].ToString();
                    }
                    if (data["premium_type_nmbr"].ToString() != "")
                    {
                        ddlJenisPremi.SelectedValue = data["premium_type_nmbr"].ToString();
                    }
                    if (data["rider_type_nmbr"].ToString() != "")
                    {
                        ddlJenisRider.SelectedValue = data["rider_type_nmbr"].ToString();
                    }
                    if (data["agent_nmbr"].ToString() != "")
                    {
                        ddlNamaAgen.SelectedValue = data["agent_nmbr"].ToString();
                    }
                    if (data["commision_type_nmbr"].ToString() != "")
                    {
                        ddlNamaKomisi.SelectedValue = data["commision_type_nmbr"].ToString();
                    }

                    //load and select mcpddl;
                    LoadMCPDDL(ddlGroup.SelectedValue);
                    if (data["job_vctn_nm"].ToString() != "")
                    {
                        ddlMmbrClasPlan.SelectedIndex = ddlMmbrClasPlan.Items.IndexOf(ddlMmbrClasPlan.Items.FindByText(data["job_vctn_nm"].ToString()));
                    }

                    if (data["mcp_nm"].ToString() != "")
                    {
                        txtJobfct.Text = data["mcp_nm"].ToString();
                    }
                    if (data["fund_src_nm"].ToString() != "")
                    {
                        ddlFunds.SelectedIndex = ddlFunds.Items.IndexOf(ddlFunds.Items.FindByText(data["fund_src_nm"].ToString()));
                    }
                    if (data["app_receive_dt"].ToString() != "")
                    {
                        txtAplicantRec.Text = Convert.ToDateTime(data["app_receive_dt"]).ToString("yyyy-MMM-dd");
                    }
                    if (data["Completion_dt"].ToString() != "")
                    {
                        txtAplicantCompplete.Text = Convert.ToDateTime(data["Completion_dt"]).ToString("yyyy-MMM-dd");
                    }
                    if (data["efctv_dt"].ToString() != "")
                    {
                        txtEffectvDt.Text = Convert.ToDateTime(data["efctv_dt"]).ToString("yyyy-MMM-dd");
                        efftdt.Text = Convert.ToDateTime(data["efctv_dt"]).ToString("yyyy-MMM-dd");
                    }
                    if (data["create_dt"].ToString() != "")
                    {
                        txtCreateDt.Text = Convert.ToDateTime(data["create_dt"]).ToString("yyyy-MM-dd");
                    }
                    if (data["termination_dt"].ToString() != "")
                    {
                        txtTerminate.Text = Convert.ToDateTime(data["termination_dt"]).ToString("yyyy-MM-dd");
                    }
                    if (data["kit_delivery_dt"].ToString() != "")
                    {
                        txtCerDevDt.Text = Convert.ToDateTime(data["kit_delivery_dt"]).ToString("yyyy-MM-dd");
                    }
                    if (data["first_premium_flg"].ToString() != "")
                    {
                        ddlFirstPremiumPaid.SelectedValue = data["first_premium_flg"].ToString();
                    }
                    if (data["oth_DPPK_flg"].ToString() != "")
                    {
                        ddlHaveOtherDplk.SelectedValue = data["oth_DPPK_flg"].ToString();
                    }
                    if (data["maturity_dt"].ToString() != "")
                    {
                        txtMatDate.Text = Convert.ToDateTime(data["maturity_dt"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (data["pin_print_dt"].ToString() != "")
                    {
                        txtPrintDate.Text = Convert.ToDateTime(data["pin_print_dt"]).ToString("yyyy-MM-dd");
                    }
                    if (data["salary_efctv_dt"].ToString() != "")
                    {
                        txtSalaryDate.Text = Convert.ToDateTime(data["salary_efctv_dt"]).ToString("yyyy-MM-dd");
                    }
                    if (data["salary_amt"].ToString() != "")
                    {
                        txtSalaryAmt.Text = data["salary_amt"].ToString();
                    }
                    if (data["sum_insured_efctv_dt"].ToString() != "")
                    {
                        txtSumInsuredEfctvDt.Text = data["sum_insured_efctv_dt"].ToString();
                    }

                    LoadGvSumInsured(int.Parse(ddlCerInfo.SelectedValue), txtSumInsuredEfctvDt.Text);

                    if (data["status_efctv_dt"].ToString() != "")
                    {
                        txtStatusEfctvDt.Text = Convert.ToDateTime(data["status_efctv_dt"]).ToString("yyyy-MM-dd");
                    }
                    if (data["status_type_nm"].ToString() != "")
                    {
                        ddlStatus.SelectedValue = data["status_type_nm"].ToString();
                    }
                    if (data["branch"].ToString() != "")
                    {
                        txtBrac.Text = data["branch"].ToString();
                    }
                    if (data["cycle"].ToString() != "")
                    {
                        txtEffectvDt.Text = Convert.ToDateTime(data["cycle"]).ToString("yyyy-MM-dd");
                    }
                    if (data["v_acc_nmbr"].ToString() != "")
                    {
                        txtVirtualAcc.Text = data["v_acc_nmbr"].ToString();
                    }
                    if (data["apu_ppt"].ToString() != "")
                    {
                        ddlApp.SelectedValue = data["apu_ppt"].ToString();
                    }

                    LoadGvMCP(int.Parse(ddlGroup.SelectedValue), int.Parse(ddlMmbrClasPlan.SelectedValue), int.Parse(ddlCerInfo.SelectedValue));
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

        private void InsertInvDrct(CertificateInfoModels certifInvDrct)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                db.BeginTransaction();
                if (certifInvDrct.InsertCertifInvDrct(db))
                {
                    db.CommitTransaction();                 
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
            LoadGvInvestDirect(int.Parse(ddlCerInfo.SelectedValue));

            //if state is newclient, go to the next tab. if else, nothing happens.
            if (Request.QueryString["state"] == "NewClient")
            {
                TabHelper.NextTab(this.Page);
            }   
        }


        private void CheckInvDrct()
        {
            bool isExist;
            CertificateInfoModels certifInvDirection = new CertificateInfoModels();

            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                if (Request.QueryString["state"] == "Edit")
                {
                    certifInvDirection.CertificateNumber = int.Parse(ddlCerInfo.SelectedValue);
                }
                else if (Request.QueryString["state"] == "NewClient")
                {
                    certifInvDirection.CertificateNumber = Convert.ToInt32(Session["NewCertifNmbr"]);
                }
                certifInvDirection.EffectiveDate = DateTime.Parse(txtEffectvDt.Text);
                certifInvDirection.InvTypeNmbr = int.Parse(ddlIvstNm.SelectedValue);
                certifInvDirection.MoneyTypeNmbr = int.Parse(ddlMoneytp.SelectedValue);
                certifInvDirection.Percentage = float.Parse(pdc.Text);

                isExist = certifInvDirection.IsExist(db);
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

            if (isExist == false)
            {
                InsertInvDrct(certifInvDirection);
            }
        }

        private void InsertCertif()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                db.BeginTransaction();
                CertificateInfoModels certificate = new CertificateInfoModels();
                //asign properties
                //CertificateNumber is null because it is insertion function
                //OldCertificateNumber is null because it is insertion function

                if (Request.QueryString["state"] == "NewClient")
                {
                    certificate.ClientNumber = Convert.ToInt32(Session["newClientId"]);
                }
                else if (Request.QueryString["state"] == "Edit" || int.Parse(ddlCerInfo.SelectedValue) == 0)
                {
                    certificate.ClientNumber = Convert.ToInt32(Session["ClientIdDetail"]);
                }

                certificate.GroupNumber = Convert.ToInt32(ddlGroup.SelectedValue);
                certificate.EmployeeNumber = txtEmpoyeCode.Text;
                certificate.EmploymentDate = Convert.ToDateTime(txtEmployedDT.Text);
                certificate.RetirementAge = int.Parse(txtRetireAge.Text);
                certificate.PaycenterNumber = int.Parse(ddlPayC.SelectedValue);
                certificate.PaymentTypeNumber = int.Parse(ddlPayCenterMethod.SelectedValue);
                certificate.CitizenshipCode = int.Parse(ddlCityz.SelectedValue);
                certificate.TaxIdNumber = txtTax.Text;
                certificate.JobVacationNumber = int.Parse(ddlMmbrClasPlan.SelectedValue);
                certificate.FundSourceNumber = int.Parse(ddlFunds.SelectedValue);
                certificate.ApplicationReceiveDate = DateTime.Parse(txtAplicantRec.Text);
                certificate.CompletionDate = DateTime.Parse(txtAplicantCompplete.Text);
                certificate.EffectiveDate = DateTime.Parse(txtEffectvDt.Text);
                certificate.TerminationDate = DateTime.Parse(txtTerminate.Text);
                certificate.KitDeliveryDate = DateTime.Parse(txtCerDevDt.Text);
                certificate.FirstPremiumFlag = ddlFirstPremiumPaid.SelectedIndex == 0 ? false : true;
                certificate.OtherDPPKFlag = ddlHaveOtherDplk.SelectedIndex == 0 ? false : true;
                certificate.SumInsured = txtSumInsuredAmmount.Text == "" ? 0 : float.Parse(txtSumInsuredAmmount.Text);
                certificate.SumInsuredEffectiveDate = DateTime.Parse(txtSumInsuredEfctvDt.Text);
                certificate.SalaryAmount = float.Parse(txtSalaryAmt.Text);
                certificate.SalaryEffectiveDate = DateTime.Parse(txtSalaryDate.Text);
                certificate.StatusEffectiveDate = DateTime.Parse(txtStatusEfctvDt.Text);
                certificate.StatusTypeName = ddlStatus.SelectedValue;
                certificate.Branch = txtBrac.Text;
                //salesname is empty
                certificate.PremiumTypeNumber = int.Parse(ddlJenisPremi.SelectedValue);
                certificate.RiderTypeNumber = int.Parse(ddlJenisRider.SelectedValue);
                certificate.AgentNumber = int.Parse(ddlNamaAgen.SelectedValue);
                certificate.CommissionTypeNumber = int.Parse(ddlNamaKomisi.SelectedValue);
                certificate.PlanTypeNumber = int.Parse(ddlJenisPlan.SelectedValue);
                certificate.ApuPpt = ddlApp.SelectedIndex == 0 ? false : true;

                //execute transaction
                Session["NewCertifNmbr"] = certificate.InsertOrUpdateCertificate(db);
                if (Convert.ToInt32(Session["NewCertifNmbr"]) > 0)
                {
                    db.CommitTransaction();
                    //show the investation direction insert button
                    btnAddInvDrct.Style.Remove("display");
                    LoadCertifDDL();
                }
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
            }
            finally
            {
                db.Close();
            }
        }

        #endregion

        

        

        
    }
}