<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentApproval.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.PaymentApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                                <label for="" class="col-md col-form-label">PAYMENT UNAPPROVE</label>
                            </div>
                            <div class="mb-3 row">
                                <div class="col" style="overflow-x: auto">
                                    <asp:GridView ID="DGR_UNAPPROVED" Font-Size="XX-Small" AutoGenerateColumns="false" OnPageIndexChanging="DGR_UNAPPROVED_PageIndexChanging" AllowPaging="true" PageSize="5" PageIndex="0" OnRowCommand="DGR_UNAPPROVED_RowCommand" CssClass="gridview-table" runat="server">
                                        <Columns>
                                            <asp:BoundField DataField="company_nm" HeaderText="Company Name"></asp:BoundField>
                                            <asp:BoundField DataField="group_nmbr" HeaderText="Group Number"></asp:BoundField>
                                            <asp:BoundField DataField="paycenter_nmbr" HeaderText="Paycenter Number"></asp:BoundField>
                                            <asp:BoundField DataField="efctv_dt" HeaderText="Period" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                                            <asp:BoundField DataField="seq_nmbr" HeaderText="Sequence"></asp:BoundField>
                                            <asp:BoundField DataField="type" HeaderText="Type"></asp:BoundField>
                                            <asp:BoundField DataField="type_description" HeaderText="Type Description"></asp:BoundField>
                                            <asp:BoundField DataField="amt" HeaderText="Amount" DataFormatString="{0:n2}"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="BTN_APPROVE" CssClass="btn btn-primary btn-sm" Text="Approve" CommandName="ApprovePayment" CommandArgument="<%#Container.DataItemIndex%>" runat="server"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="BTN_CANCEL" CssClass="btn btn-primary btn-sm" Text="Cancel" CommandName="CancelPayment" CommandArgument="<%#Container.DataItemIndex%>" runat="server"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom" />
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="" class="col-md col-form-label">PAYMENT APPROVED</label>
                            </div>
                            <div class="mb-3 row">
                                <div class="col" style="overflow-x: auto">
                                    <asp:GridView ID="DGR_APPROVAL" Font-Size="XX-Small" AutoGenerateColumns="false" OnPageIndexChanging="DGR_APPROVAL_PageIndexChanging" AllowPaging="true" PageSize="5" PageIndex="0" OnRowCommand="DGR_APPROVAL_RowCommand" CssClass="gridview-table" runat="server">
                                        <Columns>
                                            <asp:BoundField DataField="company_nm" HeaderText="Company Name"></asp:BoundField>
                                            <asp:BoundField DataField="group_nmbr" HeaderText="Group Number"></asp:BoundField>
                                            <asp:BoundField DataField="paycenter_nmbr" HeaderText="Paycenter Number"></asp:BoundField>
                                            <asp:BoundField DataField="efctv_dt" HeaderText="Period" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                                            <asp:BoundField DataField="seq_nmbr" HeaderText="Sequence"></asp:BoundField>
                                            <asp:BoundField DataField="type" HeaderText="Type"></asp:BoundField>
                                            <asp:BoundField DataField="type_description" HeaderText="Type Description"></asp:BoundField>
                                            <asp:BoundField DataField="amt" HeaderText="Amount" DataFormatString="{0:n2}"></asp:BoundField>
                                            <asp:BoundField DataField="approval_dt" HeaderText="Approval Date" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="BTN_CANCEL_APPROVED" CssClass="btn btn-primary btn-sm" CommandArgument="<%#Container.DataItemIndex%>" runat="server" Text="Cancel" CommandName="cancel"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom" />
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="" class="col-md col-form-label">PAYMENT CANCELED</label>
                            </div>
                            <div class="mb-3 row">
                                <div class="col" style="overflow-x: auto">
                                    <asp:GridView ID="DGR_CANCELED" Font-Size="XX-Small" AutoGenerateColumns="false" OnPageIndexChanging="DGR_CANCELED_PageIndexChanging" AllowPaging="true" PageSize="5" PageIndex="0" CssClass="gridview-table" runat="server">
                                        <Columns>
                                            <asp:BoundField DataField="company_nm" HeaderText="Company Name"></asp:BoundField>
                                            <asp:BoundField DataField="group_nmbr" HeaderText="Group Number"></asp:BoundField>
                                            <asp:BoundField DataField="paycenter_nmbr" HeaderText="Paycenter Number"></asp:BoundField>
                                            <asp:BoundField DataField="efctv_dt" HeaderText="Period" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                                            <asp:BoundField DataField="seq_nmbr" HeaderText="Sequence"></asp:BoundField>
                                            <asp:BoundField DataField="type" HeaderText="Type"></asp:BoundField>
                                            <asp:BoundField DataField="type_description" HeaderText="Type Description"></asp:BoundField>
                                            <asp:BoundField DataField="amt" HeaderText="Amount" DataFormatString="{0:n2}"></asp:BoundField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom" />
                                    </asp:GridView>
                                </div>
                            </div>
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
