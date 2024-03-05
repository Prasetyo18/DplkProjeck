using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Class;
using DPLKCORE.Logic;
using DPLKCORE.Logic.Administrasi;

using System.Net;
using System.Net.Sockets;

namespace DPLKCORE
{
    public partial class SiteBu : System.Web.UI.MasterPage
    {
        String SessionMainMenu = "session.main.menu.";
        String SessionMenu = "session.menu.";

        protected void Page_Init(object sender, EventArgs e)
        {
            imgbtnLogout.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayConnection();

            if (Session.Count > 0)
            {
                try
                {
                    var host = "";
                    //var host = System.Net.Dns.GetHostEntry(Request.ServerVariables["REMOTE_HOST"]);
                    //System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(Request.UserHostAddress);
                    //String[] comp_name = host.HostName.Split('.');
                   // Session[SessionDef.HOSTNAME] = comp_name[0].ToUpper();
                    Session[SessionDef.HOSTNAME] = host;
                   

                    //lblCompName.Text = Session[SessionDef.HOSTNAME].ToString();
                    //lblUsername.Text = Session[SessionDef.SessionUsername].ToString();
                    //lblUnitKerja.Text = Session[SessionDef.SessionUnitKerja].ToString();
                    //lblTanggal.Text = DateTime.Now.ToString(References.DATE_FORMAT);

                    DisplayMainMenu();
                }
                catch (Exception ex)
                {
                    //lblCompName.Text = ex.Message;
                }                
            }
            else
            {
                int count = NavigationMenu.Items.Count;

                for (int i = 0; i < count; i++)
                {
                    MenuItem item = NavigationMenu.Items[i];

                    //remove all
                    NavigationMenu.Items.Remove(item);
                }
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (ib == imgbtnLogout)
            {
                Connection cn = new Connection();
                Framework.Database db = new Framework.Database(cn.ConnectionStringDBJiwa);


                Auth ul = new Auth();
                db.Open();
                db.BeginTransaction();

                ul.ActivityType = 2;//Logout
                ul.IPAddress = GetIpAddress();
                ul.IdPemakai = Session[SessionDef.SessionEmail].ToString();
                ul.Info = "Logout OK";
                ul.InserT_Log_Activity(db);

                db.CommitTransaction();

                Session.Abandon();
                Response.Redirect("~/Login.aspx");
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

        private void DisplayConnection()
        {
            var connstr = new System.Data.SqlClient.SqlConnectionStringBuilder(new Connection().ConnectionStringDBJiwa);
            String server = connstr.DataSource;
            String database = connstr.InitialCatalog;

            //lblServer.Text = server;
            //lblDatabase.Text = database;
        }

        private void DisplayMainMenu()
        {
            List<AksesMenu> data = null;
            MenuItem menu = new MenuItem();
            AksesMenu am = new AksesMenu();
            am.User = new User();
            am.User.IdUser = Session[SessionDef.SessionIdUser].ToString();
            if (Session[SessionMainMenu] == null)
            {
                data = am.GetGroupMenu();
                Session[SessionMainMenu] = data;
            }
            else
            {
                data = (List<AksesMenu>)Session[SessionMainMenu];
            }


            foreach (var item in data)
            {
                String sessionMainMenu = SessionMainMenu + item.GroupMenu.IdGroup;
                menu = new MenuItem();
                menu.Text = item.GroupMenu.NamaGroup;
                menu.Value = item.GroupMenu.IdGroup;
                NavigationMenu.Items.Add(menu);

                DisplayMenu(menu, item.GroupMenu.IdGroup, am.User.IdUser, sessionMainMenu);
            }
        }

        private void DisplayMenu(MenuItem menu, String id_group, String id_profile, String sessionGrpMenu)
        {
            List<AksesMenu> data = null;
            MenuItem child = new MenuItem();
            AksesMenu am = new AksesMenu();
            am.User = new User();
            if (Session[sessionGrpMenu] == null)
            {
                data = am.GetMenu(id_group, id_profile);
                Session[sessionGrpMenu] = data;
            }
            else
            {
                data = (List<AksesMenu>)Session[sessionGrpMenu];
            }

            foreach (var item in data)
            {
                String sessionMenu = SessionMenu + item.Menu.IdMenu;
                child = new MenuItem();
                child.Text = item.Menu.NamaMenu;
                child.Value = item.Menu.IdMenu;
                child.NavigateUrl = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);
                menu.ChildItems.Add(child);

                DisplaySubMenu(child, child.Value, sessionMenu);
            }
        }

        private void DisplaySubMenu(MenuItem child, String id_parent, String sessionMenu)
        {
            List<AksesMenu> data = null;
            MenuItem subChild = new MenuItem();
            AksesMenu am = new AksesMenu();

            if (Session[sessionMenu] == null)
            {
                data = am.GetSubMenu(id_parent, Session[SessionDef.SessionIdUser].ToString().Trim());
                Session[sessionMenu] = data;
            }
            else
            {
                data = (List<AksesMenu>)Session[sessionMenu];
            }

            foreach (var item in data)
            {
                subChild = new MenuItem();
                subChild.Text = item.Menu.NamaMenu;
                subChild.Value = item.Menu.IdMenu;
                subChild.NavigateUrl = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);
                child.ChildItems.Add(subChild);
            }
        }
    }
}
