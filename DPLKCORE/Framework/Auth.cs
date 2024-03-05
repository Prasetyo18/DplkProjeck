using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using DPLKCORE.Class;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DPLKCORE.Framework
{
    public class Auth : Connection
    {
        public String IdPemakai { get; set; }
        public String Password { get; set; }
        public String IPAddress { get; set; }
        public Int32 Attempt { get; set; }
        public Int32 statusEmail { get; set; }
        public Int32 ActivityType { get; set; }
        public String Info { get; set; }

        public const int AUTH_OK = 0;
        public const int USER_DOESNT_EXIST = -1;
        public const int PASSWORD_DOESNT_MATCH = -2;

        public int DoLogin() 
        {
            int errCode = AUTH_OK;
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            String query = "select * from T_User (NOLOCK) where email = @id_profile";
            //String query = "select * from T_User (NOLOCK) where id_profile = @id_profile";
               
            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@id_profile", this.IdPemakai);
                DataTable dt = db.ExecuteQuery();

                if (dt.Rows.Count > 0)
                {
                    //Hashtable session = new Hashtable(); //??
                    //HttpContext.Current.Session.Add(SessionDef.SessionIdUser, this.IdPemakai);
                    HttpContext.Current.Session.Add(SessionDef.SessionIdUser, dt.Rows[0]["id_profile"]);
                    HttpContext.Current.Session.Add(SessionDef.SessionUsername, dt.Rows[0]["nama_lengkap"]);
                    HttpContext.Current.Session.Add(SessionDef.SessionUnitKerja, dt.Rows[0]["bagian"]);
                    HttpContext.Current.Session.Add(SessionDef.SessionJabatan, dt.Rows[0]["jabatan"]);
                    HttpContext.Current.Session.Add(SessionDef.SessionEmail, this.IdPemakai);
                }
                else
                {
                    //errCode = USER_DOESNT_EXIST;
                    errCode = PASSWORD_DOESNT_MATCH;
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
            return errCode;
        }

        public void DeleteLoginLimitation(Framework.Database db)
        {
            String query = "Delete TLoginLimitation where email = @email";

            try
            {
                db.setQuery(query);
                /// db.AddParameter("@ipaddress", this.IPAddress);
                db.AddParameter("@email", this.IdPemakai);
                db.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public String ErrMessage(int errCode)
        {
            String Message = "";
            if (errCode == USER_DOESNT_EXIST)
            {
                Message = "Periksa Kembali Email dan Password";
            }
            else if (errCode == PASSWORD_DOESNT_MATCH)
            {
                Message = "Periksa Kembali Email dan Password";
            }
            else
            {
                Message = "Login Berhasil";
            }
            return Message;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public List<Auth> GetDataUserLimitation()
        {
            List<Auth> data = new List<Auth>();
            Connection cn = new Connection();
            Framework.Database db = new Framework.Database(cn.ConnectionStringDBJiwa);
            //String query = " select * from TLoginLimitation ";
            //String query = @" select * from TLoginLimitation where IPAddress = @IPAddress or  email = @Email ";
            String query = @" select * from TLoginLimitation where email = @Email ";


            List<String> args = new List<String>();
            try
            {
                db.Open();

                //if (!String.IsNullOrEmpty(this.IPAddress))
                //{
                //    args.Add(" IPAddress = @IPAddress ");
                //}


                //if (!String.IsNullOrEmpty(this.Email))
                //{
                //    args.Add(" email = @Email ");
                //}


                String where = Query.Where(args);
                //String order = " order by nourut ";
                db.setQuery(query);


                if (!String.IsNullOrEmpty(this.IPAddress))
                {
                    db.AddParameter("@IPAddress", this.IPAddress);
                }

                if (!String.IsNullOrEmpty(this.IdPemakai))
                {
                    db.AddParameter("@Email", this.IdPemakai);
                }

                System.Data.Common.DbDataReader reader = db.ExecuteReader();

                while (reader.Read())
                {
                    Auth u = new Auth();

                    if (reader["email"] != DBNull.Value)
                    {
                        u.IdPemakai = reader["email"].ToString();
                    }
                    if (reader["IPAddress"] != DBNull.Value)
                    {
                        u.IPAddress = reader["IPAddress"].ToString();
                    }
                    if (reader["Attempt"] != DBNull.Value)
                    {
                        u.Attempt = Convert.ToInt32(reader["Attempt"]);
                    }
                    if (reader["statusEmail"] != DBNull.Value)
                    {
                        u.statusEmail = Convert.ToInt32(reader["statusEmail"]);
                    }


                    data.Add(u);
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Close();
            }
            return data;
        }

        public void UpdateLoginLimitation(Framework.Database db)
        {
            //            String query = @"Update TLoginLimitation set attempt = @attempt, batasTerkunci = @batasTerkunci, statusEmail = @statusEmail
            //            where email = @email or ipaddress = @ipaddress ";

            String query = @"Update TLoginLimitation set attempt = @attempt, statusEmail = @statusEmail
            where email = @email ";

            try
            {
                db.setQuery(query);
                //db.AddParameter("@ipaddress", this.IPAddress);
                db.AddParameter("@email", this.IdPemakai);
                db.AddParameter("@attempt", this.Attempt);
                db.AddParameter("@statusemail", this.statusEmail);
                db.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void InsertLoginLimitation(Framework.Database db)
        {
            String query = "INSERT INTO TLoginLimitation(ipaddress, email, attempt, statusemail) VALUES(@ipaddress, @email, @attempt, @statusemail)";

            try
            {
                db.setQuery(query);
                db.AddParameter("@ipaddress", this.IPAddress);
                db.AddParameter("@email", this.IdPemakai);
                db.AddParameter("@attempt", this.Attempt);
                db.AddParameter("@statusemail", this.statusEmail);
                db.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void InserT_Log_Activity(Framework.Database db)
        {
            String query = "INSERT INTO T_Log_Activity(ipaddress, email, activitytype, info, timestamp) VALUES(@ipaddress, @email, @activitytype, @info, @timestamp)";

            try
            {
                db.setQuery(query);
                db.AddParameter("@ipaddress", this.IPAddress);
                db.AddParameter("@email", this.IdPemakai);
                db.AddParameter("@activitytype", this.ActivityType);
                db.AddParameter("@info", this.Info);
                db.AddParameter("@timestamp", DateTime.Now);
                db.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}