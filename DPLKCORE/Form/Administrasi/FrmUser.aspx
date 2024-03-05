<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmUser.aspx.cs"
    Inherits="DPLKCORE.Form.Administrasi.FrmUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Administration /</span>
                    <asp:Label ID="Label1" runat="server"></asp:Label></h4>


                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnSave" runat="server" CausesValidation="false"
                                ImageUrl="~/Images/Button/Save/Save 48.png" ToolTip="Save Data" />
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   
                            <asp:ImageButton ID="imgbtnCancel" runat="server" ImageAlign="Right" ImageUrl="~/Images/Button/Back 48.png"
                                ToolTip="Back"></asp:ImageButton>
                        </td>
                    </tr>
                </table>



                <div class="row mt-3">
                    <div class="col-md-6">

                        <center>
                   <asp:Panel ID="pnlNotif" runat="server" Height="20px">
                    <asp:Label ID="lblNotif" runat="server" Font-Bold="true" Font-Size="X-Large" CssClass=blink></asp:Label>
                </asp:Panel>
                </center>

                    </div>
                </div>
            </div>



            <div class="card-body">
                <table>
                    <tr>
                        <td>Id User</td>
                        <td>
                            <asp:TextBox ID="txtIdUser" runat="server" CssClass="form-control" Width="350"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Nama Lengkap</td>
                        <td>
                            <asp:TextBox ID="txtNamaLengkap" runat="server" CssClass="form-control" Width="350" Style="text-transform: uppercase;"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Password</td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Width="350"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Bagian</td>
                        <td>
                            <asp:DropDownList ID="ddlBagian" runat="server" CssClass="form-control">
                                <asp:ListItem>CLAIM</asp:ListItem>
                                <asp:ListItem>FINANCE</asp:ListItem>
                                <asp:ListItem>INVESTMENT</asp:ListItem>
                                <asp:ListItem>OPERATION & TECHNIQUE</asp:ListItem>
                                <asp:ListItem>POS</asp:ListItem>
                                <asp:ListItem>SALES & MARKETING GROUP</asp:ListItem>
                                <asp:ListItem>SYSTEM INFORMATION</asp:ListItem>
                                <asp:ListItem>UNDERWRITING</asp:ListItem>
                                <asp:ListItem>CUSTOMER SERVICE</asp:ListItem>
                                <asp:ListItem>DPLK</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Jabatan</td>
                        <td>
                            <asp:DropDownList ID="ddlJabatan" runat="server" CssClass="form-control">
                                <asp:ListItem>STAFF</asp:ListItem>
                                <asp:ListItem>SUPERVISOR</asp:ListItem>
                                <asp:ListItem>HEAD</asp:ListItem>
                                <asp:ListItem>GROUP HEAD</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Status</td>
                        <td>
                            <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>&nbsp;Tidak Aktif&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem>&nbsp;Aktif</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>


            </div>
        </div>
    </div>
</asp:Content>
