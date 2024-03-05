<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EntryPayrollContribution.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.EntryPayrollContribution" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="SearchPanel" TagPrefix="uc" Src="~/Form/UserControl/SearchPanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>Entry - Payroll Contribution</h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <div class="mb-3 row">
                                <label for="txtCerNum" class="col-md-2 col-form-label">Certificate Number :</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtCerNum" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Button ID="btnSearch" runat="server" class="btn btn-primary" Text="Search" />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="txtPeriod" class="col-md-2 col-form-label">Billing Period  :</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPeriod" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-md-10 offset-md-2">
                                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" class="btn btn-primary" Text="Add" />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-md" style="overflow-x:auto">
                                    <asp:GridView ID="GvEntryPayroll" OnRowCommand="GvEntryPayroll_RowCommand"   AutoGenerateColumns="false" CssClass="gridview-table" Font-Size="X-Small" runat="server" AllowPaging="true" PageIndex="0" PageSize="5">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="BTN_DELETE" CommandName="DeleteData" CommandArgument="<%#Container.DataItemIndex%>" runat="server" Text="Delete"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="group_nmbr" HeaderText="Group Number">
                                                <HeaderStyle Width="50%" Font-Names="arial" Font-Size="8pt"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cer_nmbr" HeaderText="Certificate Number">
                                                <HeaderStyle Width="50%" Font-Names="arial" Font-Size="8pt"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="client_nm" HeaderText="Client Name">
                                                <HeaderStyle Width="50%" Font-Names="arial" Font-Size="8pt"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="birth_dt" HeaderText="DOB">
                                                <HeaderStyle Width="50%" Font-Names="arial" Font-Size="8pt"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="employee_nmbr" HeaderText="Employee Number">
                                                <HeaderStyle Width="50%" Font-Names="arial" Font-Size="8pt"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_ER" runat="server" CssClass="NumberBox" Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Contribution ER
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_EE" runat="server" CssClass="NumberBox" Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Contribution EE
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_TU" runat="server" CssClass="NumberBox" Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Top Up
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_FT" runat="server" CssClass="NumberBox" Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Dana Pengalihan
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_BULAN" runat="server" CssClass="NumberBox" Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Periode Bulan
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_TAHUN" runat="server" CssClass="NumberBox" Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Periode Tahun
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Position="Bottom" PageButtonCount="5" Mode="NextPreviousFirstLast" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-md-10 offset-md-2">
                                    <asp:Button ID="btnProcces" OnClick="btnProcces_Click" runat="server" class="btn btn-primary" Text="Process" />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-md">
                                    <asp:GridView ID="GvSTA" AutoGenerateColumns="false" CssClass="gridview-table" Font-Size="X-Small" runat="server" AllowPaging="true" PageIndex="0" PageSize="5">
                                        <Columns>
                                            <asp:BoundField DataField="cer_nmbr" HeaderText="Certificate Number">
                                                <HeaderStyle Width="5cm"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="client_nm" HeaderText="Client Name"></asp:BoundField>
                                            <asp:BoundField DataField="sMessage" HeaderText="Error Message"></asp:BoundField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Position="Bottom" PageButtonCount="5" Mode="NextPreviousFirstLast" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <asp:ModalPopupExtender ID="searchModal" TargetControlID="btnSearch" BackgroundCssClass="modalBackground" PopupControlID="searchPanel" runat="server">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="searchPanel" CssClass="myPanel" runat="server">
                                <uc:SearchPanel ID="UCSearchPanel" runat="server"/>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
