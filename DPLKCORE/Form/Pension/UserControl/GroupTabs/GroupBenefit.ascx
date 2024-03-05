<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupBenefit.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.GroupTabs.GroupBenefit" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlMCPType" class="col-md-2 col-form-label">MCP Type: </label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlMCPType" class="form-select" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlBenefitNm" class="col-md-2 col-form-label">Benefit Name: </label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlBenefitNm" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlSumMethod" class="col-md-2 col-form-label">Sum Insured Calc Method: </label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlSumMethod" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtSumAmmount" class="col-md-2 col-form-label">Default Sum Insured Ammount: </label>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtSumAmmount" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtMaxAmmount" class="col-md-2 col-form-label">Max Sum Insured Ammount: </label>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtMaxAmmount" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlDiscCoi" class="col-md-2 col-form-label">Discount Coi: </label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlDiscCoi" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtDiscPct" class="col-md-2 col-form-label">Discount Pct: </label>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtDiscPct" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlLoadCoi" class="col-md-2 col-form-label">Load Coi: </label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlLoadCoi" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtLoadPct" class="col-md-2 col-form-label">Load Pct: </label>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtLoadPct" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlCoiRate" class="col-md-2 col-form-label">Coi Rate: </label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlCoiRate" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtEffectiveDt" class="col-md-2 col-form-label">Effective Date: </label>
        <div class="col-md-4">
            <asp:TextBox type="date" runat="server" ID="txtEffectiveDt" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtMaxEntryAge" class="col-md-2 col-form-label">Max Entry Age: </label>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtMaxEntryAge" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtMaxCovAge" class="col-md-2 col-form-label">Max Coverage Age: </label>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtMaxCovAge" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlClaimTypeAddBenefit" class="col-md-2 col-form-label">Claim Type for Additional Benefit: </label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlClaimTypeAddBenefit" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnSaveBenefit" runat="server" class="btn btn-primary" OnClick="btnSaveBenefit_Click" Text="Insert" />
        </div>
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnUpdateBenefit" runat="server" class="btn btn-primary" OnClick="btnUpdateBenefit_Click" Text="Update" />
        </div>
    </div>
</div>