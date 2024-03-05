using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DPLKCORE.Logic.Pension
{
    public class DDLAdminSuspense
    {
        public string GroupNmbr { get; set; }
        public string GroupNm { get; set; }

        public string PaycenterNmbr { get; set; }
        public string PaycenterNm { get; set; }

        public string SuspenseTypeNmbr { get; set; }
        public string SuspenseTypeNm { get; set; }

        public string KdPIC { get; set; }
        public string NamaPIC { get; set; }

        public string Address { get; set; }

        

        public static void LoadDDLAddress(Database db, DropDownList ddl, string suspenseNmbr)
        {
            ddl.DataSource = GetAddresses(db, suspenseNmbr);
            ddl.DataTextField = "Address";
            ddl.DataBind();
        }

        public static List<DDLAdminSuspense> GetAddresses(Database db, string suspenseNmbr)
        {
            string query = "EXEC DDL_ADDRESS_SUSPENSE_APPROVAL @suspense_nmbr";
            List<DDLAdminSuspense> output = new List<DDLAdminSuspense>();
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@suspense_nmbr", suspenseNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLAdminSuspense obj = new DDLAdminSuspense();
                        if (reader["address"] != null)
                        {
                            obj.Address = reader["address"].ToString();
                        }
                        output.Add(obj);
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
            return output;
        }

        public static void LoadDDLPIC(Database db, DropDownList ddl, string suspenseNmbr)
        {
            ddl.DataSource = GetPICs(db, suspenseNmbr);
            ddl.DataTextField = "NamaPIC";
            ddl.DataValueField = "KdPIC";
            ddl.DataBind();
        }

        private static List<DDLAdminSuspense> GetPICs(Database db, string suspenseNmbr)
        {
            string query = "EXEC DDL_PIC_SUSPENSE_APPROVAL @suspense_nmbr";
            List<DDLAdminSuspense> output = new List<DDLAdminSuspense>();
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@suspense_nmbr", suspenseNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLAdminSuspense obj = new DDLAdminSuspense();
                        if (reader["kd_pic"] != null)
                        {
                            obj.KdPIC = reader["kd_pic"].ToString();
                        }
                        if (reader["nama_pic"] != null)
                        {
                            obj.NamaPIC = reader["nama_pic"].ToString();
                        }
                        output.Add(obj);
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
            return output;
        }


        public static void LoadDDLPaycenter(Database db, DropDownList ddl, string GroupNmbr)
        {
            ddl.DataSource = GetPayceters(db, GroupNmbr);
            ddl.DataTextField = "PaycenterNm";
            ddl.DataValueField = "PaycenterNmbr";
            ddl.DataBind();
        }

        private static List<DDLAdminSuspense> GetPayceters(Database db, string GroupNmbr)
        {
            string query = "DDL_PAYCENTER_ON_SCR_ADMINSUSPENSE @group_nmbr";
            List<DDLAdminSuspense> output = new List<DDLAdminSuspense>();
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@group_nmbr", GroupNmbr);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLAdminSuspense obj = new DDLAdminSuspense();
                        if (reader["paycenter_nmbr"] != null)
                        {
                            obj.PaycenterNmbr = reader["paycenter_nmbr"].ToString();
                        }
                        if (reader["paycenter_nm"] != null)
                        {
                            obj.PaycenterNm = reader["paycenter_nm"].ToString();
                        }
                        output.Add(obj);
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
            return output;
        }

        public static void LoadDDLSuspenseType(Database db, DropDownList ddl)
        {
            ddl.DataSource = GetSuspenseTypes(db);
            ddl.DataTextField = "SuspenseTypeNm";
            ddl.DataValueField = "SuspenseTypeNmbr";
            ddl.DataBind();
        }

        private static List<DDLAdminSuspense> GetSuspenseTypes(Database db)
        {
            string query = "EXEC DDL_PARAM 'suspense_type'";
            List<DDLAdminSuspense> output = new List<DDLAdminSuspense>();
            try
            {
                db.Open();
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLAdminSuspense obj = new DDLAdminSuspense();
                        if (reader["suspn_type_nmbr"] != null)
                        {
                            obj.SuspenseTypeNmbr = reader["suspn_type_nmbr"].ToString();
                        }
                        if (reader["suspn_type_nm"] != null)
                        {
                            obj.SuspenseTypeNm = reader["suspn_type_nm"].ToString();
                        }
                        output.Add(obj);
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
            return output;
        }



        public static void LoadDDLGroup(Database db, DropDownList ddl)
        {
            ddl.DataSource = getGroups(db);
            ddl.DataTextField = "GroupNm";
            ddl.DataValueField = "GroupNmbr";
            ddl.DataBind();
        }

        public static List<DDLAdminSuspense> getGroups(Database db)
        {
            string query = "EXEC DDL_GROUP_ON_SCR_CERTIFICATEINFO";
            List<DDLAdminSuspense> output = new List<DDLAdminSuspense>();
            try
            {
                db.Open();
                db.setQuery(query);
                DbDataReader reader = db.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DDLAdminSuspense obj = new DDLAdminSuspense();
                        if (reader["group_nmbr"] != null)
                        {
                            obj.GroupNmbr = reader["group_nmbr"].ToString();
                        }
                        if (reader["company_nm"] != null)
                        {
                            obj.GroupNm = String.Format("{0} - {1}", reader["company_nm"].ToString(), reader["group_nmbr"].ToString());
                        }
                        output.Add(obj);
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
            return output;
        }
    }
}