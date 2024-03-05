using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DPLKCORE.Framework;
using DPLKCORE.Class;

namespace DPLKCORE.Logic.Administrasi
{
    public class Menu : GroupMenu
    {
        public String IdMenu { get; set; }
        public String IdParent { get; set; }
        public String NamaMenu { get; set; }
        public String URL { get; set; }
        public String NamaParent { get; set; }
        public int Level { get; set; }
        public int StatusMenu { get; set; }
        public GroupMenu GroupMenu { get; set; }
        public User User { get; set; }
        public String idUser { get; set; }

        public List<Menu> GetListMenu(Database db)
        {
            List<Menu> data = new List<Menu>();

            String query = "select m.id_menu, m.id_group, m.nama_menu nama_menu, m.url, m.[level], m.status_menu, "
                        + "nama_group, m.id_parent, md.nama_menu nama_parent "
                        + "from T_Menu  m (NOLOCK) "
                        + "left join T_Group_Menu gm (NOLOCK) on gm.id_group = m.id_group "
                        + "left join T_Menu md (NOLOCK) on m.id_parent = md.id_menu ";

            List<String> args = new List<String>();

            try
            {
                if (!String.IsNullOrEmpty(this.NamaMenu))
                    args.Add(" m.nama_menu LIKE @nama_menu");

                if (!String.IsNullOrEmpty(this.IdMenu))
                    args.Add(" m.id_menu = @id_menu");

                if (!String.IsNullOrEmpty(this.IdGroup))
                    args.Add(" m.id_group = @id_group ");

                if (!String.IsNullOrEmpty(this.IdParent))
                    args.Add(" m.id_parent = @id_parent ");

                String where = Query.Where(args);
                String orderby = " ORDER BY m.id_group";

                // Set Query
                db.setQuery(query + where + orderby);

                // Parameter
                if (!String.IsNullOrEmpty(this.NamaMenu))
                    db.AddParameter("@nama_menu", "%" + this.NamaMenu + "%");

                if (!String.IsNullOrEmpty(this.IdMenu))
                    db.AddParameter("@id_menu", this.IdMenu);

                if (!String.IsNullOrEmpty(this.IdGroup))
                    db.AddParameter("@id_group", this.IdGroup);

                if (!String.IsNullOrEmpty(this.IdParent))
                    db.AddParameter("@id_parent", this.IdParent);

                // Execute Reader
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    Menu m = RowMapperListMenu(reader);
                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }

        public List<Menu> GetMenu(Database db)
        {
            List<Menu> data = new List<Menu>();

            String query = "select id_profile, m.id_menu, m.id_group, m.nama_menu nama_menu, m.url, m.[level], m.status_menu, "
                        + "nama_group, m.id_parent, md.nama_menu nama_parent "
                        + "from T_Menu m (NOLOCK) "
                        + "left join T_Group_Menu gm (NOLOCK) on gm.id_group = m.id_group "
                        + "left join T_Menu md (NOLOCK) on m.id_parent = md.id_menu "
                        + "left join T_Akses_Menu am (NOLOCK) on am.id_menu = m.id_menu ";

            List<String> args = new List<String>();

            try
            {
                if (!String.IsNullOrEmpty(this.NamaMenu))
                    args.Add(" nama_menu LIKE @nama_menu");

                if (!String.IsNullOrEmpty(this.IdMenu))
                    args.Add(" m.id_menu = @id_menu");

                if (!String.IsNullOrEmpty(this.IdGroup))
                    args.Add(" m.id_group = @id_group ");

                if (!String.IsNullOrEmpty(this.IdParent))
                    args.Add(" m.id_parent = @id_parent ");

                if (!String.IsNullOrEmpty(this.User.IdUser))
                {
                    if (this.IdMenu == "ADM004")
                        args.Add(" id_profile <> @id_profile ");
                    else
                        args.Add(" id_profile = @id_profile ");
                }

                String where = Query.Where(args);
                String orderby = " ORDER BY m.id_group";

                // Set Query
                db.setQuery(query + where + orderby);

                // Parameter
                if (!String.IsNullOrEmpty(this.NamaMenu))
                    db.AddParameter("@nama_menu", "%" + this.NamaMenu + "%");

                if (!String.IsNullOrEmpty(this.IdMenu))
                    db.AddParameter("@id_menu", this.IdMenu);

                if (!String.IsNullOrEmpty(this.IdGroup))
                    db.AddParameter("@id_group", this.IdGroup);

                if (!String.IsNullOrEmpty(this.IdParent))
                    db.AddParameter("@id_parent", this.IdParent);

                if (!String.IsNullOrEmpty(this.User.IdUser))
                    db.AddParameter("@id_profile", this.User.IdUser);

                // Execute Reader
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    Menu m = RowMapperMenu(reader);
                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }

        public String RedirectFinpay(Database db)
        {
            String data = "";

            String query = "select a.id_menu from T_Menu m (NOLOCK) "
                            + " inner join T_Akses_Menu a (NOLOCK) on a.id_menu = m.Id_Menu "
                            + " where "
                            + " url = @url and id_profile = @idUser";
            try
            {
                db.Open();
                db.setQuery(query);

                db.AddParameter("@url", this.URL);
                db.AddParameter("@idUser", this.idUser);

                Object result = db.ExecuteScalar();
                data = Convert.ToString(result);
            }
            catch (Exception ex)
            {

                throw new Exception (ex.Message);
            }
            finally
            {
                db.Close();
            }

            return data;
        }
        public List<Menu> GetMenuAkses(Database db)
        {
            List<Menu> data = new List<Menu>();

            //String query = "select id_profile, m.id_menu, m.id_group, m.nama_menu nama_menu, m.url, m.[level], "
            //            + "m.status_menu, nama_group, m.id_parent, md.nama_menu nama_parent "
            //            + "from T_Menu m "
            //            + "left join T_Group_Menu gm on gm.id_group = m.id_group "
            //            + "left join T_Menu md on m.id_parent = md.id_menu "
            //            + "left join T_Akses_Menu am on am.id_menu = m.id_menu "
            //            + "where  m.id_parent = @id_parent and m.id_menu not in "
            //            + "(select id_menu from T_Akses_Menu where id_profile = @id_profile) "
            //            + "and m.status_menu = 1 "
            //            + "ORDER BY nama_menu ";

            String query = " select m.id_menu, m.id_group, m.nama_menu nama_menu, m.url, m.[level],  "
                            + " m.status_menu, nama_group, m.id_parent, md.nama_menu nama_parent   "
                            + " from T_Menu m (NOLOCK) "
                            + " left join T_Group_Menu gm (NOLOCK) on gm.id_group = m.id_group  "
                            + " left join T_Menu md (NOLOCK) on m.id_parent = md.id_menu  "
                            + " left join T_Akses_Menu am (NOLOCK) on am.id_menu = m.id_menu "
                            + " where  m.id_parent = @id_parent and m.id_menu not in   "
                            + " (select id_menu from T_Akses_Menu (NOLOCK) where id_profile = @id_profile)  "
                            + " and m.status_menu = 1  "
                            + " group by m.id_menu, m.id_group, m.nama_menu, m.url, m.[level],  "
                            + " m.status_menu, nama_group, m.id_parent, md.nama_menu ";

            List<String> args = new List<String>();

            try
            {
                // Set Query
                db.setQuery(query);

                // Parameter
                if (!String.IsNullOrEmpty(this.IdParent))
                    db.AddParameter("@id_parent", this.IdParent);

                if (!String.IsNullOrEmpty(this.User.IdUser))
                    db.AddParameter("@id_profile", this.User.IdUser);

                // Execute Reader
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    Menu mn = RowMapperMenu(reader);
                    data.Add(mn);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }

        public static Menu RowMapperMenu(System.Data.Common.DbDataReader reader)
        {
            Menu m = new Menu();
            m.GroupMenu = new GroupMenu();
            m.User = new User();

            //if (reader["id_profile"] != DBNull.Value)
            //    m.User.IdUser = reader["id_profile"].ToString().Trim();

            if (reader["id_menu"] != DBNull.Value)
                m.IdMenu = reader["id_menu"].ToString().Trim();

            if (reader["id_parent"] != DBNull.Value)
                m.IdParent = reader["id_parent"].ToString().Trim();

            if (reader["id_group"] != DBNull.Value)
                m.IdGroup = reader["id_group"].ToString().Trim();

            if (reader["nama_group"] != DBNull.Value)
                m.NamaGroup = reader["nama_group"].ToString().Trim();

            if (reader["nama_menu"] != DBNull.Value)
                m.NamaMenu = reader["nama_menu"].ToString().Trim();

            if (reader["url"] != DBNull.Value)
                m.URL = reader["url"].ToString().Trim();

            if (reader["level"] != DBNull.Value)
                m.Level = Convert.ToInt32(reader["level"]);

            if (reader["status_menu"] != DBNull.Value)
                m.StatusMenu = Convert.ToInt32(reader["status_menu"]);

            if (reader["nama_parent"] != DBNull.Value)
                m.NamaParent = reader["nama_parent"].ToString().Trim();

            return m;
        }

        public static Menu RowMapperListMenu(System.Data.Common.DbDataReader reader)
        {
            Menu m = new Menu();
            m.GroupMenu = new GroupMenu();

            if (reader["id_menu"] != DBNull.Value)
                m.IdMenu = reader["id_menu"].ToString().Trim();

            if (reader["id_parent"] != DBNull.Value)
                m.IdParent = reader["id_parent"].ToString().Trim();

            if (reader["id_group"] != DBNull.Value)
                m.IdGroup = reader["id_group"].ToString().Trim();

            if (reader["nama_group"] != DBNull.Value)
                m.NamaGroup = reader["nama_group"].ToString().Trim();

            if (reader["nama_menu"] != DBNull.Value)
                m.NamaMenu = reader["nama_menu"].ToString().Trim();

            if (reader["url"] != DBNull.Value)
                m.URL = reader["url"].ToString().Trim();

            if (reader["level"] != DBNull.Value)
                m.Level = Convert.ToInt32(reader["level"]);

            if (reader["status_menu"] != DBNull.Value)
                m.StatusMenu = Convert.ToInt32(reader["status_menu"]);

            if (reader["nama_parent"] != DBNull.Value)
                m.NamaParent = reader["nama_parent"].ToString().Trim();

            return m;
        }

        public void InsertMenu(Database db)
        {
            String query = "INSERT INTO T_Menu (id_group, id_menu, id_parent, nama_menu, url, [level], status_menu) "
                        + "VALUES(@id_group, @id_menu, @id_parent, @nama_menu, @url, @level, @status_menu)";

            try
            {
                db.setQuery(query);

                db.AddParameter("@id_group", this.IdGroup);
                db.AddParameter("@id_menu", this.IdMenu);

                if (!String.IsNullOrEmpty(this.IdParent))
                    db.AddParameter("@id_parent", this.IdParent);
                else
                    db.AddParameter("@id_parent", DBNull.Value);

                db.AddParameter("@nama_menu", this.NamaMenu);
                db.AddParameter("@url", this.URL);
                db.AddParameter("@level", this.Level);
                db.AddParameter("@status_menu", this.StatusMenu);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateMenu(Database db)
        {
            String query = "UPDATE T_Menu SET [id_group]=@id_group, [id_parent]=@id_parent, [nama_menu] = @nama_menu,"
                        + " [url] = @url, [level] = @level, [status_menu] = @status_menu"
                        + " WHERE id_menu = @id_menu";
            try
            {
                db.setQuery(query);

                db.AddParameter("@id_menu", this.IdMenu);
                db.AddParameter("@id_group", this.IdGroup);
                db.AddParameter("@id_parent", this.IdParent);
                db.AddParameter("@nama_menu", this.NamaMenu);
                db.AddParameter("@url", this.URL);
                db.AddParameter("@level", this.Level);
                db.AddParameter("@status_menu", this.StatusMenu);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Database db)
        {
            String query = "delete T_Menu WHERE id_menu = @id_menu";
            try
            {
                db.setQuery(query);
                db.AddParameter("@id_menu", this.IdMenu);
                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Menu> GetParentMenu(Database db)
        {
            List<Menu> data = new List<Menu>();

            String query = "select id_parent = id_menu, id_menu, nama_menu, status_menu, level "
                        + "from T_Menu m (NOLOCK) "
                        + "where id_group = @id_group and status_menu = 1 and level = 0 "
                        + "order by m.id_parent ";

            try
            {
                // Set Query
                db.setQuery(query);

                // Parameter
                if (!String.IsNullOrEmpty(this.IdGroup))
                    db.AddParameter("@id_group", this.IdGroup);

                // Execute Reader
                System.Data.Common.DbDataReader reader = db.ExecuteReader();
                while (reader.Read())
                {
                    Menu m = RowMapperParentMenu(reader);
                    data.Add(m);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
            }

            return data;
        }

        public static Menu RowMapperParentMenu(System.Data.Common.DbDataReader reader)
        {
            Menu m = new Menu();

            if (reader["id_parent"] != DBNull.Value)
                m.IdMenu = reader["id_parent"].ToString().Trim();

            if (reader["nama_menu"] != DBNull.Value)
                m.NamaMenu = reader["nama_menu"].ToString().Trim();

            return m;
        }

        public void LoadParentToDDL(DropDownList ddl, Database db)
        {
            Menu m = new Menu();
            List<Menu> data = new List<Menu>();


            data.AddRange(this.GetParentMenu(db));
            
            ddl.DataSource = data;
            ddl.DataValueField = "IdMenu";
            ddl.DataTextField = "NamaMenu";
            ddl.DataBind();

        }

        public void LoadMenuToDDL(DropDownList ddl, Database db)
        {
            Menu m = new Menu();
            List<Menu> data = new List<Menu>();
            data.AddRange(this.GetMenuAkses(db));
            ddl.DataSource = data;
            ddl.DataValueField = "IdMenu";
            ddl.DataTextField = "NamaMenu";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("", ""));
        }

        public int GetNoUrutMenu(string prefix)
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);

            int noUrut = 1;

            String query = "SELECT MAX(right(id_menu,2)) as NoUrut FROM T_Menu (NOLOCK) where id_menu like @param";

            try
            {
                db.Open();
                db.setQuery(query);
                db.AddParameter("@param", "%" + prefix + "%");

                Object result = db.ExecuteScalar();
                if (result != DBNull.Value)
                    noUrut = Convert.ToInt32(result) + 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Close();
            }
            return noUrut;
        }
    }
}