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
    public partial class FrmGroupMenu : System.Web.UI.Page
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
                if (Request.QueryString["state"] == ActionDef.EDIT)
                    DisplayData();
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (ib == imgbtnSave)
                SaveData();
            else if (ib == imgbtnCancel)
                Response.Redirect("~/Form/Administrasi/FrmListGroupMenu.aspx");
        }

        private void DisplayData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            GroupMenu gm = new GroupMenu();
            try
            {
                db.Open();

                gm.IdGroup = Request.QueryString["id_group"];
                var data = gm.GetGroupMenu(db).FirstOrDefault();
                txtNamaGroup.Text = data.NamaGroup;
                rblStatus.SelectedIndex = data.StatusGroup;
                txtPrefix.Text = data.Prefix;
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
            GroupMenu gm = new GroupMenu();
            try
            {
                db.Open();
                db.BeginTransaction();

                gm.NamaGroup = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNamaGroup.Text.Trim());
                gm.StatusGroup = rblStatus.SelectedIndex;
                gm.Prefix = txtPrefix.Text.Trim().ToUpper();

                if(Request.QueryString["state"] == "add")
                {
                    gm.IdGroup = GetIdGroup(db);
                    gm.Urutan = Convert.ToInt16(gm.IdGroup.Substring(3, 2));
                    gm.InsertGroupMenu(db);
                }
                else
                {
                    gm.IdGroup = Request.QueryString["id_group"];
                    gm.UpdateGroupMenu(db);
                }

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
            finally
            {
                db.Close();
            }
        }

        private string GetIdGroup(Database db)
        {
            GroupMenu gm = new GroupMenu();
            int nourut = gm.GetNoUrutGm(db);
            String idGroup = "GRM" + nourut;

            return idGroup;
        }
    }
}