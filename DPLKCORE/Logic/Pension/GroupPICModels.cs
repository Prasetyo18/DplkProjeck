using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class GroupPICModels
    {
        public string KdPic { get; set; }
        public int GroupNmbr { get; set; }
        public string Title { get; set; }
        public string NamaPic { get; set; }
        public string Jabatan { get; set; }
        public bool? IsActive { get; set; }



        public int GetLatestKdPicNmbr(Database db)
        {
            int newKdPICNmbr = 0;
            string query = "select max(right(kd_pic,4)) as kd_pic from group_pic";
            try
            {
                db.setQuery(query);
                newKdPICNmbr += Convert.ToInt32(db.ExecuteScalar()) + 1;
                return newKdPICNmbr;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Dictionary<string, object> LoadData(Database db, int groupNmbr)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            string query = "select kd_pic, title, nama_pic, jabatan, case when is_active = 0 then 'Aktif' else 'Nonaktif' end as is_active from group_pic where group_nmbr = @group_nmbr";
            try
            {
                db.setQuery(query);
                db.AddParameter("@group_nmbr", groupNmbr);
                DataTable dt = db.ExecuteQuery();
                if (dt.Rows.Count > 0)
                {
                    DataRow firstRow = dt.Rows[0];
                    foreach (DataColumn col in dt.Columns)
                    {
                        output.Add(col.ColumnName, firstRow[col]);
                    }
                }
                return output;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public bool InsertGroupPIC(Database db)
        {
            String query = "INSERT INTO group_pic " +
                           "VALUES (@KdPic, @GroupNmbr, @Title, @NamaPic, @Jabatan, @IsActive)";
            try
            {
                db.setQuery(query);

                db.AddParameter("@KdPic", this.KdPic);
                db.AddParameter("@GroupNmbr", this.GroupNmbr);
                db.AddParameter("@Title", this.Title);
                db.AddParameter("@NamaPic", this.NamaPic);
                db.AddParameter("@Jabatan", this.Jabatan);
                db.AddParameter("@IsActive", this.IsActive);

                db.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateGroupPIC(Database db)
        {
            String query = "UPDATE group_pic " +
                           "SET title = @Title, nama_pic = @NamaPic, jabatan = @Jabatan, is_active = @IsActive " +
                           "WHERE kd_pic = @KdPic AND group_nmbr = @GroupNmbr";
            try
            {
                db.setQuery(query);

                db.AddParameter("@KdPic", this.KdPic);
                db.AddParameter("@GroupNmbr", this.GroupNmbr);
                db.AddParameter("@Title", this.Title);
                db.AddParameter("@NamaPic", this.NamaPic);
                db.AddParameter("@Jabatan", this.Jabatan);
                db.AddParameter("@IsActive", this.IsActive);

                db.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteGroupPIC(Database db, string kdPic, int groupNmbr)
        {
            String query = "DELETE FROM GroupPIC WHERE KdPic = @KdPic AND GroupNmbr = @GroupNmbr";
            try
            {
                db.setQuery(query);

                db.AddParameter("@KdPic", kdPic);
                db.AddParameter("@GroupNmbr", groupNmbr);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}