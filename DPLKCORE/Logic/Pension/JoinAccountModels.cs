using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class JoinAccountModels
    {
        public int TrnsSeqNmbr { get; set; }
        public DateTime TrnsHstEfctvDt { get; set; }
        public string SourceCer { get; set; }
        public int SourceGrp { get; set; }
        public string SourceNm { get; set; }
        public string DstCer { get; set; }
        public int DstGrp { get; set; }
        public string DstNm { get; set; }
        public string SourceCompany { get; set; }
        public string DstCompany { get; set; }

        public List<JoinAccountModels> GetAccountListing(Database db)
        {
            List<JoinAccountModels> accountListings = new List<JoinAccountModels>();

            try
            {
                String query = "exec sp_join_account_listing";

                db.Open(); 
                db.setQuery(query);

                using (SqlDataReader reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        JoinAccountModels accountListing = new JoinAccountModels
                        {
                            TrnsSeqNmbr = (int)reader["trns_seq_nmbr"],
                            TrnsHstEfctvDt = (DateTime)reader["trns_hst_efctv_dt"],
                            SourceCer = reader["source_cer"].ToString(),
                            SourceGrp = (int)reader["source_grp"],
                            SourceNm = reader["source_nm"].ToString(),
                            DstCer = reader["dst_cer"].ToString(),
                            DstGrp = (int)reader["dst_grp"],
                            DstNm = reader["dst_nm"].ToString(),
                            SourceCompany = reader["source_company"].ToString(),
                            DstCompany = reader["dst_company"].ToString()
                        };

                        accountListings.Add(accountListing);
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

            return accountListings;
        }
    }

    public class CertificateOnJoinAccount
    {
        public string ClientName { get; set; }
        public string CompanyName { get; set; }

       
            public FundSwitchingByMoneyType.CertificateGroupNameModel GetCertificateGroupName(Database db, int certificateNumber)
            {
                try
                {
                    db.setQuery("exec TXT_CERTIFICATE_GROUP_NAME_ON_SCR_JOINACCOUNT @cer_nmbr");
                    db.AddParameter("@cer_nmbr", certificateNumber);

                    FundSwitchingByMoneyType.CertificateGroupNameModel result = new FundSwitchingByMoneyType.CertificateGroupNameModel();

                    using (var reader = db.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.ClientName = reader["client_nm"].ToString();
                            result.CompanyName = reader["company_nm"].ToString();
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
               
                    throw new Exception("Error in GetCertificateGroupName", ex);
                }
            }
        }

    public class JoinAccountCheckingResult
    {
        public int ErrorNumber { get; set; }
        public string ErrorDescription { get; set; }
    }

    public class JoinAccountCheckingModel
    {
        public JoinAccountCheckingResult ExecuteJoinAccountChecking(Database db, int certificateNumberSource, int certificateNumberDestination, DateTime effectiveDate, int transactionType, int? groupNumber = null)
        {
            try
            {
                db.setQuery("exec sp_join_account_cheking @cer_nmbr_src, @cer_nmbr_dst, @efctv_dt, @trns_type, @group_nmbr");
                db.AddParameter("@cer_nmbr_src", certificateNumberSource);
                db.AddParameter("@cer_nmbr_dst", certificateNumberDestination);
                db.AddParameter("@efctv_dt", effectiveDate);
                db.AddParameter("@trns_type", transactionType);
                db.AddParameter("@group_nmbr", groupNumber);

                JoinAccountCheckingResult result = new JoinAccountCheckingResult();

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.ErrorNumber = Convert.ToInt32(reader["err_number"]);
                        result.ErrorDescription = reader["err_desc"].ToString();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
          
                throw new Exception("Error in ExecuteJoinAccountChecking", ex);
            }
        }

        public void JoinAccountChecking(Database db, int certificateNumberSource, int certificateNumberDestination, DateTime effectiveDate, int transactionType, int? groupNumber = null)
        {
            try
            {
                db.setQuery("exec sp_join_account_cheking @cer_nmbr_src, @cer_nmbr_dst, @efctv_dt, @trns_type, @group_nmbr");
                db.AddParameter("@cer_nmbr_src", certificateNumberSource);
                db.AddParameter("@cer_nmbr_dst", certificateNumberDestination);
                db.AddParameter("@efctv_dt", effectiveDate);
                db.AddParameter("@trns_type", transactionType);
                db.AddParameter("@group_nmbr", groupNumber);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
              
                throw new Exception("Error in ExecuteJoinAccountChecking", ex);
            }
        }
    }


}
