<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupCharge.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.GroupTabs.GroupCharge" %>


<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlChargeType" class="col-md-2 col-form-label">Charge Type:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlChargeType" OnSelectedIndexChanged="ddlChargeType_SelectedIndexChanged" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtEffecDt" class="col-md-2 col-form-label">Effective Date:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtEffecDt" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlPaymentRes" class="col-md-2 col-form-label" runat="server">Payment Responsibility: </label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlPaymentRes" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlFreq" class="col-md-2 col-form-label">Frequency :</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlFreq" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtNextChgDt" class="col-md-2 col-form-label">Next Charge Date:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtNextChgDt" class="form-control" runat="server" type="date" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtChrgTerDt" class="col-md-2 col-form-label">Charge Termination Date:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtChrgTerDt" class="form-control" runat="server" type="date" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtBilPer" class="col-md-2 col-form-label">Billed Percentage:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtBilPer" class="form-control" runat="server" /><br />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtDeductPer" class="col-md-2 col-form-label">Deduct Percentage:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtDeductPer" class="form-control" runat="server" /><br />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtChargeRate" class="col-md-2 col-form-label">Charge Rate :</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtChargeRate" runat="server" class="form-control"></asp:TextBox>
            <asp:DropDownList ID="ddlChargeRate" class="form-select" AutoPostBack="true" runat="server"></asp:DropDownList>
            <asp:CheckBox ID="ckChargeRate" Text="Use Table" runat="server" AutoPostBack="true" OnCheckedChanged="ckChargeRate_CheckedChanged" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtChargeAmmout" class="col-md-2 col-form-label">Charge Amount:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtChargeAmmout" class="form-control" runat="server" /><br />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtMaxFreqYear" class="col-md-2 col-form-label">Max Freq Per Year</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtMaxFreqYear" class="form-control" runat="server" /><br />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtMaxFeeFreqPerYear" class="col-md-2 col-form-label">
            Max Free Freq Per Year
        </label>
        <div class="col-md-4">
            <asp:TextBox ID="txtMaxFeeFreqPerYear" class="form-control" runat="server" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtMaxChargeAmt" class="col-md-2 col-form-label">
            Max Charge Amount</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtMaxChargeAmt" class="form-control" runat="server" />
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnGroupCharge" OnClick="btnGroupCharge_Click" class="btn btn-primary" runat="server" Text="Insert" />
        </div>
    </div>
    <div class="mb-3 row" style="overflow-x:auto; overflow-y:auto">
        <asp:GridView ID="GV_NEWDATA" class="gridview-table" runat="server" AllowPaging="true" PageIndex="0" PageSize="5" AutoGenerateColumns="true">

        </asp:GridView>
    </div>
    <div class="mb-3 row" style="overflow-x:auto; overflow-y:auto">
        <asp:GridView ID="GV_SUMMARY" class="gridview-table" runat="server" AllowPaging="true" PageIndex="0" PageSize="5" AutoGenerateColumns="true">

        </asp:GridView>
    </div>
</div>
