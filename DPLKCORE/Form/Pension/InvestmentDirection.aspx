<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvestmentDirection.aspx.cs" Inherits="DPLKCORE.Form.Pension.InvestmentDirection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Investment Direction</h1>

        <div>
            <label for="DDLcompany">Company Name:</label>
            <asp:DropDownList ID="DDLcompany" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="dllGroup">Group Number :</label>
            <asp:DropDownList ID="dllGroup" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="ddlInvestOption">Investment Option :</label>
            <asp:DropDownList ID="ddlInvestOption" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="ddlChargeRaymentRep">Charge Frequency :</label>
            <asp:DropDownList ID="ddlChargeRaymentRep" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="txtNextChargeDt">Next Charge Date:</label>
            <asp:TextBox ID="txtNextChargeDt" runat="server" type="date" /><br />
            <%-----%>

            <label for="txtBilled">Billed (%):</label>
            <asp:TextBox ID="txtBilled" runat="server" /><br />
            <%-----%>

            <label for="txtDeducated">Deducted (%):</label>
            <asp:TextBox ID="txtDeducated" runat="server" /><br />
            <%-----%>

            <label for="txtChargeRate">Charge Rate (%):</label>
            <asp:TextBox ID="txtChargeRate" runat="server" /><br />
            <%-----%>

            <label for="txtChageAmt">Charge Amount:</label>
            <asp:TextBox ID="txtChageAmt" runat="server" /><br />
            <%-----%>

            <label for="txtMaxInvChsdPerc">Maximum Investment Choosed Percentage:</label>
            <asp:TextBox ID="txtMaxInvChsdPerc" runat="server" /><br />
            <%-----%>

            <label for="txtMinInvChsdPerc">Minimum Investment Choosed Percentage:</label>
            <asp:TextBox ID="txtMinInvChsdPerc" runat="server" /><br />
            <%-----%>

            <asp:Button ID="btnInvest" runat="server" Text="Insert Group" />
        </div>
    </div>
</asp:Content>
