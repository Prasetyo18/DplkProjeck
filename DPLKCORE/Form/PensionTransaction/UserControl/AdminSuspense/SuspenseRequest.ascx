<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuspenseRequest.ascx.cs" Inherits="DPLKCORE.Form.PensionTransaction.UserControl.AdminSuspense.SuspenseRequest" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="TXT_SUSPNNO" class="col-md-2 col-form-label">SUSPENSE NUMBER</label>
        <div class="col-md-4">
            <asp:TextBox ID="TXT_SUSPNNO" CssClass="form-control" runat="server" Enabled="False" AutoPostBack="True"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="TXT_SUSPNAMT" class="col-md-2 col-form-label">SUSPENSE AMOUNT</label>
        <div class="col-md-4">
            <asp:TextBox ID="TXT_SUSPNAMT" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="TXT_SUSPNDESC" class="col-md-2 col-form-label">SUSPENSE DESCRIPTION</label>
        <div class="col-md-4">
            <asp:TextBox ID="TXT_SUSPNDESC" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="DDL_SUSPNTYPE" class="col-md-2 col-form-label">SUSPENSE TYPE</label>
        <div class="col-md-4">
            <asp:DropDownList ID="DDL_SUSPNTYPE" CssClass="form-select" runat="server" AutoPostBack="True"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="TXT_RECEIVEDT" class="col-md-2 col-form-label">RECEIVED DATE</label>
        <div class="col-md-4">
            <asp:TextBox ID="TXT_RECEIVEDT" CssClass="form-control" type="date" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="" class="col-md-2 col-form-label">GROUP NUMBER</label>
        <div class="col-4">
            <asp:DropDownList ID="DDL_GROUP" CssClass="form-select" OnSelectedIndexChanged="DDL_GROUP_SelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="DDL_PAYCENTER" class="col-md-2 col-form-label">PAYCENTER</label>
        <div class="col-md-4">
            <asp:DropDownList ID="DDL_PAYCENTER" CssClass="form-select" runat="server" AutoPostBack="True"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="TxtRef_Bank" class="col-md-2 col-form-label">REFERENCE BANK</label>
        <div class="col-md-4">
            <asp:TextBox ID="TxtRef_Bank" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-4">
            <asp:Button ID="BTN_SUBMIT" runat="server" OnClick="BTN_SUBMIT_Click" CssClass="btn btn-primary" Text="SUBMIT"></asp:Button>
        </div>
    </div>
    <hr />
    <div class="mb-3 row" style="overflow-x:auto">
        <asp:GridView ID="GVRequest" CssClass="gridview-table" Font-Size="X-Small" OnPageIndexChanging="GVRequest_PageIndexChanging" AllowPaging="true" PageSize="5" PageIndex="0" OnRowCommand="GVRequest_RowCommand" runat="server">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="BTN_EDIT" CommandName="EditRow" Text="EDIT" CssClass="btn btn-sm btn-edit" CommandArgument="<%#Container.DataItemIndex%>" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="suspn_nmbr" HeaderText="Suspense No.">
                    <HeaderStyle Width="5%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="suspn_amt" HeaderText="Suspense Amount" DataFormatString="{0:N2}">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="suspn_desc1" HeaderText="Bank Description">
                    <HeaderStyle Width="20%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="suspn_type_nm" HeaderText="Suspense Type">
                    <HeaderStyle Width="15%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="received_dt" HeaderText="Received Date">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="group_nmbr" HeaderText="Group No.">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="company_nm" HeaderText="Company">
                    <HeaderStyle Width="20%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="paycenter_nm" HeaderText="Paycenter">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Ref_Bank" HeaderText="Ref Bank"></asp:BoundField>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5"/>
        </asp:GridView>
    </div>
</div>
