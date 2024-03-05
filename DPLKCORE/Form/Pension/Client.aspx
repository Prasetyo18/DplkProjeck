<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="DPLKCORE.Form.Pension.Client" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="CertificateInfo" TagPrefix="uc" Src="~/Form/Pension/UserControl/ClientTabs/ClientCertificateInfo.ascx" %>
<%@ Register TagName="FundInfo" TagPrefix="uc" Src="~/Form/Pension/UserControl/ClientTabs/ClientFundInfo.ascx" %>
<%@ Register TagName="StatAndSalary" TagPrefix="uc" Src="~/Form/Pension/UserControl/ClientTabs/StatusAndSalary.ascx" %>
<%@ Register TagName="Transaction" TagPrefix="uc" Src="~/Form/Pension/UserControl/ClientTabs/ClientTransaction.ascx" %>
<%@ Register TagName="ClaimHistorical" TagPrefix="uc" Src="~/Form/Pension/UserControl/ClientTabs/ClientClaimHistorical.ascx" %>


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
                            <div class="mb-3 row">
                                <label for="txtCientNmbr" class="col-md-2 col-form-label">Client Code :</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtClientNmbr" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:TabContainer ID="ClientTabs" runat="server">
                                <asp:TabPanel ID="ClientInfoTab" runat="server">
                                    <HeaderTemplate>
                                        CLIENT INFO
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div class="card-body">
                                            <div class="mb-3 row">
                                            <label for="txtClientNm" class="col-md-2 col-form-label">Client Name:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtClientNm" runat="server" class="form-control" /><br />
                                            </div>
                                        </div>
                                        <div class="mb-3 row">
                                            <label for="ddlIdentity" class="col-md-2 col-form-label">Idientity Type:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlIdentity" runat="server" class="form-select"></asp:DropDownList><br />
                                            </div>
                                        </div>

                                        <div class="mb-3 row">
                                            <label for="txtIdentity" class="col-md-2 col-form-label">Identity Number:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtIdentity" runat="server" class="form-control"></asp:TextBox><br />
                                            </div>
                                        </div>

                                        <div class="mb-3 row">
                                            <label for="ddlGender" class="col-md-2 col-form-label">Gender:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlGender" runat="server" class="form-select">
                                                    <asp:ListItem Text="Male" Value="M" />
                                                    <asp:ListItem Text="Female" Value="F" />
                                                </asp:DropDownList><br />
                                            </div>
                                        </div>

                                        <div class="mb-3 row">
                                            <label for="txtbirthDate" class="col-md-2 col-form-label">Birth Date:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtbirthDate" runat="server" class="form-control"></asp:TextBox><br />
                                            </div>
                                        </div>

                                        <div class="mb-3 row">
                                            <label for="txtPlace" class="col-md-2 col-form-label">Place of Birth:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPlace" runat="server" class="form-control"></asp:TextBox><br />
                                            </div>
                                        </div>

                                        <div class="mb-3 row">
                                            <label for="ddlMartialStatus" class="col-md-2 col-form-label">Martial Status:</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlMartialStatus" runat="server" class="form-select"></asp:DropDownList><br />
                                            </div>
                                        </div>

                                        <div class="mb-3 row">
                                            <label for="txtMaidenName" class="col-md-2 col-form-label">Maiden Name:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtMaidenName" runat="server" class="form-control"></asp:TextBox><br />
                                            </div>
                                        </div>

                                        <div class="mb-3 row">
                                            <label for="txtEmail" class="col-md-2 col-form-label">Email:</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox><br />
                                            </div>
                                        </div>

                                        <div class="mb-3 row">
                                            <label for="txtAdArt" class="col-md-2 col-form-label">NPWP:</label>
                                            <div class="col-md-4">
                                                <asp:FileUpload ID="fuNPWP" runat="server" /><br />
                                            </div>
                                        </div>
                                        <div class="mb-3 row">
                                            <div class="col-md-10 offset-md-2">
                                                <asp:Button ID="btnClientAdd" runat="server" class="btn btn-primary" Text="Insert" />
                                            </div>
                                            <div class="col-md-10 offset-md-2">
                                                <asp:Button ID="btnClientUpdate" runat="server" class="btn btn-primary" Text="Update" />
                                            </div>
                                        </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="CertificateInfoTab" runat="server">
                                    <HeaderTemplate>
                                        CERTIFICATE INFO
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:CertificateInfo ID="UCCertificateInfo" runat="server" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="FundInfoTab" runat="server">
                                    <HeaderTemplate>
                                        FUNDINFO
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:FundInfo ID="UCFundInfo" runat="server" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="StatandSalaryTab" runat="server">
                                    <HeaderTemplate>
                                        STATUS AND SALARY
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:StatAndSalary ID="UCStatAndSalary" runat="server"/>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TransactionTab" runat="server">
                                    <HeaderTemplate>
                                        TRANSACTION
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:Transaction ID="UCTransaction" runat="server"/>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="ClaimHistoricalTab" runat="server">
                                    <HeaderTemplate>
                                        CLAIM HISTORICAL
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:ClaimHistorical ID="UCClaimHistorical" runat="server"/>
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                            <div class="card-body">
                                <div class="col-md-10">
                                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" Text="Back" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
