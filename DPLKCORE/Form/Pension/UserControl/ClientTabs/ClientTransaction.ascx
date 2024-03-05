<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientTransaction.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.ClientTabs.ClientTransaction" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlCertifNmbr">Certificate Code</label>
        <div class="col-mb-4">
            <asp:DropDownList ID="ddlCertifNmbr" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtClientNm">Certificate Name</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtClientNm" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <%--btn go--%>
    <div class="mb-3 row">
        <div class="col-mb-4">
            <asp:Button ID="btnGo" class="btn btn-primary" OnClick="btnGo_Click" Text="GO" runat="server"/>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtGroupNmbr">Group Number</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtGroupNmbr" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtCompanyNm">Company Name</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtCompanyNm" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtTransacDtStart">Start Date</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtTransacDtStart" class="form-control" type="date" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtTransacDtEnd">End Date</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtTransacDtEnd" class="form-control" type="date" runat="server"></asp:TextBox>
        </div>
    </div>
    <%--btn submit--%>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2"">
            <asp:Button ID="btnSubmit" class="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" runat="server"/>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-8" style="overflow-x:auto">
            <label for="" class="col-md-12 col-form-label">Transaction Summary</label>
            <div class="col-md-4">
                <asp:GridView OnRowCommand="GvTransacSummary_RowCommand" CssClass="gridview-table" Width="100%" Font-Size="XX-Small" OnPageIndexChanging="GvTransacSummary_PageIndexChanging" runat="server" ID="GvTransacSummary" AllowPaging="true" PageIndex="0" PageSize="10">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="mb-3">
                                    <asp:Button ID="btnDetail" CommandName="Detail" Text="Detail" CommandArgument="<%#Container.DataItemIndex %>" runat="server"/>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                </asp:GridView>
            </div>
        </div>
        <div class="col-4">
            <label for="" class="col-md-12 col-form-label">Transaction Detail</label>
            <div class="col-md-4">
                <asp:GridView CssClass="gridview-table" Width="100%" Font-Size="Small" OnPageIndexChanging="GvTransacDetail_PageIndexChanging" runat="server" ID="GvTransacDetail" AllowPaging="true" PageIndex="0" PageSize="10">
                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
