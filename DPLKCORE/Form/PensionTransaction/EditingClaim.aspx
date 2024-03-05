<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditingClaim.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.EditingClaim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCSearchPanel" TagPrefix="uc" Src="~/Form/UserControl/SearchPanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid px-4">
        <div class="card-header">
            <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>Transaction - Editing Claim</h4>
            <div class="tab-content">
                <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                    <div class="card-body">
                        <div class="mb-3 row">
                            <div class="col-md" style="overflow-x: auto">
                                <asp:GridView ID="GvClaim" OnRowCommand="GvClaim_RowCommand" OnPageIndexChanging="GvClaim_PageIndexChanging" CssClass="gridview-table" AllowPaging="true" PageIndex="0" PageSize="10" Font-Size="XX-small" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <div class="mb-3">
                                                    <asp:Button ID="btnEdit" CommandName="RowEditing" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn btn-primary btn-sm btn-edit" runat="server" Text="EDIT" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="register" HeaderText="Register" Visible="false" SortExpression="Register" />
                                        <asp:BoundField DataField="tfer_type_nmbr" HeaderText="Transfer Type Number" Visible="true" SortExpression="TferTypeNumber" />
                                        <asp:BoundField DataField="trns_seq_nmbr" HeaderText="Transaction Sequence Number" Visible="true" SortExpression="TrnsSeqNumber" />
                                        <asp:BoundField DataField="cer_nmbr" HeaderText="Certificate Number" SortExpression="CerNumber" />
                                        <asp:BoundField DataField="client_nm" HeaderText="Client Name" SortExpression="ClientName" />
                                        <asp:BoundField DataField="company_nm" HeaderText="Company Name" SortExpression="CompanyName" />
                                        <asp:BoundField DataField="tfer_type_nm" HeaderText="Transfer Type Name" SortExpression="TferTypeName" />
                                        <asp:BoundField DataField="tfer_amt" HeaderText="Transfer Amount" SortExpression="TferAmount" />
                                        <asp:BoundField DataField="bank_central_nm" HeaderText="Bank Central Name" SortExpression="BankCentralName" />
                                        <asp:BoundField DataField="bank_addr" HeaderText="Bank Address" SortExpression="BankAddress" />
                                        <asp:BoundField DataField="acct_nmbr" HeaderText="Account Number" SortExpression="AccountNumber" />
                                        <asp:BoundField DataField="acct_nm" HeaderText="Account Name" SortExpression="AccountName" />
                                        <asp:BoundField DataField="kode_bank" HeaderText="Kode Bank" SortExpression="KodeBank" />
                                        <asp:BoundField DataField="BATCH_ID" HeaderText="Batch ID" SortExpression="BatchId" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <div class="mb-3">
                                                    <asp:Button ID="btnDelete" CommandName="RowDelete" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn btn-primary btn-sm btn-danger" runat="server" Text="DELETE" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                                </asp:GridView>
                            </div>
                        </div>

                        <%--hidden controls--%>
                        <div class="form-group" style="display: none">
                            <label for="txtSeqNmbr" class="col-form-label">txtSeqNmbr</label>
                            <asp:TextBox ID="txtSeqNmbr" Enabled="false" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group" style="display: none">
                            <label for="txtTferType" class="col-form-label">txtTferType</label>
                            <asp:TextBox ID="txtTferType" Enabled="false" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                        <%--text controls--%>
                        <div class="form-group">
                            <label for="txtCer" class="col-form-label">Participant Number :</label>
                            <asp:TextBox ID="txtCer" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="txtClient" class="col-form-label">Participant Name :</label>
                            <asp:TextBox ID="txtClient" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="txtCompany" class="col-form-label">Company :</label>
                            <asp:TextBox ID="txtCompany" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="txtType" class="col-form-label">Transfer Type:</label>
                            <asp:TextBox ID="txtType" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="txtAmt" class="col-form-label">Transfer Ammount:</label>
                            <asp:TextBox ID="txtAmt" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="txtBank" class="col-form-label">Bank Name :</label>
                            <div class="row">
                                <div class="col-11">
                                    <asp:TextBox ID="txtBank" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1">
                                    <asp:Button ID="btnsearch" OnClick="btnsearch_Click" CssClass="btn btn-primary btn-edit" Text="...." runat="server" />
                                </div>
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label for="txtAccNmbr" class="col-form-label">Account No  :</label>
                            <asp:TextBox ID="txtAccNmbr" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label for="txtAccNm" class="col-form-label">Account Name :</label>
                            <asp:TextBox ID="txtAccNm" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div>
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" class="btn btn-primary" Text="Save"  runat="server"/>
                            </div>
                        </div>
                        <asp:ModalPopupExtender ID="searchModal" TargetControlID="btnsearch" BackgroundCssClass="modalBackground" PopupControlID="searchPanel" runat="server">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="searchPanel" CssClass="myPanel" runat="server">
                            <uc:UCSearchPanel ID="UCSearchPanel" runat="server"></uc:UCSearchPanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
