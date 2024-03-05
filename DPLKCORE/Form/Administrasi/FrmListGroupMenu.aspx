<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmListGroupMenu.aspx.cs"
    Inherits="DPLKCORE.Form.Administrasi.FrmListGroupMenu" MaintainScrollPositionOnPostback="true" %>

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
                            <asp:ImageButton ID="imgbtnCari" runat="server" CausesValidation="false"
                                ImageUrl="~/Images/Button/Search/Search 48.png" ToolTip="Search"></asp:ImageButton>
                        </td>
                        <td>&nbsp;&nbsp;
                                   
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CausesValidation="false"
                                ImageUrl="~/Images/Button/Add/Add 48.png" ToolTip="Add"></asp:ImageButton>
                        </td>
                        <td>&nbsp;&nbsp;
                                   
                            <asp:ImageButton ID="imgbtnRefresh" runat="server" CausesValidation="false"
                                ImageUrl="~/Images/Button/Refresh/Refresh 48.png" ToolTip="Refresh"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
                <div class="row mt-3">
                    <div class="col-md-6">


                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="imgbtnCari">
                            <div class="col-md-8">
                                <table>
                                    <tr>
                                        <td>Nama Group Menu</td>
                                        <td>
                                            <asp:TextBox ID="txtNamaGroup" runat="server" CssClass="form-control" Width="350"
                                                Style="text-transform: uppercase;"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-3" align="right">
                            </div>
                        </asp:Panel>

                        <center>
                    <asp:Panel ID="Panel1" runat="server" Height="20px">
                            <asp:Label ID="lblNotif" runat="server" Font-Bold="true"  Font-Size="Large"></asp:Label>
                    </asp:Panel>
                </center>
                    </div>

                </div>
            </div>

            <div class="card-body">

                <asp:GridView ID="gridDisplay" runat="server" Width="100%" AllowPaging="true"
                    PageIndex="0" AutoGenerateColumns="false" CssClass="gridstyle"
                    PagerStyle-HorizontalAlign="Center" OnPageIndexChanging="OnPageIndexChanging"
                    OnRowCommand="OnGridRowCommand" OnRowDataBound="OnGridDataBound"
                    Font-Names="Roboto;">
                    <Columns>
                        <asp:BoundField HeaderText="Kode Group Menu" DataField="IdGroup" />
                        <asp:BoundField HeaderText="Nama Group Menu" DataField="NamaGroup" />
                        <asp:BoundField HeaderText="Urutan" DataField="Urutan" />
                        <asp:BoundField HeaderText="Status" DataField="StatusGroup" />
                        <asp:BoundField HeaderText="Prefix" DataField="Prefix" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" ToolTip="Edit" ImageAlign="AbsMiddle"
                                    ImageUrl="~/Images/Button/Edit/Edit 24.png" CommandName="editData" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Delete" ImageAlign="AbsMiddle"
                                    ImageUrl="~/Images/Button/Delete/Delete 24.png" CommandName="deleteData" />
                                <asp:ConfirmButtonExtender ID="conExtDelete" runat="server" TargetControlID="btnDelete"
                                    ConfirmText="Hapus Group Menu?">
                                </asp:ConfirmButtonExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>DATA TIDAK DITEMUKAN</EmptyDataTemplate>
                    <EmptyDataRowStyle ForeColor="Red" />
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                </asp:GridView>

            </div>


        </div>
    </div>





</asp:Content>
