<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupBillingStatus.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.GroupTabs.GroupBillingStatus" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="txtNextFeePeriode" class="col-md-2 col-form-label">Next Fee Periode :</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtNextFeePeriode" runat="server" class="form-control" type="date" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtNextFeeGenerateDt" class="col-md-2 col-form-label">Next Fee Generate Date :</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtNextFeeGenerateDt" class="form-control" runat="server" type="date" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlFreq" class="col-md-2 col-form-label">Fee Frequency  :</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlFreq" class="form-select" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtNextContributionPrd" class="col-md-2 col-form-label">Next Contribution Periode :</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtNextContributionPrd" class="form-control" runat="server" type="date" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtNextContributionDt" class="col-md-2 col-form-label">Next Contribution GenerateDate :</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtNextContributionDt" runat="server" class="form-control" type="date" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlConFrq" class="col-md-2 col-form-label">Contribution Frequency :</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlConFrq" runat="server" class="form-select">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnBilling" runat="server" class="btn btn-primary" OnClick="btnBilling_Click" Text="Insert" />
        </div>
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnBillingUpdate" runat="server" class="btn btn-primary" OnClick="btnBIllingUpdate_Click" Text="Update" />
        </div>
    </div>
</div>

<%--hidden controls--%>
<div class="mb-3 row" style="display: none">
    <div class="col-md-4">
        <asp:TextBox ID="txtNextPslPeriod" runat="server" class="form-control" type="date" />
        <asp:TextBox ID="txtNextPslDate" runat="server" class="form-control" type="date" />
        <asp:DropDownList ID="ddlPslFreq" runat="server" class="form-select">
        </asp:DropDownList>
        <asp:TextBox ID="txtPymFreq" runat="server" class="form-control" type="date" />
    </div>
</div>

