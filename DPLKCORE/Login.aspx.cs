using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Class;
using DPLKCORE.Logic;

using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

using System.Net.Sockets;

namespace DPLKCORE
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            imgbtnLogin.Click += ButtonClicked;
        }

        private void DoLogin(String username, String password)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                // create login instance
                Auth auth = new Auth();
                auth.IdPemakai = username;
                auth.Password = password;

                //auth.IdPemakai = txtUsername.Text.Trim();
                //auth.Password = txtPassword.Text.Trim();

                // try to login
                try
                {
                    int result = auth.DoLogin();

                    // if login failed then show error message
                    // else load form peserta and close login form
                    if (result != Auth.AUTH_OK)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = auth.ErrMessage(result);
                        lblMessage.CssClass = "label label-danger";

                        if (result == Auth.PASSWORD_DOESNT_MATCH)
                        {
                            CekLoginLimit();
                            txtPassword.Focus();
                        }
                    }
                    else
                    {
                        Connection cn = new Connection();
                        Framework.Database db = new Framework.Database(cn.ConnectionStringDBJiwa);


                        Auth ul = new Auth();

                        db.Open();
                        db.BeginTransaction();

                        ul.IdPemakai = txtUsername.Text.Trim();
                        ul.DeleteLoginLimitation(db);

                        ul.ActivityType = 1;//Login
                        //ul.IPAddress = GetIpAddress(Request.UserHostAddress);
                        ul.IPAddress = GetIpAddress();
                        ul.IdPemakai = txtUsername.Text.Trim();
                        ul.Info = "Login OK";
                        ul.InserT_Log_Activity(db);

                        db.CommitTransaction();
                        // login to system
                        String url = "~/Default.aspx";
                        Response.Redirect(url, true);
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Login Error."
                    + Environment.NewLine
                    + ex.Message;
                }
            }
        }

        private void CekLoginLimit()
        {

            Auth usr = new Auth();
            usr.IdPemakai = txtUsername.Text.Trim();
            //usr.IPAddress = GetIpAddress(Request.UserHostAddress);
            usr.IPAddress = GetIpAddress();
            List<Auth> datalimit = usr.GetDataUserLimitation();
            SaveLoginLimitation(datalimit, usr.IPAddress);


        }

        private void SaveLoginLimitation(List<Auth> dataLimit, String IP)
        {

            Connection cn = new Connection();
            Framework.Database db = new Framework.Database(cn.ConnectionStringDBJiwa);

            db.Open();
            db.BeginTransaction();

            Auth u = new Auth();

            try
            {

                if (dataLimit.Count > 0)
                {

                    u.IPAddress = dataLimit[0].IPAddress;
                    u.IdPemakai = dataLimit[0].IdPemakai;
                    u.Attempt = dataLimit[0].Attempt + 1;

                    if (u.Attempt >= 3)
                    {
                        u.statusEmail = 0;//terkunci
                        u.Info = "Account Locked";
                    }
                    else
                    {
                        u.statusEmail = 1;
                        u.Info = "Login Failed";
                    }

                    u.UpdateLoginLimitation(db);


                    u.ActivityType = 1;//Login
                    u.IPAddress = IP;
                    u.IdPemakai = txtUsername.Text.Trim();
                    u.InserT_Log_Activity(db);
                }
                else
                {
                    u.IPAddress = IP;
                    u.IdPemakai = txtUsername.Text.Trim();
                    u.Attempt = 1;
                    u.statusEmail = 1;
                    u.InsertLoginLimitation(db);

                    u.ActivityType = 1;
                    u.Info = "Login Failed";
                    u.InserT_Log_Activity(db);
                }

                db.CommitTransaction();
            }
            catch (Exception)
            {
                db.RollbackTransaction();
                throw;
            }
            finally
            {
                db.Close();
            }

        }

        private void DoLoginOld()
        {
            Page.Validate();
            if (Page.IsValid)
            {
                // create login instance
                Auth auth = new Auth();
                auth.IdPemakai = txtUsername.Text.Trim();
                auth.Password = txtPassword.Text.Trim();

                // try to login
                try
                {
                    int result = auth.DoLogin();

                    // if login failed then show error message
                    // else load form peserta and close login form
                    if (result != Auth.AUTH_OK)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = auth.ErrMessage(result);
                        lblMessage.CssClass = "label label-danger";

                        if (result == Auth.USER_DOESNT_EXIST)
                        {
                            txtUsername.Focus();
                        }
                        else if (result == Auth.PASSWORD_DOESNT_MATCH)
                        {
                            txtPassword.Focus();
                        }
                    }
                    else
                    {
                        // login to system
                        String url = "~/Default.aspx";
                        Response.Redirect(url, true);
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Login Error."
                    + Environment.NewLine
                    + ex.Message;
                }
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (ib == imgbtnLogin)
            {
                //DoLogin();
                //LoginAPI();
                cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim());
                if (cptCaptcha.UserValidated)
                {
                    String Login = LoginAPI();

                    if (Login == "locked")
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Akun anda terkunci silahkan hubungi IT";
                        lblMessage.CssClass = "label label-danger";
                        //CekLoginLimit();
                    }
                    else
                    {
                        DoLogin(Login, txtPassword.Text);
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "CAPTCHA Tidak Valid";
                    lblMessage.CssClass = "label label-danger";
                }
                

                
            }
        }

        private String GetIpAddress()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }

        private String GetIpAddressOld(string userip)
        {
            //userip = Request.UserHostAddress;
            String IPUSR = userip;
            if (Request.UserHostAddress != null)
            {
                Int64 info = new Int64();
                string Src = info.ToString("X");
                if (Src == "0")
                {
                    if (userip == "127.0.0.1")
                    {
                        //Response.Write("visited Localhost!");
                        IPUSR = userip;
                    }
                    else
                    {
                        IPUSR = userip;
                    }
                }
            }
            return IPUSR;
        } 

        public String LoginAPI()
        {
            String email = "";
            String status = "";
            try
            {

                JObject o = JObject.FromObject(new
                {
                    email = txtUsername.Text,
                    password = txtPassword.Text
                });

                String postdata = o.ToString();
                //Connection con = new Connection();

                String URL = "";

                //if (con.ConnectionStringTuguInsurance.Contains("repository"))
                //{
                //    URL = "http://192.168.38.181:8011/api/logindetail";
                //}
                //else
                //{
                //URL = "http://192.168.38.181:8011/api/logindetail";

                URL = "http://192.168.38.181:8011/api/logindetaildev";
                //}

                Auth auth = new Auth();
                auth.IdPemakai = txtUsername.Text;
                //auth.IPAddress = GetIpAddress(Request.UserHostAddress);
                auth.IPAddress = GetIpAddress();
                List<Auth> datalimit = auth.GetDataUserLimitation();

                if (datalimit.Count > 0 && datalimit[0].statusEmail == 0 && datalimit[0].Attempt >= 3)
                {
                    lblMessage.Visible = true;
                    lblMessage.CssClass = "label label-danger";
                    lblMessage.Text = "Akun anda terkunci silahkan hubungi IT";
                    email = "locked";
                }
                else
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = postdata.Length;
                    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                    requestWriter.Write(postdata);
                    requestWriter.Close();

                    // get the response
                    WebResponse webResponse = request.GetResponse();
                    Stream webStream = webResponse.GetResponseStream();
                    StreamReader responseReader = new StreamReader(webStream);
                    string response = responseReader.ReadToEnd();

                    var jsonLinq = JObject.Parse(response);

                    List<JToken> results = jsonLinq.Children().ToList();

                    status = results[0].ToString();

                    //foreach (JProperty item in results)
                    //{

                    //    item.CreateReader();

                    //    //var jsonLinqValue = JObject.Parse(item.Value.ToString());
                    //    //List<JToken> resultsValue = jsonLinqValue.Children().ToList();


                    //    if (item.Name == "success")
                    //    {
                    //        status = item.Value.ToString();                        
                    //    }
                    //}

                    if (status.ToLower() == "\"success\": true")
                    {
                        email = txtUsername.Text;
                    }
                    //else if (datalimit.Count > 0 && datalimit[0].Attempt >= 2)
                    //{
                    //    email = "locked";
                    //}
                    //if (status.ToLower() == "\"success\": false" && datalimit[0].Attempt < 2)
                    else
                    {
                        email = "false";
                    }
                    //else
                    //{ email = "false"; }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

            return email;
        }
    }
}