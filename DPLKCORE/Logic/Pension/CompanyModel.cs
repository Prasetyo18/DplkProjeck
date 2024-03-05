using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DPLKCORE.Framework;
using DPLKCORE.Class;
using DPLKCORE.Logic.Administrasi;
using Microsoft.Office.Core;
using System.Web.UI.WebControls;
using System.Collections;
using CrystalDecisions.ReportAppServer.DataDefModel;
using System.Data;
using Database = DPLKCORE.Framework.Database;
using Connection = DPLKCORE.Framework.Connection;

namespace DPLKCORE.Class.Pension
{
    public class CompanyModel
    {


        public int ClientNmbr { get; set; }

        public string CompanyNm { get; set; }

        public short HasPaycenter { get; set; }

        public string Npwp { get; set; }

        public int BusinessLineNmbr { get; set; }
        public String BusinessLineNmbrStrNmbr { get; set; }
        public String BusinessLineNmbrStrNM { get; set; }

        public string ContactPerson { get; set; }

        public DateTime LastChangeDt { get; set; }

        public string Siup { get; set; }

        public short? MnySrcType { get; set; }

        public string PayorNm { get; set; }

        public string BankNm { get; set; }

        public string AccountNmbr { get; set; }

        public string AccountNm { get; set; }

        public string Email { get; set; }

        public string AdArt { get; set; }

        public bool? PdpFlg { get; set; }

        public string OldClientNmbr { get; set; }


        public void LoadDataParamToDDLBisnis(DropDownList ddl, Framework.Database db)
        {
            CompanyModel m = new CompanyModel();
            List<CompanyModel> data = new List<CompanyModel>();

            data.AddRange(this.GetParamDDL(db));

            ddl.DataSource = data;
            ddl.DataValueField = "BusinessLineNmbrStrNmbr";
            ddl.DataTextField = "BusinessLineNmbrStrNM";
            ddl.DataBind();
        }


        public List<CompanyModel> GetParamDDL(Framework.Database db)
        {
            List<CompanyModel> data = new List<CompanyModel >();

            String query = "EXEC DDL_PARAM 'business_sctr' ";



            try
            {
                db.setQuery(query);

   
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    CompanyModel m = new CompanyModel();


                    if (reader["business_sctr_nmbr"] != DBNull.Value)
                        m.BusinessLineNmbrStrNmbr = reader["business_sctr_nmbr"].ToString().Trim();

                    if (reader["business_sctr_nm"] != DBNull.Value)
                        m.BusinessLineNmbrStrNM = reader["business_sctr_nm"].ToString().Trim();


                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }


        public List<CompanyModel> GetCompanies()
        {
            List<CompanyModel> companies = new List<CompanyModel>();
            Connection con = new Connection();
            String cnstr = con.ConnectionStringPension;
            Database db = new Database(cnstr);

            String query = "SELECT * FROM Company";

            try
            {
                db.Open();
                db.setQuery(query);

                DataTable result = db.ExecuteQuery();

                foreach (DataRow row in result.Rows)
                {
                    CompanyModel company = new CompanyModel
                    {
                        ClientNmbr = Convert.ToInt32(row["client_nmbr"]),
                        CompanyNm = row["company_nm"].ToString(),
                        HasPaycenter = Convert.ToInt16(row["has_paycenter"]),
                        Npwp = row["NPWP"].ToString(),
                        BusinessLineNmbr = Convert.ToInt32(row["business_line_nmbr"]),
                        ContactPerson = row["contact_person"].ToString(),
                        LastChangeDt = Convert.ToDateTime(row["last_change_dt"]),
                        Siup = row["SIUP"].ToString(),
                        MnySrcType = row["Mny_src_type"] != DBNull.Value ? (short?)Convert.ToInt16(row["Mny_src_type"]) : null,
                        PayorNm = row["Payor_nm"].ToString(),
                        BankNm = row["Bank_nm"].ToString(),
                        AccountNmbr = row["Account_nmbr"].ToString(),
                        AccountNm = row["Account_nm"].ToString(),
                        Email = row["email"].ToString(),
                     
                        AdArt = row["ad_art"].ToString(),
                        PdpFlg = row["pdp_flg"] != DBNull.Value ? (bool?)Convert.ToBoolean(row["pdp_flg"]) : null,
                        OldClientNmbr = row["old_client_nmbr"].ToString()

                    };

                    companies.Add(company);
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

            return companies;
        }


        public void InsertCompany(  DPLKCORE.Framework.Database x)
        {

            String query = "exec sp_company_i " +
                           "@company_name, @has_paycenter, @NPWP, @business_line_nmbr, " +
                           "@contact_person, @SIUP, @mny_src_type, @payor_nm, @bank_nm, " +
                           "@account_nmbr, @account_nm, @email, @ad_art, @pdp_flg, @old_client_nmbr";
            try
            {
                x.setQuery(query);

                x.AddParameter("@company_name", this.CompanyNm);
                x.AddParameter("@has_paycenter", this.HasPaycenter);
                x.AddParameter("@NPWP", this.Npwp);
                x.AddParameter("@business_line_nmbr", this.BusinessLineNmbr);
                x.AddParameter("@contact_person", this.ContactPerson);
                x.AddParameter("@SIUP", this.Siup);
                x.AddParameter("@mny_src_type", this.MnySrcType);
                x.AddParameter("@payor_nm", this.PayorNm);
                x.AddParameter("@bank_nm", this.BankNm);
                x.AddParameter("@account_nmbr", this.AccountNmbr);
                x.AddParameter("@account_nm", this.AccountNm);
                x.AddParameter("@email", this.Email);
                x.AddParameter("@ad_art", this.AdArt);
                x.AddParameter("@pdp_flg", this.PdpFlg);
                x.AddParameter("@old_client_nmbr", this.OldClientNmbr);

                x.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateCompany(DPLKCORE.Framework.Database x)
        {
            String query = "exec SP_COMPANY_U @CompanyId, @CompanyNm, @HasPaycenter, @Npwp, @BusinessLineNmbr," +
                "@ContactPerson, @Siup, @MnySrcType, @PayorNm, @BankNm, @AccountNmbr, @AccountNm," +
                "@Email, @AdArt, @PdpFlg, @OldClientNmbr";
            try
            {
                x.setQuery(query);

                x.AddParameter("@client_nmbr", this.ClientNmbr); //key
                x.AddParameter("@CompanyNm", this.CompanyNm);
                x.AddParameter("@HasPaycenter", this.HasPaycenter);
                x.AddParameter("@Npwp", this.Npwp);
                x.AddParameter("@BusinessLineNmbr", this.BusinessLineNmbr);
                x.AddParameter("@ContactPerson", this.ContactPerson);
                x.AddParameter("@Siup", this.Siup);
                x.AddParameter("@MnySrcType", this.MnySrcType);
                x.AddParameter("@PayorNm", this.PayorNm);
                x.AddParameter("@BankNm", this.BankNm);
                x.AddParameter("@AccountNmbr", this.AccountNmbr);
                x.AddParameter("@AccountNm", this.AccountNm);
                x.AddParameter("@Email", this.Email);
                x.AddParameter("@AdArt", this.AdArt);
                x.AddParameter("@PdpFlg", this.PdpFlg);
                x.AddParameter("@OldClientNmbr", this.OldClientNmbr);
                x.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteCompany(DPLKCORE.Framework.Database x)
        {
            String query = "DELETE FROM COMPANY WHERE client_nmbr = @ClientNmbr";
            try
            {
                x.setQuery(query);

                x.AddParameter("@ClientNmbr", this.ClientNmbr);

                x.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}


