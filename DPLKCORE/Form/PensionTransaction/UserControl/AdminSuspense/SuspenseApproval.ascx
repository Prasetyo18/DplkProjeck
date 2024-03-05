<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuspenseApproval.ascx.cs" Inherits="DPLKCORE.Form.PensionTransaction.UserControl.AdminSuspense.SuspenseApproval" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="DGR_UNAPPROVED" class="col-md col-form-label">SUSPENSE UNAPPROVED</label>
    </div>
    <div class="mb-3 row">
        <div class="col" style="overflow-x:auto">
            <asp:GridView ID="DGR_UNAPPROVED" OnRowDataBound="DGR_UNAPPROVED_RowDataBound" CssClass="gridview-table" OnRowCommand="DGR_UNAPPROVED_RowCommand" AllowPaging="true" PageIndex="0" PageSize="5" Font-Size="XX-Small" OnPageIndexChanging="DGR_UNAPPROVED_PageIndexChanging" runat="server">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BTN_Approved" CommandName="Approved" CssClass="btn btn-primary btn-sm" CommandArgument="<%#Container.DataItemIndex %>" Text="Approved" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Suspn_nmbr" HeaderText="Suspense No.">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspn_amt" HeaderText="Amount" DataFormatString="{0:N2}">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspn_desc1" HeaderText="Bank Description">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspn_type_nm" HeaderText="Suspense Type">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="received_dt" HeaderText="Received Date">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Group_nmbr" HeaderText="Group No.">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="company_nm" HeaderText="Company">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="paycenter_nm" HeaderText="Paycenter">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Ref_Bank" HeaderText="Ref Bank"></asp:BoundField>
                    <asp:TemplateField HeaderText="PIC Letter">
                        <ItemTemplate>
                            <asp:DropDownList ID="DDL_UP" CssClass="form-select" Font-Size="XX-Small" runat="server" Width="150px"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_PERIHAL" runat="server" CssClass="form-control" Font-Size="XX-Small" TextMode="MultiLine" Width="150px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <asp:DropDownList ID="DDL_ADDRESS" runat="server" CssClass="form-select" Font-Size="XX-Small" Width="250px"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BT_DELETE" runat="server" CssClass="btn btn-primary btn-sm" CommandName="DeleteRow" CommandArgument="<%#Container.DataItemIndex%>" Text="DELETE"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom"/>
            </asp:GridView>
        </div>
    </div>
    <hr />
    <div class="mb-3 row">
        <label for="" class="col-md col-form-label">SUSPENSE APPROVED</label>
    </div>
    <div class="mb-3 row">
        <label for="TXT_SUSPNNO" class="col-md-2 col-form-label">SUSPENSE NUMBER</label>
        <div class="col-md-4">
            <asp:textbox id="TXT_SUSPNNO" CssClass="form-control" runat="server" AutoPostBack="True" Enabled="False"></asp:textbox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="TXT_SUSPNAMT" class="col-md-2 col-form-label">SUSPENSE AMOUNT</label>
        <div class="col-md-4">
            <asp:textbox id="TXT_SUSPNAMT" CssClass="form-control" runat="server"></asp:textbox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="TXT_SUSPNDESC" class="col-md-2 col-form-label">BANK DESCRIPTION</label>
        <div class="col-md-4">
            <asp:textbox id="TXT_SUSPNDESC" CssClass="form-control" runat="server" Enabled="False"></asp:textbox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="TXT_GROUPNO" class="col-md-2 col-form-label">GROUP NUMBER</label>
        <div class="col-md-4">
            <asp:textbox id="TXT_GROUPNO" CssClass="form-control" runat="server" ></asp:textbox>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-4 offset-2">
            <asp:button id="BTN_EDITSUSPEN" OnClick="BTN_EDITSUSPEN_Click" CssClass="btn btn-primary btn-sm" runat="server" Text="SAVE"></asp:button>
        </div>
    </div>

    <hr />
    <div class="mb-3 row">
        <div class="col" style="overflow-x:auto">
            <asp:GridView ID="DGR_APPROVED" OnRowCommand="DGR_APPROVED_RowCommand" AllowPaging="true" PageIndex="0" PageSize="10" Font-Size="XX-Small" OnPageIndexChanging="DGR_APPROVED_PageIndexChanging" CssClass="gridview-table" runat="server">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BT_UPDATE" runat="server" CssClass="btn btn-primary btn-sm" CommandArgument="<%#Container.DataItemIndex%>" CommandName="UpdateRow" Text="UPDATE"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="suspn_nmbr" HeaderText="Suspense No.">
                        <HeaderStyle HorizontalAlign="Left" Width="10%" VerticalAlign="Top"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Suspn_amt" HeaderText="Amount" DataFormatString="{0:N2}">
                        <HeaderStyle Width="10cm"></HeaderStyle>
                        <ItemStyle Font-Size="8pt" Font-Names="Arial" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspn_use_amt" HeaderText="Use Amount" DataFormatString="{0:N2}">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="suspn_desc1" HeaderText="Bank Description">
                        <HeaderStyle Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Suspn_type_nm" HeaderText="Suspense Type">
                        <HeaderStyle Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="received_dt" HeaderText="Received Date">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Group_nmbr" HeaderText="Group Number">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Company_nm" HeaderText="Company">
                        <HeaderStyle Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="paycenter_nm" HeaderText="Paycenter">
                        <HeaderStyle Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Ref_Bank" HeaderText="Ref Bank"></asp:BoundField>
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom"/>
            </asp:GridView>
        </div>
    </div>
    <hr />
    <div class="mb-3 row">
        <label for="DGR_REST" class="col-md col-form-label">SUSPENSE UNUSED</label>
    </div>
    <div class="mb-3 row">
        <div class="col" style="overflow-x:auto">
            <asp:GridView ID="DGR_REST" PageIndex="0" PageSize="5" AllowPaging="true" OnPageIndexChanging="DGR_REST_PageIndexChanging" Font-Size="XX-Small" CssClass="gridview-table" runat="server">
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
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" Position="Bottom"/>
            </asp:GridView>
        </div>
    </div>
</div>

