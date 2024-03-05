using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.UserControl
{
    public partial class SearchPanel : System.Web.UI.UserControl
    {
        public Button BtnSearch
        {
            get
            {
                return btSearch;
            }
            set
            {
                this.btSearch = value;
            }
        }

        public Button BtnCancel
        {
            get
            {
                return btnClose;
            }
            set
            {
                this.BtnCancel = value;
            }
        }

        public GridView GvResult
        {
            get
            {
                return DGSearchResult;
            }
            set
            {
                this.GvResult = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                if (Session["searchType"] != null)
                {
                    Setup();
                }
            }
        }

        //DATA BINDING
        private void populateLbTitle()
        {
            Connection con = new Connection();
            String constring = con.ConnectionStringPension;
            Database db = new Database(constring);

            try
            {
                db.Open();
                //populate the LbTitle
                String queryDesc = "select descr from SEARCH_FORM_TYPE where code=@type";
                db.setQuery(queryDesc);
                db.AddParameter("@type", LbType.Text);
                DataTable dt = db.ExecuteQuery();
                if (dt.Rows.Count > 0)
                {
                    LbTitle.Text = dt.Rows[0]["descr"].ToString();
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

        private void populateDGFields()
        {
            Connection con = new Connection();
            String constring = con.ConnectionStringPension;
            Database db = new Database(constring);

            try
            {
                db.Open();
                //populate the DGCategorySearch
                String queryGetField = "select fieldname, fieldalias, type from SEARCH_FORM_CRITERIA where code=@type";
                db.setQuery(queryGetField);
                db.AddParameter("@type", Session["searchType"].ToString());
                DataTable result = db.ExecuteQuery();
                if (result.Rows.Count > 0)
                {
                    DGSearchCategory.DataSource = result;
                    DGSearchCategory.DataBind();
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

        public void Setup()
        {
            DGSearchResult.DataSource = null;
            DGSearchResult.DataBind();

            Connection con = new Connection();
            String constring = con.ConnectionStringPension;
            Database db = new Database(constring);
            try
            {

                var group_nmbr = "";
                var company_nm = "";
                db.Open();

                LbType.Text = Session["searchType"].ToString();
                LbCaller.Text = Session["searchCaller"].ToString();

                if (LbType.Text == "12")
                {

                    String queryTypeCaller = "SELECT gi.group_nmbr, co.company_nm FROM company co INNER JOIN group_info gi ON co.client_nmbr = gi.client_nmbr WHERE co.client_nmbr = @filter";
                    db.setQuery(queryTypeCaller);
                    db.AddParameter("@filter", Request.QueryString["filter"].ToString());

                    SqlDataReader dr = db.ExecuteReader();
                    group_nmbr = dr["group_nmbr"].ToString();
                    company_nm = dr["company_nm"].ToString();
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Close();
            }
            populateLbTitle();
            populateDGFields();
        }

        //DATA SEARCH
        private void Search()
        {
            //clear the DGSearchResult before start searching
            DGSearchResult.DataSource = null;
            DGSearchResult.DataBind();

            //post to the paycenter.aspx and give the GridViewPaycenter a new data to bind
            //the new data should be from query that comply to the search rule defined by user.

            //db instance
            Connection con = new Connection();
            String constring = con.ConnectionStringPension;
            Database db = new Database(constring);

            try
            {
                if (LbTitle.Text != "")
                {
                    //collect the Data
                    String getSearchQuery = "select sql from SEARCH_FORM_TYPE where code=@type";
                    db.Open();
                    db.setQuery(getSearchQuery);
                    db.AddParameter("@type", Session["searchType"].ToString());

                    DataTable resultQuery = db.ExecuteQuery();
                    String q1 = resultQuery.Rows[0]["sql"].ToString();
                    String q2 = "";
                    for (int i = 0; i < DGSearchCategory.Items.Count; i++)
                    {
                        string opr = " like '%";
                        if (DGSearchCategory.Items[i].Cells[2].Text != "TXT")
                        {
                            opr = " = '";
                        }

                        TextBox txt = (TextBox)DGSearchCategory.Items[i].Cells[3].FindControl("TXT");
                        if (txt.Text.Trim() != "")
                        {
                            q2 = q2 + DGSearchCategory.Items[i].Cells[0].Text + opr + txt.Text;
                            if (DGSearchCategory.Items[i].Cells[2].Text != "TXT")
                            {
                                q2 = q2 + "' and ";
                            }
                            else
                            {
                                q2 = q2 + "%' and ";
                            }
                        }
                    }

                    if (q2.Length > 0)
                    {
                        string buff = q2.Substring(q2.Length - 4, 4);
                        if (buff == "and ")
                        {
                            q2 = q2.Substring(0, q2.Length - 4);
                        }
                        q2 = " where " + q2;
                    }

                    String finalQuery = q1 + q2;
                    db.setQuery(finalQuery);
                    DataTable dataResult = db.ExecuteQuery();
                    int var = dataResult.Rows.Count;
                    Session["data"] = dataResult;
                    DGSearchResult.DataSource = dataResult;
                    DGSearchResult.DataBind();
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

        protected void btSearch_Click(object sender, EventArgs e)
        {
            Search();
            
        }

        protected void DGSearchResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "SelectRow":
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    int finalIndex = Math.Abs((DGSearchResult.PageIndex * DGSearchResult.PageSize) - rowIndex);

                    GridViewRow row = DGSearchResult.Rows[finalIndex];
                    Session["searchResult"] = row;
                    break;
                default:
                    break;
            }

        }

        protected void DGSearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGSearchResult.PageIndex = e.NewPageIndex;
            if (Session["data"] != null)
            {
                DGSearchResult.DataSource = Session["data"];
                DGSearchResult.DataBind();
            }
        }
    }
}