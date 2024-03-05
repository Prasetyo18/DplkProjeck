using DPLKCORE.Class.Pension;
using DPLKCORE.Framework;
using DPLKCORE.Class.Pension;
using System;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.Pension
{
    public partial class Company : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCompanyAdd.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {

                LoadBusinessLinesToDDL();
                LoadMoneySourceTypesToDDL();
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;

            if (ib == btnCompanyAdd)
            {
                InsertCompanyData();
            }
        }

        protected void btnCompanyAdd_Click(object sender, EventArgs e)
        {
            InsertCompanyData();
        }

        private void LoadBusinessLinesToDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            CompanyModel ddlHelper = new CompanyModel();

            db.Open();
            ddlHelper.LoadDataParamToDDLBisnis(ddlBusinessLine, db);
            db.Close();
        }

        private void LoadMoneySourceTypesToDDL()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            DDL ddlHelper = new DDL();

            db.Open();
            ddlHelper.LoadDataParamToDDL(ddlMnySrcType, db);
            db.Close();
        }

        private void InsertCompanyData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringPension);
            try
            {
                CompanyModel c = new CompanyModel();

                db.Open();
                db.BeginTransaction();

                c.CompanyNm = txtCompanyNm.Text;
                c.HasPaycenter = short.Parse(DropDownListHasPay.SelectedValue);
                c.Npwp = txtNpwp.Text;
                c.BusinessLineNmbr = int.Parse(ddlBusinessLine.SelectedValue);
                c.ContactPerson = txtContactPerson.Text;
                c.Siup = txtSiup.Text;
                c.MnySrcType = short.Parse(ddlMnySrcType.SelectedValue);
                c.PayorNm = txtPayorNm.Text;
                c.BankNm = txtBankNm.Text;
                c.AccountNmbr = txtAccountNmbr.Text;
                c.AccountNm = txtAccountNm.Text;
                c.Email = txtEmail.Text;
                c.AdArt = txtAdArt.Text;
                c.PdpFlg = DropDownListPdp.SelectedValue == "0";
                c.OldClientNmbr = txtOldClientNmbr.Text;
                

                

                c.InsertCompany(db);
                db.CommitTransaction();

/*                Response.Redirect("~/CompanyList.aspx");
*/            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
            }
            finally
            {
                db.Close();
            }
        }
    }
}
