<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientClaimHistorical.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.ClientTabs.ClientClaimHistorical" %>
<div class="card-body">
    <div class="mb-3-row">
        <label for="ddlCertifNmbr">Certificate Number</label>
        <div class="col-mb-4">
            <asp:DropDownList ID="ddlCertifNmbr" class="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCertifNmbr_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3-row">
        <label for="txtClientNm">Participant Name</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtClientNm" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3-row">
        <label for="txtGroupNmbr">Group Number</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtGroupNmbr" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3-row">
        <label for="txtCompanyNm">Company Name</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtCompanyNm" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <hr />
    <div class="mb-3 row">
        <div class="col-6">
            <label for="GvAvailableClaim" class="col-md col-form-label">Available Claim</label>
            <div class="col-md">
                <asp:GridView OnPageIndexChanged="GvAvailableClaim_PageIndexChanged" CssClass="gridview-table" Width="100%" Font-Size="XX-Small" runat="server" ID="GvAvailableClaim" AllowPaging="true" PageIndex="0" PageSize="10">
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                </asp:GridView>
            </div>
        </div>
        <div class="col-6">
            <label for="GvClaimHistorical" class="col-md col-form-label">Claim Historical</label>
            <div class="col-md">
                <asp:GridView OnPageIndexChanging="GvClaimHistorical_PageIndexChanged" OnRowCommand="GvClaimHistorical_RowCommand" OnSelectedIndexChanged="GvClaimHistorical_SelectedIndexChanged" CssClass="gridview-table" Width="100%" Font-Size="XX-Small" runat="server" ID="GvClaimHistorical" AllowPaging="true" PageIndex="0" PageSize="10">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="mb-3">
                                    <asp:Button ID="btnDetail" CommandName="View" CommandArgument="<%#Container.DataItemIndex %>" runat="server"/>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <hr />
    <div class="mb-3 row">
        <div class="col-6">
            <label for="" class="col-md col-form-label">Detail Claim</label>
            <div class="col-md-4">
                <div class="mb-3-row">
                    <label for="txtFLP">FLP</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtFLP" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtCDV">CDV</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtCDV" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <hr />

                <div class="mb-3-row">
                    <label for="txtGrossAmt">Gross Ammount</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtGrossAmt" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtTaxAmt">Tax Ammount</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtTaxAmt" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtFeeAmt">Fee Ammount</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtFeeAmt" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtNetAmt">Net Ammount</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtNetAmt" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <hr />

                <div class="mb-3-row">
                    <label for="txtLumpsumAmt">Lumpsum Ammount</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtLumpsumAmt" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtBankNm">Bank Name</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtBankNm" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtAccountNmbr">Account Number</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtAccountNmbr" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtAccountNm">Account Name</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtAccountNm" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtAnnuityAmt">Annuity Ammount</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtAnnuityAmt" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtBankNm2">Bank Name 2</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtBankNm2" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtAccountNmbr2">Account Number 2</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtAccountNmbr2" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3-row">
                    <label for="txtAccountNm2">Account Name</label>
                    <div class="col-mb-4">
                        <asp:TextBox ID="txtAccountNm2" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-6">
            <label for="GvDetailHistoryClaimStat" class="col-md col-form-label">Detail Historical Claim Status</label>
            <div class="col-md-4">
                <asp:GridView OnPageIndexChanging="GvDetailHistoryClaimStat_PageIndexChanged" CssClass="gridview-table" Width="100%" Font-Size="Small" runat="server" ID="GvDetailHistoryClaimStat" AllowPaging="true" PageIndex="0" PageSize="10">
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                </asp:GridView>
            </div>
        </div>
    </div>

</div>
