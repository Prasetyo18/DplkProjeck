using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
  

    public class CertificateTransactions
    {
        public string EfctvDate { get; set; }
        public int TrnsSeqNumber { get; set; }
        public string TrnsType { get; set; }
        public int CerNumber { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FeeAmount { get; set; }
    }

    public class DgrCerTransSummaryProcessor
    {
        public List<CertificateTransactions> GetDgrCerTransSummary(Database db, int cerNumber, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<CertificateTransactions> result = new List<CertificateTransactions>();

                db.setQuery("EXEC [dbo].[DGR_CER_TRNS_SUMMARY] @cer_nmbr, @start_dt, @end_dt");
                db.AddParameter("@cer_nmbr", cerNumber);
                db.AddParameter("@start_dt", startDate);
                db.AddParameter("@end_dt", endDate);

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CertificateTransactions item = new CertificateTransactions
                        {
                            EfctvDate = reader["efctv_dt"].ToString(),
                            TrnsSeqNumber = Convert.ToInt32(reader["trns_seq_nmbr"]),
                            TrnsType = reader["trns_type"].ToString(),
                            CerNumber = Convert.ToInt32(reader["cer_nmbr"]),
                            GrossAmount = Convert.ToDecimal(reader["gross_amt"]),
                            TaxAmount = Convert.ToDecimal(reader["tax_amt"]),
                            FeeAmount = Convert.ToDecimal(reader["fee_amt"])
                        };

                        result.Add(item);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetDgrCerTransSummary", ex);
            }
        }
    }

    public class DgrClaimOnCertificateClaimHistoryItem
    {
        public DateTime DateProcess { get; set; }
        public string Type { get; set; }
        public string RegisterID { get; set; }
        public int BatchID { get; set; }
        public int SeqID { get; set; }
    }

    public class DgrClaimOnCertificateClaimHistoryProcessor
    {
        public List<DgrClaimOnCertificateClaimHistoryItem> GetDgrClaimOnCertificateClaimHistory(Database db, int cerNumber)
        {
            try
            {
                List<DgrClaimOnCertificateClaimHistoryItem> result = new List<DgrClaimOnCertificateClaimHistoryItem>();

                db.setQuery("EXEC [dbo].[DGR_CLAIM_ON_CERTIFICATE_CLAIM_HISTORY] @CER_NMBR");
                db.AddParameter("@CER_NMBR", cerNumber);

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DgrClaimOnCertificateClaimHistoryItem item = new DgrClaimOnCertificateClaimHistoryItem
                        {
                            DateProcess = Convert.ToDateTime(reader["Date Process"]),
                            Type = reader["Type"].ToString(),
                            RegisterID = reader["Register ID"].ToString(),
                            BatchID = Convert.ToInt32(reader["Batch ID"]),
                            SeqID = Convert.ToInt32(reader["Seq ID"])
                        };

                        result.Add(item);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetDgrClaimOnCertificateClaimHistory", ex);
            }
        }
    }

    public class CertificateClaimDetail
    {
        public int No { get; set; }
        public string Type { get; set; }
        public string Gross { get; set; }
        public string Fee { get; set; }
        public string Tax { get; set; } 
        public string Net { get; set; }
        public string TransferTo { get; set; }
        public string FLP_CDV { get; set; }


        public List<CertificateClaimDetail> GetCertificateClaimDetails(Database db, int cerNmbr, string trnsDt, string trnsTypeNm, string registerId, int seqId)
        {
            List<CertificateClaimDetail> result = new List<CertificateClaimDetail>();
            string query = "EXEC usp_Certificate_Claim_detail @cer_nmbr, @trns_dt, @trns_type_nm, @register_id, @seq_id";

            try
            {
                db.setQuery(query);
                db.AddParameter("@cer_nmbr", cerNmbr);
                db.AddParameter("@trns_dt", trnsDt);
                db.AddParameter("@trns_type_nm", trnsTypeNm);
                db.AddParameter("@register_id", registerId);
                db.AddParameter("@seq_id", seqId);

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CertificateClaimDetail detail = new CertificateClaimDetail
                        {
                            No = reader.GetInt32(0),
                            Type = reader.GetString(1),
                            Gross = reader.GetString(2),
                            Fee = reader.GetString(3),
                            Tax = reader.GetString(4),
                            Net = reader.GetString(5),
                            TransferTo = reader.GetString(6),
                            FLP_CDV = reader.GetString(7)
                        };

                        result.Add(detail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }


        public class ClaimStatusHistory
        {
            public string TrackName { get; set; }
            public string TrackDate { get; set; }
            public string Processor { get; set; }


            public List<ClaimStatusHistory> GetHistoricalClaimStatus(Database db, string registerId)
            {
                List<ClaimStatusHistory> result = new List<ClaimStatusHistory>();
                string query = "EXEC dbo.DGR_HISTORICAL_CLAIM_STATUS_ON_CERTIFICATE_CLAIM_HISTORY @register_id";

                try
                {
                    db.setQuery(query);
                    db.AddParameter("@register_id", registerId);

                    using (var reader = db.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClaimStatusHistory history = new ClaimStatusHistory
                            {
                                TrackName = reader.GetString(0),
                                TrackDate = reader.GetString(1),
                                Processor = reader.GetString(2)
                            };

                            result.Add(history);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return result;
            }

        }

    }


}