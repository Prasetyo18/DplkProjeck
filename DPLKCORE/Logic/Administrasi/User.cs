using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Class;
using DPLKCORE.Logic.Administrasi;

namespace DPLKCORE.Logic.Administrasi
{
    public class User : Menu 
    {
        public String IdUser { get; set; }
        public String NamaLengkap { get; set; }
        public String Password { get; set; }
        public String Jabatan { get; set; }
        public String Bagian { get; set; }
        public Int32 StatusUser { get; set; }

        public List<User> GetUser(Database db)
        {
            List<User> data = new List<User>();

            String query = "select * from T_User (NOLOCK) ";

            List<String> args = new List<String>();

            // Try To Connect Database
            try
            {
                if (!String.IsNullOrEmpty(this.NamaLengkap))
                    args.Add(" nama_lengkap LIKE @nama_lengkap");

                if (!String.IsNullOrEmpty(this.IdUser))
                    args.Add(" id_profile = @id_profile");

                String where = Query.Where(args);
                String orderby = " ORDER BY nama_lengkap";

                // Set Query
                db.setQuery(query + where + orderby);

                // Parameter
                if (!String.IsNullOrEmpty(this.NamaLengkap))
                    db.AddParameter("@nama_lengkap", "%" + this.NamaLengkap + "%");

                if (!String.IsNullOrEmpty(this.IdUser))
                    db.AddParameter("@id_profile", this.IdUser);

                // Execute Reader
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    User u = new User();
                    u.IdUser = reader["id_profile"].ToString();
                    u.NamaLengkap = reader["nama_lengkap"].ToString();
                    u.Bagian = reader["bagian"].ToString();

                    if (reader["jabatan"] != DBNull.Value)
                        u.Jabatan = reader["jabatan"].ToString();

                    u.StatusUser = Convert.ToInt32(reader["status_user"]);
                    data.Add(u);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }

        public void InsertUser(Database db) 
        {
            String query = "INSERT INTO T_User (id_profile, nama_lengkap, bagian, jabatan, status_user) "
                        + "VALUES(@id_profile, @nama_lengkap, @bagian, @jabatan, @status_user)";
            try
            {
                db.setQuery(query);

                db.AddParameter("@id_profile", this.IdUser);
                db.AddParameter("@nama_lengkap", this.NamaLengkap);
               // db.AddParameter("@password", this.Password);
                db.AddParameter("@bagian", this.Bagian);

                if (this.Jabatan == null)
                    db.AddParameter("@jabatan", DBNull.Value);
                else
                    db.AddParameter("@jabatan", this.Jabatan);

                db.AddParameter("@status_user", this.StatusUser);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUser(Database db)
        {
            String query = "UPDATE T_User SET [nama_lengkap]=@nama_lengkap, "
                        + " [bagian] = @bagian, [jabatan] = @jabatan, [status_user] = @status_user"
                        + " WHERE id_profile = @id_profile";
            try
            {
                db.setQuery(query);

                db.AddParameter("@id_profile", this.IdUser);
                db.AddParameter("@nama_lengkap", this.NamaLengkap);
                //db.AddParameter("@password", this.Password);
                db.AddParameter("@bagian", this.Bagian);

                if (this.Jabatan == null)
                    db.AddParameter("@jabatan", DBNull.Value);
                else
                    db.AddParameter("@jabatan", this.Jabatan);

                db.AddParameter("@status_user", this.StatusUser);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(Database db)
        {
            String query = "delete T_User WHERE id_profile = @id_profile";
            try
            {
                db.setQuery(query);
                db.AddParameter("@id_profile", this.IdUser);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}