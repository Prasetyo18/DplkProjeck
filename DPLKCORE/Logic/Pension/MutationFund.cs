using DPLKCORE.Class;
using DPLKCORE.Framework;
using System;
using System.Data.SqlClient;

namespace DPLKCORE.Logic.Pension
{
    public class MutationFund
    {
      
    }

    public class MutationFundLogic
    {
        public int SourceCertificateNumber { get; set; }
        public string SourceName { get; set; }
        public float Amount { get; set; }
        public string ProcessorId { get; set; }
        public int DestinationCertificateNumber { get; set; }
        public string DestinationName { get; set; }

        public string MutateFunds(Database db)
        {
            try
            {
                string result = "exec sp_mutation_fund " +
                                "@source_cer, @source_nm, @amount, @processor_id, @destination_cer, @destination_nm";

                db.setQuery(result);
                db.AddParameter("@source_cer", this.SourceCertificateNumber);
                db.AddParameter("@source_nm", this.SourceName);
                db.AddParameter("@amount", this.Amount);
                db.AddParameter("@processor_id", this.ProcessorId);
                db.AddParameter("@destination_cer", this.DestinationCertificateNumber);
                db.AddParameter("@destination_nm", this.DestinationName);

                db.ExecuteNonQuery();

                return "Mutation successful!"; 
            }
            catch (Exception ex)
            {
                throw new Exception("Mutation failed: " + ex.Message);
            }
        }
    }
}
