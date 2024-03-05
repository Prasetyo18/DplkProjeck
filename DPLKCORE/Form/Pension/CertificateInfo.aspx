<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificateInfo.aspx.cs" Inherits="DPLKCORE.Form.Pension.CertificateInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>Certificate Indo</h2>

        <div>


            <label for="txtClientCode">Client Code:</label>
            <asp:TextBox ID="txtClientCode" runat="server" /><br />
            <asp:Button ID="btnSearch" runat="server" Text="Search" />
            <asp:Button ID="btnGo" runat="server" Text=" Go " />


            <label for="ddlCerInfo">CERTIFICATE CODE:</label>
            <asp:DropDownList ID="ddlCerInfo" runat="server">
            </asp:DropDownList><br />


            <label for="txtOldClient">OLD CERF. CODE :</label>
            <asp:TextBox ID="txtOldClient" runat="server" /><br />

            <label for="txtClientName">CLIENT NAME-- :</label>
            <asp:TextBox ID="txtClientName" runat="server" /><br />

            <label for="ddlGroup">GROUP :</label>
            <asp:DropDownList ID="ddlGroup" runat="server">
            </asp:DropDownList><br />

            <label for="txtEmpoyeCode">EMPLOYEE CODE:</label>
            <asp:TextBox ID="txtEmpoyeCode" runat="server" /><br />

            <label for="txtEmployedDT">EMPLOYEEMENT DATE:</label>
            <asp:TextBox ID="txtEmployedDT" runat="server" type="date" /><br />


            <label for="txtRetireAge">RETIREMENT AGE--</label>
            <asp:TextBox ID="txtRetireAge" runat="server" /><br />

            <label for="ddlPayC">PAYCENTER :</label>
            <asp:DropDownList ID="ddlPayC" runat="server">
            </asp:DropDownList><br />

            <label for="ddlPayCenterMethod">PAYCENTER  METHOD:</label>
            <asp:DropDownList ID="ddlPayCenterMethod" runat="server">
            </asp:DropDownList><br />

            <label for="ddlCityz">CITIZENSHIP:</label>
            <asp:DropDownList ID="ddlCityz" runat="server">
            </asp:DropDownList><br />

            <label for="ddlTax">TAX ID--:</label>
            <asp:DropDownList ID="ddlTax" runat="server">
            </asp:DropDownList><br />

            <label for="ddlJenisPremi">JENIS PREMI:</label>
            <asp:DropDownList ID="ddlJenisPremi" runat="server">
            </asp:DropDownList><br />

            <label for="ddlJenisRider">JENIS RIDER:</label>
            <asp:DropDownList ID="ddlJenisRider" runat="server">
            </asp:DropDownList><br />

            <label for="ddlJenisPlan">JENIS PLAN:</label>
            <asp:DropDownList ID="ddlJenisPlan" runat="server">
            </asp:DropDownList><br />

            <label for="ddlNamaAgen">NAMA AGEN:</label>
            <asp:DropDownList ID="ddlNamaAgen" runat="server">
            </asp:DropDownList><br />

            <label for="ddlNamaKomisi">JENIS KOMISI:</label>
            <asp:DropDownList ID="ddlNamaKomisi" runat="server">
            </asp:DropDownList><br />

            <label for="ddlMmbrClasPlan">MEMBER CLASS PLAN--:</label>
            <asp:DropDownList ID="ddlMmbrClasPlan" runat="server">
            </asp:DropDownList><br />

            <label for="txtVirtualAcc">VIRTUAL ACCOUNT NUMBER--:</label>
            <asp:TextBox ID="txtVirtualAcc" runat="server" /><br />

            <label for="ddlFunds">FUND SOURCE--:</label>
            <asp:DropDownList ID="ddlFunds" runat="server">
            </asp:DropDownList><br />

            <label for="txtAplicantRec">APPLICATION RECEIVED--:</label>
            <asp:TextBox ID="txtAplicantRec" runat="server" type="date" /><br />

            <label for="txtAplicantCompplete">APPLICATION COMPLETED--:</label>
            <asp:TextBox ID="txtAplicantCompplete" runat="server" type="date" /><br />

            <label for="txtEffectvDt">EFFECTIVE DATE--:</label>
            <asp:TextBox ID="txtEffectvDt" runat="server" type="date" /><br />


            <label for="txtCreateDt">CREATE DATE--:</label>
            <asp:TextBox ID="txtCreateDt" runat="server" type="date" /><br />

            <label for="txtTerminate">TERMINATION DATE--:</label>
            <asp:TextBox ID="txtTerminate" runat="server" type="date" /><br />

            <label for="txtCerDevDt">CERTIFICATE DELIVERY DATE--</label>
            <asp:TextBox ID="txtCerDevDt" runat="server" type="date" /><br />

            <label for="ddlFirstPremiumPaid">FIRST PREMIUM PAID--:</label>
            <asp:DropDownList ID="ddlFirstPremiumPaid" runat="server">
                <asp:ListItem Text="Yes" Value="0" />
                <asp:ListItem Text="No" Value="1" />
            </asp:DropDownList><br />

            <label for="ddlHaveOtherDplk">HAVE OTHER DPPK--:</label>
            <asp:DropDownList ID="ddlHaveOtherDplk" runat="server">
                <asp:ListItem Text="Yes" Value="0" />
                <asp:ListItem Text="No" Value="1" />
            </asp:DropDownList><br />


            <label for="ddlStatus">STATUS:</label>
            <asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList><br />

            <label for="txtCerDevDt">STATUS EFFECTIVE DATE</label>
            <asp:TextBox ID="TextBox1" runat="server" type="date" /><br />

            <label for="txtSalaryDate">SALARY EFFECTIVE DATE</label>
            <asp:TextBox ID="txtSalaryDate" runat="server" type="date" /><br />

            <label for="txtSalaryAmt">SALARY AMOUNT  </label>
            <asp:TextBox ID="txtSalaryAmt" runat="server" /><br />

            <label for="txtBrac">BRANCH </label>
            <asp:TextBox ID="txtBrac" runat="server" /><br />

            <label for="ddlApp">APU PPT:</label>
            <asp:DropDownList ID="ddlApp" runat="server">
                <asp:ListItem Text="Yes" Value="0" />
                <asp:ListItem Text="No" Value="1" />
            </asp:DropDownList><br />

            <asp:Button ID="btnCER" runat="server" Text="Insert Group" />



            <h2>Investment Direction
            </h2>

            <label for="efftdt">EFFECTIVE DATE--</label>
            <asp:TextBox ID="efftdt" runat="server" type="date" /><br />

            <label for="ddlIvstNm">INVESTMENT NAME--:</label>
            <asp:DropDownList ID="ddlIvstNm" runat="server">
            </asp:DropDownList><br />

            <label for="ddlMoneytp">MONEY TYPE--:</label>
            <asp:DropDownList ID="ddlMoneytp" runat="server">
            </asp:DropDownList><br />

            <label for="pdc">PERCENTAGE</label>
            <asp:TextBox ID="pdc" runat="server" /><br />


        </div>
    </div>
</asp:Content>
