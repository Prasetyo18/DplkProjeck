<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteSwitching.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.DeleteSwitching" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4">
                    <span class="text-muted fw-light">Transaction - </span>Delete Switching
                </h4>
            </div>
            <div class="card-body">
                <asp:TextBox ID="TXT_CERTIFICATE" runat="server" placeholder="Enter Certificate Number"></asp:TextBox>

                <br />

                <asp:TextBox ID="TXT_CLIENT_NAME" runat="server" placeholder="Client Name" ReadOnly="true"></asp:TextBox>

                <br />

                <asp:TextBox ID="TXT_BATCH_ID" runat="server" placeholder="Enter Batch ID"></asp:TextBox>

                <br />

            </div>
        </div>
    </div>
</asp:Content>

