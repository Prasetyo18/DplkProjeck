<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAksesMenu.aspx.cs"
    Inherits="DPLKCORE.Form.Administrasi.FrmAksesMenu" MaintainScrollPositionOnPostback = "true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    
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
                                <td>Id User</td>
                                <td> : <asp:Label ID="lblId" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text = "TAMBAH AKSES MENU" Font-Bold = true></asp:Label>
                                </td>
                                <td>
                                    <asp:ImageButton ID="tambahAksesMenu" runat="server" ImageUrl="~/Images/Button/Add/Add 32.png"
                                        ToolTip="Tambah Akses Menu" CommandName="tambahAksesMenu" />
                                </td>
                            </tr>
                        </table>


                    
                <asp:Panel ID="pnlNotif" runat="server" Height="20px">
                    <asp:Label ID="lblNotif" runat="server" Font-Bold="true" Font-Size="X-Large" CssClass=blink></asp:Label>
                </asp:Panel>

                    </div></div></div>
        <div class="card-body">   

                  <asp:ImageButton ID="imgbtnCancel" runat="server" ImageAlign="Right" ImageUrl="~/Images/Button/Back 48.png"
                            ToolTip="Back"></asp:ImageButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="imgbtnSave" runat="server" CausesValidation="false" 
                            ImageUrl="~/Images/Button/Save/Save 48.png" ToolTip="Save Data" />
                   
             
                <asp:GridView ID="gridTambah" runat="server" Width="100%"
                    PageIndex="0" AutoGenerateColumns="false" CssClass="gridstyle"
                    PagerStyle-HorizontalAlign="Center" Font-Names="Roboto;"
                    onrowdatabound="OnGridTambahDataBound" >
                    <Columns>
                        <asp:BoundField HeaderText="No. " ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Group Menu">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlGroupMenu" runat="server" 
                                    AutoPostBack = true  OnSelectedIndexChanged="OnGroupSelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parent Menu">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlParentMenu" runat="server"
                                    AutoPostBack = true  OnSelectedIndexChanged="OnParentSelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Menu">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>     
             
                <asp:GridView ID="gridDisplay" runat="server" Width="100%"
                    PageIndex="0" AutoGenerateColumns="false" CssClass="gridstyle"
                    PagerStyle-HorizontalAlign="Center" Font-Names="Roboto;"
                    onrowdatabound="OnGridDisplayDataBound" onrowcommand="OnGridDisplayRowCommand" >
                    <Columns>
                        <asp:BoundField HeaderText="No. " ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Group Menu" />
                        <asp:BoundField HeaderText="Parent Menu" />
                        <asp:BoundField HeaderText="Menu" />
                        <asp:BoundField HeaderText="Id Menu" >
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnHapusAksesMenu" runat="server" ImageUrl="~/Images/Button/Delete/Delete 24.png"
                                    ToolTip="Hapus Akses Menu" CommandName="hapusAksesMenu" />
                                <asp:ConfirmButtonExtender ID="conExtDelete" runat="server" TargetControlID="btnHapusAksesMenu"
                                    ConfirmText="Hapus Hak Akses?">
                                </asp:ConfirmButtonExtender>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
           </div>

    </div>
 </div>

</asp:Content>
