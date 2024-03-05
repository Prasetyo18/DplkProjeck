using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class EntryPayrollContributionModels
    {
        public string GroupNumber { get; set; }
        public string CertificateNumber { get; set; }
        public string NIP { get; set; }
        public float ContributionER { get; set; }
        public float ContributionEE { get; set; }
        public float TopUp { get; set; }
        public float FT { get; set; }
        public string DateOfBirthString { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string HostName { get; set; }
        public DateTime ContributionPeriod { get; set; }

        public DataTable UploadPayroll(Database db)
        {
            DataTable result = new DataTable();
            string query = "exec USP_UPLOAD_PAYROLL_CONTRIBUTION @period,@hostname";
            try
            {
                db.setQuery(query);
                db.AddParameter("@period", this.ContributionPeriod);
                db.AddParameter("@hostname",this.HostName);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool InsertPayroll(Database db)
        {
            string query = "insert into UPLOAD_payroll_contribution (GroupNbr, CerNbr, NIP, dob, ConER, ConEE, TopUP, FT, bulan, tahun, hostname) "
                    + " values ("
                    + " @group_nmbr,"
                    + " @cer_nmbr,"
                    + " @nip,"
                    + " @dob,"
                    + " @con_er,"
                    + " @con_ee,"
                    + " @top_up,"
                    + " @ft,"
                    + " @month,"
                    + " @year,"
                    + " @hostname)";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr",this.GroupNumber);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                db.AddParameter("@nip", this.NIP);
                db.AddParameter("@dob", this.DateOfBirth);
                db.AddParameter("@con_er", this.ContributionER);
                db.AddParameter("@con_ee", this.ContributionEE);
                db.AddParameter("@top_up", this.TopUp);
                db.AddParameter("@ft", this.FT);
                db.AddParameter("@month", this.Month);
                db.AddParameter("@year", this.Year);
                db.AddParameter("@hostname", this.HostName);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return true;
        }


        public static bool DeleteWithHostName(Database db, string userHostName)
        {
            string query = "DELETE FROM UPLOAD_PAYROLL_CONTRIBUTION where Hostname=@user_hostname";
            try
            {
                db.setQuery(query);
                db.AddParameter("@user_hostname", userHostName);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return true;
        }

        public Dictionary<string, object> GetCertificateData(Database db)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            string query = "select group_nmbr, cer_nmbr, client_nm, birth_dt, employee_nmbr from certificate c"
				+" inner join client cl on c.client_nmbr = cl.client_nmbr"
				+" where cer_nmbr = @cer_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", this.CertificateNumber);
                DataTable dt = db.ExecuteQuery();
                if (dt.Rows.Count > 0)
                {
                    DataRow FirstRow = dt.Rows[0];
                    foreach (DataColumn col in dt.Columns)
                    {
                        output.Add(col.ColumnName, FirstRow[col]);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return output;
        }
    }

    public class ContribDetailModel
    {
        public int ContributionSequenceNumber { get; set; }
        public int ContributionDetailSequenceNumber { get; set; }
        public int GroupNumber { get; set; }
        public string CertificateNumber { get; set; }
        public int MoneyTypeNumber { get; set; }
        public float ContributionAmount { get; set; }
        public float PaidAmount { get; set; }
        public DateTime LastChangeDate { get; set; }
        public int PayCenterNumber { get; set; }
    }

    public class ContribReqModel
    {
        public int ContributionSequenceNumber { get; set; }
        public int GroupNumber { get; set; }
        public DateTime ContributionEffectiveDate { get; set; }
        public DateTime ContributionRunDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ReversalDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public float ContributionAmount { get; set; }
        public float PaidAmount { get; set; }
        public short SuspenseUseCode { get; set; }
        public DateTime PaidDate { get; set; }
        public string Comment { get; set; }
        public DateTime LastChangeDate { get; set; }
        public int PayCenterNumber { get; set; }
        public int LapseStatus { get; set; }
    }

    public class ContribDetailGroupModel
    {
        public int GroupNumber { get; set; }
        public int ContributionSequenceNumber { get; set; }
        public string CertificateNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }



}