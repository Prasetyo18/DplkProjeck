<%@ Page Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="PayoutSuspense.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.PayoutSuspense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCSearchPanel" TagPrefix="uc" Src="~/Form/UserControl/SearchPanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>New Business - Client Info</h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <div class="mb-3 row">
                                <label for="" class="col-md col-form-label">SUSPENSE IDENTIFIED</label>
                            </div>
                            <div class="mb-3 row">
                                <div class="col" style="overflow-x: auto">
                                    <asp:GridView ID="DGR_SUSPENSE_IDENTIFIED" AutoGenerateColumns="false" OnRowCommand="DGR_SUSPENSE_IDENTIFIED_RowCommand" AllowPaging="true"
                                        OnPageIndexChanging="DGR_SUSPENSE_IDENTIFIED_PageIndexChanging"
                                        PageIndex="0" PageSize="5" Font-Size="XX-Small" CssClass="gridview-table" runat="server">
                                        <Columns>
                                            <asp:BoundField DataField="suspn_nmbr" HeaderText="Suspense No.">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="group_nmbr" HeaderText="Group">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="company_nm" HeaderText="Company">
                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="rest_amt" HeaderText="Rest Amount">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" VerticalAlign="Top"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="suspn_desc1" HeaderText="Description">
                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="suspn_type_nm" HeaderText="Suspense Type">
                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="received_date" HeaderText="Received Date">
                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="BTN_RETUR" Text="Retur" CommandName="Retur" CssClass="btn btn-warning btn-sm" CommandArgument="<%#Container.DataItemIndex%>" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="" class="col-md-2 col-form-label">Retur Information</label>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_SEQ" class="col-md-2 col-form-label">Transaction No.</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_SEQ" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_SUSPN" class="col-md-2 col-form-label">Suspense No.</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_SUSPN" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_DESC" class="col-md-2 col-form-label">Suspense Desc</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_DESC" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_AMT" class="col-md-2 col-form-label">Amount To Retur</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_AMT" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="DDL_INV" class="col-md-2 col-form-label">
                                    Investment 
						Source</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="DDL_INV" runat="server" CssClass="mandatory form-select" ReadOnly="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_REGIS" class="col-md-2 col-form-label">Register Number</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_REGIS" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-1">
                                    <asp:Button ID="BT_GO" Text="GO" OnClick="BT_GO_Click" CssClass="btn btn-primary btn-sm" runat="server" />
                                </div>
                                <div class="col-1">
                                    <asp:Button ID="BT_SEARCH" Text="SEARCH" OnClick="BT_SEARCH_Click" CssClass="btn btn-primary btn-sm" runat="server" />
                                    <asp:Button ID="BT_SEARCH_dummy" style="display:none" runat="server"/>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_CHEQUE" class="col-md-2 col-form-label">Cheque Charge Amount</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_CHEQUE" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_ACCTNO" class="col-md-2 col-form-label">Account Number</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_ACCTNO" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_ACCTNM" class="col-md-2 col-form-label">Account Name</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_ACCTNM" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_BANKNM" class="col-md-2 col-form-label">Bank Name</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_BANKNM" runat="server" CssClass="mandatory form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="BT_SEARCH_BANK" Text="SEARCH" OnClick="BT_SEARCH_BANK_Click" CssClass="btn btn-primary btn-sm" runat="server" />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_RETURINFO" class="col-md-2 col-form-label">Retur Information</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TXT_RETURINFO" runat="server" CssClass="mandatory form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-md-4 offset-2">
                                    <asp:Button ID="BT_SAVE" Text="SAVE" OnClick="BT_SAVE_Click" CssClass="btn btn-primary btn-sm" runat="server" />
                                </div>
                            </div>
                            <asp:ModalPopupExtender ID="searchModal" TargetControlID="BT_SEARCH_dummy" BackgroundCssClass="modalBackground" PopupControlID="searchPanel" runat="server">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="searchPanel" CssClass="myPanel" runat="server">
                                <uc:UCSearchPanel ID="UCSearchPanel" runat="server"></uc:UCSearchPanel>
                            </asp:Panel>
                            <div id="myModal" class="modal">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <span class="close">&times;</span>
                                    </div>
                                    <div class="modal-body">
                                        <p id="modalMessage">Modal Message</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        // JavaScript function to display modal with message
        function showModal(message) {
            var modal = document.getElementById("myModal");
            var modalMessage = document.getElementById("modalMessage");
            modalMessage.innerHTML = message;
            modal.style.display = "block";
        }

        // JavaScript function to hide modal
        function closeModal() {
            var modal = document.getElementById("myModal");
            modal.style.display = "none";
        }

        // Close the modal when the user clicks on the close button
        document.querySelector(".close").addEventListener("click", function () {
            closeModal();
        });

        // Close the modal when the user clicks anywhere outside of it
        window.onclick = function (event) {
            var modal = document.getElementById("myModal");
            if (event.target == modal) {
                closeModal();
            }
        }
    </script>
</asp:Content>
