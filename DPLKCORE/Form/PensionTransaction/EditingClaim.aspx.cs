using DPLKCORE.Framework;
using DPLKCORE.Framework.Helper;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction
{
    public partial class EditingClaim : System.Web.UI.Page
    {
        Connection conn;
        Database db;

        protected void Page_Init(object sender, EventArgs e)
        {
            UCSearchPanel.GvResult.PageIndexChanging += OnPageIndexChanging;
            UCSearchPanel.GvResult.RowCommand += OnRowCommand;
            UCSearchPanel.BtnSearch.Click += ButtonClicked;
        }

        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                if (Session["searchResult"] != null)
                {
                    GridViewRow row = Session["searchResult"] as GridViewRow;
                    txtBank.Text = row.Cells[1].Text;
                    searchModal.Hide();
                }
            }
        }


        protected void OnPageIndexChanging(object sender, EventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                searchModal.Show();
            }
        }
        protected void ButtonClicked(object sender, EventArgs e)
        {
            Button ib = (Button)sender;
            if (ib == UCSearchPanel.BtnSearch)
            {
                searchModal.Show();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            Session["searchType"] = 3;
            Session["searchCaller"] = "TXT_BANK";
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void ClearText()
        {
            txtTferType.Text = "";
            txtSeqNmbr.Text = "";
            txtCer.Text = "";
            txtClient.Text = "";
            txtCompany.Text = "";
            txtType.Text = "";
            txtAmt.Text = "";
            txtBank.Text = "";
            txtAccNmbr.Text = "";
            txtAccNm.Text = "";
        }

        private void SaveData()
        {
            try
            {
                db.Open();
                db.BeginTransaction();

                EditingClaimModels editClaim = new EditingClaimModels();
                editClaim.cer_nmbr = int.Parse(txtCer.Text);
                editClaim.trns_seq_nmbr = int.Parse(txtSeqNmbr.Text);
                editClaim.tfer_type_nmbr = int.Parse(txtTferType.Text);
                editClaim.bank_central_nm = txtBank.Text;
                editClaim.acct_nm = txtAccNm.Text;
                editClaim.acct_nmbr = txtAccNmbr.Text;

                if (editClaim.EditClaim(db))
                {
                    db.CommitTransaction();
                    ClearText();
                    BindGridView();
                }
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        private void BindGridView()
         
        {
            try
            {
                db.Open();
                EditingClaimModels editingClaimModels = new EditingClaimModels();
                DataTable data = editingClaimModels.getEditClaim(db);
                if (data.Rows.Count > 0)
                {
                    GvClaim.DataSource = data;
                    GvClaim.DataBind();
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

        protected void GvClaim_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ClearText();
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int abosulteIndex = Math.Abs((GvClaim.PageIndex * GvClaim.PageSize) - rowIndex);
                GridViewRow row = GvClaim.Rows[abosulteIndex];
                if (e.CommandName == "RowEditing")
                {
                    txtTferType.Text = row.Cells[2].Text;
                    txtSeqNmbr.Text = row.Cells[3].Text;
                    txtCer.Text = row.Cells[4].Text;
                    txtClient.Text = row.Cells[5].Text;
                    txtCompany.Text = row.Cells[6].Text;
                    txtType.Text = row.Cells[7].Text;
                    txtAmt.Text = row.Cells[8].Text;
                    txtBank.Text = row.Cells[9].Text + " - " + row.Cells[10].Text;
                    txtAccNmbr.Text = row.Cells[11].Text;
                    txtAccNm.Text = row.Cells[12].Text;
                }
                else if (e.CommandName == "RowDelete")
                {
                    Session["cerNmbr"] = row.Cells[4].Text;
                    Session["BatchId"] = row.Cells[14].Text;
                    DeleteClaim();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void DeleteClaim()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                EditingClaimModels editClaim = new EditingClaimModels();
                editClaim.cer_nmbr = Convert.ToInt32(Session["cerNmbr"]);
                editClaim.batch_id = Convert.ToInt32(Session["BatchId"]);
                if (editClaim.ValidateClaim(db))
                {
                    if (editClaim.DeleteClaim(db))
                    {
                        db.CommitTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            BindGridView();
        }

        protected void GvClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvClaim.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            searchModal.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
            
            

        //protected void btnEditClaim_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        EditingClaimModels editingClaim = new EditingClaimModels();

        //        string bank_update = txtBank.Text;
        //        string acct_nmbr = txtAcc.Text;
        //        string acct_nm = txtAcccount.Text;

          
        //        editingClaim.EditClaim(database, bank_update, acct_nmbr, acct_nm);

        //        BindGridView();

        //        ScriptManager.RegisterStartupScript(this, GetType(), "hideEditClaimModal", "$('#editClaimModal').modal('hide');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write($"Exception: {ex.Message}");
        //    }
        //}

    }
}
