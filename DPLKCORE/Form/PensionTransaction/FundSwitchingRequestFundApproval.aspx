<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FundSwitchingRequestFundApproval.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.FundSwitchingRequestFundApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4">
                    <span class="text-muted fw-light">Transaction - </span>Fund Switching Request Amount 
                </h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <div class="mb-3 row">
                                <div class="col-md-10">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" class="btn btn-primary" Text="Save" />
                                    <asp:Button ID="btnValidate" runat="server" OnClick="btnValidate_Click" class="btn btn-primary" Text="Validate" />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-4 mb-2">
                                    <asp:GridView ID="GV_LIST" OnRowCommand="GV_LIST_RowCommand" AutoGenerateColumns="false" AllowPaging="true" PageIndex="0" PageSize="5" runat="server">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <HeaderTemplate>
                                                    <asp:Button ID="BT_ALL" runat="server" Text="Select All" Width="104px" CommandName="ALL"></asp:Button>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CB" runat="server" Text="Approve?"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Cert. No" HeaderText="Cert. No">
                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Date Of Request" HeaderText="Date Of Request">
                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Batch" HeaderText="Batch">
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="PASAR UANG" HeaderText="PASAR UANG"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="PENDAPATAN TETAP" HeaderText="PENDAPATAN TETAP"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="SAHAM"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="SYARIAH"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="US DOLLAR"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="KONDUR"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="EMOI"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="vico"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="STAR ENERGI"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="cnooc"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="PREMIER OIL"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="ENI BUKAT"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="CHEVRON"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="MANDIRI II"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="MAGMA"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="PETROCINA"></asp:BoundField>
                                            <asp:BoundField Visible="False" DataField="NON FUND"></asp:BoundField>
                                            <asp:BoundField DataField="AMOUNT">
                                                <HeaderStyle Width="50%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <asp:GridView ID="GV_Detail" AutoGenerateColumns="false" AllowPaging="true" PageIndex="0" PageSize="5" runat="server">
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
