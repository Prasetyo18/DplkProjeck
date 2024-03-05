<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EntryNotaBilling.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.EntryNotaBilling" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Entry Nota Billing</h1>

        <div>
            <div class="form-group">
                <label for="txtNoNota">No Nota:</label>
                <asp:TextBox ID="txtNoNota" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="NamaPerusahaan">Nama Perusahaan :</label>
                <asp:TextBox ID="txtNamaPerusahaan" runat="server" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtNoKontrak">No Kontrak:</label>
                <asp:TextBox ID="txtNoKontrak" runat="server" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="JmlKaryawan">Jumlah Karyawan:</label>
                <asp:TextBox ID="txtJmlKaryawan" runat="server" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="JangkaPembayaran">Jangka Pembayaran :</label>
                <asp:TextBox ID="txtJangkaPembayaran" runat="server" required></asp:TextBox>
            </div>


            <div class="form-group">
                <label for="txtKodePos">Kode Pos:</label>
                <asp:TextBox ID="txtKodePos" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtPerihal">Perihal:</label>
                <asp:TextBox ID="txtPerihal" runat="server" required></asp:TextBox>
            </div>


            <div class="form-group">
                <label for="txtNamaBank">Nama Bank:</label>
                <asp:TextBox ID="txtNamaBank" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="KantorPerwakilan">Kantor Perwaklikan  :</label>
                <asp:TextBox ID="txtKantorPerwakilan" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtTglJatuhTempo">Tgl Jatuh Tempo:</label>
                <asp:TextBox ID="txtTglJatuhTempo" runat="server" type="date" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtCabangBank">Cabang Bank:</label>
                <asp:TextBox ID="txtCabangBank" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtNoRekening">No Rekening:</label>
                <asp:TextBox ID="txtNoRekening" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtAtasNama">Atas Nama:</label>
                <asp:TextBox ID="txtAtasNama" runat="server" required></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtPenyetuju">Penyetuju:</label>
                <asp:TextBox ID="txtPenyetuju" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="Alamat1">Alamat 1:</label>
                <asp:TextBox ID="txtAlamat1" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtAlamat2">Alamat 2:</label>
                <asp:TextBox ID="txtAlamat2" runat="server"></asp:TextBox>
            </div>


            <div class="form-group">
                <label for="txtAlamat3">Alamat 3:</label>
                <asp:TextBox ID="txtAlamat3" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtUserID">User ID:</label>
                <asp:TextBox ID="txtUserID" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="Up">UP:</label>
                <asp:TextBox ID="txtUp" runat="server" required></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Button ID="btnEntry" runat="server" Text="Submit" OnClick="btnEntry_Click" />
            </div>
        </div>

    </div>
</asp:Content>
