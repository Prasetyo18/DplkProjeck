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
    public partial class ClientFundInfo : System.Web.UI.UserControl
    {
        private int ClientId;
        private static Connection conn;
        private static Database db;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnInsertFund.Click += ButtonClicked;
            if (!IsPostBack)
            {
                conn = new Connection();
                db = new Database(conn.ConnectionStringPension);

                
                if (Request.QueryString["state"] == "Edit")
                {
                    ddlTypeOutput.SelectedIndex = 0;
                    LbTypeOutput.Text = ddlTypeOutput.SelectedItem.Text;
                    Setup();
                }
                else if (Request.QueryString["state"] == "NewClient")
                {
                    ddlTypeOutput.SelectedIndex = 0;
                    LbTypeOutput.Text = ddlTypeOutput.SelectedItem.Text;
                }   
            }
        }

        protected void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;
            if (ib == btnInsertFund)
            {
                FillGridview();
            }
        }

        private void FillGridview()
        {
            ClientFundModels fund = new ClientFundModels();
            try
            {
                if (ddlCerNmbr.Items.Count > 0)
                {
                    db.Open();
                    fund.CerNmbr = int.Parse(ddlCerNmbr.SelectedValue);
                    fund.AsOfDate = DateTime.Parse(txtAsOfDate.Text);
                    fund.GroupByType = short.Parse(ddlTypeOutput.SelectedValue);
                    GridviewFund.DataSource = fund.ReadFund(db);
                    GridviewFund.DataBind();
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

        private void Setup()
        {
            LoadCertifDDL();
            LoadClientNm();
            LbTypeOutput.Text = ddlTypeOutput.SelectedItem.Text;
        }

        public void LoadClientNm()
        {
            try
            {
                if (Request.QueryString["state"] == "Edit")
                {
                    ClientId = Convert.ToInt32(Session["ClientIdDetail"]);
                }
                else
                {
                    ClientId = Convert.ToInt32(Session["newClientId"]);
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

        public void LoadCertifDDL()
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
                ddlHelper.LoadDDLCertif(db, ddlCerNmbr, ClientId);
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

        protected void GridviewFund_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridviewFund_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void ddlTypeOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            LbTypeOutput.Text = ddlTypeOutput.SelectedItem.Text;
        }
    }
}