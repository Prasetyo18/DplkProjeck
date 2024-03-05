using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class SuspenseApprovalModels
    {
        public string pic { get; set; }
        public string address { get; set; }
        public string perihal { get; set; }
        public long SuspenseNmbr { get; set; }
        public string SuspenseDesc1 { get; set; }
        public int StatusTypeNmbr { get; set; }

        public int GroupNmbr { get; set; }
        public double SuspenseAmmount { get; set; }



        public void UpdateSuspenseHistory(Database db)
        {
            string query = "update suspense_hst set group_nmbr = @group_nmbr ,"
                                + " suspn_amt = @suspn_amt where suspn_nmbr = @suspn_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@group_nmbr",this.GroupNmbr);
                db.AddParameter("@suspn_amt",this.SuspenseAmmount);
                db.AddParameter("@suspn_nmbr",this.SuspenseNmbr);

                db.ExecuteNonQuery();
                db.CommitTransaction();
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


        public void DeleteSuspenseHistory(Database db)
        {
            string query = "delete suspense_hst where suspn_nmbr = @suspn_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@suspn_nmbr", this.SuspenseNmbr);
                db.ExecuteNonQuery();

                db.CommitTransaction();
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

        public void DeleteSuspenseApproveHistory(Database db)
        {
            string query = "delete suspn_apprv_hst where suspn_nmbr = @suspn_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@suspn_nmbr", this.SuspenseNmbr);
                db.ExecuteNonQuery();

                db.CommitTransaction();
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

        public void InsertSuspense(Database db)
        {
            string query = "sp_suspn_apprv_hst_i " +
                        "@suspn_nmbr, " +
                        "@suspn_desc1, " +
                        "@status_type_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.AddParameter("@suspn_nmbr",this.SuspenseNmbr);
                db.AddParameter("@suspn_desc1",this.SuspenseDesc1);
                db.AddParameter("@status_type_nmbr",this.StatusTypeNmbr);

                db.ExecuteNonQuery();

                db.CommitTransaction();
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

        public void UpdateSuspense(Database db)
        {
            string query = "update suspense_hst set adm_approve_cd = 1, suspn_desc2 = 'pic:@pic;address:@address;perihal:@perihal'" +
                        "where adm_approve_cd = 0 and " +
                        "suspn_nmbr = @suspense_nmbr";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@pic", this.pic);
                db.AddParameter("@address", this.address);
                db.AddParameter("@perihal", this.perihal);
                db.AddParameter("@suspense_nmbr", this.SuspenseNmbr);

                db.ExecuteNonQuery();

                db.CommitTransaction();
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


        public static DataTable GetRestData(Database db)
        {
            string query = "EXEC GRID_SUSPNREST_ON_SCR_SUSPENSEAPPROVED";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return result;
        }

        public static DataTable GetApprovedData(Database db)
        {
            string query = "EXEC GRID_APROVED_ON_SCR_SUSPENSEAPPROVED";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return result;
        }

        public static DataTable GetUnapprovedData(Database db)
        {
            string query = "EXEC GRID_UNAPROVED_ON_SCR_SUSPENSEAPPROVED";
            DataTable result;
            try
            {
                db.Open();
                db.setQuery(query);
                result = db.ExecuteQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return result;
        }
    }
}