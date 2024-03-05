<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FundMovement_MoneyType.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.FundMovement_MoneyType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="SearchPanel" TagPrefix="uc" Src="~/Form/UserControl/SearchPanel.ascx" %>
<%@ Register TagName="FundSwitching" TagPrefix="uc" Src="~/Form/PensionTransaction/UserControl/FundSwitching_MoneyType.ascx" %>
<%@ Register TagName="CertificateList" TagPrefix="uc" Src="~/Form/PensionTransaction/UserControl/FundSwitching_CertificateList.ascx" %>
<%@ Register TagName="GroupList" TagPrefix="uc" Src="~/Form/PensionTransaction/UserControl/FundSwitching_GroupList_MoneyType.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>New Business - Group Info</h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <div class="mb-3 row">
                                <div class="col">
                                    <asp:Button ID="btnFundMovement" OnClick="btnFundMovement_Click" Text="Fund Movement" CssClass="btn btn-primary" runat="server" />
                                </div>
                                <div class="col">
                                    <asp:Button ID="btnCertificateList" OnClick="btnCertificateList_Click" Text="Certificate List" CssClass="btn btn-primary" runat="server" />
                                </div>
                                <div class="col">
                                    <asp:Button ID="btnGroupList" OnClick="btnGroupList_Click" Text="Group List" CssClass="btn btn-primary" runat="server" />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col">
                                    <asp:Label ID="lbTitle" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <asp:TabContainer ID="FundMovementTabs" runat="server">
                                    <asp:TabPanel ID="FundSwitchingTab" runat="server">
                                        <ContentTemplate>
                                            <uc:FundSwitching ID="UCFundSwitching" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="CertificateListTab" runat="server">
                                        <ContentTemplate>
                                            <uc:CertificateList ID="UCCertificateList" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="GroupListTab" runat="server">
                                        <ContentTemplate>
                                            <uc:GroupList ID="UCGroupList" runat="server"></uc:GroupList>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
