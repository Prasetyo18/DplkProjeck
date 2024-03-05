<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyIndex.aspx.cs" Inherits="DPLKCORE.Form.Pension.CompanyIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4">
                    <span class="text-muted fw-light">List / Company</span>
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <asp:ImageButton ID="btnAddCompany" runat="server" CausesValidation="false"
                                ImageUrl="~/Images/Button/Add/Add 48.png" ToolTip="Add User" OnClick="btnAddCompany_Click" />
                            <asp:ImageButton ID="imgbtnRefresh" runat="server" CausesValidation="false"
                                ImageUrl="~/Images/Button/Refresh/Refresh 48.png" ToolTip="Refresh Data" />
                        </div>
                    </div>
                </h4>
                <div style="overflow-x:auto;"">
                    <asp:GridView ID="GridViewCompanyIndex" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" PageIndex="0" PageSize="5" OnPageIndexChanging="GridViewCompanyIndex_PageIndexChanging"
                        CssClass="table table-bordered table-striped">
                        <Columns>
                            <asp:BoundField HeaderText="Company Name" DataField="CompanyNm" />
                            <asp:BoundField HeaderText="Has Paycenter" DataField="HasPaycenter" />
                            <asp:BoundField HeaderText="NPWP" DataField="Npwp" />
                            <asp:BoundField HeaderText="Business Line" DataField="BusinessLineNmbr" />
                            <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                            <asp:BoundField HeaderText="SIUP" DataField="Siup" />
                            <asp:BoundField HeaderText="Money Type" DataField="MnySrcType" />
                            <asp:BoundField HeaderText="Payor Name" DataField="PayorNm" />
                            <asp:BoundField HeaderText="Bank Name" DataField="BankNm" />
                            <asp:BoundField HeaderText="Account Number" DataField="AccountNmbr" />
                            <asp:BoundField HeaderText="Account Name" DataField="AccountNm" />
                            <asp:BoundField HeaderText="Email" DataField="Email" />
                            <asp:BoundField HeaderText="AdArt" DataField="AdArt" />
                            <asp:BoundField HeaderText="PdpFlg" DataField="PdpFlg" />
                            <asp:BoundField HeaderText="OldClientNmbr" DataField="OldClientNmbr" />
                        </Columns>
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
