using DPLKCORE.Framework;
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
    public partial class StatusAndSalary : System.Web.UI.UserControl
    {
        private int ClientId;
        private Connection conn;
        private Database db;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);
            btnGo.Click += ButtonClicked;

            if (!IsPostBack)
            {
                Setup();
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;
            if (ib == btnGo)
            {
                LoadData();
            }
        }

        public void Setup()
        {
            LoadCertifCode();
            LoadClientName();
        }

        private void LoadData()
        {
            LoadStatHistory();
            LoadSalaryHistory();
            LoadGroupCompanyTxt();
        }

        private void LoadGroupCompanyTxt()
        {
            StatusAndSalaryModels stat = new StatusAndSalaryModels();
            try
            {
                if (ddlCertifNmbr.Items.Count > 0)
                {
                    db.Open();
                    stat.CertifNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                    stat.GetGroupCompanyTxt(db, txtGroupNmbr, txtCompanyNm);
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

        private void LoadCertifCode()
        {
            try
            {
                if (Request.QueryString["state"] == "Edit")
                {
                    ClientId = Convert.ToInt32(Session["ClientIdDetail"]);
                }
                else if (Request.QueryString["state"] == "NewClient")
                {
                    ClientId = Convert.ToInt32(Session["newClientId"]);
                }
                
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadDDLCertif(db, ddlCertifNmbr, ClientId);
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

        private void LoadClientName()
        {
            try
            {
                if (Request.QueryString["state"] == "NewClient")
                {
                    ClientId = Convert.ToInt32(Session["newClientId"]);
                }
                else if (Request.QueryString["state"] == "Edit")
                {
                    ClientId = Convert.ToInt32(Session["ClientIdDetail"]);
                }
                db.Open();
                DDLClient ddlHelper = new DDLClient();
                ddlHelper.LoadClientNm(db, txtClientNm, ClientId);
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

        protected void GridviewStatHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridviewStatHistory.PageIndex = e.NewPageIndex;
            LoadStatHistory();
        }


        protected void GridviewSalaryHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridviewSalaryHistory.PageIndex = e.NewPageIndex;
            LoadSalaryHistory();
        }

        #region loadGridviews
        private void LoadStatHistory()
        {
            StatusAndSalaryModels stat = new StatusAndSalaryModels();
            try
            {
                if (ddlCertifNmbr.Items.Count > 0)
                {
                    db.Open();
                    stat.CertifNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                    DataTable dt = stat.LoadGvStatusHistory(db);
                    GridviewStatHistory.DataSource = dt;
                    GridviewStatHistory.DataBind();
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

        private void LoadSalaryHistory()
        {
            StatusAndSalaryModels stat = new StatusAndSalaryModels();
            try
            {
                if (ddlCertifNmbr.Items.Count > 0)
                {
                    db.Open();
                    stat.CertifNmbr = int.Parse(ddlCertifNmbr.SelectedValue);
                    DataTable dt = stat.LoadGvSalaryHistory(db);
                    GridviewSalaryHistory.DataSource = dt;
                    GridviewSalaryHistory.DataBind();
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
        #endregion  
    }
}