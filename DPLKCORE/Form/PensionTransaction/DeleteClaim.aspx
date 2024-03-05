<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteClaim.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.DeleteClaim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Delete Claim</h1>

        <asp:TextBox ID="TXT_CERTIFICATE" runat="server" placeholder="Enter Certificate Number"></asp:TextBox>
        <asp:Button ID="BTN_GET_CLIENT" runat="server" Text="Get Client" OnClick="BTN_GET_CLIENT_Click" />

        <br />

        <asp:TextBox ID="TXT_CLIENT_NAME" runat="server" placeholder="Client Name" ReadOnly="true"></asp:TextBox>

        <br />

        <asp:TextBox ID="TXT_BATCH_ID" runat="server" placeholder="Enter Batch ID"></asp:TextBox>

        <br />

        <asp:Button ID="BTN_DELETE" runat="server" Text="Delete" OnClick="BTN_DELETE_Click" />
    </div>
</asp:Content>
