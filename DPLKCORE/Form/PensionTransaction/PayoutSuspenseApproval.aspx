<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayoutSuspenseApproval.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.PayoutSuspenseApproval" %>

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
                                <div class="col" style="overflow-x: auto">
                                    <asp:GridView AutoGenerateColumns="false" ID="DGR_SUSPENSE_APPROVAL" CssClass="gridview-table" AllowPaging="true" PageIndex="0" PageSize="5" runat="server"
                                        OnPageIndexChanging="DGR_SUSPENSE_APPROVAL_PageIndexChanging" OnRowCommand="DGR_SUSPENSE_APPROVAL_RowCommand" Font-Size="X-Small">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_APPROVE" CssClass="btn btn-primary btn-sm" CommandArgument="<%#Container.DataItemIndex%>" runat="server" CommandName="ApproveData" Text="Approved"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="seq_nmbr" HeaderText="ID">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="regisid" HeaderText="Claim Register ID">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cer_nmbr" HeaderText="Cert. No">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="client_nm" HeaderText="Participant Name">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="bank_nm" HeaderText="Bank Address">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="bank_address" HeaderText="Branch">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="acct_nmbr" HeaderText="Acct. No">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="acct_nm" HeaderText="Acct. Name">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="amount" HeaderText="Gross Amount" DataFormatString="{0:N2}">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="check_amt" HeaderText="Cheque Charge" DataFormatString="{0:N2}">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Net_amt" HeaderText="Transfer Amount" DataFormatString="{0:N2}">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Suspense_nmbr" HeaderText="Suspn. ID">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Prepare_by" HeaderText="Prepare By">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_DEL" CssClass="btn btn-danger btn-sm"  CommandArgument="<%#Container.DataItemIndex%>" runat="server" Text="Delete" CommandName="DeleteData"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
