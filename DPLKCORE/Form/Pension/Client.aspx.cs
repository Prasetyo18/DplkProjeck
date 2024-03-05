using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DPLKCORE.Framework.Helper;
using DPLKCORE.Form.Pension.UserControl.ClientTabs;

namespace DPLKCORE.Form.Pension
{
    public partial class Client : System.Web.UI.Page
    {
        private string _dbip;
        private string _fullpath;
        private string _path;
        private string _ftprootpath; 
        private string _ftpuid; 
        private string _ftppwd;

        protected string path;
        private int _port;

        protected void Page_Init(object sender, EventArgs e)
        {
            btnClientAdd.Click += buttonClicked;
            btnClientUpdate.Click += buttonClicked;
            btnCancel.Click += buttonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                LoadIdentityToDDL();
                LoadDDLMarital();
                if (Request.QueryString["state"] == "Edit")
                {
                    btnClientUpdate.Style.Remove("display");
                    btnClientAdd.Style.Add("display", "none");

                    txtClientNmbr.Enabled = false;
                    txtClientNmbr.Text = Session["ClientIdDetail"].ToString();
                    LoadRecord();
                }
                else if (Request.QueryString["state"] == "NewClient")
                {
                    btnClientAdd.Style.Remove("display");
                    btnClientUpdate.Style.Add("display", "none");

                    txtClientNmbr.Enabled = false;
                    txtClientNmbr.Text = "New";

                }
                
            }
        }

        private void LoadRecord()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                ClientInfoModels clientModel = new ClientInfoModels();
                clientModel.ClientNmbr = int.Parse(txtClientNmbr.Text);
                Dictionary<string, object> data = clientModel.LoadById(db);
                if (data.Count > 0)
                {
                    if (data["client_nm"].ToString() != "" )
                    {
                        txtClientNm.Text = data["client_nm"].ToString();
                    }
                    if (data["identity_type"].ToString() != "")
                    {
                        ddlIdentity.SelectedIndex = ddlIdentity.Items.IndexOf(ddlIdentity.Items.FindByText(data["identity_type"].ToString()));
                    }
                    if (data["identity_nmbr"].ToString() != "")
                    {
                        txtIdentity.Text = data["identity_nmbr"].ToString();
                    }
                    if (data["gender"].ToString() != "")
                    {
                        ddlGender.SelectedValue = data["gender"].ToString();
                    }
                    if (data["birth_dt"].ToString() != "")
                    {
                        txtbirthDate.Text = data["birth_dt"].ToString();
                    }
                    if (data["dob_place"].ToString() != "")
                    {
                        txtPlace.Text = data["dob_place"].ToString();
                    }
                    if (data["marital_status_nmbr"].ToString() != "")
                    {
                        ddlMartialStatus.SelectedValue = data["marital_status_nmbr"].ToString();
                    }
                    if (data["maiden_nm"].ToString() != "")
                    {
                        txtMaidenName.Text = data["maiden_nm"].ToString();
                    }
                    if (data["email_addr"].ToString() != "")
                    {
                        txtEmail.Text = data["email_addr"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;
            if (ib == btnClientAdd)
            {
                InsertClient();
                ClientCertificateInfo certificate = ControlHelper.FindControlRecursive<ClientCertificateInfo>(this.Page, "UCCertificateInfo");
                ClientFundInfo fund = ControlHelper.FindControlRecursive<ClientFundInfo>(this.Page, "UCFundInfo");
                
                if (certificate != null)
                {
                    certificate.LoadClientNm();
                }

                if (fund != null)
                {
                    fund.LoadClientNm();
                }

                
                TabHelper.NextTab(this.Page);
            }

            if (ib == btnClientUpdate)
            {

            }

            if (ib == btnCancel)
            {
                Response.Redirect("ClientList.aspx", false);
            }
        }

        #region load data

        private void LoadIdentityToDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            ClientInfoModels ddlHelper = new ClientInfoModels();

            db.Open();
            ddlHelper.LoadDDLIndentity(ddlIdentity, db);


            db.Close();
        }
        private void LoadDDLMarital()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            ClientInfoModels ddlHelper = new ClientInfoModels();

            db.Open();
            ddlHelper.LoadDDLMarital(ddlMartialStatus, db);


            db.Close();
        }

        #endregion

        #region insert and update transaction
        private void prepareFTP()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                db.Open();
                ClientInfoModels client = new ClientInfoModels();
                Dictionary<string, object> result = client.PrepareFTP(db);

                if (result.Count > 0)
                {
                    if (result["IN_DBFTP_IP"].ToString() != "")
                    {
                        _dbip = result["IN_DBFTP_IP"].ToString();
                    }

                    if (result["IN_DBFTP_PORT"].ToString() != "")
                    {
                        _port = Convert.ToInt32(result["IN_DBFTP_PORT"]);
                    }

                    if (result["IN_DBFTP_UID"].ToString() != "")
                    {
                        _ftpuid = result["IN_DBFTP_UID"].ToString();
                    }

                    if (result["IN_DBFTP_UID"].ToString() != "")
                    {
                        _ftppwd = result["IN_DBFTP_UID"].ToString();
                    }

                    _path = Request.PhysicalApplicationPath + "Upload\\";
                    _ftprootpath = Request.PhysicalApplicationPath;
                }

                ViewState["path"] = _path;
                ViewState["dbip"] = _dbip;
                ViewState["port"] = _port;
                ViewState["ftpuid"] = _ftpuid;
                ViewState["ftppwd"] = _ftppwd;
                ViewState["ftprootpath"] = _ftprootpath;

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

        private void UploadFile()
        {
            string fileName;

            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];

            if (userPostedFile.ContentLength > 0)
            {
                fileName = HttpContext.Current.Request.UserHostName.Replace(":", "") + "_" + Path.GetFileName(userPostedFile.FileName);
                _fullpath = _path + fileName;

                if (File.Exists(_fullpath))
                {
                    File.Delete(_fullpath);
                }
                path = _fullpath;
                userPostedFile.SaveAs(_fullpath);
            }
        }

        protected void InsertClient()
        {
            if (fuNPWP.HasFile)
            {
                prepareFTP();
                UploadFile();

                string fileName = Path.GetFileName(fuNPWP.FileName);
                string ext = Path.GetExtension(fileName);
            }

            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                ClientInfoModels clientModel = new ClientInfoModels();


                db.Open();
                db.BeginTransaction();

                //clientnmbr is null because this is insert function.
                clientModel.ClientNm = txtClientNm.Text;
                clientModel.IdentityType = Convert.ToInt16(ddlIdentity.SelectedValue);
                clientModel.IdentityNmbr = txtIdentity.Text;
                clientModel.Gender = ddlGender.SelectedValue;
                clientModel.BirthDt = Convert.ToDateTime(txtbirthDate.Text);
                clientModel.DobPlace = txtPlace.Text;
                clientModel.MaritalStatusNmbr = Convert.ToInt16(ddlMartialStatus.SelectedValue);
                clientModel.MaidenNm = txtMaidenName.Text;
                clientModel.EmailAddr = txtEmail.Text;
                clientModel.Path = path;

                Session["newClientId"] = clientModel.InsertClient(db).ToString();
                db.CommitTransaction();
                txtClientNmbr.Text = Session["newClientId"].ToString();

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
        #endregion  
    }
}
