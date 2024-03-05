using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Logic.Pension;
using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;


namespace DPLKCORE.Form.PensionTransaction
{
    public partial class JoinAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<JoinAccountModels> accountListings = GetAccountListing();

                GridViewJoinAccount.DataSource = accountListings;
                GridViewJoinAccount.DataBind();
            }
        }

 
        private List<JoinAccountModels> GetAccountListing()
        {
            Connection con = new Connection();
            String cnstr = con.ConnectionStringPension;
            Database db = new Database(cnstr); 

            JoinAccountModels joinAccount = new JoinAccountModels();
            return joinAccount.GetAccountListing(db);
        }


    }
}