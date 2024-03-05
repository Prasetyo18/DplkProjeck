using DPLKCORE.Class;
using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DPLKCORE.Logic.Pension
{
    public class EntryNotaBillingModels
    {
        public string NoNota { get; set; }
        public string NoKontrak { get; set; }
        public DateTime TglJatuhTempo { get; set; }
        public int JmlKaryawan { get; set; }
        public string JangkaPembayaran { get; set; }
        public DateTime TglTerbit { get; set; }
        public string KantorPerwakilan { get; set; }
        public string Up { get; set; }
        public string NamaPerusahaan { get; set; }
        public string Alamat1 { get; set; }
        public string Alamat2 { get; set; }
        public string Alamat3 { get; set; }
        public string KodePos { get; set; }
        public string Perihal { get; set; }
        public string NamaBank { get; set; }
        public string CabangBank { get; set; }
        public string NoRekening { get; set; }
        public string AtasNama { get; set; }
        public string Penyetuju { get; set; }
        public string Hostname { get; set; }
        public string UserID { get; set; }
        
    }

    public class NotaDebetResult
    {
        public int ErrorNumber { get; set; }
        public string ErrorDescription { get; set; }
    }

    public class NotaDebetProcessor
    {
        public NotaDebetResult InsertNotaDebet(Database db, EntryNotaBillingModels notaDebetData)
        {
            try
            {
                db.setQuery("exec SPD_NOTA_DEBET_INSERT @No_Nota, @No_Kontrak, @Tgl_Jatuh_Tempo, " +
                            "@Jml_Karyawan, @Jangka_Pembayaran, @Tgl_Terbit, @Kantor_Perwakilan, " +
                            "@Up, @Nama_Perusahaan, @Alamat1, @Alamat2, @Alamat3, @Kode_Pos, " +
                            "@Perihal, @Nama_Bank, @Cabang_Bank, @No_Rekening, @Atas_Nama, @Penyetuju, @hostname, @UserID");

                db.AddParameter("@No_Nota", notaDebetData.NoNota);
                db.AddParameter("@No_Kontrak", notaDebetData.NoKontrak);
                db.AddParameter("@Tgl_Jatuh_Tempo", notaDebetData.TglJatuhTempo);
                db.AddParameter("@Jml_Karyawan", notaDebetData.JmlKaryawan);
                db.AddParameter("@Jangka_Pembayaran", notaDebetData.JangkaPembayaran);
                db.AddParameter("@Tgl_Terbit", notaDebetData.TglTerbit);
                db.AddParameter("@Kantor_Perwakilan", notaDebetData.KantorPerwakilan);
                db.AddParameter("@Up", notaDebetData.Up);
                db.AddParameter("@Nama_Perusahaan", notaDebetData.NamaPerusahaan);
                db.AddParameter("@Alamat1", notaDebetData.Alamat1);
                db.AddParameter("@Alamat2", notaDebetData.Alamat2);
                db.AddParameter("@Alamat3", notaDebetData.Alamat3);
                db.AddParameter("@Kode_Pos", notaDebetData.KodePos);
                db.AddParameter("@Perihal", notaDebetData.Perihal);
                db.AddParameter("@Nama_Bank", notaDebetData.NamaBank);
                db.AddParameter("@Cabang_Bank", notaDebetData.CabangBank);
                db.AddParameter("@No_Rekening", notaDebetData.NoRekening);
                db.AddParameter("@Atas_Nama", notaDebetData.AtasNama);
                db.AddParameter("@Penyetuju", notaDebetData.Penyetuju);
                db.AddParameter("@hostname", notaDebetData.Hostname);
                db.AddParameter("@UserID", notaDebetData.UserID);

                NotaDebetResult result = new NotaDebetResult();

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.ErrorNumber = Convert.ToInt32(reader["ErrorNumber"]);
                        result.ErrorDescription = reader["ErrorDescription"].ToString();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in InsertNotaDebet", ex);
            }
        }
    }

    //public void LoadDDEntryNota(DropDownList ddl, Database db)
    //{
    //    List<string> companyNames = GetCompanyEntry(db);

    //    ddl.DataSource = companyNames;
    //    ddl.DataBind();
    //}

    //public List<string> GetCompanyEntry(Database db)
    //{
    //    List<string> companyNames = new List<string>();

    //    string query = "SELECT DISTINCT [company name] FROM V_CLAIM_REQUEST_2 ORDER BY 1";

    //    try
    //    {
    //        db.setQuery(query);
    //        using (System.Data.Common.DbDataReader reader = db.ExecuteReader())
    //        {
    //            while (reader.Read())
    //            {
    //                if (reader["company name"] != DBNull.Value)
    //                {
    //                    string companyName = reader["company name"].ToString().Trim();
    //                    companyNames.Add(companyName);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(AppMessage.MSG_FAIL_DISPLAY + Environment.NewLine + ex.Message);
    //    }

    //    return companyNames;
    //}

}