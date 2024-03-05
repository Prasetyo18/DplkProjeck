<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FundSwitching.ascx.cs" Inherits="DPLKCORE.Form.PensionTransaction.UserControl.FundSwitching" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCSearchPanel" TagPrefix="uc" Src="~/Form/UserControl/SearchPanel.ascx" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="txtBatchId" class="form-label">Batch ID</label>
        <div class="col-4 mb-2">
            <asp:TextBox ID="txtBatchId" class="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-4 mb-2">
            <asp:Button ID="btnNewBatch" CssClass="btn btn-primary" OnClick="btnNewBatch_Click" Text="New" runat="server" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtCerNmbr" class="form-label">Certificate Number</label>
        <div class="col-4 mb-2">
            <asp:TextBox ID="txtCerNmbr" class="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-4 mb-2">
            <asp:Button ID="btnGo" CssClass="btn btn-primary" OnClick="btnGo_Click" Text="Go" runat="server" />
            <asp:Button ID="btnSearch" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" runat="server" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtClientNm" class="form-label">Client Name</label>
        <div class="col-4 mb-2">
            <asp:TextBox ID="txtClientNm" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtTransactionDt" class="form-label">Transaction Date</label>
        <div class="col-4 mb-2">
            <asp:TextBox ID="txtTransactionDt" class="form-control" type="date" Enabled="false" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label>Current Fund</label>
    </div>
    <div class="mb-3 row">
        <div class="col-4 mb-2">
            <asp:GridView ID="GvFund" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:BoundField DataField="inv_type_nm" HeaderText="Fund Name" />
                    <asp:BoundField DataField="acct_val" HeaderText="Asset" DataFormatString="{0:n2}" />
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="5" />
            </asp:GridView>
        </div>
    </div>
    <div class="mb-3 row">
        <label>Fund Switching Estimation</label>
    </div>
    <div class="mb-3 row">
        <asp:GridView ID="GvEst" AutoGenerateColumns="false" runat="server">
            <Columns>
                <asp:BoundField DataField="inv_type_src" HeaderText="Fund Source"></asp:BoundField>
                <asp:BoundField DataField="inv_type_dst" HeaderText="Fund Destination"></asp:BoundField>
                <asp:BoundField DataField="amount" HeaderText="Fund Switching Amount" DataFormatString="{0:n2}"></asp:BoundField>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="5" />
        </asp:GridView>
    </div>
    <div class="mb-3 row">
        <label for="ddlSourceFund" class="form-label">Source Fund</label>
        <div class="col-4 mb-2">
            <asp:DropDownList ID="ddlSourceFund" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlDestinationFund" class="form-label">Destination Fund</label>
        <div class="col-4 mb-2">
            <asp:DropDownList ID="ddlDestinationFund" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlSwitchingMethod" class="form-label">Switching Method</label>
        <div class="col-4 mb-2">
            <asp:DropDownList ID="ddlSwitchingMethod" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtSwitchingValue" class="form-label">Switching Value</label>
        <div class="col-4 mb-2">
            <asp:TextBox ID="txtSwitchingValue" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtAmtToProcess" class="form-label">Ammount To Process</label>
        <div class="col-4 mb-2">
            <asp:TextBox ID="txtAmtToProcess" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="" class="form-label"></label>
        <div class="col-4 mb-2">
            <asp:Button ID="btnCalculation" OnClick="btnCalculation_Click" Text="Calculation" class="btn btn-primary" runat="server"/>
            <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" class="btn btn-primary" runat="server"/>
            <%--btn--%>
        </div>
    </div>
    <asp:ModalPopupExtender ID="searchModal" TargetControlID="btnSearch" BackgroundCssClass="modalBackground" PopupControlID="searchPanel" runat="server">
    </asp:ModalPopupExtender>
    <asp:Panel ID="searchPanel" CssClass="myPanel" runat="server">
        <uc:ucsearchpanel id="UCSearchPanel" runat="server"></uc:ucsearchpanel>
    </asp:Panel>

</div>
