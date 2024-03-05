using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction
{
    public partial class SuratPengantarKartu : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            btnSimpan.Click += ButtonClicked;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {

               
            }
        }
        private void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;

            if (ib == btnSimpan)
            {
                InsertSurat();
            }
        }

        private void InsertSurat()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            //try
            //{
            //    SuratPengantarKartuModels c = new SuratPengantarKartuModels();

            //    db.Open();
            //    db.BeginTransaction();

            //    c.CompanyNm = txtCompanyNm.Text;
            //    c.HasPaycenter = short.Parse(DropDownListHasPay.SelectedValue);
            //    c.Npwp = txtNpwp.Text;
            //    c.BusinessLineNmbr = int.Parse(ddlBusinessLine.SelectedValue);
            //    c.ContactPerson = txtContactPerson.Text;
            //    c.Siup = txtSiup.Text;
            //    c.MnySrcType = short.Parse(ddlMnySrcType.SelectedValue);
            //    c.PayorNm = txtPayorNm.Text;
            //    c.BankNm = txtBankNm.Text;
            //    c.AccountNmbr = txtAccountNmbr.Text;
            //    c.AccountNm = txtAccountNm.Text;
            //    c.Email = txtEmail.Text;
            //    c.AdArt = txtAdArt.Text;
            //    c.PdpFlg = DropDownListPdp.SelectedValue == "0";
            //    c.OldClientNmbr = txtOldClientNmbr.Text;




            //    c.InsertCompany(db);
            //    db.CommitTransaction();

            //    /*                Response.Redirect("~/CompanyList.aspx");
            //    */
            //}
            //catch (Exception ex)
            //{
            //    db.RollbackTransaction();
            //}
            //finally
            //{
            //    db.Close();
            //}
        }

    }
}