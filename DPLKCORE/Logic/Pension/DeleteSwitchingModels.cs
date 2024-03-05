using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class DeleteSwitchingModels
    {
        public int CertificateNumber { get; set; }
        public int BatchId { get; set; }

      
            public void DeleteSwitchings(Database db, DeleteSwitchingModels model)
            {
                try
                {
                    string query = "exec [dbo].[usp_delete_switching] @cer_nmbr, @batch_id";

                    db.setQuery(query);
                    db.AddParameter("@cer_nmbr", model.CertificateNumber);
                    db.AddParameter("@batch_id", model.BatchId);

                    db.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }


