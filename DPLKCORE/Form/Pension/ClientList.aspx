<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientList.aspx.cs" Inherits="DPLKCORE.Form.Pension.ClientListing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>Client List</h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <div class="mb-3">
                                <asp:Button ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn btn-primary btn-md" runat="server" Text="Add new Client" />
                            </div>
                            <div style="overflow-x: auto; overflow-y: auto">
                                <div class="mb-3 row">
                                    <asp:GridView OnRowCommand="GridviewClientList_RowCommand" CssClass="gridview-table" Width="100%" Font-Size="Small" OnPageIndexChanging="GridviewClientList_PageIndexChanging" runat="server" ID="GridviewClientList" AllowPaging="true" PageIndex="0" PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <div class="mb-3">
                                                        <asp:Button ID="btnEdit" CommandName="EditClientRow" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn btn-primary btn-sm btn-edit" runat="server" Text="EDIT" />
                                                    </div>
                                                    <%--<div class="mb-3">
                                                    <asp:Button ID="btnDetail" CssClass="btn btn-primary btn-sm btn-detail" runat="server" Text="DETAIL" />
                                                </div>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
