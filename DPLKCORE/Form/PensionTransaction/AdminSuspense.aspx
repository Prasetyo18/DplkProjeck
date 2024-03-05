<%@ Page Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeBehind="AdminSuspense.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.AdminSuspense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="SuspenseRequest" TagPrefix="uc" Src="~/Form/PensionTransaction/UserControl/AdminSuspense/SuspenseRequest.ascx" %>
<%@ Register TagName="SuspenseApproval" TagPrefix="uc" Src="~/Form/PensionTransaction/UserControl/AdminSuspense/SuspenseApproval.ascx" %>
<%@ Register TagName="SuspenseReport" TagPrefix="uc" Src="~/Form/PensionTransaction/UserControl/AdminSuspense/SuspenseReport.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>New Business - Client Info</h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <asp:TabContainer ID="ClientTabs" runat="server">
                                <asp:TabPanel ID="TabPanelSuspenseRequest" runat="server">
                                    <HeaderTemplate>
                                        Suspense Request
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:SuspenseRequest ID="UCSuspenseRequest" runat="server"></uc:SuspenseRequest>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanelSuspenseApproval" runat="server">
                                    <HeaderTemplate>
                                        Suspense Approval
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:SuspenseApproval ID="UCSuspenseApproval" runat="server"/>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanelSuspenseReport" runat="server">
                                    <HeaderTemplate>
                                        Suspense Report
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:SuspenseReport ID="UCSuspenseReport" runat="server"/>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
