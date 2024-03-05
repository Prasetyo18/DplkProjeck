using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Class;

namespace DPLKCORE.Logic.Administrasi
{
    public class GroupMenu : Connection
    {
        public String IdGroup { get; set; }
        public String NamaGroup { get; set; }
        public Int16 Urutan { get; set; }
        public int StatusGroup { get; set; }
        public String Prefix { get; set; }
        public Menu Menu { get; set; }
        public User User { get; set; }

        public List<GroupMenu> GetGroupMenu(Database db) 
        {
            List<GroupMenu> data = new List<GroupMenu>();

            String query = "SELECT * FROM T_Group_Menu (NOLOCK) ";

            List<String> args = new List<String>();

            try 
            {
                if (!String.IsNullOrEmpty(this.NamaGroup))
                    args.Add(" nama_group like @nama_group ");

                if (!String.IsNullOrEmpty(this.IdGroup))
                    args.Add(" id_group = @id_group ");

                String where = Query.Where(args);

                // Set Query
                db.setQuery(query + where + " ORDER BY urutan ");

                if (!String.IsNullOrEmpty(this.NamaGroup))
                    db.AddParameter("@nama_group", "%" + this.NamaGroup + "%");

                if (!String.IsNullOrEmpty(this.IdGroup))
                    db.AddParameter("@id_group", this.IdGroup);

                // Execute Reader
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read()) 
                {
                    GroupMenu grpmn = this.RowMapperGroupMenu(reader);
                    data.Add(grpmn);
                }
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return data;
        }

        public GroupMenu RowMapperGroupMenu(System.Data.Common.DbDataReader reader) 
        {
            GroupMenu gm = new GroupMenu();

            if (reader["id_group"] != DBNull.Value)
                gm.IdGroup = reader["id_group"].ToString().Trim();

            if (reader["nama_group"] != DBNull.Value)
                gm.NamaGroup = reader["nama_group"].ToString().Trim();

            if (reader["urutan"] != DBNull.Value)
                gm.Urutan = Convert.ToInt16(reader["urutan"]);

            if (reader["status_group"] != DBNull.Value)
                gm.StatusGroup = Convert.ToInt32(reader["status_group"]);

            if (reader["prefix"] != DBNull.Value)
                gm.Prefix = reader["prefix"].ToString().Trim();

            return gm;
        }
        
        public static List<GroupMenu> GetNoUrut(GroupMenu gm)
        {
            var data = new List<GroupMenu>();

            Connection con = new Connection();
            string constr = con.ConnectionStringDBJiwa;
            Database db = new Database(constr);

            try
            {
                db.Open();

                String query = "SELECT * FROM T_Group_Menu (NOLOCK)";
                List<String> args = new List<String>();

                if (!String.IsNullOrEmpty(gm.IdGroup))
                    args.Add(" id_group = @id_group");

                String where = Query.Where(args);
                String orderby = " ORDER BY no_urut";

                // Set Query
                db.setQuery(query + where + orderby);

                if (!String.IsNullOrEmpty(gm.IdGroup))
                    db.AddParameter("@id_group", gm.IdGroup);

                // Execute Reader
                //System.Data.Common.DbDataReader reader = db.ExecuteReader();
                //while (reader.Read())
                //{
                //    GroupMenu grpmn = GroupMenu.RowMapper(reader);
                //    data.Add(grpmn);
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }
            finally
            {
                db.Close();
            }

            return data;
        }

        // Generate ID Group
        public string GenIdGroupMenu() 
        {
            String IdGroupMenu = "";
            Int32 Posisi = 0;
            String Tahun = Convert.ToString(DateTime.Now.Year);
            String query = "SELECT MAX(RIGHT(id_group,3)) FROM T_Group_Menu (NOLOCK) "
                         + " WHERE SUBSTRING(id_group,4,4) = year(getdate())";

            // Create Connection
            Connection con = new Connection();
            String cnstr = con.ConnectionStringDBJiwa;
            Database db = new Database(cnstr);

            try 
            {
                db.Open();
                db.setQuery(query);

                Object skalar = db.ExecuteScalar();

                if (skalar != DBNull.Value)
                    Posisi = Convert.ToInt32(skalar) + 1;
                else
                    Posisi = 1;

                IdGroupMenu = String.Format("GRM{0}", Tahun) + String.Format("{0:000}", Posisi);
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return IdGroupMenu;
        }

        // Get No Urut
        public int GetNoUrut() 
        {
            Int32 NoUrut = 0;
            Connection con = new Connection();
            String cnstr = con.ConnectionStringDBJiwa;
            Database db = new Database(cnstr);

            String query = "SELECT MAX(urutan) AS no_urut FROM T_Group_Menu (NOLOCK)";

            try {
                db.Open();
                db.setQuery(query);

                Object skalar = db.ExecuteScalar();

                if (skalar != DBNull.Value)
                    NoUrut = Convert.ToInt32(skalar) + 1;
                else
                    NoUrut = 1;
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            return NoUrut;
        }

        // Insert Data
        public void InsertGroupMenu(Database db) 
        {
            String query = "INSERT INTO T_Group_Menu (id_group, nama_group, urutan, status_group, prefix)"
                        + " VALUES (@id_group, @nama_group, @urutan, @status_group, @prefix)";
            try 
            {
                db.setQuery(query);

                db.AddParameter("@id_group", this.IdGroup);
                db.AddParameter("@nama_group", this.NamaGroup);
                db.AddParameter("@urutan", this.Urutan);
                db.AddParameter("@status_group", this.StatusGroup);
                db.AddParameter("@prefix", this.Prefix);

                db.ExecuteNonQuery();
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        // Update Data
        public void UpdateGroupMenu(Database db) 
        {
            String query = "UPDATE T_Group_Menu SET nama_group = @nama_group, status_group = @status_group, prefix = @prefix "
                        + "WHERE id_group = @id_group";
            try
            {
                db.setQuery(query);

                db.AddParameter("@id_group", this.IdGroup);
                db.AddParameter("@nama_group", this.NamaGroup);
                db.AddParameter("@status_group", this.StatusGroup);
                db.AddParameter("@prefix", this.Prefix);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Delete Data Group Menu
        public void DeleteGroupMenu(Database db) 
        {
            String query = "delete T_Group_Menu WHERE id_group = @id_group";
            try
            {
                db.setQuery(query);
                db.AddParameter("@id_group", this.IdGroup);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetNoUrutGm(Database db)
        {
            int noUrut = 1;

            String query = "SELECT MAX(right(id_group,2)) as NoUrut FROM T_Group_Menu (NOLOCK)";

            try
            {
                db.setQuery(query);

                Object result = db.ExecuteScalar();
                if (result != DBNull.Value)
                    noUrut = Convert.ToInt32(result) + 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return noUrut;
        }

        public void LoadGroupToDDL(DropDownList ddl, Database db)
        {
            GroupMenu gm = new GroupMenu();
            List<GroupMenu> data = new List<GroupMenu>();
            data.AddRange(this.GetGroupMenu(db));

            ddl.DataSource = data;
            ddl.DataValueField = "IdGroup";
            ddl.DataTextField = "NamaGroup";
            ddl.DataBind();
        }
    }
}