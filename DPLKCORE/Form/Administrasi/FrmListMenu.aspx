<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmListMenu.aspx.cs" 
Inherits="DPLKCORE.Form.Administrasi.FrmListMenu" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


        <div class="container-fluid px-4">
            <div class="card my-4">
                <div class="card-header">
                    <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Adiminstration /</span> List Menu</h4>
                   <%-- <asp:LinkButton ID="btnCreateNew" runat="server" CssClass="btn btn-primary" OnClick="btnCreateNew_Click">
                        <span class="tf-icons bx bx-book-add"></span>&nbsp; Create New
                    </asp:LinkButton>--%>

                    <div class="row mt-3">
                        <div class="col-md-6">
                           <%-- <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSearch">
                                <div class="input-group mb-3">
                                    <asp:TextBox ID="txtSearchString" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-outline-secondary" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </asp:Panel>--%>

                            
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="imgbtnCari">
                    <div class="container">
                        <div class="col-md-8">
                            <table>
                                <tr>
                                    <td>Nama Menu</td>
                                    <td>
                                        <asp:TextBox ID="txtNamaMenu" runat="server" CssClass="form-control" Width=350 
                                            Style="text-transform: uppercase;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-3" align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgbtnCari" runat="server" CausesValidation="false"
                                            ImageUrl="~/Images/Button/Search/Search 48.png" ToolTip="Search Menu"></asp:ImageButton>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgbtnAdd" runat="server" CausesValidation="false"
                                            ImageUrl="~/Images/Button/Add/Add 48.png" ToolTip="Add Menu"></asp:ImageButton>
                                    </td>
                                    <td>&nbsp;&nbsp;
                                        <asp:ImageButton ID="imgbtnRefresh" runat="server" CausesValidation="false"
                                            ImageUrl="~/Images/Button/Refresh/Refresh 48.png" ToolTip="Refresh Data"></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>

                            
                    <asp:Panel ID="Panel1" runat="server" Height="20px">
                          <asp:Label ID="lblNotif" runat="server" Font-Bold="true"  Font-Size="Large"></asp:Label>
                    </asp:Panel>



                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive text-nowrap">


                        <asp:GridView ID="gridDisplay"  runat="server" CssClass="table table-striped"
                            
                    PageIndex="0" AutoGenerateColumns="false" PageSize = 20
                    PagerStyle-HorizontalAlign="Center" onpageindexchanging="OnPageIndexChanging" 
                    onrowcommand="OnGridRowCommand" onrowdatabound="OnGridDataBound" 
                    Font-Names="Roboto;">
                    <Columns>
                        <asp:BoundField HeaderText="Kode Menu" DataField="IdMenu" />
                        <asp:BoundField HeaderText="Nama Group Menu" DataField="NamaGroup" />
                        <asp:BoundField HeaderText="Nama Parent Menu" DataField="NamaParent" />
                        <asp:BoundField HeaderText="Nama Menu" DataField="NamaMenu" />
                        <asp:BoundField HeaderText="URL" DataField="URL" />
                        <asp:BoundField HeaderText="Status" DataField="StatusMenu" />
                        <asp:TemplateField ItemStyle-HorizontalAlign=Center>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" ToolTip="Edit" ImageAlign=AbsMiddle
                                    ImageUrl="~/Images/Button/Edit/Edit 24.png" CommandName="editData" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
            </div>

           <%-- <div class="text-center mt-4">
                Page <%= gvCompanies.PageIndex + 1 %> of <%= gvCompanies.PageCount %>
                <%= RenderPager() %>
            </div>--%>
        </div>

</asp:Content>
