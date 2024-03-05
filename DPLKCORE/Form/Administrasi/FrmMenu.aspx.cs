using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Class;
using DPLKCORE.Logic.Administrasi;

namespace DPLKCORE.Form.Administrasi
{
    public partial class FrmMenu : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            imgbtnSave.Click += ButtonClicked;
            imgbtnCancel.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                LoadGroupToDDL();
                lblgroup.Visible = false;
                ddlGroupMenu.Visible = true;

                if (Request.QueryString["state"] == ActionDef.EDIT)
                {
                    DisplayData();
                    lblgroup.Visible = true;
                    ddlGroupMenu.Visible = false;
                }
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (ib == imgbtnSave)
                SaveData();
            else if (ib == imgbtnCancel)
                Response.Redirect("~/Form/Administrasi/FrmListMenu.aspx?id_menu=ADM004");
        }

        private void DisplayData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            Logic.Administrasi.Menu m = new Logic.Administrasi.Menu();
            m.User = new Logic.Administrasi.User();
            try
            {
                db.Open();

                m.IdMenu = Request.QueryString["id_menu"];
                var data = m.GetListMenu(db).FirstOrDefault();

                lblgroup.Text = data.NamaGroup;
                rblLevel.SelectedIndex = data.Level;
                txtNamaMenu.Text = data.NamaMenu;
                rblStatus.SelectedIndex = data.StatusMenu;
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

        private void SaveData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            Logic.Administrasi.Menu m = new Logic.Administrasi.Menu();

            try
            {
                db.Open();

                m.IdGroup = ddlGroupMenu.SelectedValue.Trim();
                var data = m.GetGroupMenu(db).FirstOrDefault();

                db.Close();

                try
                {
                    db.Open();
                    db.BeginTransaction();

                    string prefix = data.Prefix.Trim();
                    int nourut = GetNoUrutMenu(prefix);
                    if (rblLevel.SelectedIndex == 1)
                        m.IdParent = ddlParentMenu.SelectedValue.Trim();

                    m.NamaMenu = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNamaMenu.Text.Trim());
                    m.URL = txtUrl.Text.Trim();
                    m.Level = rblLevel.SelectedIndex;
                    m.StatusMenu = rblStatus.SelectedIndex;

                    if (Request.QueryString["state"] == "add")
                    {
                        if (nourut < 10)
                            m.IdMenu = data.Prefix.Trim() + "0" + nourut.ToString();
                        else
                            m.IdMenu = data.Prefix.Trim() + nourut.ToString();

                        m.InsertMenu(db);
                    }
                    else
                        m.UpdateGroupMenu(db);

                    db.CommitTransaction();

                    lblNotif.Visible = true;
                    lblNotif.Text = "Data Berhasil Disimpan";
                    lblNotif.ForeColor = System.Drawing.Color.Green;
                    imgbtnSave.Visible = false;
                }
                catch (Exception ex)
                {
                    db.RollbackTransaction();

                    lblNotif.Visible = true;
                    lblNotif.Text = "Error : " + ex.Message;
                    lblNotif.ForeColor = System.Drawing.Color.Red;
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int GetNoUrutMenu(string pre)
        {
            Logic.Administrasi.Menu m = new Logic.Administrasi.Menu();
            int nourut = m.GetNoUrutMenu(pre);
            return nourut;
        }

        public void LoadGroupToDDL()
        {
            //create object
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            GroupMenu gm = new GroupMenu();
            Logic.Administrasi.Menu m = new Logic.Administrasi.Menu();

            db.Open();
            gm.LoadGroupToDDL(ddlGroupMenu, db);
            db.Close();
        }

        protected void OnGroupSelectedIndexChanged(object sender, EventArgs e)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            Logic.Administrasi.Menu m = new Logic.Administrasi.Menu();
            try
            {
                db.Open();
                m.IdGroup = ddlGroupMenu.SelectedValue.Trim();
                m.LoadParentToDDL(ddlParentMenu, db);
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

        protected void OnLevelSelectedIndexChanged(object sender, EventArgs e)
        {
            if(rblLevel.SelectedIndex == 1)
            {
                Label1.Visible = true;
                ddlParentMenu.Visible = true;

                Connection conn = new Connection();
                Database db = new Database(conn.ConnectionStringDBJiwa);
                Logic.Administrasi.Menu m = new Logic.Administrasi.Menu();
                try
                {
                    db.Open();
                    m.IdGroup = ddlGroupMenu.SelectedValue.Trim();
                    m.LoadParentToDDL(ddlParentMenu, db);
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
            else
            {
                Label1.Visible = false;
                ddlParentMenu.Visible = false;
            }
        }
    }
}