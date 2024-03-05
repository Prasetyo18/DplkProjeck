<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuspenseReport.ascx.cs" Inherits="DPLKCORE.Form.PensionTransaction.UserControl.AdminSuspense.SuspenseReport" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="TXT_SUSPNNO" class="col-md-2 col-form-label">SUSPENSE NUMBER</label>
        <div class="col-md-4">
            <asp:TextBox ID="TXT_SUSPNNO" CssClass="form-control" runat="server" AutoPostBack="True"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="TXT_GROUPNO" class="col-md-2 col-form-label">GROUP NUMBER</label>
        <div class="col-md-4">
            <asp:TextBox ID="TXT_GROUPNO" CssClass="form-control" runat="server" AutoPostBack="True"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-4 offset-2">
            <asp:Button ID="BTN_SEARCH" OnClick="BTN_SEARCH_Click" CssClass="btn btn-primary btn-sm" runat="server" Text="SEARCH"></asp:Button>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col" style="overflow-x: auto">
            <asp:GridView ID="DGR_REST" CssClass="gridview-table" Font-Size="XX-Small" runat="server" AllowPaging="true" PageIndex="0" PageSize="5" OnPageIndexChanging="DGR_REST_PageIndexChanging" OnRowCommand="DGR_REST_RowCommand">
                <Columns>
                    <asp:BoundField DataField="suspn_nmbr" HeaderText="Suspense No.">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspense_amt" HeaderText="Amount">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspense_use_amt" HeaderText="Use Amount">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="rest_amt" HeaderText="Rest Amount">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspn_desc1" HeaderText="Bank Description">
                        <HeaderStyle Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspn_type_nm" HeaderText="Suspense Type">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="RECEIVED_DATE" HeaderText="Received Date">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="group_nmbr" HeaderText="Group Number">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="company_nm" HeaderText="Company">
                        <HeaderStyle Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="paycenter_nm" HeaderText="Paycenter">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Ref_Bank" HeaderText="Ref Bank"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BT_REPORT" runat="server" CommandName="report" CommandArgument="ViewReport" Text="PRINT REPORT"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom"/>
            </asp:GridView>
        </div>
    </div>
</div>

