<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupMCP.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.GroupTabs.GroupMCP" %>

<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlMCPType" class="col-md-2 col-form-label">MCP Type: </label>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlMCPType" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMCPType_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="mb-3 row">
                <label for="txtCntrbER" class="col-md-6 col-form-label">CNTRB Amount ER: </label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCntrbER" class="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                        ControlToValidate="txtCntrbER"
                        ValidationExpression="\d+(\.\d+)?$"
                        ErrorMessage="Please enter a valid float number."
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txtCntrbEE" class="col-md-6 col-form-label">CNTRB Amount EE: </label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCntrbEE" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txtCntrbTU" class="col-md-6 col-form-label">CNTRB Amount TU: </label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCntrbTU" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txtCntrbFT" class="col-md-6 col-form-label">CNTRB Amount FT: </label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCntrbFT" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="mb-3 row">
                <label for="txtCntrbRateER" class="col-md-6 col-form-label">CNTRB Rate ER: </label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCntrbRateER" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txtCntrbRateEE" class="col-md-6 col-form-label">CNTRB Rate EE: </label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCntrbRateEE" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txtCntrbRateTU" class="col-md-6 col-form-label">CNTRB Rate TU: </label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCntrbRateTU" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="mb-3 row">
                <label for="txtCntrbRateFT" class="col-md-6 col-form-label">CNTRB Rate FT: </label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtCntrbRateFT" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnMCPSave" OnClick="btnMCPSave_Click" runat="server" class="btn btn-primary" Text="Insert" />
        </div>
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnMCPUpdate" OnClick="btnMCPUpdate_Click" runat="server" class="btn btn-primary" Text="Update" />
        </div>
    </div>
</div>
