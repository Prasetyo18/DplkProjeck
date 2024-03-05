<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupInvestmentDirection.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.GroupTabs.GroupInvestmentDirection" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlInvestOption" class="col-md-2 col-form-label">Investment Option :</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlInvestOption" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlPaymentRes" class="col-md-2 col-form-label">Charge Payment Responsibility</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlPaymentRes" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlChargeFreq" class="col-md-2 col-form-label">Charge Frequency:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlChargeFreq" runat="server" class="form-select">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtNextChargeDt" class="col-md-2 col-form-label">Next Charge Date:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtNextChargeDt" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtBilled" class="col-md-2 col-form-label">Billed (%):</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtBilled" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtDeducated" class="col-md-2 col-form-label">Deducted (%):</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtDeducated" runat="server" class="form-control" /><br />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtChargeRate" class="col-md-2 col-form-label">Charge Rate (%):</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtChargeRate" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtChargeAmt" class="col-md-2 col-form-label">Charge Amount:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtChargeAmt" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtMaxInvChsdPerc" class="col-md-2 col-form-label">Maximum Investment Choosed Percentage:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtMaxInvChsdPerc" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtMinInvChsdPerc" class="col-md-2 col-form-label">Minimum Investment Choosed Percentage:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtMinInvChsdPerc" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnInvest" class="btn btn-primary" runat="server" OnClick="btnInvest_Click" Text="Insert" />
        </div>
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnInvestUpdate" class="btn btn-primary" runat="server" OnClick="btnInvestUpdate_Click" Text="Update" />
        </div>
    </div>

</div>
