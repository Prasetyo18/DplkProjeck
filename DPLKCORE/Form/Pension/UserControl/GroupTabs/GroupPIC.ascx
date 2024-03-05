<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupPIC.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.GroupTabs.GroupPIC" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlTitle" class="col-md-2 col-form-label">Title: </label>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlTitle" class="form-select" AppendDataBoundItems="true">
                <asp:ListItem Text="Bapak" Value="0" />
                <asp:ListItem Text="Ibu" Value="1" />
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtPicName" class="col-md-2 col-form-label">PIC Name: </label>
        <div class="col-md-6">
            <asp:TextBox runat="server" ID="txtPicName" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtJabatan" class="col-md-2 col-form-label">Jabatan: </label>
        <div class="col-md-6">
            <asp:TextBox runat="server" ID="txtJabatan" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlStatus" class="col-md-2 col-form-label">Status: </label>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlStatus" class="form-select" AppendDataBoundItems="true">
                <asp:ListItem Text="Aktif" Value="0" />
                <asp:ListItem Text="NonAktif" Value="1" />
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnSavePIC" runat="server" class="btn btn-primary" OnClick="btnSavePIC_Click" Text="Save" />
        </div>
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnUpdate" runat="server" class="btn btn-primary" OnClick="btnUpdate_Click" Text="Update" />
        </div>
    </div>
</div>