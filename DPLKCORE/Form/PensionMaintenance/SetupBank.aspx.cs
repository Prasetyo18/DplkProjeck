using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionMaintenance
{
    public partial class SetupBank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            //using (DbBank dbContext = new DbBank())
            //{
            //    DbBank bank = new DbBank
            //    {
            //        BankNmbr = TXT_BANKCODE.Text,
            //        BankNm = TXT_BANK_NM.Text,
            //        BankAddr = TXT_BANKADDR.Text
            //    };

            //    dbContext.DbBanks.Add(bank);
            //    dbContext.SaveChanges();
            //}

            CLEAR();
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data has been saved');", true);
        }

        private void CLEAR()
        {
            TXT_BANK_NM.Text = "";
            TXT_BANKADDR.Text = "";
            TXT_BANKCODE.Text = "";
        }
    }
}