<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="DPLKCORE.Form.Pension.Group" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register TagName="GroupCharge" TagPrefix="uc" Src="UserControl/GroupTabs/GroupCharge.ascx"%>
<%@ Register TagName="MCP" TagPrefix="uc" Src="UserControl/GroupTabs/GroupMCP.ascx" %>
<%@ Register TagName="InvestDir" TagPrefix="uc" Src="UserControl/GroupTabs/GroupInvestmentDirection.ascx" %>
<%@ Register TagName="BillingStat" TagPrefix="uc" Src="UserControl/GroupTabs/GroupBillingStatus.ascx" %>
<%@ Register TagName="Benefit" TagPrefix="uc" Src="UserControl/GroupTabs/GroupBenefit.ascx" %>
<%@ Register TagName="Pic" TagPrefix="uc" Src="UserControl/GroupTabs/GroupPIC.ascx" %>


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
                                <label for="ddlCompany" class="col-md-2 col-form-label">Company Name:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlCompany" runat="server" class="form-select">
                                    </asp:DropDownList><br />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="txtGroupNumber" class="col-md-2 col-form-label">Group Number :</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtGroupNumber" class="form-control" runat="server" /><br />
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:TabContainer ID="GroupTabs" runat="server" OnActiveTabChanged="GroupTabs_ActiveTabChanged">
                                <asp:TabPanel ID="GroupInfoTab" runat="server">
                                    <HeaderTemplate>
                                        GROUP INFO
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div class="card-body">
                                            <div class="mb-3 row">
                                                <label runat="server" id="lbOldGroup" for="txtOldGrbNmbr" class="col-md-2 col-form-label">Old Group Number:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtOldGrbNmbr" class="form-control" runat="server" />
                                                </div>
                                            </div>
                                            <div class="mb-3 row">
                                                <label for="ddlProductType" class="col-md-2 col-form-label">Product Type:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlProductType" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged" class="form-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlAllow" class="col-md-2 col-form-label">Allow Withdraw Type:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlAllow" class="form-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlWdSrc" class="col-md-2 col-form-label">Withdraw Source:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlWdSrc" class="form-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlCry" class="col-md-2 col-form-label">Currency:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlCry" runat="server" class="form-select"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlOperation" class="col-md-2 col-form-label">Operation Administrator:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlOperation" class="form-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                            <hr />
                                            <br />

                                            <%--separator--%>
                                            <div class="mb-3 row">
                                                <label for="txtMinAnnurity" class="col-md-2 col-form-label">Minimum Annuity Percentage:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtMinAnnurity" class="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txtMinAnnurityAmt" class="col-md-2 col-form-label">Minimum Annuity Amount:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtMinAnnurityAmt" class="form-select" runat="server" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txtAnualMax" class="col-md-2 col-form-label">Annual Max Withdrawal Frequency:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtAnualMax" class="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txtMinWithAmt" class="col-md-2 col-form-label">Min Withdrawal Amount:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtMinWithAmt" runat="server" class="form-control" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txtMaxWithdrawalPer" class="col-md-2 col-form-label">Max Withdrawal Percentage:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtMaxWithdrawalPer" class="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txtMinYrforwith" class="col-md-2 col-form-label">Min Year for W/D after Participant:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtMinYrforwith" class="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlUU" class="col-md-2 col-form-label">Support UU Benefit 92:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlUU" runat="server" class="form-select">
                                                        <asp:ListItem Text="Yes" Value="0" />
                                                        <asp:ListItem Text="No" Value="1" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <br />
                                            <hr />
                                            <br />
                                            <%--separator--%>


                                            <div class="mb-3 row">
                                                <label for="txtNormalRetire" class="col-md-2 col-form-label">Normal Retire Age:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtNormalRetire" class="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txterlyRetire" class="col-md-2 col-form-label">Early Retire Age:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txterlyRetire" class="form-control" runat="server" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txteftdt" class="col-md-2 col-form-label">Effective Date:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txteftdt" class="form-control" runat="server" type="date" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlPremium" class="col-md-2 col-form-label">Premium Method Type:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlPremium" class="form-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlComision" class="col-md-2 col-form-label">Commission Type:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlComision" runat="server" class="form-select"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlAgentNmbr" class="col-md-2 col-form-label">Agent Name:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlAgentNmbr" class="form-select" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <br />
                                            <hr />
                                            <br />

                                            <%--separator--%>

                                            <div class="mb-3 row">
                                                <label for="ddlAccBalFreq" class="col-md-2 col-form-label">Account Balance Report Frequency:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlAccBalFreq" runat="server" class="form-select"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txtBackdatedEfectiv" class="col-md-2 col-form-label">Backdated Effective:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtBackdatedEfectiv" runat="server" type="date" class="form-select" />
                                                </div>
                                            </div>


                                            <br />
                                            <hr />
                                            <br />
                                            <%--separator--%>

                                            <div class="mb-3 row">
                                                <label for="txtSpakRecDt" class="col-md-2 col-form-label">SPAK Received Date:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtSpakRecDt" runat="server" class="form-control" type="date" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlGroupt" class="col-md-2 col-form-label">Group Type:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlGroupt" runat="server" class="form-select">
                                                        <asp:ListItem Text="Organization" Value="0" />
                                                        <asp:ListItem Text="Individual" Value="1" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlIncContriFlg" class="col-md-2 col-form-label">Fee Include on Contribution:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlIncContriFlg" runat="server" class="form-select">
                                                        <asp:ListItem Text="Include" Value="0" />
                                                        <asp:ListItem Text="Exclude" Value="1" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="txtClaimPrc" class="col-md-2 col-form-label">Claim Processed Day:</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtClaimPrc" runat="server" class="form-control" />
                                                </div>
                                            </div>

                                            <div class="mb-3 row">
                                                <label for="ddlAffliatedTo" class="col-md-2 col-form-label">Affiliated To:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlAffliatedTo" runat="server" class="form-select"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="mb-3 row">
                                                <div class="col-md-10 offset-md-2">
                                                    <asp:Button ID="btnGroup" runat="server" class="btn btn-primary" Text="Insert" />
                                                </div>
                                                <div class="col-md-10 offset-md-2">
                                                    <asp:Button ID="btnUpdateGroupInfo" runat="server" class="btn btn-primary" Text="Update" />
                                                </div>
                                            </div>

                                            <%--hidden controls--%>
                                            <div class="mb-3 row" style="display: none">
                                                <asp:DropDownList ID="ddlMaturityTypeNmbr" class="form-select" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtMaturityNm" class="form-select" runat="server" type="date" />
                                                <asp:DropDownList ID="ddlPsl" class="form-select" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlPlsType" class="form-select" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlAlcFund" class="form-select" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlPslFreq" class="form-select" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>

                                </asp:TabPanel>
                                <asp:TabPanel ID="MemberClassPlan" runat="server">
                                    <HeaderTemplate>
                                        MEMBER CLASS PLAN
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:MCP ID="UCMemberClassPlan" runat="server"></uc:MCP>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="GroupCharge" runat="server">
                                    <HeaderTemplate>
                                        GROUP CHARGE
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:GroupCharge ID="UCGroupCharge" runat="server" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="InvestmentDirection" runat="server">
                                    <HeaderTemplate>
                                        INVESTMENT DIRECTION
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:InvestDir ID="UCInvestDir" runat="server" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="BillingStatus" runat="server">
                                    <HeaderTemplate>
                                        BILLING STATUS
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:BillingStat ID="UCBillingStat" runat="server" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="Benefit" runat="server">
                                    <HeaderTemplate>
                                        BENEFIT
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:Benefit ID="UCBenefit" runat="server" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="Pic" runat="server">
                                    <HeaderTemplate>
                                        PIC
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uc:Pic runat="server" ID="UCPic" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </div>
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
    
</asp:Content>

