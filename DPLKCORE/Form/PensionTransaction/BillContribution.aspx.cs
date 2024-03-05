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
    public partial class BillContribution : System.Web.UI.Page
    {
        Database db;
        Connection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (!IsPostBack)
            {
                //setup and fill DDL
                Setup();
            }
        }

        private void Setup()
        {
            fillDDLMode();
        }

        private void fillDDLMode()
        {
            try
            {
                db.Open();
                DDLBillingContribution ddlHelper = new DDLBillingContribution();
                ddlHelper.LoadDDLMode(db, DropDownListMode);
                DropDownListMode.SelectedIndex = 0;
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

        private void fillDDLCompany()
        {
            try
            {
                db.Open();
                DDLBillingContribution ddlHelper = new DDLBillingContribution();
                ddlHelper.LoadDDLCompany(db, DropDownListCompany);
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


        protected void DropDownListMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDDLCompany();
        }

        protected void DropDownListCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDDLGroup();
        }

        private void fillDDLGroup()
        {
            try
            {
                db.Open();
                DDLBillingContribution ddlHelper = new DDLBillingContribution();
                ddlHelper.LoadDDLGroup(db, DropDownListGroup, DropDownListCompany.SelectedValue);
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

        protected void BtnShow_Click(object sender, EventArgs e)
        {
            //display the data master gridview
            if (LoadDataMaster())
            {
                //enable grid buttons and controls
                EnableGridviewControls();
            }
        }

        private void EnableGridviewControls()
        {
            for (int i = 0; i < GridViewBillContribution.Rows.Count; i++)
            {
                Button bt1 = (Button)GridViewBillContribution.Rows[i].FindControl("BT_PAYMENT");
                Button bt2 = (Button)GridViewBillContribution.Rows[i].FindControl("BT_REVERSE");
                Button bt3 = (Button)GridViewBillContribution.Rows[i].FindControl("BT_SUSPENSE");
                TextBox TXT = (TextBox)GridViewBillContribution.Rows[i].FindControl("TXT_REM");

                bt1.Enabled = true;
                bt2.Enabled = true;
                bt3.Enabled = true;
                TXT.Enabled = true;

                if (GridViewBillContribution.Rows[i].Cells[13].Text == "&nbsp;")
                {
                    TXT.Text = "";
                }
                else
                {
                    TXT.Text = GridViewBillContribution.Rows[i].Cells[13].Text;
                }

                if (GridViewBillContribution.Rows[i].Cells[10].Text != "0.00" || GridViewBillContribution.Rows[i].Cells[12].Text != "&nbsp;")
                {
                    bt1.Enabled = false;
                    bt2.Enabled = false;
                    bt3.Enabled = false;
                    TXT.Enabled = false;
                }

            }
        }

        private bool LoadDataMaster()
        {
            try
            {
                db.Open();
                DPLKCORE.Logic.Pension.PaymentHistory.BillContributionScreenParameters bill = new DPLKCORE.Logic.Pension.PaymentHistory.BillContributionScreenParameters();
                bill.GroupNumber = int.Parse(DropDownListGroup.SelectedValue);
                bill.Mode = int.Parse(DropDownListMode.SelectedValue);
                DataTable data = bill.GetBillContributionScreen(db);
                if (data.Rows.Count > 0)
                {
                    GridViewBillContribution.DataSource = data;
                    GridViewBillContribution.DataBind();
                    return true;
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
            return false;
        }

        protected void GridViewBillContribution_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewBillContribution.PageIndex = e.NewPageIndex;
            if (LoadDataMaster())
            {
                EnableGridviewControls();
            } 
        }

        protected void GridViewBillContribution_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "bt_payment":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    int absoluteRow = Math.Abs((GridViewBillContribution.PageIndex * GridViewBillContribution.PageSize) - rowIndex);
                    GridViewRow row = GridViewBillContribution.Rows[absoluteRow];
                    TextBox TEXT = (TextBox) row.Cells[1].FindControl("TXT_REM");
                    if (row.Cells[10].Text == "0.00" && row.Cells[12].Text == "&nbsp;")
                    {
                        //query1
                        try
                        {
                            db.Open();
                            DPLKCORE.Logic.Pension.PaymentHistory.BillContributionScreenParameters billContrib = new PaymentHistory.BillContributionScreenParameters();
                            billContrib.Mode = int.Parse(DropDownListMode.SelectedValue);
                            billContrib.GroupNumber = int.Parse(DropDownListGroup.SelectedValue);
                            billContrib.SeqNmbr = Convert.ToInt32(row.Cells[5]);

                            if (billContrib.CheckTerminateNew(db) != 0)
                            {
                                //popup with message: "Some Participant has terminate in this payment or inv direction not exists, reverse and re upload again "
                                return;
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
                        

                        double dtotalSuspn = 0;
                        double diff_amt = 0;

                        DataGrid DgSuspend = (DataGrid)row.Cells[3].FindControl("DGR_SUSPENSE");
                        for (int i = 0; i < DgSuspend.Items.Count; i++)
                        {
                            CheckBox cb = (CheckBox)DgSuspend.Items[i].Cells[0].FindControl("CB3");
                            if (cb.Checked)
                            {
                                dtotalSuspn = dtotalSuspn+double.Parse(DgSuspend.Items[i].Cells[5].Text.Replace(",",""));
                            }
                        }

                        diff_amt = double.Parse(row.Cells[9].Text.Replace(",", "")) - dtotalSuspn;

                        if (diff_amt <=1)
                        {
                            for (int i = 0; i < DgSuspend.Items.Count; i++)
                            {
                                CheckBox cb = (CheckBox) DgSuspend.Items[i].Cells[0].FindControl("CB3");
                                if (cb.Checked)
                                {
                                    //query2
                                    try
                                    {
                                        db.Open();
                                        db.BeginTransaction();
                                        DPLKCORE.Logic.Pension.PaymentHistory.PaymentContributionModel model = new DPLKCORE.Logic.Pension.PaymentHistory.PaymentContributionModel();
                                        model.GroupNumber = int.Parse(DropDownListGroup.SelectedValue);
                                        model.BillContributionNumber = Convert.ToInt32(row.Cells[6]);
                                        model.PaycenterNumber = Convert.ToInt32(row.Cells[7]);
                                        model.Trans = int.Parse(DropDownListMode.SelectedValue);
                                        model.SuspenseNumber = long.Parse(DgSuspend.Items[i].Cells[1].Text);
                                        model.SuspenseRestAmount = float.Parse(DgSuspend.Items[1].Cells[5].Text.Replace(",", ""));
                                        model.PaidAmount = float.Parse(row.Cells[9].Text.Replace(",", ""));
                                        model.ReceivedDate = null;
                                        model.WaivedFlag = 0;
                                        model.Note = null;

                                        model.ProcessPaymentContribution(db);
                                        db.CommitTransaction();
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
                            }

                            //query3
                            try
                            {
                                db.Open();
                                db.BeginTransaction();
                                DPLKCORE.Logic.Pension.PaymentHistory.PaymentContributionCompletedModel payContribCompleted = new DPLKCORE.Logic.Pension.PaymentHistory.PaymentContributionCompletedModel();
                                payContribCompleted.GroupNumber = int.Parse(DropDownListGroup.SelectedValue);
                                payContribCompleted.PaycenterNumber = Convert.ToInt32(row.Cells[7]);
                                payContribCompleted.BillContributionSeqNumber = Convert.ToInt32(row.Cells[6]);
                                payContribCompleted.PaidAmount = int.Parse(row.Cells[9].Text.Replace(",", ""));
                                payContribCompleted.TransType = int.Parse(DropDownListMode.SelectedValue);
                                payContribCompleted.Comment = TEXT.Text;

                                payContribCompleted.ProcessPaymentContributionCompleted(db);
                                db.CommitTransaction();
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

                            //query4
                            try
                            {
                                db.Open();
                                db.BeginTransaction();
                                UspFppPerkiraan uspFpp = new UspFppPerkiraan();
                                uspFpp.SeqNmbr = int.Parse(row.Cells[6].Text);
                                uspFpp.GroupNmbr = int.Parse(DropDownListGroup.SelectedValue);
                                uspFpp.ModeOfTransaction = int.Parse(DropDownListMode.SelectedValue);
                                uspFpp.UserNm = Session["session.iduser"].ToString();
                                uspFpp.ApprovalNm = "Taupid";

                                uspFpp.ExecUspFppPerkiraan(db);
                                db.CommitTransaction();

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

                            //go to report view
                            //Response.Write("<script language='javascript'>window.open('http://" +Dns.GetHostName()+ "/ReportServer?%2fPensionReport%2fAJTM+-+FLP&rc:Toolbar=true&rc:Parameters=false&Company=" +DDL_COMPANY.SelectedItem.Text+ "&seq_nmbr=" +e.Item.Cells[6].Text+ "&group_nmbr=" +DDL_GROUP.SelectedValue + "&mode=" +DDL_MODE.SelectedValue+ "','MenuAccess','status=no,scrollbars=yes,width=800,height=600');</script>");
                            
                        }
                        else
                        {
                            //popup with message "Suspense Not enough "
                            return;
                        }

                    }
                    if (LoadDataMaster())
                    {
                        EnableGridviewControls();
                    }
                    break;
                case "bt_reverse":
                    int rowIndex2 = Convert.ToInt32(e.CommandArgument);
                    int absoluteRow2 = Math.Abs((GridViewBillContribution.PageIndex * GridViewBillContribution.PageSize) - rowIndex2);
                    GridViewRow row2 = GridViewBillContribution.Rows[absoluteRow2];

                    if (row2.Cells[10].Text == "0.00")
                    {
                        //query 5
                        try
                        {
                            db.Open();
                            db.BeginTransaction();
                            DPLKCORE.Logic.Pension.PaymentHistory.ReversalBillContributionModel reversalBill = new DPLKCORE.Logic.Pension.PaymentHistory.ReversalBillContributionModel();
                            reversalBill.Mode = int.Parse(DropDownListMode.SelectedValue);
                            reversalBill.Mode = int.Parse(DropDownListGroup.SelectedValue);
                            reversalBill.SequenceNumber = int.Parse(row2.Cells[6].Text);
                            reversalBill.ReversalBillContribution(db);
                            db.CommitTransaction();
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

                    if (LoadDataMaster())
                    {
                        EnableGridviewControls();
                    }
                    break;
                case "Suspense":
                    int rowIndex3 = Convert.ToInt32(e.CommandArgument);
                    int absoluteRow3 = Math.Abs((GridViewBillContribution.PageIndex * GridViewBillContribution.PageSize) - rowIndex3);
                    GridViewRow row3 = GridViewBillContribution.Rows[absoluteRow3];
                    DataGrid DgSuspense2 = (DataGrid)row3.Cells[2].FindControl("DGR_SUSPENSE");

                    //query 6
                    try
                    {
                        db.Open();
                        DDLBillingContribution ddlHelper = new DDLBillingContribution();
                        ddlHelper.GroupNmbr = DropDownListGroup.SelectedValue;
                        ddlHelper.PaycenterNmbr = row3.Cells[7].Text;

                        DataTable data = ddlHelper.LoadDgSuspense(db);

                        DgSuspense2.DataSource = data;
                        DgSuspense2.DataBind();
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        db.Close();
                    }
                    break;
                case "bt_detail":
                    //go to report view
                    //Response.Write("<script language='javascript'>window.open('http://" +Dns.GetHostName()+ "/ReportServer?%2fPensionReport%2fAJTM+-+DETAIL+CONTRIBUTION&rc:Toolbar=true&rc:Parameters=false&group_nmbr=" +DDL_GROUP.SelectedValue + "&seq_nmbr=" +e.Item.Cells[6].Text+ "&paycenter_nmbr=" +e.Item.Cells[7].Text+  "&mode=" +DDL_MODE.SelectedValue+ "','MenuAccess','status=no,scrollbars=yes,width=800,height=600');</script>");
                    break;
                case "bt_kuitansi":
                    //go to report view
                    //Response.Write("<script language='javascript'>window.open('http://" +Dns.GetHostName()+ "/ReportServer?%2fPensionReport%2fAJTM+-+KUITANSI&rc:Toolbar=true&rc:Parameters=false&group_nmbr=" +DDL_GROUP.SelectedValue + "&seq_nmbr=" +e.Item.Cells[6].Text+ "&paycenter_nmbr=" +e.Item.Cells[7].Text+  "&mode=" +DDL_MODE.SelectedValue+ "','MenuAccess','status=no,scrollbars=yes,width=800,height=600');</script>");
                    break;
                default:
                    break;
            }
        }
    }
}