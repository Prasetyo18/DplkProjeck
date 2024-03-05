using DPLKCORE.Framework;
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
    public partial class EntryPayrollContribution : System.Web.UI.Page
    {
        private Database db;
        private Connection conn;

        protected void Page_Init(object sender, EventArgs e)
        {
            UCSearchPanel.GvResult.RowCommand += OnRowCommand;
            UCSearchPanel.GvResult.PageIndexChanging += OnPageIndexChanging;
            UCSearchPanel.BtnSearch.Click += OnButtonClick;
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                searchModal.Show();
            }
        }

        protected void OnButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn == UCSearchPanel.BtnSearch)
            {
                searchModal.Show();
            }
        }

        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == UCSearchPanel.GvResult)
            {
                if (Session["searchResult"] != null)
                {
                    GridViewRow row = Session["searchResult"] as GridViewRow;
                    txtCerNum.Text = row.Cells[1].Text;
                    searchModal.Hide();
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            Session["searchType"] = 2;
            Session["searchCaller"] = "TXT_CER_NMBR";

            if (!IsPostBack)
            {
                
            }

        }

        //delete data entry from gventrypayroll
        protected void GvEntryPayroll_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteData")
                {
                    int RowIndex = Convert.ToInt32(e.CommandArgument);
                    int absoluteIndex = Math.Abs((GvEntryPayroll.PageIndex * GvEntryPayroll.PageSize) - RowIndex);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("group_nmbr");
                    dt.Columns.Add("cer_nmbr");
                    dt.Columns.Add("client_nm");
                    dt.Columns.Add("birth_dt");
                    dt.Columns.Add("employee_nmbr");
                    dt.Columns.Add("er");
                    dt.Columns.Add("ee");
                    dt.Columns.Add("tu");
                    dt.Columns.Add("ft");
                    dt.Columns.Add("bln");
                    dt.Columns.Add("thn");

                    for (int i = 0; i < GvEntryPayroll.Rows.Count; i++)
                    {
                        TextBox TXTER = (TextBox)GvEntryPayroll.Rows[i].Cells[6].FindControl("TXT_ER");
                        TextBox TXTEE = (TextBox)GvEntryPayroll.Rows[i].Cells[7].FindControl("TXT_EE");
                        TextBox TXTTU = (TextBox)GvEntryPayroll.Rows[i].Cells[8].FindControl("TXT_TU");
                        TextBox TXTFT = (TextBox)GvEntryPayroll.Rows[i].Cells[9].FindControl("TXT_FT");
                        TextBox TXTBULAN = (TextBox)GvEntryPayroll.Rows[i].Cells[10].FindControl("TXT_BULAN");
                        TextBox TXTTAHUN = (TextBox)GvEntryPayroll.Rows[i].Cells[11].FindControl("TXT_TAHUN");

                        DataRow dr = dt.NewRow();
                        dr["group_nmbr"] = GvEntryPayroll.Rows[i].Cells[1].Text;
                        dr["cer_nmbr"] = GvEntryPayroll.Rows[i].Cells[2].Text;
                        dr["client_nm"] = GvEntryPayroll.Rows[i].Cells[3].Text;
                        dr["birth_dt"] = GvEntryPayroll.Rows[i].Cells[4].Text;
                        dr["employee_nmbr"] = GvEntryPayroll.Rows[i].Cells[5].Text;
                        dr["er"] = TXTER.Text;
                        dr["ee"] = TXTEE.Text;
                        dr["tu"] = TXTTU.Text;
                        dr["ft"] = TXTFT.Text;
                        dr["bln"] = TXTBULAN.Text;
                        dr["thn"] = TXTTAHUN.Text;
                        dt.Rows.Add(dr);
                    }

                    dt.Rows.RemoveAt(absoluteIndex);
                    GvEntryPayroll.DataSource = dt;
                    GvEntryPayroll.DataBind();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        //if BtnAdd is clicked, CollectData method
        private void CollectData()
        {
            try
            {
                db.Open();
                DataTable dt = new DataTable();
                
                dt.Columns.Add("group_nmbr", typeof(string));
                dt.Columns.Add("cer_nmbr", typeof(string));
                dt.Columns.Add("client_nm", typeof(string));
                dt.Columns.Add("birth_dt", typeof(DateTime));
                dt.Columns.Add("employee_nmbr", typeof(string));
                dt.Columns.Add("er", typeof(string));
                dt.Columns.Add("ee", typeof(string));
                dt.Columns.Add("tu", typeof(string));
                dt.Columns.Add("ft", typeof(string));
                dt.Columns.Add("bln", typeof(string));
                dt.Columns.Add("thn", typeof(string));

                foreach (GridViewRow row in GvEntryPayroll.Rows)
                {
                    TextBox TXTER = (TextBox)row.Cells[6].FindControl("TXT_ER");
                    TextBox TXTEE = (TextBox)row.Cells[7].FindControl("TXT_EE");
                    TextBox TXTTU = (TextBox)row.Cells[8].FindControl("TXT_TU");
                    TextBox TXTFT = (TextBox)row.Cells[9].FindControl("TXT_FT");
                    TextBox TXTBULAN = (TextBox)row.Cells[10].FindControl("TXT_BULAN");
                    TextBox TXTTAHUN = (TextBox)row.Cells[11].FindControl("TXT_TAHUN");

                    DataRow dr = dt.NewRow();
                    dr["group_nmbr"] = row.Cells[1].Text;
                    dr["cer_nmbr"] = row.Cells[2].Text;
                    dr["client_nm"] = row.Cells[3].Text;
                    dr["birth_dt"] = Convert.ToDateTime(row.Cells[4].Text);
                    dr["employee_nmbr"] = row.Cells[5].Text;
                    dr["er"] = TXTER.Text;
                    dr["ee"] = TXTEE.Text;
                    dr["tu"] = TXTTU.Text;
                    dr["ft"] = TXTFT.Text;
                    dr["bln"] = TXTBULAN.Text;
                    dr["thn"] = TXTTAHUN.Text;
                    dt.Rows.Add(dr);
                }
                EntryPayrollContributionModels entryPayroll = new EntryPayrollContributionModels();
                entryPayroll.CertificateNumber = txtCerNum.Text;
                Dictionary<string, object> data = entryPayroll.GetCertificateData(db);
                DataRow newRow = dt.NewRow();
                if (data.Count > 0)
                {
                    if (data["group_nmbr"].ToString() != "")
                    {
                        newRow["group_nmbr"] = data["group_nmbr"].ToString();
                    }
                    if (data["cer_nmbr"].ToString() != "")
                    {
                        newRow["cer_nmbr"] = data["cer_nmbr"].ToString();
                    }
                    if (data["client_nm"].ToString() != "")
                    {
                        newRow["client_nm"] = data["client_nm"].ToString();
                    }
                    if (data["birth_dt"].ToString() != "")
                    {
                        newRow["birth_dt"] = data["birth_dt"].ToString();
                    }
                    if (data["employee_nmbr"].ToString() != "")
                    {
                        newRow["employee_nmbr"] = data["employee_nmbr"].ToString();
                    }
                }
                dt.Rows.Add(newRow);

                GvEntryPayroll.DataSource = dt;
                GvEntryPayroll.DataBind();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            CollectData();
        }

        //if BtnProcess is clicked, UploadPayrollContribution
        private void ProcessData()
        {
            //delete payroll based on hostname
            if (DeletePayroll())
            {
                if (InsertPayroll())
                {
                    DataTable result = UploadPayroll();
                    GvSTA.DataSource = result;
                    GvSTA.DataBind();

                    GvEntryPayroll.DataSource = null;
                    GvEntryPayroll.DataBind();
                    
                }
            }
        }

        private DataTable UploadPayroll()
        {
            DataTable result = new DataTable();
            try
            {
                db.Open();
                db.BeginTransaction();

                EntryPayrollContributionModels entryPayroll = new EntryPayrollContributionModels();
                entryPayroll.ContributionPeriod = DateTime.Parse(txtPeriod.Text);
                entryPayroll.HostName = HttpContext.Current.Request.UserHostName;
                result = entryPayroll.UploadPayroll(db);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return result;
        }

        private bool InsertPayroll()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                foreach (GridViewRow row in GvEntryPayroll.Rows)
                {
                    TextBox TXTER = (TextBox)row.Cells[6].FindControl("TXT_ER");
                    TextBox TXTEE = (TextBox)row.Cells[7].FindControl("TXT_EE");
                    TextBox TXTTU = (TextBox)row.Cells[8].FindControl("TXT_TU");
                    TextBox TXTFT = (TextBox)row.Cells[9].FindControl("TXT_FT");
                    TextBox TXTBULAN = (TextBox)row.Cells[10].FindControl("TXT_BULAN");
                    TextBox TXTTAHUN = (TextBox)row.Cells[11].FindControl("TXT_TAHUN");

                    EntryPayrollContributionModels entryPayroll = new EntryPayrollContributionModels();
                    entryPayroll.GroupNumber = row.Cells[1].Text.Replace("&nbsp;", "");
                    entryPayroll.CertificateNumber = row.Cells[2].Text.Replace("&nbsp;", "");
                    entryPayroll.NIP = row.Cells[5].Text.Replace("&nbsp;", "");
                    entryPayroll.DateOfBirth = DateTime.Parse(row.Cells[4].Text.Replace("&nbsp;", ""));
                    entryPayroll.ContributionER = float.Parse(TXTER.Text.Replace(",", ""));
                    entryPayroll.ContributionEE = float.Parse(TXTEE.Text.Replace(",", ""));
                    entryPayroll.TopUp = float.Parse(TXTTU.Text.Replace(",", ""));
                    entryPayroll.FT = float.Parse(TXTFT.Text.Replace(",", ""));
                    entryPayroll.Month = TXTBULAN.Text;
                    entryPayroll.Year = TXTTAHUN.Text;
                    entryPayroll.HostName = HttpContext.Current.Request.UserHostName;

                    if (!entryPayroll.InsertPayroll(db))
                    {
                        return false;
                    }
                }
                db.CommitTransaction();
                return true;
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

        private bool DeletePayroll()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                if (EntryPayrollContributionModels.DeleteWithHostName(db, HttpContext.Current.Request.UserHostName))
                {
                    db.CommitTransaction();
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
            return true;
        }


        protected void btnProcces_Click(object sender, EventArgs e)
        {
            ProcessData();
        }

    }
}