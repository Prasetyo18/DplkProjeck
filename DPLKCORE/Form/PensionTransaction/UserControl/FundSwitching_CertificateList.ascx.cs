using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction.UserControl
{
    public partial class FundSwitching_CertificateList : System.Web.UI.UserControl
    {
        Database db;
        Connection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (!IsPostBack)
            {
                if (GV_SUMMARY.DataSource == null)
                {
                    LB_SUMMARY.Style.Add("display", "none");
                }
                else
                {
                    LB_SUMMARY.Style.Remove("display");
                }
                fillDDL();
            }
        }

        private void fillDDL()
        {
            try
            {
                db.Open();
                DDLFundSwitching.LoadTransactionDt(db, DDL_TRNSDT);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }

            try
            {
                db.Open();
                DDLFundSwitching.LoadTransactionDt(db, DDL_PROCESS);
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

        protected void BTN_REFRESH_Click(object sender, EventArgs e)
        {
            //fill gv list
            FillGvList();
            //fill gv summary
            FillGvSummary();
            //fill gv approval
            FillGvProcess();
        }

        private void FillGvProcess()
        {
            try
            {
                db.Open();
                GV_PROCESS.DataSource = FSCertificateListModels.LoadProcessData(db);
                GV_PROCESS.DataBind();

                for (int i = 0; i < GV_PROCESS.Rows.Count; i++)
                {
                    Button btn = (Button)GV_PROCESS.Rows[i].Cells[0].FindControl("BT_PROCESS");
                    if (GV_PROCESS.Rows[i].Cells[7].Text == "10")
                    {
                        btn.Enabled = false;
                    }
                    else if (GV_PROCESS.Rows[i].Cells[7].Text == "1")
                    {
                        btn.Enabled = true;
                    }
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

        private void FillGvSummary()
        {
            try
            {
                db.Open();
                GV_SUMMARY.DataSource = FSCertificateListModels.LoadSummaryData(db, DDL_TRNSDT.SelectedValue);
                GV_SUMMARY.DataBind();
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

        private void FillGvList()
        {
            try
            {
                db.Open();
                GV_LST.DataSource = FSCertificateListModels.LoadListData(db, DDL_TRNSDT.SelectedValue);
                GV_LST.DataBind();
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

        protected void BTN_REFRESH2_Click(object sender, EventArgs e)
        {
            //fill gv list
            FillGvList();
            //fill gv summary
            FillGvSummary();
            //fill gv approval
            FillGvProcess();
        }

        protected void DDL_TRNSDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fill gv list
            FillGvList();
            //fill gv summary
            FillGvSummary();
            //fill gv approval
            FillGvProcess();
        }

        protected void DDL_PROCESS_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_PROCESS.DataSource = FSCertificateListModels.LoadProcessData(db, DDL_PROCESS.SelectedValue);
            GV_PROCESS.DataBind();
        }


        protected void GV_LST_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_LST.PageIndex = e.NewPageIndex;
            FillGvList();
        }

        protected void GV_SUMMARY_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_SUMMARY.PageIndex = e.NewPageIndex;
            FillGvSummary();
        }

        protected void GV_PROCESS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_PROCESS.PageIndex = e.NewPageIndex;
            FillGvProcess();
        }

        protected void BTN_APPROVE_ALL_Click(object sender, EventArgs e)
        {
            ApproveAll();
        }

        protected void BTN_PROCESS_ALL_Click(object sender, EventArgs e)
        {
            ProcessAll();
        }

        private void ApproveAll()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                string sUID = Session["session.iduser"].ToString();
                for (int i = 0; i < GV_LST.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GV_LST.Rows[i].Cells[7].FindControl("CB");
                    if (cb.Checked)
                    {
                        FSCertificateListModels fsCertifList = new FSCertificateListModels();
                        fsCertifList.CerNmbr = GV_LST.Rows[i].Cells[2].Text;
                        fsCertifList.EfctvDt = DateTime.Parse(DDL_TRNSDT.SelectedValue);
                        fsCertifList.BatchId = int.Parse(GV_LST.Rows[i].Cells[0].Text);
                        fsCertifList.UID = sUID;
                        fsCertifList.Mode = 0;

                        fsCertifList.CheckSwitchingProcess(db);
                    }
                }
                db.CommitTransaction();

                FillGvList();
                FillGvSummary();
                FillGvProcess();
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

        private void ProcessAll()
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                string sUID = Session["session.iduser"].ToString();
                DataTable data = FSCertificateListModels.GetAllProcessData(db);
                if (data.Rows.Count > 0)
                {
                    for (int i = 0; i < GV_PROCESS.Rows.Count; i++)
                    {
                        CheckBox cb = (CheckBox)GV_PROCESS.Rows[i].Cells[0].FindControl("CB2");
                        if (cb.Checked == true && GV_PROCESS.Rows[i].Cells[7].Text == "1")
                        {
                            FSCertificateListModels fsCertifList = new FSCertificateListModels();
                            fsCertifList.CerNmbr = GV_PROCESS.Rows[i].Cells[3].Text;
                            fsCertifList.EfctvDt = DateTime.Parse(DDL_PROCESS.SelectedValue);
                            fsCertifList.BatchId = int.Parse(GV_PROCESS.Rows[i].Cells[6].Text);
                            fsCertifList.UID = sUID;
                            fsCertifList.Mode = 1;

                            fsCertifList.CheckSwitchingProcess(db);
                            
                        }
                    }
                    db.CommitTransaction();
                }
                else
                {
                    //pop message unit price not available;
                }

                FillGvList();
                FillGvSummary();
                FillGvProcess();
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

        protected void GV_PROCESS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex;
            int absoluteRowIndex;
            GridViewRow row;
            if (e.CommandName.ToLower() == "all")
            {
                for(int i=0; i<GV_PROCESS.Rows.Count; i++)
					{
						CheckBox cb = (CheckBox) GV_PROCESS.Rows[i].Cells[0].FindControl("CB2");
						cb.Checked = true;

						if (GV_PROCESS.Rows[i].Cells[7].Text =="10")
						{
							cb.Checked = false;
					
						}
						if (GV_PROCESS.Rows[i].Cells[7].Text =="1")
						{
							cb.Checked = true;
						}
					}
            }
            else if (e.CommandName.ToLower() == "process")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
                absoluteRowIndex = Math.Abs((GV_PROCESS.PageIndex * GV_PROCESS.PageSize) - rowIndex);
                row = GV_PROCESS.Rows[absoluteRowIndex];
                string sUID = Session["session.iduser"].ToString();

                DataTable data = FSCertificateListModels.GetAllProcessData(db);
                if (data.Rows.Count > 0)
                {

                    FMTransactionHistory fmth = new FMTransactionHistory();
                    fmth.CerNmbr = int.Parse(row.Cells[3].Text);
                    fmth.EfctvDt = DateTime.Parse(row.Cells[4].Text);
                    fmth.BatchId = int.Parse(row.Cells[6].Text);

                    fmth.InsertFmTransactionHistory(db);
                    fmth.InserFMTransactionHistorySwitch(db);

                    FPP fpp = new FPP();
                    fpp.SeqNmbr = int.Parse(row.Cells[6].Text.Replace("&nbsp;", ""));
                    fpp.GroupNmbr = int.Parse(row.Cells[3].Text);
                    fpp.ModeOfTransaction = 11;
                    fpp.UserNm = sUID;
                    fpp.ApprovalNm = sUID;

                    fpp.UpdateFppPerkiraan(db);

                    fpp.ModeOfTransaction = 12;
                    fpp.UpdateFppPerkiraan(db);

                    fpp.ModeOfTransaction = 13;
                    fpp.UpdateFppPerkiraan(db);
                }
                else
                {
                    //popup message unit price not available
                }
                
                FillGvList();
                FillGvSummary();
                FillGvProcess();
            }
        }

        protected void GV_SWITCH_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex;
            int absoluteRow;
            GridView GvSwitch;
            GridViewRow row;

            if (e.CommandName.ToLower() == "delete")
            {
                //check again
                int gv_list_SelectedRow = Convert.ToInt32(Session["gv_lst_selectedRowIndex"]);
                GvSwitch = (GridView)GV_LST.Rows[gv_list_SelectedRow].FindControl("GV_SWITCH");
                rowIndex = Convert.ToInt32(e.CommandArgument);
                absoluteRow = Math.Abs((GvSwitch.PageIndex * GvSwitch.PageSize) - rowIndex);
                row = GvSwitch.Rows[absoluteRow];

                try
                {
                    db.Open();
                    db.BeginTransaction();
                    FundSwitchingModels certifList = new FundSwitchingModels();
                    certifList.CertificateNumber = int.Parse(row.Cells[3].Text);
                    certifList.EffectiveDate = DateTime.Parse(DDL_TRNSDT.SelectedValue);
                    certifList.FundSource = row.Cells[0].Text;
                    certifList.FundDestination = row.Cells[2].Text;
                    certifList.Mode = 1;
                    certifList.BatchId = int.Parse(row.Cells[4].Text.Replace("&nbsp;",""));
                    certifList.FundMovementApprovalDelete(db);
                    db.CommitTransaction();
                }
                catch (Exception ex)
                {
                    db.RollbackTransaction();
                    throw new Exception();
                }
                finally
                {
                    db.Close();
                }

            }
        }

        protected void GV_LST_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex;
            int absoluteRow;
            GridViewRow row;
            
            if (e.CommandName.ToLower() == "view")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
                absoluteRow = Math.Abs((GV_LST.PageIndex * GV_LST.PageSize) - rowIndex);
                row = GV_LST.Rows[absoluteRow];
                Session["gv_lst_selectedRowIndex"] = absoluteRow;

                GridView GvSwitch = (GridView)row.Cells[4].FindControl("GV_SWITCH");
                Button btn = (Button)row.Cells[4].FindControl("BT_VIEW");

                FundSwitchingModels fundSwitching = new FundSwitchingModels();
                fundSwitching.CertificateNumber = int.Parse(row.Cells[2].Text);
                fundSwitching.EffectiveDate = DateTime.Parse(DDL_TRNSDT.SelectedValue);
                fundSwitching.Mode = 5;
                fundSwitching.BatchId = int.Parse(row.Cells[0].Text);

                DataTable data = fundSwitching.GetFundMovementGrid(db);
                GvSwitch.DataSource = data;
                GvSwitch.DataBind();

            }
            else if (e.CommandName.ToLower() == "deleted")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
                absoluteRow = Math.Abs((GV_LST.PageIndex * GV_LST.PageSize) - rowIndex);
                row = GV_LST.Rows[absoluteRow];

                Session["gv_lst_selectedRowIndex"] = absoluteRow;

                GridView GvSwitch = (GridView) row.Cells[4].FindControl("GV_SWITCH");
                for (int i = 0; i < GvSwitch.Rows.Count; i++)
                {
                    FundSwitchingModels fs = new FundSwitchingModels();
                    fs.CertificateNumber = int.Parse(row.Cells[2].Text);
                    fs.EffectiveDate = DateTime.Parse(DDL_TRNSDT.SelectedValue);
                    fs.FundSource = GvSwitch.Rows[i].Cells[0].Text;
                    fs.FundDestination = GvSwitch.Rows[i].Cells[1].Text;
                    fs.Mode = 0;
                    fs.BatchId = int.Parse(row.Cells[0].Text);

                    fs.FundMovementApprovalDelete(db);
                }

                FillGvList();
                FillGvSummary();
                FillGvProcess();
            }
            else if (e.CommandName.ToLower() == "apprv")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument);
                absoluteRow = Math.Abs((GV_LST.PageIndex * GV_LST.PageSize) - rowIndex);
                row = GV_LST.Rows[absoluteRow];

                Session["gv_lst_selectedRowIndex"] = absoluteRow;

                string sUID = Session["session.iduser"].ToString();

                //query sp_fund_mvmnt_apprv_delete
                FundSwitchingModels fs = new FundSwitchingModels();
                fs.CertificateNumber = int.Parse(row.Cells[2].Text);
                fs.EffectiveDate = DateTime.Parse(DDL_TRNSDT.SelectedValue);
                fs.FundSource = "MONEY MARKED";
                fs.FundDestination = "MONEY MARKED";
                fs.Mode = 1;
                fs.BatchId = int.Parse(row.Cells[0].Text.Replace("&nbsp;", ""));

                fs.FundMovementApprovalDelete(db);

                //usp_fpp_perkiraan mode 11
                FPP fpp = new FPP();
                fpp.SeqNmbr = int.Parse(row.Cells[0].Text.Replace("&nbsp;",""));
                fpp.GroupNmbr = int.Parse(row.Cells[2].Text);
                fpp.ModeOfTransaction = 11;
                fpp.UserNm = sUID;
                fpp.ApprovalNm = "";
                fpp.UpdateFppPerkiraan(db);            

                //usp_fpp_perkiraan mode 12
                fpp.ModeOfTransaction = 12;
                fpp.UpdateFppPerkiraan(db);

                //usp_fpp_perkiraan mode 13
                fpp.ModeOfTransaction = 13;
                fpp.UpdateFppPerkiraan(db);

                //rebind all gridview
                FillGvList();
                FillGvSummary();
                FillGvProcess();


                //open report viewer Response.Write("<script language='javascript'>window.open('http://" +Dns.GetHostName()+ "/ReportServer?%2fPensionReport%2fAJTM+-+FLP+SWITCHING&rc:Toolbar=true&rc:Parameters=false&batch_id=" + e.Item.Cells[0].Text + "&grp=" +e.Item.Cells[2].Text+ "','MenuAccess','status=no,scrollbars=yes,width=800,height=600');</script>");
            }
            else if (e.CommandName.ToLower() == "all")
            {
                for (int i = 0; i < GV_LST.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GV_LST.Rows[i].Cells[0].FindControl("CB");
                    cb.Checked = true;
                }
            }
           
        }

        protected void GV_LST_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}