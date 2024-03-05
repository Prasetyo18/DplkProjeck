using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;
using System.Text;
using DPLKCORE.Framework;
using DPLKCORE.Logic;
using DPLKCORE.Class;

namespace DPLKCORE.Form.Administrasi
{
    public partial class FrmListUser : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            imgbtnCari.Click += ButtonClickedButton;
            imgbtnRefresh.Click += ButtonClicked;
            imgbtnAdd.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!Page.IsPostBack)
                DisplayData();

            if (Request.QueryString["id_menu"] == "ADM002")
            {
                imgbtnAdd.Visible = true;
                Label1.Text = "LIST USER";
            }
            else if (Request.QueryString["id_menu"] == "ADM005")
            {
                imgbtnAdd.Visible = false;
                Label1.Text = "LIST HAK AKSES";
            }
        }

        public void DisplayData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            Logic.Administrasi.User u = new Logic.Administrasi.User();

            try
            {
                db.Open();

                u.NamaLengkap = txtNamaLengkap.Text.Trim();
                var data = u.GetUser(db);
                gridDisplay.DataSource = data;
                gridDisplay.DataBind();
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

        public void ButtonClickedButton(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (b == imgbtnCari)
                DisplayData();

           
        }

        public void ButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (ib == imgbtnRefresh)
            {
                lblNotif.Visible = false;
                txtNamaLengkap.Text = "";
                DisplayData();
            }
            else if (ib == imgbtnAdd)
                Response.Redirect(String.Format("~/Form/Administrasi/FrmUser.aspx?state={0}", ActionDef.ADD));
        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridDisplay.PageIndex = e.NewPageIndex;
            DisplayData();
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = 0;
            String idmenu = Request.QueryString["id_menu"];
            String url = "";

            if (e.CommandName == "editData")
            { 
                i = Convert.ToInt32(e.CommandArgument);
                if (idmenu == "ADM002")
                {
                    url = String.Format("~/Form/Administrasi/FrmUser.aspx?state={0}&id_profile={1}",
                        ActionDef.EDIT, gridDisplay.Rows[i].Cells[1].Text.Trim());
                }
                else if (idmenu == "ADM005")
                {
                    url = String.Format("~/Form/Administrasi/FrmAksesMenu.aspx?id_menu={0}&state={1}&id_profile={2}",
                        "ADM005", ActionDef.EDIT, gridDisplay.Rows[i].Cells[1].Text.Trim());
                }
                
                Response.Redirect(url);
            }
            else if (e.CommandName == "deleteData")
            {
                i = Convert.ToInt32(e.CommandArgument);

                Connection conn = new Connection();
                Database db = new Database(conn.ConnectionStringDBJiwa);
                Logic.Administrasi.User u = new Logic.Administrasi.User();
                Logic.Administrasi.AksesMenu am = new Logic.Administrasi.AksesMenu();
                try
                {
                    db.Open();
                    db.BeginTransaction();

                    u.IdUser = gridDisplay.Rows[i].Cells[1].Text.Trim();
                    am.IdUser = u.IdUser;
                    am.DeleteUserAksesMenu(db);
                    u.DeleteUser(db);

                    db.CommitTransaction();

                    lblNotif.Text = "Data Berhasil Dihapus";
                    lblNotif.Visible = true;
                    lblNotif.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    db.RollbackTransaction();

                    lblNotif.Text = "Error : " + ex.Message;
                    lblNotif.Visible = true;
                    lblNotif.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        protected void OnGridDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Logic.Administrasi.User u = (Logic.Administrasi.User)e.Row.DataItem;

                int i = (gridDisplay.PageIndex * gridDisplay.PageSize) + e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = i.ToString();

                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.CommandArgument = e.Row.RowIndex.ToString();

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                btnDelete.CommandArgument = e.Row.RowIndex.ToString();

                String status = e.Row.Cells[5].Text.Trim();
                if (status == "1")
                    e.Row.Cells[5].Text = "Aktif";
                else if (status == "0")
                    e.Row.Cells[5].Text = "Tidak Aktif";
            }
        }
    }
}