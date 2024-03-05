<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JoinAccount.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.JoinAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Join Account</h1>

        <asp:GridView ID="GridViewJoinAccount" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            PageSize="10" CssClass="table table-bordered table-striped">
            <Columns>
                <asp:BoundField DataField="trns_seq_nmbr" HeaderText="Transaction Sequence Number" SortExpression="trns_seq_nmbr" />
                <asp:BoundField DataField="trns_hst_efctv_dt" HeaderText="Transaction History Effective Date" SortExpression="trns_hst_efctv_dt" />
                <asp:BoundField DataField="source_cer" HeaderText="Source Certificate" SortExpression="source_cer" />
                <asp:BoundField DataField="source_grp" HeaderText="Source Group" SortExpression="source_grp" />
                <asp:BoundField DataField="source_nm" HeaderText="Source Name" SortExpression="source_nm" />
                <asp:BoundField DataField="dst_cer" HeaderText="Destination Certificate" SortExpression="dst_cer" />
                <asp:BoundField DataField="dst_grp" HeaderText="Destination Group" SortExpression="dst_grp" />
                <asp:BoundField DataField="dst_nm" HeaderText="Destination Name" SortExpression="dst_nm" />
                <asp:BoundField DataField="source_company" HeaderText="Source Company" SortExpression="source_company" />
                <asp:BoundField DataField="dst_company" HeaderText="Destination Company" SortExpression="dst_company" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
