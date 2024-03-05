<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatusAndSalary.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.ClientTabs.StatusAndSalary" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlCertifNmbr" class="col-md-2 col-form-label">Certificate Code</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlCertifNmbr" class="form-select" runat="server"></asp:DropDownList>
            <asp:Button ID="btnGo" class="btn btn-primary" Text="Go" runat="server"/>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtClientNm" class="col-md-2 col-form-label">Client Name</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtClientNm" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtGroupNmbr" class="col-md-2 col-form-label">Group Number</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtGroupNmbr" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtCompanyNm" class="col-md-2 col-form-label">Company Name</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtCompanyNm" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <hr />
    <div class="mb-3 row">
        <div class="col-6">
            <label for="" class="col-md-2 col-form-label">Status History</label>
            <div class="col-md-4">
                <asp:GridView CssClass="gridview-table" Width="100%" Font-Size="Small" OnPageIndexChanging="GridviewStatHistory_PageIndexChanging" runat="server" ID="GridviewStatHistory" AllowPaging="true" PageIndex="0" PageSize="10">
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                </asp:GridView>
            </div>
        </div>
        <div class="col-6">
            <label for="" class="col-md-2 col-form-label">Salary History</label>
            <div class="col-md-4">
                <asp:GridView CssClass="gridview-table" Width="100%" Font-Size="Small" OnPageIndexChanging="GridviewSalaryHistory_PageIndexChanging" runat="server" ID="GridviewSalaryHistory" AllowPaging="true" PageIndex="0" PageSize="10">
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
