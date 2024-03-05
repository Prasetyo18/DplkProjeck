<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientFundInfo.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.ClientTabs.ClientFundInfo" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlCerNmbr" class="col-md-2 col-form-label">Certificate Code</label>
        <div class="col-mb-4">
            <asp:DropDownList ID="ddlCerNmbr" class="form-select" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtClientNm" class="col-md-2 col-form-label">Client Name</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtClientNm" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtAsOfDate" class="col-md-2 col-form-label">As of Date</label>
        <div class="col-mb-4">
            <asp:TextBox ID="txtAsOfDate" class="form-control" runat="server" type="date"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlTypeOutput" class="col-md-2 col-form-label" ></label>
        <div class="col-mb-4">
            <asp:DropDownList ID="ddlTypeOutput" class="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeOutput_SelectedIndexChanged">
                <asp:ListItem Text="TOTAL" Value="0"></asp:ListItem>
                <asp:ListItem Text="PER MONEY TYPE" Value="1"></asp:ListItem>
                <asp:ListItem Text="PER INVESTASI" Value="2"></asp:ListItem>
                <asp:ListItem Text="PER MONEY TYPE PER INVESTASI" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnInsertFund" runat="server" class="btn btn-primary" Text="Insert" />
        </div>
    </div>
    <div class="mb-3 row">
        <asp:Label id="LbTypeOutput" class="col-md-2 col-form-label" runat="server"></asp:Label>
    </div>
    <div class="mb-3 row">
        <asp:GridView OnRowCommand="GridviewFund_RowCommand" CssClass="gridview-table" Width="100%" Font-Size="Small" OnPageIndexChanging="GridviewFund_PageIndexChanging" runat="server" ID="GridviewFund" AllowPaging="true" PageIndex="0" PageSize="10">
            <Columns>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <div class="mb-3">
                            <asp:Button ID="btnEdit" CommandName="EditRow" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn btn-primary btn-sm btn-edit" runat="server" Text="EDIT" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
        </asp:GridView>
    </div>
</div>
