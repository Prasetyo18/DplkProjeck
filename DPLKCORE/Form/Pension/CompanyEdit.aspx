<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="DPLKCORE.Form.Pension.CompanyEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Company Index</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>

                <div>
                    <label for="txtCompanyNm">Company Name:</label>
                    <asp:TextBox ID="txtCompanyNm" runat="server" /><br />

                    <label for="chkHasPaycenter">Has Paycenter:</label>
                    <asp:CheckBox ID="chkHasPaycenter" runat="server" /><br />

                    <label for="txtNpwp">NPWP:</label>
                    <asp:TextBox ID="txtNpwp" runat="server" /><br />

                    <label for="ddlBusinessLine">Business Line:</label>
                    <asp:DropDownList ID="ddlBusinessLine" runat="server">
                    </asp:DropDownList><br />

                    <label for="txtContactPerson">Contact Person:</label>
                    <asp:TextBox ID="txtContactPerson" runat="server" /><br />

                    <label for="txtSiup">SIUP:</label>
                    <asp:TextBox ID="txtSiup" runat="server" /><br />

                    <label for="ddlMnySrcType">Money Source Type:</label>
                    <asp:DropDownList ID="ddlMnySrcType" runat="server">
                    </asp:DropDownList><br />

                    <label for="txtPayorNm">Payor Name:</label>
                    <asp:TextBox ID="txtPayorNm" runat="server" /><br />

                    <label for="txtBankNm">Bank Name:</label>
                    <asp:TextBox ID="txtBankNm" runat="server" /><br />

                    <label for="txtAccountNmbr">Account Number:</label>
                    <asp:TextBox ID="txtAccountNmbr" runat="server" /><br />

                    <label for="txtAccountNm">Account Name:</label>
                    <asp:TextBox ID="txtAccountNm" runat="server" /><br />

                    <label for="txtEmail">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" /><br />

                    <label for="txtAdArt">Ad Art:</label>
                    <asp:TextBox ID="txtAdArt" runat="server" /><br />

                    <label for="chkPdpFlg">PDP Flag:</label>
                    <asp:CheckBox ID="chkPdpFlg" runat="server" /><br />

                    <label for="txtOldClientNmbr">Old Client Number:</label>
                    <asp:TextBox ID="txtOldClientNmbr" runat="server" /><br />


                    <asp:Button ID="btnCompanyEdits" runat="server" Text="Submit" OnClick="btnCompanyAdd_Click" />
                </div>
            </div>
        </form>
    </body>
    </html>

</asp:Content>

        <div>
            <h1>Company Edit</h1>

            <asp:Button ID="btnCompanyEdit" runat="server" Text="Back to Company" OnClick="btnCompanyEdit_Click" />
            <asp:Button ID="btnCompanyDelete" runat="server" Text="Delete Company" OnClick="btnCompanyDelete_Click" />
        </div>
    </form>
</body>
</html>
