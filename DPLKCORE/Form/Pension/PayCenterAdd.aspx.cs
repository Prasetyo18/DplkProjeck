using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.Pension
{
    public partial class PayCenterAdd : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnInsertPaycenter.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                LoadDDLPaycenter();
                LoadDDLMasterPaycenter();
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;

            if (ib == btnInsertPaycenter)
            {
                InsertPayCenter();
            }
        }

        private void LoadDDLPaycenter()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLPaycenter ddlHelper = new DDLPaycenter();

            db.Open();
            ddlHelper.LoadDataParamToDDL(ddlPayName, db);
            db.Close();
        }

        private void LoadDDLMasterPaycenter()
        {
            ddlMaster.Items.Clear();
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDLMasterPaycenter ddlHelper = new DDLMasterPaycenter();

            db.Open();
            ddlHelper.LoadDataParamToDDL(ddlMaster, db, int.Parse(ddlPayName.SelectedValue));
        }

        private void InsertPayCenter()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);

            try
            {
                PaycenterModel c = new PaycenterModel();

                db.Open();
                db.BeginTransaction();

                c.PaycenterNm = txtPaycenterNm.Text;
                c.ClientNmbr = int.Parse(ddlPayName.SelectedValue);
                c.MasterPaycenterNmbr = int.Parse(ddlMaster.SelectedValue);
                c.ContactPerson = txtContactPerson.Text;

                c.InsertPayCenter(db);
                db.CommitTransaction();
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

        protected void ddlPayName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDDLMasterPaycenter();
        }
    }
}
