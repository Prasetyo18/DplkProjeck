using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPLKCORE.Logic.Pension;
using System.Data;

namespace DPLKCORE.Form.PensionTransaction
{
    public partial class FundSwitchingRequestFundApproval : System.Web.UI.Page
    {
        Database db;
        Connection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new Connection();
            db = new Database(conn.ConnectionStringPension);

            if (!IsPostBack)
            {
                Go();

            }
        }

        private void Go()
        {
            try
            {
                GV_LIST.DataSource = FundSwitchingRequestFundApprovalModels.Get_CM_Swithcing_approve_amount(db);
                GV_LIST.DataBind();
            }
            catch (Exception ex)
            {

                throw new Exception();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateFundMovementEst();
        }

        private void UpdateFundMovementEst()
        {
            try
            {

                for (int i = 0; i < GV_LIST.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GV_LIST.Rows[i].Cells[0].FindControl("CB");
                    if (cb.Checked)
                    {
                        FundSwitchingRequestFundApprovalModels requestFundApproval = new FundSwitchingRequestFundApprovalModels();
                        requestFundApproval.CertificateNumber = int.Parse(GV_LIST.Rows[i].Cells[1].Text);
                        requestFundApproval.UserId = HttpContext.Current.Request.UserHostName;
                        requestFundApproval.BatchId = int.Parse(GV_LIST.Rows[i].Cells[3].Text);
                        requestFundApproval.Update_Fund_Movement_Est(db);
                    }
                }

                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            GV_LIST.PageIndex = 0;
            Go();
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            FillApproval();
        }

        private void FillApproval()
        {
            double MM = 0;
            double FIX = 0;
            double SHM = 0;
            double SYA = 0;
            double USD = 0;
            double KONDUR = 0;
            double EMOI = 0;
            double VICO = 0;
            double STAR = 0;
            double CNOOC = 0;
            double PREMIER = 0;
            double ENI = 0;
            double CHEVRON = 0;
            double MANDIRI = 0;
            double MAGMA = 0;
            double PETROCINA = 0;
            double NONFUND = 0;

            for (int i = 0; i < GV_LIST.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)GV_LIST.Rows[i].Cells[0].FindControl("CB");
                if (cb.Checked)
                {
                    MM = MM +  double.Parse(GV_LIST.Rows[i].Cells[4].Text.Replace(",","")); 						
					FIX = FIX +  double.Parse(GV_LIST.Rows[i].Cells[5].Text.Replace(",","")); 						
					SHM = SHM +  double.Parse(GV_LIST.Rows[i].Cells[6].Text.Replace(",","")); 						
					SYA = SYA +  double.Parse(GV_LIST.Rows[i].Cells[7].Text.Replace(",","")); 						
					USD = USD +  double.Parse(GV_LIST.Rows[i].Cells[8].Text.Replace(",","")); 						
					KONDUR = KONDUR +  double.Parse(GV_LIST.Rows[i].Cells[9].Text.Replace(",","")); 						
					EMOI = EMOI +  double.Parse(GV_LIST.Rows[i].Cells[10].Text.Replace(",","")); 						
					VICO = VICO +  double.Parse(GV_LIST.Rows[i].Cells[11].Text.Replace(",","")); 						
					STAR = STAR +  double.Parse(GV_LIST.Rows[i].Cells[12].Text.Replace(",","")); 						
					CNOOC = CNOOC +  double.Parse(GV_LIST.Rows[i].Cells[13].Text.Replace(",","")); 						
					PREMIER = PREMIER +  double.Parse(GV_LIST.Rows[i].Cells[14].Text.Replace(",","")); 						
					ENI = ENI +  double.Parse(GV_LIST.Rows[i].Cells[15].Text.Replace(",","")); 						
					CHEVRON = CHEVRON +  double.Parse(GV_LIST.Rows[i].Cells[16].Text.Replace(",","")); 						
					MANDIRI = MANDIRI +  double.Parse(GV_LIST.Rows[i].Cells[17].Text.Replace(",","")); 						
					MAGMA = MAGMA +  double.Parse(GV_LIST.Rows[i].Cells[18].Text.Replace(",","")); 						
					PETROCINA = PETROCINA +  double.Parse(GV_LIST.Rows[i].Cells[19].Text.Replace(",","")); 						
					NONFUND = NONFUND +  double.Parse(GV_LIST.Rows[i].Cells[20].Text.Replace(",",""));
                }

                string parameters = MM + " ," + FIX + "," + SHM + "," + SYA + "," +
                "" + USD + "," + KONDUR + "," + EMOI.ToString() + "," + VICO + "," + STAR + "," + CNOOC + "," +
                "" + PREMIER + "," + ENI + "," + CHEVRON + "," + MANDIRI + "," + MAGMA + "," + PETROCINA + "," + NONFUND;

                FillGVDetail(parameters);
            }
        }

        private void FillGVDetail(string pars)
        {
            try
            {
                GV_Detail.DataSource = FundSwitchingRequestFundApprovalModels.Get_CM_Swithcing_approve_amount(db, pars);
                GV_Detail.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void GV_LIST_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "all")
            {
                for (int i = 0; i < GV_LIST.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GV_LIST.Rows[i].Cells[0].FindControl("CB");
                    cb.Checked = true;
                }                
            }
        }


    }
}