using DPLKCORE.Framework;
using DPLKCORE.Logic.Pension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Form.PensionTransaction
{
    public partial class EntryNotaBilling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");
        }

        protected void btnEntry_Click(object sender, EventArgs e)
        {
            try
            {
                EntryNotaBillingModels notaDebetData = new EntryNotaBillingModels
                {
                    NoNota = txtNoNota.Text,
                    NoKontrak = txtNoKontrak.Text,
                    TglJatuhTempo = Convert.ToDateTime(txtTglJatuhTempo.Text),
                    JmlKaryawan = Convert.ToInt32(txtJmlKaryawan.Text),
                    JangkaPembayaran = txtJangkaPembayaran.Text, 
                    TglTerbit = DateTime.Now,
                    KantorPerwakilan = txtKantorPerwakilan.Text,
                    Up = txtUp.Text,
                    NamaPerusahaan = txtNamaPerusahaan.Text,
                    Alamat1 = txtAlamat1.Text,
                    Alamat2 = txtAlamat2.Text,
                    Alamat3 = txtAlamat3.Text,
                    KodePos = txtKodePos.Text,
                    Perihal = txtPerihal.Text,
                    NamaBank = txtNamaBank.Text,
                    CabangBank = txtCabangBank.Text,
                    NoRekening = txtNoRekening.Text,
                    AtasNama = txtAtasNama.Text,
                    Penyetuju = txtPenyetuju.Text,
                    Hostname = Environment.MachineName,
                    UserID = txtUserID.Text
                };

                Connection con = new Connection();
                String cnstr = con.ConnectionStringPension;
                Database db = new Database(cnstr);

                NotaDebetProcessor processor = new NotaDebetProcessor();
                NotaDebetResult result = processor.InsertNotaDebet(db, notaDebetData);

                if (result.ErrorNumber == 0)
                {
                    Response.Redirect("EntryNotaBilling.aspx");
                }
                else
                {
                    Response.Write("Error: " + result.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Exception: " + ex.Message);
            }
        }
    }
}