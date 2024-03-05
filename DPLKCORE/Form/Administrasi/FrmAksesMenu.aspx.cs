using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DPLKCORE.Framework;
using DPLKCORE.Class;
using DPLKCORE.Logic.Administrasi;

namespace DPLKCORE.Form.Administrasi
{
    public partial class FrmAksesMenu : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            imgbtnSave.Click += ButtonClicked;
            imgbtnCancel.Click += ButtonClicked;
            tambahAksesMenu.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["state"] == ActionDef.EDIT)
                {
                    DisplayData();

                    if(gridDisplay.Visible == true)
                        imgbtnSave.Visible = true;
                    else
                        imgbtnSave.Visible = false;
                }
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (ib == imgbtnSave)
                SaveData();
            else if (ib == imgbtnCancel)
                Response.Redirect("~/Form/Administrasi/FrmListUser.aspx?id_menu=ADM005");
            else if(ib == tambahAksesMenu)
                AddAksesMenu();
        }

        private void DisplayData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            AksesMenu am = new AksesMenu();
            am.User = new User();

            try
            {
                db.Open();

                am.User.IdUser = Request.QueryString["id_profile"];
                lblId.Text = am.User.IdUser;
                var data = am.GetAksesMenu(db);
                gridDisplay.DataSource = data;
                gridDisplay.DataBind();
            }
            catch (Exception ex)
            {
                lblNotif.Visible = true;
                lblNotif.ForeColor = System.Drawing.Color.Red;
                lblNotif.Text = ex.Message;
            }
            finally
            {
                db.Close();
            }
        }

        private void SaveData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            AksesMenu am = new AksesMenu();
            try
            {
                db.Open();
                db.BeginTransaction();

                if (lblNotif.ForeColor != System.Drawing.Color.Red)
                {
                    imgbtnSave.Visible = true;
                    am.IdUser = lblId.Text.Trim();
                    foreach (GridViewRow row in gridTambah.Rows)
                    {
                        DropDownList ddlParentMenu = (DropDownList)row.FindControl("ddlParentMenu");
                        DropDownList ddlMenu = (DropDownList)row.FindControl("ddlMenu");

                        if (String.IsNullOrEmpty(ddlMenu.SelectedValue.Trim()))
                            am.IdMenu = ddlParentMenu.SelectedValue.Trim();
                        else
                            am.IdMenu = ddlMenu.SelectedValue.Trim();
                        
                        am.InsertAksesMenu(db);
                        break;
                    }

                    db.CommitTransaction();

                    lblNotif.Visible = true;
                    lblNotif.Text = "Data Berhasil Disimpan";
                    lblNotif.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();

                lblNotif.Visible = true;
                lblNotif.Text = "Error : " + ex.Message;
                lblNotif.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                db.Close();
            }

            DisplayData();
        }

        private string GetIdGroup(Database db)
        {
            GroupMenu gm = new GroupMenu();
            int nourut = gm.GetNoUrutGm(db);
            String idGroup = "GRM" + nourut;

            return idGroup;
        }

        protected void OnGroupSelectedIndexChanged(object sender, EventArgs e)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            Logic.Administrasi.Menu m = new Logic.Administrasi.Menu();
            m.User = new User();

            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlGroupMenu = (DropDownList)row.FindControl("ddlGroupMenu");
            DropDownList ddlParentMenu = (DropDownList)row.FindControl("ddlParentMenu");
            DropDownList ddlMenu = (DropDownList)row.FindControl("ddlMenu");

            try
            {
                db.Open();
                m.IdGroup = ddlGroupMenu.SelectedValue.Trim();
                m.LoadParentToDDL(ddlParentMenu, db);
                db.Close();

                if(!String.IsNullOrEmpty(ddlParentMenu.SelectedValue.Trim()))
                {
                    db.Open();
                    ddlParentMenu.Enabled = true;
                    
                    m.IdParent = ddlParentMenu.SelectedValue.Trim();
                    m.User.IdUser = lblId.Text.Trim();
                    m.LoadMenuToDDL(ddlMenu, db);
                    db.Close();

                }
                else
                {
                    db.Open();
                    ddlParentMenu.Enabled = false;
                    //db.Close();

                    //db.Open();
                    m.IdParent = ddlParentMenu.SelectedValue.Trim();
                    m.User.IdUser = lblId.Text.Trim();
                    m.IdMenu = Request.QueryString["id_menu"];
                    m.LoadMenuToDDL(ddlMenu, db);
                    db.Close();

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        protected void OnParentSelectedIndexChanged(object sender, EventArgs e)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            Logic.Administrasi.Menu m = new Logic.Administrasi.Menu();
            m.User = new User();

            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlParentMenu = (DropDownList)row.FindControl("ddlParentMenu");
            DropDownList ddlMenu = (DropDownList)row.FindControl("ddlMenu");

            
            try
            {
                db.Open();

                if(ddlParentMenu.Enabled == true)
                {
                    m.IdParent = ddlParentMenu.SelectedValue.Trim();
                    m.User.IdUser = lblId.Text.Trim();
                    m.LoadMenuToDDL(ddlMenu, db);
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

        private void AddAksesMenu()
        {
            List<AksesMenu> list = new List<AksesMenu>();
            AksesMenu am = new AksesMenu();

            foreach (GridViewRow row in gridTambah.Rows)
            {
                am = new AksesMenu();
                DropDownList ddlGroupMenu = (DropDownList)row.FindControl("ddlGroupMenu");
                DropDownList ddlParentMenu = (DropDownList)row.FindControl("ddlParentMenu");
                DropDownList ddlMenu = (DropDownList)row.FindControl("ddlMenu");

                am.NamaGroup = ddlGroupMenu.SelectedValue.Trim();
                am.NamaParent = ddlParentMenu.SelectedValue.Trim();
                am.NamaMenu = ddlMenu.SelectedValue.Trim();

                list.Add(am);
            }

            am = new AksesMenu();
            am.NamaGroup = "";
            am.NamaParent = "";
            am.NamaMenu = "";

            list.Add(am);
            gridTambah.DataSource = list;
            gridTambah.DataBind();
        }

        protected void OnGridDisplayDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Logic.Administrasi.AksesMenu am = (Logic.Administrasi.AksesMenu)e.Row.DataItem;

                int i = (gridDisplay.PageIndex * gridDisplay.PageSize) + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = i.ToString();
                e.Row.Cells[1].Text = am.Menu.NamaGroup;
                e.Row.Cells[2].Text = am.Menu.NamaParent;
                e.Row.Cells[3].Text = am.Menu.NamaMenu;
                e.Row.Cells[4].Text = am.Menu.IdMenu;

                ImageButton btnHapusAksesMenu = (ImageButton)e.Row.FindControl("btnHapusAksesMenu");
                btnHapusAksesMenu.CommandArgument = e.Row.RowIndex.ToString();
            }
        }

        protected void OnGridDisplayRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "hapusAksesMenu")
            {
                Connection conn = new Connection();
                Database db = new Database(conn.ConnectionStringDBJiwa);
                AksesMenu am = new AksesMenu();
                am.User = new User();
                try
                {
                    db.Open();
                    db.BeginTransaction();

                    am.User.IdUser = lblId.Text.Trim();
                    am.IdMenu = gridDisplay.Rows[i].Cells[4].Text.Trim();
                    am.DeleteAksesMenu(db);

                    db.CommitTransaction();
                    lblNotif.Text = "Hak Akses " + gridDisplay.Rows[i].Cells[3].Text.Trim() + " Berhasil Dihapus";
                    lblNotif.Visible = true;
                    lblNotif.ForeColor = System.Drawing.Color.Green;

                    var data = am.GetAksesMenu(db);
                    gridDisplay.DataSource = data;
                    gridDisplay.DataBind();
                }
                catch (Exception ex)
                {
                    db.RollbackTransaction();

                    lblNotif.Visible = true;
                    lblNotif.Text = "Error : " + ex.Message;
                    lblNotif.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        protected void OnGridTambahDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Logic.Administrasi.User u = (Logic.Administrasi.User)e.Row.DataItem;

                int i = (gridTambah.PageIndex * gridTambah.PageSize) + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = i.ToString();

                DropDownList ddlGroupMenu = (DropDownList)e.Row.FindControl("ddlGroupMenu");
                DropDownList ddlParentMenu = (DropDownList)e.Row.FindControl("ddlParentMenu");
                DropDownList ddlMenu = (DropDownList)e.Row.FindControl("ddlMenu");

                Connection conn = new Connection();
                Database db = new Database(conn.ConnectionStringDBJiwa);
                GroupMenu gm = new GroupMenu();
                try
                {
                    db.Open();
                    gm.LoadGroupToDDL(ddlGroupMenu, db);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    db.Close();
                }

                tambahAksesMenu.Visible = false;
            }
        }
    }
}