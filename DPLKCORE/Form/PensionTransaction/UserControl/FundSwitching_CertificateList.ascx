<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FundSwitching_CertificateList.ascx.cs" Inherits="DPLKCORE.Form.PensionTransaction.UserControl.FundSwitching_CertificateList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="card-body">
    <div class="mb-3 row">
        <label for="GV_LST" class="form-label">List of Fund Switching Unapporved</label>
        <label for="DDL_TRNSDT" class="form-label">Transaction Date</label>
        <div class="col-4 mb-2">
            <asp:DropDownList ID="DDL_TRNSDT" OnSelectedIndexChanged="DDL_TRNSDT_SelectedIndexChanged" class="form-select" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-2 mb-2 d-flex justify-content-center">
            <asp:Button ID="BTN_REFRESH" CssClass="btn btn-primary btn-sm" OnClick="BTN_REFRESH_Click" Text="Refresh" runat="server" />
        </div>
        <div class="col-md-2 mb-2 d-flex justify-content-center">
            <asp:Button ID="BTN_APPROVE_ALL" CssClass="btn btn-primary btn-sm" onclick="BTN_APPROVE_ALL_Click" Text="Approve Selected" runat="server" />
        </div>
    </div>
    <div class="mb-3 row">

        <div class="col-4 mb-2">
            <asp:GridView ID="GV_LST" OnRowCommand="GV_LST_RowCommand" class="gridview-table" AllowPaging="true" OnPageIndexChanging="GV_LST_PageIndexChanging" PageIndex="0" PageSize="10" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:BoundField DataField="Batch_ID" HeaderText="Batch ID"></asp:BoundField>
                    <asp:BoundField DataField="company" HeaderText="Company Name"></asp:BoundField>
                    <asp:BoundField DataField="cer_nmbr" HeaderText="Cert. No"></asp:BoundField>
                    <asp:BoundField DataField="client_nm" HeaderText="Client Name"></asp:BoundField>
                    <asp:BoundField DataField="efctv_dt" HeaderText="Effective Date"></asp:BoundField>
                    <asp:TemplateField HeaderText="Switching Process">
                        <ItemTemplate>
                            <asp:Button ID="BT_VIEW" runat="server" Width="98px" Text="View" CommandName="View"></asp:Button>
                            <asp:Button ID="BT_DELETE" runat="server" Width="98px" Text="Delete" CommandName="Deleted"></asp:Button>

                            <asp:GridView ID="GV_SWITCH" OnRowCommand="GV_SWITCH_RowCommand" AllowPaging="true" PageIndex="0" PageSize="5" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#3366CC"
                                BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="4">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" Text="DELETE" CommandName="delete" CommandArgument="<%#Container.DataItemIndex%>" runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="src_fund" HeaderText="Source Fund"></asp:BoundField>
                                    <asp:BoundField DataField="dst_fund" HeaderText="Destination Fund"></asp:BoundField>
                                    <asp:BoundField DataField="gross_amt" HeaderText="Amount" DataFormatString="{0:n2}"></asp:BoundField>
                                    <asp:BoundField Visible="False" DataField="cer_nmbr"></asp:BoundField>
                                    <asp:BoundField Visible="False" DataField="batchID"></asp:BoundField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" Position="Bottom" />
                            </asp:GridView>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BT_Apprv" runat="server" Text="Approved" CommandName="APPRV"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Button ID="BT_ALL" runat="server" Width="80px" Text="Select All" Font-Names="Verdana" Font-Size="XX-Small"
                                CommandName="ALL"></asp:Button>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CB" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" Position="Bottom" PageButtonCount="10" />
            </asp:GridView>
        </div>
    </div>
    <div class="mb-3 row">
        <asp:label ID="LB_SUMMARY" for="GV_SUMMARY" class="form-label" runat="server">Summary of Fund Switching Unapproved</asp:label>
        <div class="col-4 mb-2">
            <asp:GridView ID="GV_SUMMARY" AutoGenerateColumns="false" OnPageIndexChanging="GV_SUMMARY_PageIndexChanging" AllowPaging="true" PageIndex="0" PageSize="5" class="gridview-table" runat="server">
                <Columns>
                    <asp:BoundField DataField="src_fund" HeaderText="Fund Source"></asp:BoundField>
                    <asp:BoundField DataField="dst_fund" HeaderText="Destination Fund"></asp:BoundField>
                    <asp:BoundField DataField="gross_amt" HeaderText="Amount" DataFormatString="{0:n2}"></asp:BoundField>
                    <asp:BoundField DataField="batch_id" HeaderText="Batch ID"></asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" PageButtonCount="10" LastPageText="Last" Position="Bottom" />
            </asp:GridView>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="" class="form-label">List of Fund Switching Unprocessed</label>
        <label for="DDL_PROCESS" class="form-label">Transaction Date</label>
        <div class="col-4 mb-2">
            <asp:DropDownList ID="DDL_PROCESS" OnSelectedIndexChanged="DDL_PROCESS_SelectedIndexChanged" class="form-select" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-2 mb-2 d-flex justify-content-center">
            <asp:Button ID="BTN_REFRESH2" CssClass="btn btn-primary btn-sm" OnClick="BTN_REFRESH2_Click" Text="Refresh" runat="server" />
        </div>
        <div class="col-md-2 mb-2 d-flex justify-content-center">
            <asp:Button ID="BTN_PROCESS_ALL" CssClass="btn btn-primary btn-sm" OnClick="BTN_PROCESS_ALL_Click" Text="Process Selected" runat="server" />
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-4 mb-2">
            <asp:GridView ID="GV_PROCESS" OnRowCommand="GV_PROCESS_RowCommand" AutoGenerateColumns="false" OnPageIndexChanging="GV_PROCESS_PageIndexChanging" AllowPaging="true" PageIndex="0" PageSize="10" runat="server">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Button ID="BTN_ALL2" runat="server" Width="80px" Text="Select All" CommandArgument="<%#Container.DataItemIndex%>" Font-Names="Verdana"
                                Font-Size="XX-Small" CommandName="ALL"></asp:Button>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CB2" runat="server" Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BT_PROCESS" runat="server" Width="114px" Text="Process" CommandArgument="<%#Container.DataItemIndex%>" CommandName="PROCESS"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="company_nm" HeaderText="Company Name"></asp:BoundField>
                    <asp:BoundField DataField="Cer_nmbr" HeaderText="Certificate Number"></asp:BoundField>
                    <asp:BoundField DataField="efctv_dt" HeaderText="Swithcing Date" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                    <asp:BoundField DataField="acct_val" HeaderText="Total Swithcing Amount" DataFormatString="{0:n2}"></asp:BoundField>
                    <asp:BoundField DataField="batch_id" HeaderText="Batch ID"></asp:BoundField>
                    <asp:BoundField Visible="False" DataField="approve_flg"></asp:BoundField>
                    <asp:BoundField DataField="status" HeaderText="Status"></asp:BoundField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" PageButtonCount="10" LastPageText="Last" Position="Bottom" />
            </asp:GridView>
        </div>
    </div>
</div>
