<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmListUser.aspx.cs"
    Inherits="DPLKCORE.Form.Administrasi.FrmListUser" MaintainScrollPositionOnPostback="true" %>

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
                        <td></td>
                        <td>&nbsp;&nbsp;
                                       
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CausesValidation="false"
                                ImageUrl="~/Images/Button/Add/Add 48.png" ToolTip="Add User"></asp:ImageButton>
                        </td>
                        <td>&nbsp;&nbsp;
                                       
                            <asp:ImageButton ID="imgbtnRefresh" runat="server" CausesValidation="false"
                                ImageUrl="~/Images/Button/Refresh/Refresh 48.png" ToolTip="Refresh Data"></asp:ImageButton>
                        </td>
                    </tr>
                </table>

                <div class="row mt-3">
                    <div class="col-md-6">



                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="imgbtnCari">
                            <div class="input-group mb-3">
                                <asp:TextBox ID="txtNamaLengkap" runat="server" CssClass="form-control" placeholder="Nama User"
                                    Style="text-transform: uppercase;"></asp:TextBox>

                                <asp:Button ID="imgbtnCari" runat="server" CausesValidation="false"
                                    CssClass="btn btn-outline-secondary"
                                    ToolTip="Search User"></asp:Button>

                            </div>

                        </asp:Panel>

                        <center>
                   <asp:Panel ID="pnlNotif" runat="server" Height="20px">
                    <asp:Label ID="lblNotif" runat="server" Font-Bold="true" Font-Size="X-Large" CssClass=blink></asp:Label>
                </asp:Panel>
                </center>


                    </div>
                </div>
            </div>

            <div class="card-body">

                <div class="table-responsive text-nowrap">
                    <asp:GridView ID="gridDisplay" runat="server" Width="100%" AllowPaging="true"
                        PageIndex="0" AutoGenerateColumns="false" CssClass="table table-striped"
                        PagerStyle-HorizontalAlign="Center" OnPageIndexChanging="OnPageIndexChanging"
                        OnRowCommand="OnGridRowCommand" OnRowDataBound="OnGridDataBound"
                        Font-Names="Roboto;">
                        <Columns>
                            <asp:BoundField HeaderText="No. " ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Id User" DataField="IdUser" />
                            <asp:BoundField HeaderText="Nama Lengkap" DataField="NamaLengkap" />
                            <asp:BoundField HeaderText="Bagian" DataField="Bagian" />
                            <asp:BoundField HeaderText="Jabatan" DataField="Jabatan" />
                            <asp:BoundField HeaderText="Status" DataField="StatusUser" />
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
                                        ConfirmText="Hapus Hak Akses?">
                                    </asp:ConfirmButtonExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>DATA TIDAK DITEMUKAN</EmptyDataTemplate>
                        <EmptyDataRowStyle ForeColor="Red" />
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="20" />
                   
                        </asp:GridView>
                </div>
            </div>
            <%--   <div class="panel panel-default">
            <div class="panel-heading">
                <center>
                    <h3 class="panel-title" style="font-family: 'Roboto'; font-size: x-large;
                        font-weight: bold; color: #FFFFFF; background-color: #008000">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </h3>
                </center>
                <br />
            </div>
        </div>--%>
        </div>
    </div>
</asp:Content>
