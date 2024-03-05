<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmGroupMenu.aspx.cs"
    Inherits="DPLKCORE.Form.Administrasi.FrmGroupMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <div class="container-fluid px-4">
    <div class="card my-4">
        <div class="card-header">
            <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Administration /</span>  <asp:Label ID="Label1" runat="server"></asp:Label></h4>
            <a class="btn btn-primary" asp-action="NewBussinesCompanyIndex" style="background-color: #0073fe;">
                <span class="tf-icons bx bx-book-add"></span>&nbsp; Create New
            </a>

            <div class="row mt-3">
                <div class="col-md-6"> 
                   
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgbtnSave" runat="server" CausesValidation="false" 
                                        ImageUrl="~/Images/Button/Save/Save 48.png" ToolTip="Save Data" />
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" ImageAlign="Right" ImageUrl="~/Images/Button/Back 48.png"
                                        ToolTip="Back"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>




                     <asp:Panel ID="pnlNotif" runat="server" Height="20px">
                    <asp:Label ID="lblNotif" runat="server" Font-Bold="true" Font-Size="X-Large" CssClass=blink></asp:Label>
                </asp:Panel>
                    
                </div>
            </div>
        </div>

 <div class="card-body">  

     
                    <table>
                            <tr>
                                <td>Nama Group Menu</td>
                                <td><asp:TextBox ID="txtNamaGroup" runat="server" CssClass="form-control" width = 350></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Status</td>
                                <td>
                                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem >&nbsp;Tidak Aktif&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem >&nbsp;Aktif</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>Prefix</td>
                                <td><asp:TextBox ID="txtPrefix" runat="server" CssClass="form-control" MaxLength = 3 Style="text-transform: uppercase;"></asp:TextBox></td>
                            </tr>
                        </table>
     </div>
</div></div>
</asp:Content>
