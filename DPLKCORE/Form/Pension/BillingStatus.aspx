<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BillingStatus.aspx.cs" Inherits="DPLKCORE.Form.Pension.BillingStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Billing Status</h1>

        <div>
            <label for="DDLcompany">Company Name:</label>
            <asp:DropDownList ID="DDLcompany" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="dllGroup">Group Number :</label>
            <asp:DropDownList ID="dllGroup" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="txtNextFeePeriode">Next Fee Periode :</label>
            <asp:TextBox ID="txtNextFeePeriode" runat="server" type="date" /><br />
            <%-----%>

            <label for="txtNextFeeGenerateDt">Next Fee Generate Date :</label>
            <asp:TextBox ID="txtNextFeeGenerateDt" runat="server" type="date" /><br />
            <%-----%>

            <label for="ddlFreq">Fee Frequency  :</label>
            <asp:DropDownList ID="ddlFreq" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="txtNextContributionGenerate">Next Contribution Periode :</label>
            <asp:TextBox ID="txtNextContributionGenerate" runat="server" type="date" /><br />
            <%-----%>

            <label for="txtNextContribution">Next Contribution GenerateDate :</label>
            <asp:TextBox ID="txtNextContribution" runat="server" type="date" /><br />
            <%-----%>

            <label for="ddlConFrq">Contribution Frequency :</label>
            <asp:DropDownList ID="ddlConFrq" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <asp:Button ID="btnBilling" runat="server" Text="Insert Group" />
        </div>
    </div>
</asp:Content>
