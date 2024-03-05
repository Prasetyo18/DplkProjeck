<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayoutSuspenseI.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.PayoutSuspenseI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Create Payout Suspense</h1>

        <asp:Label ID="LabelTransactionNo" runat="server" Text="Transaction No.: " AssociatedControlID="TextBoxTransactionNo"></asp:Label>
        <asp:TextBox ID="TextBoxTransactionNo" runat="server"></asp:TextBox>

        <asp:Label ID="LabelSuspenseNo" runat="server" Text="Suspense No.: " AssociatedControlID="TextBoxSuspenseNo"></asp:Label>
        <asp:TextBox ID="TextBoxSuspenseNo" runat="server"></asp:TextBox>

        <asp:Label ID="LabelSuspenseDesc" runat="server" Text="Suspense Desc: " AssociatedControlID="TextBoxSuspenseDesc"></asp:Label>
        <asp:TextBox ID="TextBoxSuspenseDesc" runat="server"></asp:TextBox>

        <asp:Label ID="LabelAmountToRetur" runat="server" Text="Amount To Retur: " AssociatedControlID="TextBoxAmountToRetur"></asp:Label>
        <asp:TextBox ID="TextBoxAmountToRetur" runat="server"></asp:TextBox>

        <asp:Label ID="LabelInvestmentSource" runat="server" Text="Investment Source: " AssociatedControlID="TextBoxInvestmentSource"></asp:Label>
        <asp:TextBox ID="TextBoxInvestmentSource" runat="server"></asp:TextBox>

        <asp:Label ID="LabelRegisterNumber" runat="server" Text="Register Number: " AssociatedControlID="TextBoxRegisterNumber"></asp:Label>
        <asp:TextBox ID="TextBoxRegisterNumber" runat="server"></asp:TextBox>

        <asp:Label ID="LabelChequeChargeAmount" runat="server" Text="Cheque Charge Amount: " AssociatedControlID="TextBoxChequeChargeAmount"></asp:Label>
        <asp:TextBox ID="TextBoxChequeChargeAmount" runat="server"></asp:TextBox>

        <asp:Label ID="LabelAccountNumber" runat="server" Text="Account Number: " AssociatedControlID="TextBoxAccountNumber"></asp:Label>
        <asp:TextBox ID="TextBoxAccountNumber" runat="server"></asp:TextBox>

        <asp:Label ID="LabelAccountName" runat="server" Text="Account Name: " AssociatedControlID="TextBoxAccountName"></asp:Label>
        <asp:TextBox ID="TextBoxAccountName" runat="server"></asp:TextBox>

        <asp:Label ID="LabelBankName" runat="server" Text="Bank Name: " AssociatedControlID="TextBoxBankName"></asp:Label>
        <asp:TextBox ID="TextBoxBankName" runat="server"></asp:TextBox>

        <asp:Label ID="LabelReturInformation" runat="server" Text="Retur Information: " AssociatedControlID="TextBoxReturInformation"></asp:Label>
        <asp:TextBox ID="TextBoxReturInformation" runat="server"></asp:TextBox>

        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />

    </div>
</asp:Content>
