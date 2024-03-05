<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetupBank.aspx.cs" Inherits="DPLKCORE.Form.PensionMaintenance.SetupBank" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Setup Bank</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TXT_BANKCODE" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="TXT_BANK_NM" runat="server"></asp:TextBox><br />
            <asp:TextBox ID="TXT_BANKADDR" runat="server"></asp:TextBox><br />
            <asp:Button ID="BT_SAVE" runat="server" Text="Save" OnClick="BT_SAVE_Click" />
        </div>
    </form>
</body>
</html>
