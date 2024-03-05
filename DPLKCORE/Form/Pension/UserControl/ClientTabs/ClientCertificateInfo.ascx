<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientCertificateInfo.ascx.cs" Inherits="DPLKCORE.Form.Pension.UserControl.ClientTabs.ClientCertificateInfo" %>
<%--<asp:Button ID="btnSearch" runat="server" Text="Search" />
    <asp:Button ID="btnGo" runat="server" Text=" Go " />--%>
<div class="card-body">
    <div class="mb-3 row">
        <label for="ddlCerInfo" class="col-md-2 col-form-label">CERTIFICATE CODE:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlCerInfo" AutoPostBack="true" OnSelectedIndexChanged="ddlCerInfo_SelectedIndexChanged" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtOldClient" class="col-md-2 col-form-label">OLD CERF. CODE :</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtOldClient" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtClientName" class="col-md-2 col-form-label">CLIENT NAME-- :</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtClientName" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlGroup" class="col-md-2 col-form-label">GROUP :</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlGroup" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtEmpoyeCode" class="col-md-2 col-form-label">EMPLOYEE CODE:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtEmpoyeCode" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtEmployedDT" class="col-md-2 col-form-label">EMPLOYEEMENT DATE:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtEmployedDT" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtRetireAge" class="col-md-2 col-form-label">RETIREMENT AGE--</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtRetireAge" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlPayC" class="col-md-2 col-form-label">PAYCENTER :</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlPayC" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlPayCenterMethod" class="col-md-2 col-form-label">PAYCENTER  METHOD:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlPayCenterMethod" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlCityz" class="col-md-2 col-form-label">CITIZENSHIP:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlCityz" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtTax" class="col-md-2 col-form-label">TAX ID--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtTax" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlJenisPremi" class="col-md-2 col-form-label">JENIS PREMI:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlJenisPremi" AutoPostBack="true" OnSelectedIndexChanged="ddlJenisPremi_SelectedIndexChanged" runat="server" class="form-select"></asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlJenisRider" class="col-md-2 col-form-label">JENIS RIDER:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlJenisRider" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlJenisRider_SelectedIndexChanged" class="form-select">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlJenisPlan" class="col-md-2 col-form-label">JENIS PLAN:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlJenisPlan" runat="server" class="form-select">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlNamaAgen" class="col-md-2 col-form-label">NAMA AGEN:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlNamaAgen" runat="server" class="form-select">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlNamaKomisi" class="col-md-2 col-form-label">JENIS KOMISI:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlNamaKomisi" runat="server" class="form-select">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlMmbrClasPlan" class="col-md-2 col-form-label">MEMBER CLASS PLAN--:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlMmbrClasPlan" OnSelectedIndexChanged="ddlMmbrClasPlan_SelectedIndexChanged" runat="server" class="form-select">
            </asp:DropDownList>
        </div>
        <div class="col-md-4" style="display: none">
            <asp:TextBox ID="txtJobfct" runat="server"></asp:TextBox>
        </div>

    </div>
    <div class="mb-3 row">
        <label for="txtVirtualAcc" class="col-md-2 col-form-label">VIRTUAL ACCOUNT NUMBER--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtVirtualAcc" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-4">
            <asp:GridView ID="GvCertifMCP" AutoGenerateColumns="false" OnPageIndexChanging="GvCertifMCP_PageIndexChanging" runat="server" CssClass="gridview-table" AllowPaging="true" PageIndex="0" PageSize="10" Font-Size="Small">
                <Columns>
                    <asp:BoundField DataField="mcp_nm" HeaderText="Member Class Plan"></asp:BoundField>
                    <asp:BoundField DataField="money_type_nm" HeaderText="Money Type"></asp:BoundField>
                    <asp:BoundField DataField="cntrb_amt" HeaderText="Contribution Amount"></asp:BoundField>
                    <asp:BoundField DataField="cntrb_rt" HeaderText="Contribution Rate"></asp:BoundField>
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
            </asp:GridView>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlFunds" class="col-md-2 col-form-label">FUND SOURCE--:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlFunds" runat="server" class="form-select">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtAplicantRec" class="col-md-2 col-form-label">APPLICATION RECEIVED--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtAplicantRec" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtAplicantCompplete" class="col-md-2 col-form-label">APPLICATION COMPLETED--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtAplicantCompplete" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtEffectvDt" class="col-md-2 col-form-label">EFFECTIVE DATE--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtEffectvDt" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtCreateDt" class="col-md-2 col-form-label">CREATE DATE--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtCreateDt" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtTerminate" class="col-md-2 col-form-label">TERMINATION DATE--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtTerminate" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtCerDevDt" class="col-md-2 col-form-label">CERTIFICATE DELIVERY DATE--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtCerDevDt" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlFirstPremiumPaid" class="col-md-2 col-form-label">FIRST PREMIUM PAID--:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlFirstPremiumPaid" runat="server" class="form-select">
                <asp:ListItem Text="No" Value="0" />
                <asp:ListItem Text="Yes" Value="1" />
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-md-4" style="display: none">
        <asp:TextBox ID="txtPrintDate" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtMatDate" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtSumInsuredAmmount" runat="server"></asp:TextBox>
    </div>
    <div class="mb-3 row">
        <label for="ddlHaveOtherDplk" class="col-md-2 col-form-label">HAVE OTHER BANK--:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlHaveOtherDplk" runat="server" class="form-select">
                <asp:ListItem Text="No" Value="0" />
                <asp:ListItem Text="Yes" Value="1" />
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtSumInsuredEfctvDt" class="col-md-2 col-form-label">SUM INSURED EFFECTIVE DATE--:</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtSumInsuredEfctvDt" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="GvSumInsured" class="col-md-2 col-form-label"></label>
        <div class="col-md-4">
            <asp:GridView ID="GvSumInsured" OnRowCommand="GvSumInsured_RowCommand" AutoGenerateColumns="false" OnPageIndexChanging="GvSumInsured_PageIndexChanging" CssClass="gridview-table" AllowPaging="true" PageIndex="0" PageSize="10" Font-Size="Small" runat="server">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BT_EDIT" runat="server" Font-Size="XX-Small" Font-Names="verdana" Text="Edit"
                                CommandName="BT_EDIT" CommandArgument="<%#Container.DataItemIndex%>"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="bene_type_nm" HeaderText="SI Type"></asp:BoundField>
                    <asp:BoundField Visible="False" DataField="sum_insured" DataFormatString="{0:N2}"></asp:BoundField>
                    <asp:TemplateField HeaderText="SI Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_SI_AMT" Text='<%# Bind("sum_insured", "{0:N2}") %>' Enabled="false" runat="server" Font-Names="verdana" Font-Size="XX-Small"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BT_SAVE" runat="server" CommandArgument="<%#Container.DataItemIndex%>" Visible="false" Font-Size="XX-Small" Font-Names="verdana" Text="Save"
                                CommandName="BT_SAVE"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
            </asp:GridView>
        </div>
    </div>

    <hr />

    <div class="mb-3 row">
        <label for="ddlStatus" class="col-md-2 col-form-label">STATUS:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlStatus" runat="server" class="form-select">
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtStatusEfctvDt" class="col-md-2 col-form-label">STATUS EFFECTIVE DATE</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtStatusEfctvDt" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtSalaryDate" class="col-md-2 col-form-label">SALARY EFFECTIVE DATE</label>
        <div class="col-md-4">
            <asp:TextBox ID="txtSalaryDate" runat="server" type="date" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtSalaryAmt" class="col-md-2 col-form-label">SALARY AMOUNT  </label>
        <div class="col-md-4">
            <asp:TextBox ID="txtSalaryAmt" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="txtBrac" class="col-md-2 col-form-label">BRANCH </label>
        <div class="col-md-4">
            <asp:TextBox ID="txtBrac" runat="server" class="form-control" />
        </div>
    </div>
    <div class="mb-3 row">
        <label for="ddlApp" class="col-md-2 col-form-label">APU PPT:</label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlApp" runat="server" class="form-select">
                <asp:ListItem Text="No" Value="0" />
                <asp:ListItem Text="Yes" Value="1" />
            </asp:DropDownList>
        </div>
    </div>
    <div class="mb-3 row">
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnCertifAdd" runat="server" class="btn btn-primary" Text="Insert" />
        </div>
        <div class="col-md-10 offset-md-2">
            <asp:Button ID="btnCertifUpdate" runat="server" class="btn btn-primary" Text="Update" />
        </div>
    </div>
    <hr />
</div>

<div class="card-body">
    <h5>Investment Direction</h5>

    <div class="col-6">
        <div class="mb-3 row">
            <label for="efftdt" class="col-md-4 col-form-label">EFFECTIVE DATE--</label>
            <div class="col-md-8">
                <asp:TextBox ID="efftdt" runat="server" type="date" class="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="ddlIvstNm" class="col-md-4 col-form-label">INVESTMENT NAME--:</label>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlIvstNm" runat="server" class="form-select"></asp:DropDownList>
            </div>
        </div>
        <div class="mb-3 row">
            <label for="ddlMoneytp" class="col-md-4 col-form-label">MONEY TYPE--:</label>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlMoneytp" runat="server" class="form-select"></asp:DropDownList>
            </div>
        </div>
        <div class="mb-3 row">
            <label for="pdc" class="col-md-4 col-form-label">PERCENTAGE</label>
            <div class="col-md-8">
                <asp:TextBox ID="pdc" runat="server" class="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <div class="col-md-6 offset-md-6">
                <asp:Button ID="btnAddInvDrct" Text="Insert" class="btn btn-primary" runat="server" />
            </div>
            <%--            <div class="col-md-6 offset-md-6">
                <asp:Button ID="btnEditInvDrct" Text="Update" class="btn btn-primary" runat="server" />
            </div>--%>
        </div>
    </div>
    <div class="col-6">
        <label for="GvInvDrct" class="col-md-2 col-form-label"></label>
        <div class="col-md-4">
            <asp:GridView OnPageIndexChanging="GvInvDrct_PageIndexChanging" CssClass="gridview-table" Width="100%" Font-Size="Small" runat="server" ID="GvInvDrct" AllowPaging="true" PageIndex="0" PageSize="10">
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
            </asp:GridView>
        </div>
    </div>
</div>
