using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DPLKCORE.Framework;

namespace DPLKCORE.Logic.Pension
{
    public class WithdrawEditingModels
    {
        public string Register { get; set; }
        public int TferTypeNumber { get; set; }
        public int TrnsSeqNumber { get; set; }
        public string CerNumber { get; set; }
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public string TferTypeName { get; set; }
        public decimal TferAmount { get; set; }
        public string BankCentralName { get; set; }
        public string BankAddress { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string KodeBank { get; set; }
        public int BatchId { get; set; }
        public int CertificateNumber { get; set; }
        public int TransactionSequenceNumber { get; set; }
        public int TransferTypeNumber { get; set; }
        public string BankUpdate { get; set; }


        public void EditingClaimUpdate(Database db )
        {
            try
            {
                string query = "exec dbo.usp_editing_claim_u " +
                               "@cer_nmbr, @trns_seq_nmbr, @tfer_type_nmbr, @Bank_update, @acct_nmbr, @acct_nm";

                db.setQuery(query);
                db.AddParameter("@cer_nmbr",CertificateNumber);
                db.AddParameter("@trns_seq_nmbr", TransactionSequenceNumber);
                db.AddParameter("@tfer_type_nmbr",TransferTypeNumber);
                db.AddParameter("@Bank_update", BankUpdate);
                db.AddParameter("@acct_nmbr", AccountNumber);
                db.AddParameter("@acct_nm",AccountName);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<WithdrawEditingModels> DgrOnScrEditingClaim(Database db)
        {
            List<WithdrawEditingModels> results = new List<WithdrawEditingModels>();

            try
            {
                string query = "exec dbo.DGR_ON_SCR_EDITING_CLAIM";
                db.setQuery(query);

                using (SqlDataReader reader = db.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WithdrawEditingModels result = new WithdrawEditingModels
                            {
                                Register = reader["register"].ToString(),
                                TferTypeNumber = Convert.ToInt32(reader["tfer_type_nmbr"]),
                                TrnsSeqNumber = Convert.ToInt32(reader["trns_seq_nmbr"]),
                                CerNumber = reader["cer_nmbr"].ToString(),
                                ClientName = reader["client_nm"].ToString(),
                                CompanyName = reader["company_nm"].ToString(),
                                TferTypeName = reader["tfer_type"].ToString(), // Adjust the column name
                                TferAmount = Convert.ToDecimal(reader["tfer_amt"]),
                                BankCentralName = reader["bank_central_nm"].ToString(),
                                BankAddress = reader["bank_addr"].ToString(),
                                AccountNumber = reader["acct_nmbr"].ToString(),
                                AccountName = reader["acct_nm"].ToString(),
                                KodeBank = reader["kode_bank"].ToString(),
                                BatchId = Convert.ToInt32(reader["BATCH_ID"])
                            };

                            results.Add(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return results;
        }




    }

}