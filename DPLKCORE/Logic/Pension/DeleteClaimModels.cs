using DPLKCORE.Class.Pension;
using DPLKCORE.Form.Pension;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;


namespace DPLKCORE.Logic.Pension
{
   
    public class ClaimDeletionModel
    {
        public int CertificateNumber { get; set; }
        public int BatchId { get; set; }
        public string ClientName { get; set; }

        //public <List> ClaimDeletionModel GetClient(Database db)
        //{

        //    List<ClaimDeletionModel> data = new List<ClaimDeletionModel>();
        //    string query = "SELECT client_nm FROM client cl JOIN certificate c ON cl.client_nmbr = c.client_nmbr WHERE cer_nmbr = @cer_nmbr";

        //    try
        //    {
        //        db.setQuery(query);
        //        db.AddParameter("@cer_nmbr",this.CertificateNumber );
        //        var result = db.ExecuteQuery();


        
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    return data;
        //}


        public void DeleteClaim(Database db)
        {
            try
            {
                string query = "exec dbo.usp_delete_claim @cer_nmbr, @batch_id";

                db.setQuery(query);
                db.AddParameter("@cer_nmbr", CertificateNumber);
                db.AddParameter("@batch_id", BatchId);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}