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
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Sockets;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DPLKCORE
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        String SessionMainMenu = "session.main.menu.";
        String SessionMenu = "session.menu.";

        protected void Page_Init(object sender, EventArgs e)
        {
            imgbtnLogout.Click += ButtonClicked;
            
        }



//        protected void Page_Load(object sender, EventArgs e)
//{
//    if (!IsPostBack)
//    {
//        // Add top-level menu items
//        AddMenuItem("Pension", "javascript:void(0);", "tf-icons bx bx-layout", "Pension", "Pension");

//        // Add sub-menu items for "New Bussiness"
//        AddSubMenuItems("New Bussiness", "geserdong", "New Bussiness");
//        AddMenuItem("Company", "", "", "Company", "Pension", "NewBussinesCompany");
//        AddMenuItem("Pay Center", "", "", "PayCenter", "Pension", "NewBussinesPayCenter");
//        AddMenuItem("Group", "", "", "Group", "Pension", "NewBussinesGroupIndex");
//        AddMenuItem("Client", "", "", "Client", "Pension", "NewBussinesClientIndex");
//    }
//}

//private void AddMenuItem(string text, string navigateUrl, string cssClass, string dataI18n, string controller = null, string action = null)
//{
//    MenuItem menuItem = new MenuItem();
//    menuItem.Text = @"<i class='"+cssClass+"'></i><div data-i18n='"+dataI18n+"'>"+text+"</div>";
//    menuItem.NavigateUrl = navigateUrl;

//    if (!string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action))
//    {
//        menuItem.NavigateUrl = @"~/Pension/{controller}/{action}";
//    }

//    DynamicMenu.Items.Add(menuItem);
//}

//private void AddSubMenuItems(string parentId, string subMenuId, string dataI18n)
//{
//    MenuItem parentMenuItem = DynamicMenu.FindItem(parentId);

//    if (parentMenuItem != null)
//    {
//        MenuItem subMenuItem = new MenuItem();
//        subMenuItem.Text = @"<div data-i18n='"+dataI18n+"'>"+parentId+"</div>";
//        parentMenuItem.ChildItems.Add(subMenuItem);
//        subMenuItem.Selectable = false; // Set to true if sub-menu items should be clickable

//        // Add an ID to the sub-menu for styling or other purposes
//        subMenuItem.ChildItems.Add(new MenuItem { Text = "", Value = subMenuId, Selectable = false });
//    }
//}



//        protected void Page_Load(object sender, EventArgs e)
//{
//    if (!IsPostBack)
//    {
//        // Add top-level menu items
//        AddMenuItem("Pension", "javascript:void(0);", "tf-icons bx bx-layout", "Pension", "Pension");

//        // Add sub-menu items for "New Bussiness"
//        AddSubMenuItems("Pension", "New Bussiness", "geserdong", "New Bussiness");
//        AddMenuItem("Company", "", "", "Company", "Pension", "NewBussinesCompany");
//        AddMenuItem("Pay Center", "", "", "PayCenter", "Pension", "NewBussinesPayCenter");
//        AddMenuItem("Group", "", "", "Group", "Pension", "NewBussinesGroupIndex");
//        AddMenuItem("Client", "", "", "Client", "Pension", "NewBussinesClientIndex");
//    }
//}

//private void AddMenuItem(string text, string navigateUrl, string cssClass, string dataI18n, string controller = null, string action = null)
//{
//    HtmlGenericControl li = new HtmlGenericControl("li");
//    DynamicMenu.Controls.Add(li);

//    HtmlAnchor a = new HtmlAnchor();
//    a.HRef = navigateUrl;
//    a.Attributes["class"] = "menu-link menu-toggle";
//    li.Controls.Add(a);

//    LiteralControl icon = new LiteralControl(@"<i class='"+cssClass+"'></i>");
//    a.Controls.Add(icon);

//    LiteralControl div = new LiteralControl(@"<div data-i18n='"+dataI18n+"'>"+text+"</div>");
//    a.Controls.Add(div);

//    if (!string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action))
//    {
//        a.HRef = @"~/Pension/{controller}/{action}";
//    }
//}


//private HtmlGenericControl FindControlRecursive(Control root, string id)
//{
//    if (root.ID == id)
//    {
//        return root as HtmlGenericControl;
//    }

//    foreach (Control control in root.Controls)
//    {
//        HtmlGenericControl foundControl = FindControlRecursive(control, id);
//        if (foundControl != null)
//        {
//            return foundControl;
//        }
//    }

//    return null;
//}

//private void AddSubMenuItems(string parentId, string parentText, string subMenuId, string dataI18n)
//{
//    //HtmlGenericControl parentLi = (HtmlGenericControl)DynamicMenu.FindControl(parentId);
//    HtmlGenericControl parentLi = FindControlRecursive(DynamicMenu, parentId);

//    if (parentLi != null)
//    {


//        HtmlGenericControl subLiParent = new HtmlGenericControl("li");
//        subLiParent.Attributes["class"] = "menu-header small text-uppercase";
//        parentLi.Controls.Add(subLiParent);


//        HtmlGenericControl subLi = new HtmlGenericControl("li");
//        subLi.Attributes["class"] = "menu-item";
//        subLiParent.Controls.Add(subLi);

//        HtmlGenericControl subUl = new HtmlGenericControl("ul");
//        subUl.Attributes["class"] = "menu-sub";
//        subLi.Controls.Add(subUl);


//        HtmlAnchor subLink = new HtmlAnchor();
//        subLink.HRef = "javascript:void(0);";
//        subLink.Attributes["class"] = "menu-link menu-toggle";
//        subLi.Controls.Add(subLink);

//        LiteralControl div = new LiteralControl(@"<div data-i18n='" + dataI18n + "'>" + parentText + "</div>");
//        subLink.Controls.Add(div);

//        subLi.Controls.Add(new HtmlGenericControl("ul")); // Empty sub-menu placeholder for styling
//    }
//}
protected void Page_Load(object sender, EventArgs e)
{
        AssetManager assetManager = new AssetManager();
        List<string> scripts = new List<string>
    {
        "assets/vendor/js/helpers.js",
        "assets/js/config.js",
        "assets/vendor/libs/jquery/jquery.js",
        "assets/vendor/libs/popper/popper.js",
        "assets/vendor/js/bootstrap.js",
        "assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js",
        "assets/vendor/js/menu.js",
        "assets/vendor/libs/apex-charts/apexcharts.js",
        "assets/js/main.js",
        "assets/js/dashboards-analytics.js",
        "assets/vendor/libs/jquery/jquery.js",
        "assets/vendor/libs/popper/popper.js",
        "assets/vendor/js/bootstrap.js",
        "assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js",
        "assets/vendor/js/menu.js",
        "assets/vendor/libs/apex-charts/apexcharts.js",
        "assets/js/main.js",
        "assets/js/dashboards-analytics.js",
        "assets/vendor/libs/jquery/jquery.js",
    };

        List<string> css = new List<string>(){
        "additional-file/css/global.css",
        "assets/vendor/fonts/boxicons.css",
        "assets/vendor/css/core.css",
        "assets/vendor/css/theme-default.css",
        "assets/css/demo.css",
        "assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css",
        "assets/vendor/libs/apex-charts/apex-charts.css"
    };

        assetManager.SetScripts(scripts);
        assetManager.SetCss(css);
        assetManager.RegisterScript(Page.Header, ResolveUrl);
        assetManager.RegisterCss(Page.Header, ResolveUrl);
        DisplayMainMenu();
   
}
//protected void Page_Load(object sender, EventArgs e)
//{
//    if (!IsPostBack)
//    {
//        // Fetch menu data from the database
//        DataTable menuData = GetMenuDataFromDatabase();

//        // Dynamically create the menu items
//        HtmlGenericControl dynamicMenu = new HtmlGenericControl("ul");
//        dynamicMenu.Attributes["class"] = "menu-inner py-1";

//        // Get root menu items (items with no parent)
//        DataRow[] rootItems = menuData.Select("ParentID IS NULL OR ParentID = 0");

//        foreach (DataRow rootItem in rootItems)
//        {
//            HtmlGenericControl menuItem = new HtmlGenericControl("li");
//            menuItem.Attributes["class"] = "menu-item";

//            HtmlGenericControl menuLink = new HtmlGenericControl("a");
//            menuLink.Attributes["href"] = rootItem["Url"].ToString(); // Assuming there's a URL column
//            menuLink.Attributes["class"] = "menu-link";
//            menuLink.InnerText = rootItem["MenuName"].ToString(); // Assuming there's a MenuName column

//            menuItem.Controls.Add(menuLink);

//            // Check if the root item has child items
//           // DataRow[] childItems = menuData.Select("ParentID = {" + rootItem["MenuName"] + "}");
//            DataRow[] childItems = menuData.Select("ParentID = " + rootItem["MenuID"].ToString());

//            if (childItems.Length > 0)
//            {
//                // Create sub-menu for child items
//                HtmlGenericControl subMenu = new HtmlGenericControl("ul");
//                subMenu.Attributes["class"] = "menu-sub";

//                foreach (DataRow childItem in childItems)
//                {
//                    HtmlGenericControl subMenuItem = new HtmlGenericControl("li");
//                    subMenuItem.Attributes["class"] = "menu-item";

//                    HtmlGenericControl subMenuLink = new HtmlGenericControl("a");
//                    subMenuLink.Attributes["href"] = childItem["Url"].ToString(); // Assuming there's a URL column
//                    subMenuLink.Attributes["class"] = "menu-link";
//                    subMenuLink.InnerText = childItem["MenuName"].ToString(); // Assuming there's a MenuName column

//                    subMenuItem.Controls.Add(subMenuLink);
//                    subMenu.Controls.Add(subMenuItem);
//                }

//                menuItem.Controls.Add(subMenu);
//            }

//            dynamicMenu.Controls.Add(menuItem);
//        }

//        // Add the dynamicMenu to the container in the ASPX file
//        dynamicMenuContainer.Controls.Add(dynamicMenu);
//    }
//}

 private DataTable GetMenuDataFromDatabase()
        {
            // Fetch menu data from the database using ADO.NET
            DataTable menuData = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["connPension"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MenuID, MenuName, Url, ParentID FROM MenuItems";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.Fill(menuData);
                }
            }

            return menuData;
        }



 private void DisplayMainMenu()
 {
     List<AksesMenu> data = null;
     MenuItem menu = new MenuItem();
     AksesMenu am = new AksesMenu();
     am.User = new User();
     if (Session[SessionDef.SessionIdUser] != null)
     {
         am.User.IdUser = Session[SessionDef.SessionIdUser].ToString();

     }
     else
     {
         Response.Redirect("~/Login.aspx");
     }
     if (Session[SessionMainMenu] == null)
     {
         data = am.GetGroupMenu();
         Session[SessionMainMenu] = data;
     }
     else
     {
         data = (List<AksesMenu>)Session[SessionMainMenu];
     }

     HtmlGenericControl dynamicMenu = new HtmlGenericControl("ul");
     dynamicMenu.Attributes["class"] = "menu-inner py-1";


     HtmlGenericControl menuItem = new HtmlGenericControl("li");
     menuItem.Attributes["class"] = "menu-header small text-uppercase";


     foreach (var item in data)
     {

         HtmlGenericControl menuItem2 = new HtmlGenericControl("li");
         menuItem2.Attributes["class"] = "menu-item";

         menuItem.Controls.Add(menuItem2);

         HtmlGenericControl menuLink = new HtmlGenericControl("a");
         menuLink.Attributes["href"] = "javascript:void(0);"; // Assuming there's a URL column
         menuLink.Attributes["class"] = "menu-link menu-toggle";
         menuLink.InnerText = item.GroupMenu.NamaGroup.ToString(); // Assuming there's a MenuName column

         menuItem2.Controls.Add(menuLink);


         String sessionMainMenu = SessionMainMenu + item.GroupMenu.IdGroup;
         //menu = new MenuItem();
         //menu.Text = item.GroupMenu.NamaGroup;
         //menu.Value = item.GroupMenu.IdGroup;
         //NavigationMenu.Items.Add(menu);

         DisplayMenu(menuItem2, item.GroupMenu.IdGroup, am.User.IdUser, sessionMainMenu);
         dynamicMenu.Controls.Add(menuItem);

     }

     dynamicMenuContainer.Controls.Add(dynamicMenu);
 }


 private void DisplayMenu(HtmlGenericControl menu, String id_group, String id_user, String sessionGrpMenu)
 {
     List<AksesMenu> data = null;
     MenuItem child = new MenuItem();
     AksesMenu am = new AksesMenu();
     am.User = new User();
     if (Session[sessionGrpMenu] == null)
     {
         data = am.GetMenu(id_group, id_user);
         Session[sessionGrpMenu] = data;
     }
     else
     {
         data = (List<AksesMenu>)Session[sessionGrpMenu];
     }

     if (data.Count > 0)
     { 
     foreach (var item in data)
     {

         HtmlGenericControl subUL = new HtmlGenericControl("ul");
         subUL.Attributes["class"] = "menu-sub";
        // subUL.Controls.Add(subUL);
         menu.Controls.Add(subUL);
         
         HtmlGenericControl menuItem = new HtmlGenericControl("li");
         menuItem.Attributes["class"] = "menu-item";
         //subUL.Controls.Add(menuItem);


         HtmlGenericControl menuLink = new HtmlGenericControl("a");
         //menuLink.Attributes["href"] = item.URL; // Assuming there's a URL column
         //menuLink.Attributes["class"] = "menu-link menu-toggle";

         if (!String.IsNullOrEmpty(item.Menu.NamaMenu))
         {

             menuLink = new HtmlGenericControl("a");
             menuLink.Attributes["href"] = item.URL; // Assuming there's a URL column
             menuLink.Attributes["class"] = "menu-link menu-toggle";
             menuLink.InnerText = item.Menu.NamaMenu.ToString(); // Assuming there's a MenuName column

             menuItem.Controls.Add(menuLink);
             String sessionMenu = SessionMenu + item.Menu.IdMenu;
             //child = new MenuItem();
             //child.Text = item.Menu.NamaMenu;
             //child.Value = item.Menu.IdMenu;
             //child.NavigateUrl = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);
             //menu.ChildItems.Add(child);

             DisplaySubMenu(menuItem, item.Menu.IdMenu, sessionMenu);
         }

         subUL.Controls.Add(menuItem);
         //menuItem.Controls.Add(subUL);
     }
     }
 }


 private void DisplaySubMenu(HtmlGenericControl child, String id_parent, String sessionMenu)
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

     if (data.Count > 0)
     {

         foreach (var item in data)
         {


             HtmlGenericControl subMenu = new HtmlGenericControl("ul");
             subMenu.Attributes["class"] = "menu-sub";
             child.Controls.Add(subMenu);


             HtmlGenericControl subMenuItem = new HtmlGenericControl("li");
             subMenuItem.Attributes["class"] = "menu-item";

             HtmlGenericControl subMenuLink = new HtmlGenericControl("a");
             //subMenuLink.Attributes["href"] = item.Menu.URL; // Assuming there's a URL column
             subMenuLink.Attributes["href"] = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);

             
             subMenuLink.Attributes["class"] = "menu-link";
             subMenuLink.InnerText = item.Menu.NamaMenu.ToString(); // Assuming there's a MenuName column

             subMenuItem.Controls.Add(subMenuLink);
             subMenu.Controls.Add(subMenuItem);

             //subChild = new MenuItem();
             //subChild.Text = item.Menu.NamaMenu;
             //subChild.Value = item.Menu.IdMenu;
             //subChild.NavigateUrl = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);
             //child.ChildItems.Add(subChild);
         }
     }
 }

    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        DisplayConnection();

    //        if (Session.Count > 0)
    //        {
    //            try
    //            {
    //                var host = "";
    //                Session[SessionDef.HOSTNAME] = host;
                   
    //                DisplayMainMenu();
    //                //List<MenuItem> menuItems = DisplayMainMenuList(); // Replace this with your logic to get menu items
    //                //MenuRepeater.DataSource = menuItems;
    //                //MenuRepeater.DataBind();

    //                //GenerateMenu();

    //            }
    //            catch (Exception ex)
    //            {
    //                //lblCompName.Text = ex.Message;
    //            }                
    //        }
    //        else
    //        {
    //            //int count = NavigationMenu.Items.Count;

    //            //for (int i = 0; i < count; i++)
    //            //{
    //            //    MenuItem item = NavigationMenu.Items[i];

    //            //    //remove all
    //            //    NavigationMenu.Items.Remove(item);
    //            //}
    //        }
    //    }

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

    //    private List<MenuItem> DisplayMainMenuList()
    //    {
    //        List<AksesMenu> data = null;
    //        List<MenuItem> datasMenu = null;
    //        MenuItem menu = new MenuItem();
    //        AksesMenu am = new AksesMenu();
    //        am.User = new User();
    //        am.User.IdUser = Session[SessionDef.SessionIdUser].ToString();
    //        if (Session[SessionMainMenu] == null)
    //        {
    //            data = am.GetGroupMenu();
    //            Session[SessionMainMenu] = data;
    //        }
    //        else
    //        {
    //            data = (List<AksesMenu>)Session[SessionMainMenu];
    //        }


    //        foreach (var item in data)
    //        {
    //            String sessionMainMenu = SessionMainMenu + item.GroupMenu.IdGroup;
    //            menu = new MenuItem();
    //            menu.Text = item.GroupMenu.NamaGroup;
    //            menu.Value = item.GroupMenu.IdGroup;
    //            menu.NavigateUrl = "ListUSer.aspx";
    //            datasMenu.Add(menu);

    //            NavigationMenu.Items.Add(menu);
    //            DisplayMenu(menu, item.GroupMenu.IdGroup, am.User.IdUser, sessionMainMenu);
    //        }

    //        return datasMenu;
    //    }


    //    private void DisplayMainMenu()
    //    {
    //        List<AksesMenu> data = null;
    //        MenuItem menu = new MenuItem();
    //        AksesMenu am = new AksesMenu();
    //        am.User = new User();
    //        am.User.IdUser = Session[SessionDef.SessionIdUser].ToString();
    //        if (Session[SessionMainMenu] == null)
    //        {
    //            data = am.GetGroupMenu();
    //            Session[SessionMainMenu] = data;
    //        }
    //        else
    //        {
    //            data = (List<AksesMenu>)Session[SessionMainMenu];
    //        }


    //        foreach (var item in data)
    //        {
    //            String sessionMainMenu = SessionMainMenu + item.GroupMenu.IdGroup;
    //            menu = new MenuItem();
    //            menu.Text = item.GroupMenu.NamaGroup;
    //            menu.Value = item.GroupMenu.IdGroup;
    //            NavigationMenu.Items.Add(menu);

    //            DisplayMenu(menu, item.GroupMenu.IdGroup, am.User.IdUser, sessionMainMenu);
    //        }
    //    }

    //    private void DisplayMenu(MenuItem menu, String id_group, String id_profile, String sessionGrpMenu)
    //    {
    //        List<AksesMenu> data = null;
    //        MenuItem child = new MenuItem();
    //        AksesMenu am = new AksesMenu();
    //        am.User = new User();
    //        if (Session[sessionGrpMenu] == null)
    //        {
    //            data = am.GetMenu(id_group, id_profile);
    //            Session[sessionGrpMenu] = data;
    //        }
    //        else
    //        {
    //            data = (List<AksesMenu>)Session[sessionGrpMenu];
    //        }

    //        foreach (var item in data)
    //        {
    //            String sessionMenu = SessionMenu + item.Menu.IdMenu;
    //            child = new MenuItem();
    //            child.Text = item.Menu.NamaMenu;
    //            child.Value = item.Menu.IdMenu;
    //            child.NavigateUrl = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);
    //            menu.ChildItems.Add(child);

    //            DisplaySubMenu(child, child.Value, sessionMenu);
    //        }
    //    }

    //    private void DisplaySubMenu(MenuItem child, String id_parent, String sessionMenu)
    //    {
    //        List<AksesMenu> data = null;
    //        MenuItem subChild = new MenuItem();
    //        AksesMenu am = new AksesMenu();

    //        if (Session[sessionMenu] == null)
    //        {
    //            data = am.GetSubMenu(id_parent, Session[SessionDef.SessionIdUser].ToString().Trim());
    //            Session[sessionMenu] = data;
    //        }
    //        else
    //        {
    //            data = (List<AksesMenu>)Session[sessionMenu];
    //        }

    //        foreach (var item in data)
    //        {
    //            subChild = new MenuItem();
    //            subChild.Text = item.Menu.NamaMenu;
    //            subChild.Value = item.Menu.IdMenu;
    //            subChild.NavigateUrl = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);
    //            child.ChildItems.Add(subChild);
    //        }
    //    }


    //    //private List<AksesMenu> DisplayMainMenuValue()
    //    //{
    //    //    List<AksesMenu> data = null;
    //    //    MenuItem menu = new MenuItem();
    //    //    AksesMenu am = new AksesMenu();

    //    //    am.User = new User();
    //    //    am.User.IdUser = Session[SessionDef.SessionIdUser].ToString();
    //    //    if (Session[SessionMainMenu] == null)
    //    //    {
    //    //        data = am.GetGroupMenu();
    //    //        Session[SessionMainMenu] = data;
    //    //    }
    //    //    else
    //    //    {
    //    //        data = (List<AksesMenu>)Session[SessionMainMenu];
    //    //    }


    //    //    foreach (var item in data)
    //    //    {
    //    //        String sessionMainMenu = SessionMainMenu + item.GroupMenu.IdGroup;
    //    //        menu = new MenuItem();
    //    //        menu.Text = item.GroupMenu.NamaGroup;
    //    //        menu.Value = item.GroupMenu.IdGroup;
    //    //        NavigationMenu.Items.Add(menu);

    //    //        DisplayMenu(menu, item.GroupMenu.IdGroup, am.User.IdUser, sessionMainMenu);
    //    //    }

    //    //    return data;
    //    //}



    //    //private void DisplayMenu(MenuItem menu, String id_group, String id_profile, String sessionGrpMenu)
    //    //{
    //    //    List<AksesMenu> data = null;
    //    //    MenuItem child = new MenuItem();
    //    //    AksesMenu am = new AksesMenu();
    //    //    am.User = new User();
    //    //    if (Session[sessionGrpMenu] == null)
    //    //    {
    //    //        data = am.GetMenu(id_group, id_profile);
    //    //        Session[sessionGrpMenu] = data;
    //    //    }
    //    //    else
    //    //    {
    //    //        data = (List<AksesMenu>)Session[sessionGrpMenu];
    //    //    }

    //    //    foreach (var item in data)
    //    //    {
    //    //        String sessionMenu = SessionMenu + item.Menu.IdMenu;
    //    //        child = new MenuItem();
    //    //        child.Text = item.Menu.NamaMenu;
    //    //        child.Value = item.Menu.IdMenu;
    //    //        child.NavigateUrl = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);
    //    //        menu.ChildItems.Add(child);

    //    //        DisplaySubMenu(child, child.Value, sessionMenu);
    //    //    }
    //    //}

    //    //private List<AksesMenu> DisplaySubMenuValue(MenuItem child, String id_parent, String sessionMenu)
    //    //{
    //    //    List<AksesMenu> data = null;
    //    //    MenuItem subChild = new MenuItem();
    //    //    AksesMenu am = new AksesMenu();

    //    //    if (Session[sessionMenu] == null)
    //    //    {
    //    //        data = am.GetSubMenu(id_parent, Session[SessionDef.SessionIdUser].ToString().Trim());
    //    //        Session[sessionMenu] = data;
    //    //    }
    //    //    else
    //    //    {
    //    //        data = (List<AksesMenu>)Session[sessionMenu];
    //    //    }

    //    //    return data;

    //    //    ////foreach (var item in data)
    //    //    ////{
    //    //    ////    subChild = new MenuItem();
    //    //    ////    subChild.Text = item.Menu.NamaMenu;
    //    //    ////    subChild.Value = item.Menu.IdMenu;
    //    //    ////    subChild.NavigateUrl = String.Format("{0}?id_menu={1}", item.Menu.URL, item.Menu.IdMenu);
    //    //    ////    child.ChildItems.Add(subChild);
    //    //    ////}
    //    //}








    ////    private List<MenuItem> GetMenuItems()
    ////    {

    ////      //  DisplayMainMenu();

    ////        return new List<MenuItem>
    ////{
    ////    new MenuItem
    ////    {
    ////        Text = "Pension",
    ////        NavigateUrl = "javascript:void(0);",
    ////        ChildItems = new List<MenuItem>
    ////        {
    ////            new MenuItem { Text = "New Business", Url = "javascript:void(0);" },
    ////            new MenuItem { Text = "Update", Url = "javascript:void(0);" },
    ////            // Add other sub-menu items as needed
    ////        }
    ////    },
    ////    // Add other top-level menu items as needed
    ////};
    ////    }



    }
}
