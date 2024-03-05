using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class CertificateInfoModels
    {
        public int CertificateNumber { get; set; }
        public string OldCertificateNumber { get; set; }
        public int ClientNumber { get; set; }
        public int GroupNumber { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime? EmploymentDate { get; set; }
        public int RetirementAge { get; set; }
        public int? PaycenterNumber { get; set; }
        public int PaymentTypeNumber { get; set; }
        public int? CitizenshipCode { get; set; }
        public string TaxIdNumber { get; set; }
        public int? JobVacationNumber { get; set; }
        public int? FundSourceNumber { get; set; }
        public DateTime? ApplicationReceiveDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime? KitDeliveryDate { get; set; }
        public bool FirstPremiumFlag { get; set; }
        public bool OtherDPPKFlag { get; set; }
        public float SumInsured { get; set; }
        public DateTime? SumInsuredEffectiveDate { get; set; }
        public float SalaryAmount { get; set; }
        public DateTime? SalaryEffectiveDate { get; set; }
        public DateTime? StatusEffectiveDate { get; set; }
        public string StatusTypeName { get; set; }
        public string Branch { get; set; }
        public string SalesName { get; set; }
        public int? PremiumTypeNumber { get; set; }
        public int? RiderTypeNumber { get; set; }
        public int AgentNumber { get; set; }
        public int? CommissionTypeNumber { get; set; }
        public int? PlanTypeNumber { get; set; }
        public bool ApuPpt { get; set; }
        public string BeneTypeNm { get; set; }

        //InvestDirection
        public int? MoneyTypeNmbr { get; set; }
        public int? InvTypeNmbr { get; set; }
        public float? Percentage { get; set; }



        public bool InsertCertifSumInsured(Database db)
        {
            string query = "exec usp_sum_insured_i @cer_nmbr, @efctv_dt, @sum_insured, @bene_type_nm";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                db.AddParameter("@efctv_dt", this.SumInsuredEffectiveDate);
                db.AddParameter("@sum_insured", this.SumInsured);
                db.AddParameter("@bene_type_nm", this.BeneTypeNm);

                db.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Dictionary<string, object> LoadDataById(Database db)
        {
            string query = "EXEC sp_certificate_r @cer_nmbr";
            Dictionary<string, object> output = new Dictionary<string, object>();
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                DataTable result = db.ExecuteQuery();
                if (result.Rows.Count > 0)
                {
                    DataRow FirstRow = result.Rows[0];
                    foreach (DataColumn col in result.Columns)
                    {
                        output.Add(col.ColumnName, FirstRow[col]);
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return output;
        }

        public DataTable LoadGvMCP(Database db, int GroupNmbr, int McpNmbr, int CerNmbr)
        {
            string query = "EXEC sp_mbr_cls_plan_r @group_nmbr, @mcp_nmbr, @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", GroupNmbr);
                db.AddParameter("@mcp_nmbr", McpNmbr);
                db.AddParameter("@cer_nmbr", CerNmbr);
                DataTable result = db.ExecuteQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadGvSumInsured(Database db, int CerNmbr, string SumInsuredDt)
        {
            string query = "select b.bene_type_nm,sum_insured from sum_insured s join bene_type b on s.bene_type_nmbr = b.bene_type_nmbr where s.cer_nmbr = @cer_nmbr and s.efctv_dt = @sum_insured_dt";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr",CerNmbr);
                db.AddParameter("@sum_insured_dt", SumInsuredDt);
                DataTable result = db.ExecuteQuery();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable LoadGvInvestDirection(Database db, int CerNmbr)
        {
            string query = "EXEC sp_cer_inv_drct_r @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", CerNmbr);
                DataTable result = db.ExecuteQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertCertifInvDrct(Database db)
        {
            string query = "EXEC sp_cer_inv_drct_i @cer_nmbr, @efctv_dt, @inv_type_nmbr, @money_type_nmbr, @percentage";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                db.AddParameter("@efctv_dt", this.EffectiveDate);
                db.AddParameter("@inv_type_nmbr", this.InvTypeNmbr);
                db.AddParameter("@money_type_nmbr", this.MoneyTypeNmbr);
                db.AddParameter("@percentage", this.Percentage);

                db.ExecuteNonQuery();
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


        public bool IsExist(Database db)
        {
            string query = "EXEC sp_cer_inv_drct_check @cer_nmbr, @efctv_dt, @inv_type_nmbr, @money_type_nmbr, @percentage ";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                db.AddParameter("@efctv_dt", this.EffectiveDate);
                db.AddParameter("@inv_type_nmbr", this.InvTypeNmbr);
                db.AddParameter("@money_type_nmbr", this.MoneyTypeNmbr);
                db.AddParameter("@percentage", this.Percentage);

                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public int InsertOrUpdateCertificate(Database x )
        {
            String query = "exec sp_certificate_i " +
                           "@cer_nmbr, @old_cer_nmbr, @client_nmbr, @group_nmbr, @employee_nmbr, " +
                           "@employment_dt, @retirement_age, @paycenter_nmbr, @payment_type_nmbr, " +
                           "@citizenship_code, @tax_id_number, @job_vacation_number, @fund_source_number, " +
                           "@application_receive_date, @completion_date, @effective_date, @termination_date, " +
                           "@kit_delivery_date, @first_premium_flag, @other_dppk_flag, @sum_insured, " +
                           "@sum_insured_effective_date, @salary_amount, @salary_effective_date, " +
                           "@status_effective_date, @status_type_name, @branch, @sales_name, " +
                           "@premium_type_number, @rider_type_number, @agent_number, @commission_type_number, " +
                           "@plan_type_number, @apu_ppt";

            try
            {
                x.setQuery(query);

                x.AddParameter("@cer_nmbr", 0);
                x.AddParameter("@old_cer_nmbr", DBNull.Value);
                x.AddParameter("@client_nmbr", this.ClientNumber);
                x.AddParameter("@group_nmbr", this.GroupNumber);
                x.AddParameter("@employee_nmbr", this.EmployeeNumber);
                x.AddParameter("@employment_dt", this.EmploymentDate);
                x.AddParameter("@retirement_age", this.RetirementAge);
                x.AddParameter("@paycenter_nmbr", this.PaycenterNumber);
                x.AddParameter("@payment_type_nmbr", this.PaymentTypeNumber);
                x.AddParameter("@citizenship_code", this.CitizenshipCode);
                x.AddParameter("@tax_id_number", this.TaxIdNumber);
                x.AddParameter("@job_vacation_number", this.JobVacationNumber);
                x.AddParameter("@fund_source_number", this.FundSourceNumber);
                x.AddParameter("@application_receive_date", this.ApplicationReceiveDate);
                x.AddParameter("@completion_date", this.CompletionDate);
                x.AddParameter("@effective_date", this.EffectiveDate);
                x.AddParameter("@termination_date", this.TerminationDate);
                x.AddParameter("@kit_delivery_date", this.KitDeliveryDate);
                x.AddParameter("@first_premium_flag", this.FirstPremiumFlag);
                x.AddParameter("@other_dppk_flag", this.OtherDPPKFlag);
                x.AddParameter("@sum_insured", this.SumInsured);
                x.AddParameter("@sum_insured_effective_date", this.SumInsuredEffectiveDate);
                x.AddParameter("@salary_amount", this.SalaryAmount);
                x.AddParameter("@salary_effective_date", this.SalaryEffectiveDate);
                x.AddParameter("@status_effective_date", this.StatusEffectiveDate);
                x.AddParameter("@status_type_name", this.StatusTypeName);
                x.AddParameter("@branch", this.Branch);
                x.AddParameter("@sales_name", "");
                x.AddParameter("@premium_type_number", this.PremiumTypeNumber);
                x.AddParameter("@rider_type_number", this.RiderTypeNumber);
                x.AddParameter("@agent_number", this.AgentNumber);
                x.AddParameter("@commission_type_number", this.CommissionTypeNumber);
                x.AddParameter("@plan_type_number", this.PlanTypeNumber);
                x.AddParameter("@apu_ppt", this.ApuPpt);

                //SqlParameter outputParameter = new SqlParameter("@new_certif_nmbr", DbType.Int32);
                //outputParameter.Direction = ParameterDirection.Output;
                //x.AddOutputParam(outputParameter);

                int result = 0;
                DataTable dt = x.ExecuteQuery();
                if (dt.Rows.Count > 0)
                {
                    result += Convert.ToInt32(dt.Rows[0]["cer_nmbr"]);
                }

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
    
}