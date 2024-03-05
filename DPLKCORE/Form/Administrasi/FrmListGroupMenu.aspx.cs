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
    public partial class FrmListGroupMenu : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            imgbtnCari.Click += ButtonClicked;
            imgbtnRefresh.Click += ButtonClicked;
            imgbtnAdd.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!Page.IsPostBack)
                DisplayData();
        }

        public void DisplayData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            Logic.Administrasi.GroupMenu gm = new Logic.Administrasi.GroupMenu();
            try
            {
                db.Open();

                gm.NamaGroup = txtNamaGroup.Text.Trim();
                var data = gm.GetGroupMenu(db);
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

        public void ButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (ib == imgbtnCari)
                DisplayData();
            else if (ib == imgbtnRefresh)
            {
                lblNotif.Visible = false;
                txtNamaGroup.Text = "";
                DisplayData();
            }
            else if (ib == imgbtnAdd)
                Response.Redirect(String.Format("~/Form/Administrasi/FrmGroupMenu.aspx?state={0}", ActionDef.ADD));
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridDisplay.PageIndex = e.NewPageIndex;
            DisplayData();
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = 0;

            if (e.CommandName == "editData")
            { 
                i = Convert.ToInt32(e.CommandArgument);
                String url = String.Format("~/Form/Administrasi/FrmGroupMenu.aspx?state={0}&id_group={1}",
                        ActionDef.EDIT, gridDisplay.Rows[i].Cells[0].Text.Trim());
                Response.Redirect(url);
            }
            else if (e.CommandName == "deleteData")
            {
                i = Convert.ToInt32(e.CommandArgument);

                Connection conn = new Connection();
                Database db = new Database(conn.ConnectionStringDBJiwa);
                Logic.Administrasi.GroupMenu gm = new Logic.Administrasi.GroupMenu();
                try
                {
                    db.Open();
                    db.BeginTransaction();

                    gm.IdGroup = gridDisplay.Rows[i].Cells[0].Text.Trim();
                    gm.DeleteGroupMenu(db);

                    db.CommitTransaction();
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
        }

        protected void OnGridDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                btnEdit.CommandArgument = e.Row.RowIndex.ToString();

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                btnDelete.CommandArgument = e.Row.RowIndex.ToString();                

                String status = e.Row.Cells[3].Text.Trim();
                if (status == "1")
                    e.Row.Cells[3].Text = "Aktif";
                else if (status == "0")
                    e.Row.Cells[3].Text = "Tidak Aktif";
            }
        }
    }
}