using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DPLKCORE.Class;
using DPLKCORE.Framework;
using DPLKCORE.Logic;
using DPLKCORE.Logic.Administrasi;

namespace DPLKCORE.Logic.Administrasi
{
    public class AksesMenu : User
    {
        public Boolean MenuUser { get; set; }

        public static List<AksesMenu> Get(AksesMenu am) 
        {
            List<AksesMenu> data = new List<AksesMenu>();

            //create connection & database instance
            Connection conn = new Connection();
            String connstr = conn.ConnectionStringDBJiwa;
            DPLKCORE.Framework.Database db = new DPLKCORE.Framework.Database(connstr);

            String query = "SELECT * FROM T_Menu a (NOLOCK) join (SELECT * FROM T_Akses_Menu (NOLOCK)  "
                        + "WHERE id_profile=@id_profile) b on a.id_menu = b.id_menu "
                        + "JOIN T_Group_Menu c (NOLOCK) on a.id_group = c.id_group ";

            //try to get data
            try {
                db.Open();

                List<String> args = new List<String>();

                //if there any conditions
                if (!String.IsNullOrEmpty(am.Menu.IdMenu))
                    args.Add(" a.id_menu = @id_menu ");

                if (!String.IsNullOrEmpty(am.User.IdUser))
                    args.Add(" id_profile = @id_profile ");

                if (am.MenuUser)
                    args.Add(" a.id_menu not in(select id_menu from T_Akses_Menu (NOLOCK) where id_profile=@id_profile) ");

                String where = Query.Where(args);

                //set query
                db.setQuery(query + where);

                //add parameter
                if (!String.IsNullOrEmpty(am.Menu.IdMenu))
                    db.AddParameter("@id_menu", am.Menu.IdMenu);

                if (!String.IsNullOrEmpty(am.User.IdUser) || am.MenuUser)
                    db.AddParameter("@id_profile", am.User.IdUser);

                //execute reader
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read()) 
                {
                    AksesMenu akmen = AksesMenu.RowMapper(reader);
                    data.Add(akmen);
                }
            } 
            catch (Exception ex) 
            {                
                throw new Exception(ex.Message);
            }

            return data;
        }

        public List<AksesMenu> GetAksesMenu(Database db) 
        {
            List<AksesMenu> data = new List<AksesMenu>();

            String query = "Select b.id_group, b.id_parent, b.id_menu, nama_group, c.nama_menu nama_parent, "
                        + "b.nama_menu nama_menu "
                        + "from T_Akses_Menu a (NOLOCK) "
                        + "inner join T_Menu b (NOLOCK) on b.id_menu = a.id_menu "
                        + "left join T_Menu c (NOLOCK) on c.id_menu = b.id_parent "
                        + "inner join T_Group_Menu d on d.id_group = b.id_group";

            List<String> args = new List<String>();

            try 
            {
                db.Open();

                if (!String.IsNullOrEmpty(this.User.IdUser))
                    args.Add(" a.id_profile = @id_profile ");

                String where = Query.Where(args);

                //set query
                db.setQuery(query + where);

                //add parameter
                if (!String.IsNullOrEmpty(this.User.IdUser))
                    db.AddParameter("@id_profile", this.User.IdUser);

                //execute query
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read()) 
                {
                    AksesMenu akmen = RowMapper(reader);
                    data.Add(akmen);
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

            return data;
        }

        public List<AksesMenu> GetGroupMenu() 
        {
            var data = new List<AksesMenu>();
            Connection conn = new Connection();
            String connstr = conn.ConnectionStringDBJiwa;
            db = new DPLKCORE.Framework.Database(connstr);

            try 
            {
                db.Open();
                String query = "SELECT DISTINCT c.id_group, nama_group, nama_group, urutan, status_menu from "
                            + "T_Akses_Menu a (NOLOCK) "
                            + "join T_Menu b (NOLOCK) on a.id_menu = b.id_menu "
                            + "join T_Group_Menu c (NOLOCK) on b.id_group=c.id_group "
                            + "WHERE id_profile = '" + this.User.IdUser + "'"
                            + "AND status_menu = 1"
                            + "ORDER by urutan";

                db.setQuery(query);

                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read()) 
                {
                    AksesMenu am = new AksesMenu();
                    am.GroupMenu = new GroupMenu();

                    if (reader["id_group"] != DBNull.Value)
                        am.GroupMenu.IdGroup = reader["id_group"].ToString();

                    if (reader["nama_group"] != DBNull.Value)
                        am.GroupMenu.NamaGroup = reader["nama_group"].ToString();

                    if (reader["urutan"] != DBNull.Value)
                        am.GroupMenu.Urutan = Convert.ToInt16(reader["urutan"]);

                    data.Add(am);
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

            return data;
        }

        public List<AksesMenu> GetMenu(String id_group, String id_profile) 
        {
            List<AksesMenu> data = new List<AksesMenu>();

            String connstr = new Connection().ConnectionStringDBJiwa;
            Database db = new Database(connstr);

            String query = "SELECT a.*,c.*, d.nama_menu nama_parent, id_profile "
                        + "FROM T_Menu a (NOLOCK)  "
                        + "inner JOIN T_Akses_Menu b (NOLOCK)  ON a.id_menu=b.id_menu "
                        + "inner join T_Group_Menu c (NOLOCK) on a.id_group= c.id_group "
                        + "left join T_Menu d (NOLOCK)  on d.id_menu = a.id_parent "
                        + "WHERE a.id_group = @id_group AND id_profile = @id_profile "
                        + "AND (a.id_parent IS NULL OR a.id_parent = '') "
                        + "AND a.status_menu = 1 ";

            try 
            {
                db.Open();
                db.setQuery(query);

                db.AddParameter("@id_group", id_group);
                db.AddParameter("@id_profile", id_profile);

                //execute query
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read()) 
                {
                    AksesMenu am = new AksesMenu();
                    am.Menu = Menu.RowMapperMenu(reader);
                    data.Add(am);
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

            return data;
        }

        public List<AksesMenu> GetSubMenu(String id_parent, String id_profile) 
        {
            var data = new List<AksesMenu>();

            String connstr = new Connection().ConnectionStringDBJiwa;
            Database db = new Database(connstr);

            String query = "SELECT a.*,c.*, d.nama_menu nama_parent, id_profile "
                        + "FROM T_Menu a (NOLOCK)  "
                        + "inner JOIN T_Akses_Menu b (NOLOCK)  ON a.id_menu=b.id_menu "
                        + "inner join T_Group_Menu c (NOLOCK)  on a.id_group= c.id_group "
                        + "left join T_Menu d (NOLOCK) on d.id_menu = a.id_parent "
                        + "WHERE a.id_parent = @id_parent "
                        + "AND id_profile = @id_profile AND a.id_parent IS NOT NULL "
                        + "AND a.status_menu = 1";

            try 
            {
                db.Open();

                //set query
                db.setQuery(query);

                db.AddParameter("@id_parent", id_parent);
                db.AddParameter("@id_profile", id_profile);

                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read()) 
                {
                    AksesMenu am = new AksesMenu();
                    am.Menu = Menu.RowMapperMenu(reader);
                    data.Add(am);
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

            return data;
        }

        public List<AksesMenu> GetUserAksesMenu(Database db)
        {
            List<AksesMenu> data = new List<AksesMenu>();

            String query = "select * from T_Akses_Menu (NOLOCK) where id_menu = @id_menu ";

            List<String> args = new List<String>();

            try
            {
                db.Open();

                //set query
                db.setQuery(query);

                //add parameter
                db.AddParameter("@id_menu", this.IdMenu);

                //execute query
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    AksesMenu am = new AksesMenu();
                    am.IdUser = reader["id_profile"].ToString();
                    data.Add(am);
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

            return data;
        }

        public void InsertAksesMenu(Database db)
        {
            String query = "insert into T_Akses_Menu (id_menu, id_profile) values (@id_menu, @id_profile)";

            try
            {
                db.setQuery(query);
                db.AddParameter("@id_menu", this.IdMenu);
                db.AddParameter("@id_profile", this.IdUser);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateAksesMenu(Database db)
        {
            String query = "update T_Akses_Menu set id_menu = @ id_menu where id_profile = @id_profile)";

            try
            {
                db.setQuery(query);
                db.AddParameter("@id_menu", this.IdMenu);
                db.AddParameter("@id_profile", this.User.IdUser);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAksesMenu(Database db)
        {
            String query = "delete T_Akses_Menu where id_profile = @id_profile and id_menu = @id_menu";

            try
            {
                db.setQuery(query);
                db.AddParameter("@id_profile", this.User.IdUser);
                db.AddParameter("@id_menu", this.IdMenu);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUserAksesMenu(Database db)
        {
            String query = "delete T_Akses_Menu where id_profile = @id_profile";

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

        public static AksesMenu RowMapper(System.Data.Common.DbDataReader reader) 
        {
            AksesMenu akmen = new AksesMenu();
            akmen.Menu = new DPLKCORE.Logic.Administrasi.Menu();
            akmen.User = new DPLKCORE.Logic.Administrasi.User();

            akmen.IdGroup = reader["id_group"].ToString().Trim();
            akmen.IdParent = reader["id_parent"].ToString().Trim();
            akmen.Menu.IdMenu = reader["id_menu"].ToString().Trim();
            akmen.Menu.NamaGroup = reader["nama_group"].ToString().Trim();
            akmen.Menu.NamaParent = reader["nama_parent"].ToString().Trim();
            akmen.Menu.NamaMenu = reader["nama_menu"].ToString().Trim();

            return akmen;
        }
    }
}