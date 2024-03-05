using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class PengantarKartuApproval
    {
        public string NoSurat { get; set; }
        public string Ditujukan { get; set; }
        public DateTime TanggalSurat { get; set; }
        public string NamaClient { get; set; }
        public int CertificateNumber { get; set; }
    }

    public class SuratPengantarKartuListProcessor
    {
        public List<PengantarKartuApproval> GetSuratPengantarKartuList(Database db)
        {
            try
            {
                List<PengantarKartuApproval> result = new List<PengantarKartuApproval>();

                db.setQuery("SELECT DISTINCT sk.no_surat, ditujukan, sk.tgl_surat, cl.client_nm, pk.cer_nmbr "
                            + "FROM svrapp.dbhr.dbo.t_admsrt_surat_keluar sk "
                            + "INNER JOIN pengantar_kartu pk ON pk.no_surat = sk.no_surat "
                            + "INNER JOIN certificate cer ON cer.cer_nmbr = pk.cer_nmbr "
                            + "INNER JOIN client cl ON cl.client_nmbr = cer.client_nmbr "
                            + "WHERE cer.group_nmbr IN (10078,10341,10343,10344,10345,10346) "
                            + "AND status_surat = 0 ORDER BY sk.tgl_surat DESC");

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PengantarKartuApproval item = new PengantarKartuApproval
                        {
                            NoSurat = reader["no_surat"].ToString(),
                            Ditujukan = reader["ditujukan"].ToString(),
                            TanggalSurat = Convert.ToDateTime(reader["tgl_surat"]),
                            NamaClient = reader["client_nm"].ToString(),
                            CertificateNumber = Convert.ToInt32(reader["cer_nmbr"])
                        };

                        result.Add(item);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetSuratPengantarKartuList", ex);
            }
        }
    }

}