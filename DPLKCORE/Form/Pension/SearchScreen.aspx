<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchScreen.aspx.cs" Inherits="DPLKCORE.Form.Pension.SearchScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <div class="card-header">
            <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>Transaction - Editing Claim</h4>
            <div class="tab-content">
                <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                    <div class="card-body">
                        <h1>Search</h1>
                        <table id="Table1" style="z-index: 101; position: absolute; top: 8px; left: 8px" cellspacing="1"
                            cellpadding="1" width="98%" border="0">
                            <tr>
                                <td class="TDBGColor2">&nbsp;
                                    <asp:Label ID="LbTitle" Visible="true" runat="server" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="LbType" Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="LbCaller" Visible="false" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="mb-3">
                                        <asp:DataGrid ID="DGSearchCategory" CssClass="gridview-table" runat="server" AutoGenerateColumns="False" ShowHeader="False" Font-Size="XX-Small">
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="fieldname"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="fieldalias">
                                                <ItemStyle Font-Bold="True"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn Visible="False" DataField="type"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Action">
                                                <HeaderStyle Width="80%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT" runat="server" Width="400px" Font-Size="XX-Small" Height="17px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    </div>
                                    <div class="mb-3">
                                        <asp:Button ID="btSearch" CssClass="btn btn-primary" runat="server" Width="96px" Text="Search" OnClick="btSearch_Click"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="DGSearchResult" runat="server" Width="100%" Font-Size="Small" AllowPaging="true" PageIndex="0" PageSize="10"
                                        CssClass="gridview-table" CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#E7E7FF"
                                        OnPageIndexChanging="DGSearchResult_PageIndexChanging" OnRowCommand="DGSearchResult_RowCommand" >
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="mb-3">
                                                        <asp:Button ID="btnSelect" CommandName="Select" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn btn-primary btn-sm btn-edit" runat="server" Text="SELECT" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
