<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayCenter.aspx.cs" Inherits="DPLKCORE.Form.Pension.PayCenter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .table-striped {}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>List Paycenter</h1>
<%--        <asp:Button ID="btnAddPaycenter" runat="server" Text="Add Paycenter" OnClick="btnAddPaycenter_Click" />--%>
        <td>&nbsp;&nbsp;
          <asp:ImageButton ID="btnAddPaycenter" runat="server" CausesValidation="false"
              ImageUrl="~/Images/Button/Add/Add 48.png" ToolTip="Add PayCanter" OnClick="btnAddPaycenter_Click" />
        </td>
        <div>
            <div>
                <asp:Button ID="btnOpenFilter" runat="server" Text="Search & Filter" OnClientClick="return openFilter()"/>
            </div>
            <asp:GridView ID="GridViewPaycenter" runat="server" AutoGenerateColumns="true" AllowPaging="true" PageSize="10" PageIndex="0"
                CssClass="table table-bordered table-striped" OnPageIndexChanging="GridViewPaycenter_PageIndexChanging" Height="279px">
                <Columns>
                    <asp:BoundField HeaderText="Client Number" DataField="ClientNmbr" />
                    <asp:BoundField HeaderText="Paycenter Number" DataField="PaycenterNmbr" />
                    <asp:BoundField HeaderText="Paycenter Name" DataField="PaycenterNm" />
                    <asp:BoundField HeaderText="Master Paycenter" DataField="MasterPaycenterNmbr" />
                    <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                    <asp:BoundField HeaderText="Last Change" DataField="LastChangeDt" />
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="10" />
            </asp:GridView>
        </div>
    </div>
    <script type="text/javascript">
        function openFilter() {
            var popup = window.open('SearchScreen.aspx?type=5&caller=TXT_ID', 'SearchScreen', 'width=800, height=800, scrollbars=yes');
            return false;
        }
</script>
</asp:Content>


