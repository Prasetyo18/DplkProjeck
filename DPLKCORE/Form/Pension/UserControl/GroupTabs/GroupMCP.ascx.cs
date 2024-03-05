using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System.Data;

namespace DPLKCORE.Form.Pension.UserControl.GroupTabs
{
    public partial class GroupMCP : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDDLMCP();

                if (Request.QueryString["state"] == "Edit")
                {
                    //hide insert button
                    btnMCPSave.Style.Add("display", "none");

                    //show update button
                    btnMCPUpdate.Style.Remove("display");

                    LoadData();
                }
                else if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    //show insert button
                    btnMCPSave.Style.Remove("display");

                    //hide update button
                    btnMCPUpdate.Style.Add("display", "none");
                }
            }
        }

        protected void ddlMCPType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearData()
        {
            ClearTextBox(this);
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
        private void LoadData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                MemberClassPlanModels mcp = new MemberClassPlanModels();
                
                mcp.GroupNmbr = Convert.ToInt32(Session["groupIdDetail"]);
                mcp.McpNmbr = int.Parse(ddlMCPType.SelectedValue);
                DataTable result = mcp.LoadData(db);
                ClearData();
                if (result.Rows.Count > 0)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        var condition = row["money_type_nm"].ToString().Trim();

                        switch (condition)
                        {
                            case "Pemberi Kerja":
                                txtCntrbER.Text = row["cntrb_amt"].ToString().Replace(",","");
                                txtCntrbRateER.Text = row["cntrb_rt"].ToString().Replace(",", "");
                                break;
                            case "Peserta":
                                txtCntrbEE.Text = row["cntrb_amt"].ToString().Replace(",", "");
                                txtCntrbRateEE.Text = row["cntrb_rt"].ToString().Replace(",", "");
                                break;
                            case "Tambahan":
                                txtCntrbTU.Text = row["cntrb_amt"].ToString().Replace(",", "");
                                txtCntrbRateTU.Text = row["cntrb_rt"].ToString().Replace(",", "");
                                break;
                            case "Pengalihan Dana":
                                txtCntrbFT.Text = row["cntrb_amt"].ToString().Replace(",", "");
                                txtCntrbRateFT.Text = row["cntrb_rt"].ToString().Replace(",", "");
                                break;
                            default:
                                break;
                        }
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

        protected void btnMCPUpdate_Click(object sender, EventArgs e)
        {
            if (InsertMCP())
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
            else
            {
                //show message box that insert is failed;
            }
        }

        public T FindControlRecursive<T>(Control parent, string controlId) where T : Control
        {
            if (parent == null)
            {
                return null;
            }

            if (parent.ID == controlId)
            {
                return (T)parent;
            }

            foreach (Control control in parent.Controls)
            {
                T foundControl = FindControlRecursive<T>(control, controlId);
                if (foundControl != null)
                {
                    return foundControl;
                }
            }

            return null;
        }

        protected void btnMCPSave_Click(object sender, EventArgs e)
        {

            if (InsertMCP())
            {
                //load mcp ddl in benefit tab according to the new inserted mcp
                Session["McpType"] = ddlMCPType.SelectedItem;
                GroupBenefit benefitTab = FindControlRecursive<GroupBenefit>(Page, "UCBenefit");
                if (benefitTab != null)
                {
                    benefitTab.fillDDL();
                }

                //go to the next tab
                TabContainer groupContainer = locateTabContainer(Page);
                if (groupContainer != null)
                {
                    if (groupContainer.ActiveTabIndex < groupContainer.Tabs.Count - 1)
                    {
                        groupContainer.ActiveTabIndex += 1;
                    }
                }
            }
            else
            {
                //show message box that insert is failed;
            }
            
        }

        private bool InsertMCP()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                MemberClassPlanModels mcp = new MemberClassPlanModels();
                db.Open();
                if (Request.QueryString["state"] == "Edit")
                {
                    mcp.GroupNmbr = Convert.ToInt32(Session["groupIdDetail"]);
                }
                else if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    mcp.GroupNmbr = Convert.ToInt32(Session["newGroup"]);
                }
                
                mcp.McpNmbr = int.Parse(ddlMCPType.SelectedValue);
                
                mcp.CntrbAmtEE = float.Parse(txtCntrbEE.Text.Replace(",",""));
                mcp.CntrbAmtER = float.Parse(txtCntrbER.Text.Replace(",", ""));
                mcp.CntrbAmtFT = float.Parse(txtCntrbFT.Text.Replace(",", ""));
                mcp.CntrbAmtTU = float.Parse(txtCntrbTU.Text.Replace(",", ""));
                mcp.CntrbRtEE = float.Parse(txtCntrbRateEE.Text.Replace(",", ""));
                mcp.CntrbRtER = float.Parse(txtCntrbRateER.Text.Replace(",", ""));
                mcp.CntrbRtFT = float.Parse(txtCntrbRateFT.Text.Replace(",", ""));
                mcp.CntrbRtTU = float.Parse(txtCntrbRateTU.Text.Replace(",", ""));

                if (mcp.InsertMemberClassPlan(db))
                {
                    return true;
                }
                else
                {
                    return false;
                }


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


        private void FillDDLMCP()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLGroup ddlHelper = new DDLGroup();

            db.Open();
            ddlHelper.DDLMcpType(ddlMCPType, db);
            db.Close();

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
    }
}