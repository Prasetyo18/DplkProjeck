<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroupCharge.aspx.cs" Inherits="DPLKCORE.Form.Pension.GroupChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Group Charge </h1>

        <div>
            <label for="DDLcompany">Company Name:</label>
            <asp:DropDownList ID="DDLcompany" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="ddlGroupNmbr">Group Number:</label>
            <asp:DropDownList ID="ddlGroupNmbr" runat="server">
            </asp:DropDownList><br />

            <%-----%>

            <label for="ddlChargeType">Charge Type:</label>
            <asp:DropDownList ID="ddlChargeType" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="txtEffecDt">Effective Date:</label>
            <asp:TextBox ID="txtEffecDt" runat="server" type="date" /><br />

            <%-----%>

            <label for="ddlFreq">Frequency :</label>
            <asp:DropDownList ID="ddlFreq" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="txtNextChgDt">Next Charge Date:</label>
            <asp:TextBox ID="txtNextChgDt" runat="server" type="date" /><br />
            <%-----%>

            <label for="txtChrgTerDt">Charge Termination Date:</label>
            <asp:TextBox ID="txtChrgTerDt" runat="server" type="date" /><br />
            <%-----%>

            <label for="txtBilPer">Billed Percentage:</label>
            <asp:TextBox ID="txtBilPer" runat="server" /><br />
            <%-----%>

            <label for="txtDeductPer">Deduct Percentage:</label>
            <asp:TextBox ID="txtDeductPer" runat="server" /><br />
            <%-----%>

            <label for="txtChargeRate">Charge Rate :</label>
            <asp:TextBox ID="txtChargeRate" runat="server" /><br />
            <asp:DropDownList ID="ddlChargeRate" runat="server">
            </asp:DropDownList><br />
            <%-----%>

            <label for="txtChargeAmmout">Charge Amount:</label>
            <asp:TextBox ID="txtChargeAmmout" runat="server" /><br />
            <%-----%>

            <label for="txtMaxFreqYear">
                Max Freq Per Year
            </label>
            <asp:TextBox ID="txtMaxFreqYear" runat="server" /><br />
            <%-----%>
            <label for="txtMaxFeeFreqPerYear">
                Max Free Freq Per Year
            </label>
            <asp:TextBox ID="txtMaxFeeFreqPerYear" runat="server" /><br />
            <%-----%>

            <label for="txteftdt">
                Effective Date</label>
            <asp:TextBox ID="txteftdt" runat="server" type="date" /><br />
            <%-----%>
            <label for="txtMaxChargeAmt">
                Max Charge Amount</label>
            <asp:TextBox ID="txtMaxChargeAmt" runat="server" /><br />
            <%-----%>

            <asp:Button ID="btnGroupChange" runat="server" Text="Save" />
            <%-----%>
        </div>
    </div>
</asp:Content>
