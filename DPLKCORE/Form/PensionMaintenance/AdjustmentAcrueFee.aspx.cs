//using CrystalDecisions.ReportAppServer.DataDefModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace IndividualLife.Form.PensionMaintenance
//{
//    public class AcrueFee : System.Web.UI.Page
//    {
//        protected System.Web.UI.WebControls.TextBox TXT_DATE;
//        protected System.Web.UI.WebControls.DataGrid DGR_TOTAL;
//        protected System.Web.UI.WebControls.Button BTN_SHOW;
//        protected System.Web.UI.WebControls.DataGrid DGR_DETAIL;
//        protected System.Web.UI.WebControls.Button Button1;
//        protected System.Web.UI.WebControls.Button BTN_CALC;
//        protected System.Web.UI.WebControls.Button BTN_DETAIL;
//        protected System.Web.UI.WebControls.DropDownList DDL_ALL;
//        protected System.Web.UI.WebControls.Button BTN_DELETE;
//        protected Connection Conn;
//        private void Page_Load(object sender, System.EventArgs e)
//        {
//            Conn = new Connection(Session["sql"].ToString());
//            if (!IsPostBack)
//            {

//            }

//            TXT_DATE.Attributes["onkeydown"] = "return(myutilFormatDate(this, '/', event))";

//        }

//        #region Web Form Designer generated code
//        override protected void OnInit(EventArgs e)
//        {
//            //
//            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
//            //
//            InitializeComponent();
//            base.OnInit(e);
//        }

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.BTN_CALC.Click += new System.EventHandler(this.BTN_CALC_Click);
//            this.BTN_SHOW.Click += new System.EventHandler(this.BTN_SHOW_Click);
//            this.Button1.Click += new System.EventHandler(this.Button1_Click);
//            this.BTN_DETAIL.Click += new System.EventHandler(this.BTN_DETAIL_Click);
//            this.BTN_DELETE.Click += new System.EventHandler(this.BTN_DELETE_Click);
//            this.Load += new System.EventHandler(this.Page_Load);

//        }
//        #endregion

//        private void FILL_GRID_TOTAL()
//        {
//            Conn.QueryString = "exec DGR_ON_SCR_ACCRUE_FEE '" + TXT_DATE.Text + "',1,0";
//            Conn.ExecuteQuery(150000);
//            DataTable dtTOTAL = new DataTable();
//            dtTOTAL = Conn.GetDataTable().Copy();

//            DGR_TOTAL.DataSource = dtTOTAL;
//            DGR_TOTAL.DataBind();
//        }

//        private void FILL_GRID_DETAIL()
//        {
//            Conn.QueryString = "exec DGR_ON_SCR_ACCRUE_FEE '" + TXT_DATE.Text + "',2," + DDL_ALL.SelectedValue + "";
//            Conn.ExecuteQuery(150000);
//            DataTable dtTOTAL = new DataTable();
//            dtTOTAL = Conn.GetDataTable().Copy();

//            DGR_DETAIL.DataSource = dtTOTAL;
//            DGR_DETAIL.DataBind();

//        }
//        private void FILL_DDL()
//        {
//            string SQL;
//            SQL = "exec DDL_COMPANY_ON_SCR_ACCRUE_FEE '" + TXT_DATE.Text + "'";
//            Conn.QueryString = SQL;
//            Conn.ExecuteQuery(150000);

//            DDL_ALL.Items.Clear();
//            for (int i = 0; i < Conn.GetRowCount(); i++)
//            {
//                DDL_ALL.Items.Add(new ListItem(Conn.GetFieldValue(i, 1).ToString(), Conn.GetFieldValue(i, 0).ToString()));
//            }

//        }

//        private void BTN_SHOW_Click(object sender, System.EventArgs e)
//        {
//            FILL_GRID_TOTAL();
//        }

//        private void Button1_Click(object sender, System.EventArgs e)
//        {
//            FILL_DDL();
//        }

//        private void BTN_DETAIL_Click(object sender, System.EventArgs e)
//        {
//            FILL_GRID_DETAIL();
//        }

//        private void BTN_CALC_Click(object sender, System.EventArgs e)
//        {
//            string SQL;
//            SQL = "exec cycle_fee_calculation_job '" + TXT_DATE.Text + "'";
//            Conn.QueryString = SQL;
//            Conn.ExecuteQuery(150000);
//            FILL_GRID_TOTAL();
//            FILL_DDL();
//            GlobalTools.popMessage(this, "Accrual Fee Calculation Finish");

//        }

//        private void BTN_DELETE_Click(object sender, System.EventArgs e)
//        {
//            string SQL, MSG;
//            SQL = "delete from cycle_certificate_charge where cycle_dt = '" + TXT_DATE.Text + "'";
//            Conn.QueryString = SQL;
//            Conn.ExecuteQuery(150000);
//            MSG = "Accrue fee for " + TXT_DATE.Text + " was deleted";
//            GlobalTools.popMessage(this, MSG);
//        }
//    }
//}