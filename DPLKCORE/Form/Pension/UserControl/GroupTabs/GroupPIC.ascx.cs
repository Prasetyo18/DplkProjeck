using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using AjaxControlToolkit;
using DPLKCORE.Logic.Pension;

namespace DPLKCORE.Form.Pension.UserControl.GroupTabs
{
    public partial class GroupPIC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["state"] == "Edit")
                {
                    btnSavePIC.Style.Add("display", "none");
                    btnUpdate.Style.Remove("display");
                    
                    LoadData();
                }
                else if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    btnUpdate.Style.Add("display", "none");
                    btnSavePIC.Style.Remove("display");
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
                GroupPICModels pic = new GroupPICModels();
                Dictionary<string, object> result = pic.LoadData(db, Convert.ToInt32(Session["GroupIdDetail"]));
                if (result.Count > 0)
                {
                    Session["KdPIC"] = result["kd_pic"];
                    ddlTitle.SelectedValue = result["title"].ToString();
                    txtPicName.Text = result["nama_pic"].ToString();
                    txtJabatan.Text = result["jabatan"].ToString();
                    ddlStatus.SelectedValue = result["is_active"].ToString();
                }
                else
                {
                    Session["KdPIC"] = "";
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

        private bool UpdatePIC()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                db.BeginTransaction();

                GroupPICModels pic = new GroupPICModels();
                pic.Title = ddlTitle.SelectedValue;
                pic.GroupNmbr = Convert.ToInt32(Session["GroupIdDetail"]);
                pic.KdPic = Session["KdPIC"].ToString();
                pic.NamaPic = txtPicName.Text;
                pic.Jabatan = txtJabatan.Text;
                pic.IsActive = ddlStatus.SelectedIndex == 0 ? true : false;

                if (pic.UpdateGroupPIC(db))
                {
                    db.CommitTransaction();
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
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }


        private bool InsertPIC()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                GroupPICModels pic = new GroupPICModels();
                db.Open();
                db.BeginTransaction();

                pic.KdPic = string.Format("{0}{1:0000}", "A", pic.GetLatestKdPicNmbr(db));
                if (Request.QueryString["state"]=="Edit")
                {
                    pic.GroupNmbr = Convert.ToInt32(Session["GroupIdDetail"]);
                }
                else if (Request.QueryString["state"] == "NewGroupInfo")
                {
                    pic.GroupNmbr = Convert.ToInt32(Session["NewGroup"]);
                }
                pic.Title = ddlTitle.SelectedValue;
                pic.NamaPic = txtPicName.Text;
                pic.Jabatan = txtJabatan.Text;
                pic.IsActive = ddlStatus.SelectedIndex == 0 ?  true : false;


                if (pic.GroupNmbr > 0)
                {
                    if (pic.InsertGroupPIC(db))
                    {
                        db.CommitTransaction();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch
            {
                db.RollbackTransaction();
                return false;
            }
            finally
            {
                db.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session["KdPIC"].ToString() != "")
            {
                if (UpdatePIC())
                {
                    TabContainer groupContainer = locateTabContainer(Page);
                    if (groupContainer != null)
                    {
                        if (groupContainer.ActiveTabIndex > groupContainer.Tabs.Count - 1)
                        {
                            groupContainer.ActiveTabIndex = 1;
                        }
                    }
                }
            }
            else
            {
                if (InsertPIC())
                {
                    TabContainer groupContainer = locateTabContainer(Page);
                    if (groupContainer != null)
                    {
                        if (groupContainer.ActiveTabIndex > groupContainer.Tabs.Count - 1)
                        {
                            groupContainer.ActiveTabIndex = 1;
                        }
                    }
                }
            }
            
        }

        protected void btnSavePIC_Click(object sender, EventArgs e)
        {
            if (InsertPIC())
            {
                TabContainer groupContainer = locateTabContainer(Page);
                if (groupContainer != null)
                {
                    if (groupContainer.ActiveTabIndex < groupContainer.Tabs.Count - 1)
                    {
                        groupContainer.ActiveTabIndex = 1;
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

        
    }
}