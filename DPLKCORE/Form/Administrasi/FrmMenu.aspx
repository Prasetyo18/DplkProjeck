<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmMenu.aspx.cs"
    Inherits="DPLKCORE.Form.Administrasi.FrmMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div class="container-fluid px-4">
    <div class="card my-4">
        <div class="card-header">
            <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Administration /</span>  <asp:Label ID="Label2" runat="server"></asp:Label></h4>
            <a class="btn btn-primary" asp-action="NewBussinesCompanyIndex" style="background-color: #0073fe;">
                <span class="tf-icons bx bx-book-add"></span>&nbsp; Create New
            </a>

            <div class="row mt-3">
                <div class="col-md-6"> 

                    
                    
              

                        <table>
                            <tr>
                                <td>Group Menu</td>
                                <td>
                                    <asp:Label ID="lblgroup" runat="server" Text = "Parent" Visible = false></asp:Label>
                                    <asp:DropDownList ID="ddlGroupMenu" runat="server" CssClass="form-control" 
                                        AutoPostBack = true  OnSelectedIndexChanged="OnGroupSelectedIndexChanged" ></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Level</td>
                                <td>
                                    <asp:RadioButtonList ID="rblLevel" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack = true  OnSelectedIndexChanged="OnLevelSelectedIndexChanged">
                                        <asp:ListItem>&nbsp;Main Menu&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem>&nbsp;Sub Menu</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="Label1" runat="server" Text = "Parent" Visible = false></asp:Label></td>
                                <td><asp:DropDownList ID="ddlParentMenu" runat="server" CssClass="form-control" Visible = false></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>Nama Menu</td>
                                <td><asp:TextBox ID="txtNamaMenu" runat="server" CssClass="form-control" width = 500></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>URL</td>
                                <td><asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" width = 500></asp:TextBox></td>
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

           <asp:GridView ID="gridDisplay" runat="server" Width="100%" AllowPaging="true"
                    PageIndex="0" AutoGenerateColumns="false" CssClass="gridstyle" PageSize = 20
                    PagerStyle-HorizontalAlign="Center" Font-Names="Roboto;">
                    <Columns>
                        <asp:BoundField HeaderText="Kode Menu" DataField="" />
                        <asp:TemplateField ItemStyle-HorizontalAlign=Center>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Delete" ImageAlign=AbsMiddle
                                    ImageUrl="~/Images/Button/Delete/Delete 24.png" CommandName="deleteData" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>DATA TIDAK DITEMUKAN</EmptyDataTemplate>
                    <EmptyDataRowStyle ForeColor="Red" />
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                </asp:GridView>

 </div>
 </div>



</asp:Content>
