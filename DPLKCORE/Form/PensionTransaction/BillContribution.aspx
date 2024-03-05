<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BillContribution.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.BillContribution" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>Transaction - Bill Contribution</h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <div class="mb-3 row">
                                <div class="col-3">
                                    <label for="DropDownListMode" class="form-label">Mode:</label>
                                    <asp:DropDownList ID="DropDownListMode" class="form-select" OnSelectedIndexChanged="DropDownListMode_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                        <asp:ListItem Text="Bill" Value="0" />
                                        <asp:ListItem Text="Contribution" Value="1" />
                                        <asp:ListItem Text="Rollover" Value="2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-3">
                                    <label for="DropDownListCompany" class="form-label">Company:</label>
                                    <asp:DropDownList ID="DropDownListCompany" class="form-select" OnSelectedIndexChanged="DropDownListCompany_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-3">
                                    <label for="DropDownListGroup" class="form-label">Group:</label>
                                    <asp:DropDownList ID="DropDownListGroup" class="form-select" AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-3">
                                    <div class="mt-4">
                                        <asp:Button ID="BtnShow" CssClass="btn btn-edit" OnClick="BtnShow_Click" Text="Show Master" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-md" style="overflow-x: auto">
                                    <asp:GridView ID="GridViewBillContribution" Font-Size="XX-Small" CssClass="gridview-table" runat="server" AutoGenerateColumns="False"
                                        OnPageIndexChanging="GridViewBillContribution_PageIndexChanging" OnRowCommand="GridViewBillContribution_RowCommand" PageIndex="0" PageSize="5" AllowPaging="true">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_PAYMENT" runat="server" Font-Size="XX-Small" Font-Names="verdana" Text="Payment"
                                                        CommandName="bt_payment" CommandArgument="<%#Container.DataItemIndex %>"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_REM" runat="server" Font-Size="XX-Small" Font-Names="verdana" Width="100%"
                                                        TextMode="MultiLine" MaxLength="255"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_REVERSE" runat="server" Font-Names="verdana" Font-Size="XX-Small" Text="Reverse"
                                                        CommandName="bt_reverse" CommandArgument="<%#Container.DataItemIndex %>"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_SUSPENSE" runat="server" Font-Size="XX-Small" Font-Names="verdana" Text="Suspense"
                                                        CommandName="Suspense" CommandArgument="<%#Container.DataItemIndex %>"></asp:Button>
                                                    <asp:DataGrid ID="DGR_SUSPENSE" runat="server" Font-Size="XX-Small" Font-Names="Verdana">
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CB3" runat="server" AutoPostBack="True" Text=""></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_DETAIL" runat="server" Font-Names="verdana" Font-Size="XX-Small" Text="Detail"
                                                        CommandName="bt_detail" CommandArgument="<%#Container.DataItemIndex %>"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_KUITANSI" runat="server" Font-Names="verdana" Font-Size="XX-Small" Text="Kuitansi"
                                                        CommandName="bt_kuitansi" CommandArgument="<%#Container.DataItemIndex %>" Visible="False"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                            <asp:BoundField DataField="Paycenter" HeaderText="Paycenter" SortExpression="Paycenter" />
                                            <asp:BoundField DataField="Period" HeaderText="Period" SortExpression="Period" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                            <asp:BoundField DataField="Paid Amt" HeaderText="Paid Amount" SortExpression="PaidAmt" />
                                            <asp:BoundField DataField="Paid Date" HeaderText="Paid Date" SortExpression="PaidDate" />
                                            <asp:BoundField DataField="Reversal Date" HeaderText="Reversal Date" SortExpression="ReversalDate" />
                                            <asp:BoundField DataField="Comment" HeaderText="Comment" SortExpression="Comment" />
                                            <asp:BoundField DataField="Paycenter Name" HeaderText="Paycenter Name" SortExpression="PaycenterName" />
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="15"/>
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
