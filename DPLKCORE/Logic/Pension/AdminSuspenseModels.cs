using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DPLKCORE.Form.PensionTransaction;
using DPLKCORE.Framework;

namespace DPLKCORE.Logic.Pension
{
    public class AdminSuspenseModels
    {
        public long SuspnNumber { get; set; }
        public float SuspenseAmount { get; set; }
        public float SuspenseUseAmount { get; set; }
        public string RestAmount { get; set; }
        public string SuspenseDescription { get; set; }
        public string SuspenseDescription2 { get; set; }
        public string SuspenseType { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int GroupNumber { get; set; }
        public string CompanyName { get; set; }
        public string PaycenterName { get; set; }
        public int PaycenterNmbr { get; set; }
        public DateTime DateSorting { get; set; }
        public string ReferenceBank { get; set; }
        public short FinApproveCd { get; set; }
        public short AdmApproveCd { get; set; }
        public string Remark { get; set; }
        public short UseStatusCd { get; set; }
        public short WaivedFlag { get; set; }
        public short RcptVoucher { get; set; }
 
        public void SubmitSuspense(Database db)
        {
            string query = "sp_suspense_hst_i " +
                "@suspn_nmbr, " +
                    "@group_nmbr, " +
                    "@paycenter_nmbr, " +
                    "@suspn_amt, " +
                    "@suspn_use_amt, " +
                    "@fin_approve_cd, " +
                    "@adm_approve_cd, " +
                    "@suspn_desc1, " +
                    "@suspn_desc2, " +
                    "@remark, " +
                    "@suspn_type_nmbr, " +
                    "@received_dt, " +
                    "@use_status_cd, @waived_flg, @rcptvoucher_flg, @ref_Bank";
            try
            {
                db.Open();
                db.BeginTransaction();
                db.setQuery(query);
                db.AddParameter("@suspn_nmbr",this.SuspnNumber);
                db.AddParameter("@group_nmbr",this.GroupNumber);
                db.AddParameter("@paycenter_nmbr",this.PaycenterNmbr);
                db.AddParameter("@suspn_amt", this.SuspenseAmount);
                db.AddParameter("@suspn_use_amt", this.SuspenseUseAmount);
                db.AddParameter("@fin_approve_cd", this.FinApproveCd);
                db.AddParameter("@adm_approve_cd", this.AdmApproveCd);
                db.AddParameter("@suspn_desc1", this.SuspenseDescription);
                db.AddParameter("@suspn_desc2", this.SuspenseDescription2);
                db.AddParameter("@remark",this.Remark);
                db.AddParameter("@suspn_type_nmbr",this.SuspenseType);
                db.AddParameter("@received_dt",this.ReceivedDate);
                db.AddParameter("@use_status_cd",this.UseStatusCd);
                db.AddParameter("@waived_flg", this.WaivedFlag);
                db.AddParameter("@rcptvoucher_flg", this.RcptVoucher);
                db.AddParameter("@ref_Bank", this.ReferenceBank);

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

        public static DataTable LoadSuspenseRequestData(Database db)
        {
            string query = "EXEC GRID_SUSPENSE_ON_SCR_ADMINSUSPENSE";
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


        public List<AdminSuspenseModels> GetAllSuspenseRestData()
        {
            List<AdminSuspenseModels> suspenseRestData = new List<AdminSuspenseModels>();
            Connection con = new Connection();
            String cnstr = con.ConnectionStringPension;
            Database db = new Database(cnstr);

            string query = @"select h.suspn_nmbr,    
                            h.suspn_amt,    
                            h.suspn_desc1,    
                            st.suspn_type_nm,    
                            received_dt = dbo.fndisplaydate(h.received_dt),    
                            h.group_nmbr,    
                            co.company_nm,    
                            paycenter_nm = coalesce(p.paycenter_nm,'PLEASE SELECT'),      
                            datesorting = h.received_dt,
                            h.Ref_Bank    
                     from suspense_hst h 
                     join suspense_type st on h.suspn_type_nmbr = st.suspn_type_nmbr     
                     left join group_info g on h.group_nmbr = g.group_nmbr     
                     left join company co on g.client_nmbr = co.client_nmbr     
                     left join paycenter p on h.paycenter_nmbr = p.paycenter_nmbr     
                     where h.adm_approve_cd = 0    
                     order by h.received_dt asc";

            try
            {
                db.Open();
                db.setQuery(query);
                DataTable result = db.ExecuteQuery();

                foreach (DataRow reader in result.Rows)
                {
                    AdminSuspenseModels suspenseRest = new AdminSuspenseModels
                    {

                        SuspnNumber = Convert.ToInt64(reader["suspn_nmbr"]),
                        SuspenseAmount = float.Parse(reader["suspn_amt"].ToString()),
                        SuspenseDescription = reader["suspn_desc1"].ToString(),
                        SuspenseType = reader["suspn_type_nm"].ToString(),
                        ReceivedDate = Convert.ToDateTime(reader["received_dt"]),
                        GroupNumber = reader["group_nmbr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["group_nmbr"]),
                        CompanyName = reader["company_nm"].ToString(),
                        PaycenterName = reader["paycenter_nm"].ToString(),
                        DateSorting = Convert.ToDateTime(reader["datesorting"]),
                        ReferenceBank = reader["Ref_Bank"].ToString()
                    };

                    suspenseRestData.Add(suspenseRest);
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

            return suspenseRestData;
        }





        public List<AdminSuspenseModels> GetSuspenseApprovalData()
        {
            List<AdminSuspenseModels> suspenseApprovalData = new List<AdminSuspenseModels>();
            Connection con = new Connection();
            String cnstr = con.ConnectionStringPension;
            Database db = new Database(cnstr);

            try
            {
                string query = @"select h.suspn_nmbr,    
                                h.suspn_amt,    
                                h.suspn_use_amt,    
                                h.suspn_desc1,    
                                st.suspn_type_nm,    
                                received_dt = dbo.fndisplaydate(h.received_dt),    
                                h.group_nmbr,    
                                co.company_nm,    
                                coalesce(p.paycenter_nm,'PLEASE SELECT') as paycenter_nm,    
                                datesorting = h.received_dt,
                                h.Ref_Bank      
                         from suspense_hst h 
                         join suspense_type st on h.suspn_type_nmbr = st.suspn_type_nmbr     
                         join group_info g on h.group_nmbr = g.group_nmbr     
                         join company co on g.client_nmbr = co.client_nmbr     
                         left join paycenter p on h.paycenter_nmbr = p.paycenter_nmbr     
                         where h.adm_approve_cd = 0    
                         order by h.received_dt desc";

                db.Open();
                db.setQuery(query);
                using (SqlDataReader reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AdminSuspenseModels suspenseRest = new AdminSuspenseModels
                        {
                            SuspnNumber = Convert.ToInt64(reader["suspn_nmbr"]),

                            SuspenseAmount = float.Parse(reader["suspn_amt"].ToString()),
                            SuspenseUseAmount = float.Parse(reader["suspn_use_amt"].ToString()),
                            SuspenseDescription = reader["suspn_desc1"].ToString(),
                            SuspenseType = reader["suspn_type_nm"].ToString(),
                            ReceivedDate = Convert.ToDateTime(reader["received_dt"]),
                            GroupNumber = reader["group_nmbr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["group_nmbr"]),
                            CompanyName = reader["company_nm"].ToString(),
                            PaycenterName = reader["paycenter_nm"].ToString(),
                            DateSorting = Convert.ToDateTime(reader["datesorting"]),
                            ReferenceBank = reader["Ref_Bank"].ToString()
                        };

                        suspenseApprovalData.Add(suspenseRest);
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

            return suspenseApprovalData;
        }



        public List<AdminSuspenseModels> GetReportDataSuspense()
        {
            List<AdminSuspenseModels> report = new List<AdminSuspenseModels>();
            Connection con = new Connection();
            String cnstr = con.ConnectionStringPension;
            Database db = new Database(cnstr);
            try
            {
                string query = "SELECT h.suspn_nmbr, " +
                               "SUSPENSE_AMT=convert(varchar(100),convert(money, h.suspn_amt),1), " +
                               "SUSPENSE_USE_AMT = convert(varchar(100),convert(money, h.suspn_use_amt),1), " +
                               "REST_AMT = convert(varchar(100),convert(money, (h.suspn_amt - h.suspn_use_amt)),1), " +
                               "h.suspn_desc1, " +
                               "st.suspn_type_nm, " +
                               "RECEIVED_DATE = DBO.FNDISPLAYDATE(h.received_dt), " +
                               "h.group_nmbr, " +
                               "co.company_nm, " +
                               "PAYCENTER_nm = coalesce(p.paycenter_nm,'PLEASE SELECT'), " +
                               "datesorting = h.received_dt, " +
                               "h.Ref_Bank " +
                               "FROM suspense_hst h " +
                               "JOIN suspense_type st ON h.suspn_type_nmbr = st.suspn_type_nmbr " +
                               "JOIN group_info g ON h.group_nmbr = g.group_nmbr " +
                               "JOIN company co ON g.client_nmbr = co.client_nmbr " +
                               "LEFT JOIN paycenter p ON h.paycenter_nmbr = p.paycenter_nmbr " +
                               "WHERE h.adm_approve_cd != 0 " +
                               "ORDER BY h.received_dt DESC";

                db.Open();
                db.setQuery(query);
                using (SqlDataReader reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AdminSuspenseModels adminSuspense = new AdminSuspenseModels
                        {
                            SuspnNumber = Convert.ToInt64(reader["suspn_nmbr"]),
                            SuspenseAmount = float.Parse(reader["SUSPENSE_AMT"].ToString()),
                            SuspenseUseAmount = float.Parse(reader["SUSPENSE_USE_AMT"].ToString()),
                            RestAmount = reader["REST_AMT"].ToString(),
                            SuspenseDescription = reader["suspn_desc1"].ToString(),
                            SuspenseType = reader["suspn_type_nm"].ToString(),
                            ReceivedDate = Convert.ToDateTime(reader["RECEIVED_DATE"]),
                            GroupNumber = reader["group_nmbr"] == DBNull.Value ? 0 : Convert.ToInt32(reader["group_nmbr"]),
                            CompanyName = reader["company_nm"].ToString(),
                            PaycenterName = reader["PAYCENTER_nm"].ToString(),
                            DateSorting = Convert.ToDateTime(reader["datesorting"]),
                            ReferenceBank = reader["Ref_Bank"].ToString()
                        };

                        report.Add(adminSuspense);
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

            return report;
        }
    }


}

