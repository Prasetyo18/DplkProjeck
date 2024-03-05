using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data;
using DPLKCORE.Framework;
using DPLKCORE.Class;
using DPLKCORE.Logic.Administrasi;
using DPLKCORE.Logic;

namespace DPLKCORE.Form.Administrasi
{
    public partial class FrmUser : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            imgbtnSave.Click += ButtonClicked;
            imgbtnCancel.Click += ButtonClicked;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["state"] == ActionDef.EDIT)
                {
                    txtIdUser.Attributes["readonly"] = "readonly";
                    txtNamaLengkap.Attributes.Remove("readonly");
                    ddlBagian.Enabled = true;
                    ddlJabatan.Enabled = true;
                    rblStatus.Enabled = true;
                    imgbtnCancel.Visible = true;

                    DisplayData();
                }
                else
                    txtIdUser.Attributes.Remove("readonly");

                if (Request.QueryString["id_menu"] == "HOM002")
                {
                    txtIdUser.Attributes["readonly"] = "readonly";
                    txtNamaLengkap.Attributes["readonly"] = "readonly";
                    ddlBagian.Enabled = false;
                    ddlJabatan.Enabled = false;
                    rblStatus.Enabled = false;
                    imgbtnCancel.Visible = true;

                    DisplayData();
                }
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;

            if (ib == imgbtnSave)
                SaveData();
            else if (ib == imgbtnCancel)
                Response.Redirect("~/Form/Administrasi/FrmListUser.aspx?id_menu=ADM001");
        }

        private void DisplayData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            User u = new User();

            try
            {
                db.Open();
                if (Request.QueryString["id_menu"] == "HOM02")
                    u.IdUser = Session[SessionDef.SessionIdUser].ToString();

                if (Request.QueryString["state"] == ActionDef.EDIT)
                    u.IdUser = Request.QueryString["id_profile"];

                var data = u.GetUser(db).FirstOrDefault();
                txtIdUser.Text = u.IdUser;
                txtNamaLengkap.Text = data.NamaLengkap;
                txtPassword.Text = Decrypt(data.Password);
                ddlBagian.Text = data.Bagian;
                ddlJabatan.Text = data.Jabatan;
                rblStatus.SelectedIndex = data.StatusUser;
            }
            catch (Exception ex)
            {
                lblNotif.Visible = true;
                lblNotif.ForeColor = System.Drawing.Color.Red;
                lblNotif.Text = ex.Message;
            }
            finally
            {
                db.Close();
            }
        }

        private void SaveData()
        {
            Connection conn = new Connection();
            Database db = new Database(conn.ConnectionStringDBJiwa);
            User u = new User();

            try
            {
                db.Open();
                db.BeginTransaction();

                u.IdUser = txtIdUser.Text.Trim();
                u.NamaLengkap = txtNamaLengkap.Text.Trim().ToUpper();
               // u.Password = Encrypt(txtPassword.Text.Trim());
                u.Bagian = ddlBagian.SelectedItem.ToString().Trim().ToUpper();
                u.Jabatan = ddlJabatan.SelectedItem.ToString().Trim();
                u.StatusUser = rblStatus.SelectedIndex;

                if(Request.QueryString["state"] == "add")
                    u.InsertUser(db);
                else
                    u.UpdateUser(db);

                db.CommitTransaction();

                lblNotif.Visible = true;
                lblNotif.Text = "Data Berhasil Disimpan";
                lblNotif.ForeColor = System.Drawing.Color.Green;
                imgbtnSave.Visible = false;
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();

                lblNotif.Visible = true;
                lblNotif.Text = "Error : " + ex.Message;
                lblNotif.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                db.Close();
            }
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
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
    }
}