<%@ Page Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="CertificateMovement.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.CertificateMovement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="SearchPanel" TagPrefix="uc" Src="~/Form/UserControl/SearchPanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4">
                    <span class="text-muted fw-light">Transaction - </span>Certificate Movement Process 
                </h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <div class="mb-3 row">
                                <label for="TxtCertificate">Certificate Number</label>
                                <div class="col-4 mb-2">
                                    <asp:TextBox ID="TxtCertificate" runat="server" class="form-control" AutoPostBack="True"></asp:TextBox>
                                </div>
                                <div class="col-1 mb-2">
                                    <asp:Button ID="BTN_GO" runat="server" OnClick="BTN_GO_Click" class="btn btn-primary" Text="Go"></asp:Button>
                                </div>
                                <div class="col-1 mb-2">
                                    <asp:Button ID="BTN_SEARCH" runat="server" OnClick="BTN_SEARCH_Click" CssClass="btn btn-primary" Text="..."></asp:Button>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="LBL_CLIENT_NM">Client Name</label>
                                <div class="col-4 mb-2">
                                    <asp:Label ID="LBL_CLIENT_NM" runat="server" class="form-label"></asp:Label>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_EFCTV_DT">Effective Date</label>
                                <div class="col-4 mb-2">
                                    <asp:TextBox ID="TXT_EFCTV_DT" class="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="TXT_GROUP_OLD">Original Group</label>
                                <div class="col-4 mb-2">
                                    <asp:TextBox ID="TXT_GROUP_OLD" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-4 mb-2">
                                    <asp:Label ID="LBL_COMPANY_OLD" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="DDL_COMPANY_NEW">Destination Group</label>
                                <div class="col-4 mb-2">
                                    <asp:DropDownList ID="DDL_COMPANY_NEW" class="form-select" OnSelectedIndexChanged="DDL_COMPANY_NEW_SelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <hr />
                            <div class="mb-3 row">
                                <label for="">Investment Direction Change</label>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-6 mb-2">
                                    <label for="">Current Investment</label>
                                    <div class="col mb-2" style="overflow-x: auto">
                                        <asp:GridView ID="DGR_INV_SRC" Font-Size="XX-Small" class="gridview-table" AutoGenerateColumns="true" OnPageIndexChanging="DGR_INV_SRC_PageIndexChanging" PageIndex="0" PageSize="5" AllowPaging="true" runat="server">

                                            <PagerSettings FirstPageText="First" LastPageText="Last" Position="Bottom" PageButtonCount="5" Mode="NumericFirstLast" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-6 mb-2">
                                    <label for="">Available Investment</label>
                                    <div class="row">
                                        <div class="col mb-2" style="overflow-x: auto">
                                            <asp:GridView ID="DGR_INV_DST" Font-Size="X-Small" class="gridview-table" AutoGenerateColumns="false" PageIndex="0" PageSize="5" AllowPaging="true" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="Investment" HeaderText="Investment">
                                                        <HeaderStyle Width="30%"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MaxPct" HeaderText="Max %">
                                                        <HeaderStyle Width="10%"></HeaderStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="ER %">
                                                        <HeaderStyle Width="15%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_ER" runat="server" Width="70%"></asp:TextBox>&nbsp;%
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EE %">
                                                        <HeaderStyle Width="15%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_EE" runat="server" Width="70%"></asp:TextBox>&nbsp;%
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TU %">
                                                        <HeaderStyle Width="15%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_TU" runat="server" Width="70%"></asp:TextBox>%
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FT%">
                                                        <HeaderStyle Width="15%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_FT" runat="server" Width="70%"></asp:TextBox>%
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="inv_type_nmbr"></asp:BoundField>
                                                </Columns>
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Position="Bottom" PageButtonCount="5" Mode="NumericFirstLast" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="mb-3 row">
                                        <div class="col mb-2">
                                            <asp:Button ID="BT_VALIDATE" class="btn btn-primary btn-sm" OnClick="BT_VALIDATE_Click" runat="server" Text="Validate"></asp:Button>
                                            <asp:Button ID="BTN_SAVE" class="btn btn-primary btn-sm" OnClick="BTN_SAVE_Click" runat="server" Text="Save"></asp:Button>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <hr />
                            <div class="mb-3 row">
                                <label for="">Fund Switching</label>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-6 mb-2">
                                    <div class="col mb-2" style="overflow-x: auto">
                                        <asp:GridView ID="DGR_FUND" Font-Size="XX-Small" class="gridview-table" AutoGenerateColumns="false" PageIndex="0" PageSize="5" AllowPaging="true" runat="server">
                                            <Columns>
                                                <asp:BoundField DataField="inv_type_nm" HeaderText="Source Fund">
                                                    <HeaderStyle Width="30%"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="acct_val" HeaderText="Asset" DataFormatString="{0:n2}">
                                                    <HeaderStyle Width="30%"></HeaderStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Position="Bottom" PageButtonCount="5" Mode="NumericFirstLast" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-6 mb-2">
                                    <div class="mb-3 row">
                                        <label for="TXT_BATCHID">Batch ID</label>
                                        <div class="col-6 mb-2">
                                            <asp:TextBox ID="TXT_BATCHID" class="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-3 mb-2">
                                            <asp:Button ID="BTN_NEW" class="form-control btn btn-primary btn-sm" OnClick="BTN_NEW_Click" runat="server" Text="New Batch"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="mb-3 row">
                                        <label for="DDL_SRC_FUND">Source Fund</label>
                                        <div class="col-6 mb-2">
                                            <asp:DropDownList ID="DDL_SRC_FUND" class="form-select" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="mb-3 row">
                                        <label for="DDL_DST_FUND">Destination Fund</label>
                                        <div class="col-6 mb-2">
                                            <asp:DropDownList ID="DDL_DST_FUND" class="form-select" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="mb-3 row">
                                        <label for="DDL_METHOD">Switching Method</label>
                                        <div class="col-6 mb-2">
                                            <asp:DropDownList ID="DDL_METHOD" class="form-select" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="mb-3 row">
                                        <label for="TXT_SWITCH_VALUE">Switching Value</label>
                                        <div class="col-6 mb-2">
                                            <asp:TextBox ID="TXT_SWITCH_VALUE" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="mb-3 row">
                                        <label for="TXT_SWITCH_AMT">Switching Ammount</label>
                                        <div class="col-6 mb-2">
                                            <asp:TextBox ID="TXT_SWITCH_AMT" class="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="mb-3 row">
                                        <div class="col-4 mb-2">
                                            <asp:Button ID="BTN_CALC" class="btn btn-primary" OnClick="BTN_CALC_Click" runat="server" Text="Calculation"></asp:Button>
                                        </div>
                                        <div class="col-4 mb-2">
                                            <asp:Button ID="BTN_SAVESWITHC" class="btn btn-primary" OnClick="BTN_SAVESWITHC_Click" runat="server" Text="Save Switching"></asp:Button>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <hr>
                            <div class="mb-3 row">
                                <label for="">Fund Switching Estimation</label>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-4 mb-2" style="overflow-x: auto">
                                    <asp:GridView ID="DGR_ESTIMATION" OnRowCommand="DGR_ESTIMATION_RowCommand" class="gridview-table" AutoGenerateColumns="false" PageIndex="0" PageSize="5" AllowPaging="true" runat="server">
                                        <Columns>
                                            <asp:BoundField DataField="inv_type_src" HeaderText="Fund Source">
                                                <HeaderStyle Width="25%"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="inv_type_dst" HeaderText="Fund Destination">
                                                <HeaderStyle Width="25%"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="amount" HeaderText="Fund Switching Amount" DataFormatString="{0:n2}">
                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_CANCEL" runat="server" Text="Cancel" CommandName="Cancel" CommandArgument="<%#Container.DataItemIndex %>"></asp:Button>
                                                    <asp:Button ID="BT_APPRV" runat="server" Text="Approved" CommandName="APPRV" CommandArgument="<%#Container.DataItemIndex %>"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Position="Bottom" PageButtonCount="5" Mode="NumericFirstLast" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <asp:ModalPopupExtender ID="searchModal" TargetControlID="BTN_SEARCH" BackgroundCssClass="modalBackground" PopupControlID="searchPanel" runat="server">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="searchPanel" CssClass="myPanel" runat="server">
                                <uc:SearchPanel ID="UCSearchPanel" runat="server" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
