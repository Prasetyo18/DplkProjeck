<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WithdrawEditing.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.WithdrawEditing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>List Withdraw Editing</h1>

        <asp:GridView ID="GridViewWithdrawEditing" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Register" HeaderText="Register" SortExpression="Register" />
                <asp:BoundField DataField="TferTypeNumber" HeaderText="Transfer Type Number" SortExpression="TferTypeNumber" />
                <asp:BoundField DataField="TrnsSeqNumber" HeaderText="Transaction Sequence Number" SortExpression="TrnsSeqNumber" />
                <asp:BoundField DataField="CerNumber" HeaderText="Certificate Number" SortExpression="CerNumber" />
                <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName" />
                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName" />
                <asp:BoundField DataField="TferTypeName" HeaderText="Transfer Type Name" SortExpression="TferTypeName" />
                <asp:BoundField DataField="TferAmount" HeaderText="Transfer Amount" SortExpression="TferAmount" />
                <asp:BoundField DataField="BankCentralName" HeaderText="Bank Central Name" SortExpression="BankCentralName" />
                <asp:BoundField DataField="BankAddress" HeaderText="Bank Address" SortExpression="BankAddress" />
                <asp:BoundField DataField="AccountNumber" HeaderText="Account Number" SortExpression="AccountNumber" />
                <asp:BoundField DataField="AccountName" HeaderText="Account Name" SortExpression="AccountName" />
                <asp:BoundField DataField="KodeBank" HeaderText="Kode Bank" SortExpression="KodeBank" />
                <asp:BoundField DataField="BatchId" HeaderText="Batch ID" SortExpression="BatchId" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
