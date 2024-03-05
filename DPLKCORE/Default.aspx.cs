using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Class;

namespace DPLKCORE
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        private void ButtonClicked(object sender, EventArgs e)
        { 
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("Login.aspx");
        }

    }
}
