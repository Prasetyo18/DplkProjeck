<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPanel.ascx.cs" Inherits="DPLKCORE.Form.UserControl.SearchPanel" %>
<div class="form-group">
    <div class="mb-3">
        <asp:Label ID="LbTitle" Visible="true" runat="server" Font-Bold="True"></asp:Label>
        <asp:Label ID="LbType" Visible="false" runat="server"></asp:Label>
        <asp:Label ID="LbCaller" Visible="false" runat="server"></asp:Label>
    </div>
    <div class="mb-3">
        <asp:DataGrid ID="DGSearchCategory" runat="server" AutoGenerateColumns="False" ShowHeader="False" Font-Size="XX-Small">
            <Columns>
                <asp:BoundColumn Visible="False" DataField="fieldname"></asp:BoundColumn>
                <asp:BoundColumn DataField="fieldalias">
                    <ItemStyle Font-Bold="True"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="type"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Action">
                    <HeaderStyle Width="80%"></HeaderStyle>
                    <ItemTemplate>
                        <asp:TextBox ID="TXT" runat="server" Width="400px" Font-Size="XX-Small" Height="17px"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <div class="mb-3">
        <asp:Button ID="btSearch" CssClass="btn btn-primary" runat="server" Width="96px" Text="Search" OnClick="btSearch_Click"></asp:Button>
        <asp:Button ID="btnClose" Text="Close" class="btn btn-secondary" runat="server" />
    </div>
    <div style="overflow-y:scroll">
        <asp:GridView OnRowCommand="DGSearchResult_RowCommand" ID="DGSearchResult" runat="server" Width="100%" Font-Size="Small" AllowPaging="true" PageIndex="0" PageSize="5"
             CellPadding="3" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="#E7E7FF"
            OnPageIndexChanging="DGSearchResult_PageIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="mb-3">
                            <asp:Button ID="btnSelect" CommandName="SelectRow" CommandArgument="<%#Container.DataItemIndex %>" CssClass="btn btn-primary btn-sm btn-edit" runat="server" Text="SELECT" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" />
        </asp:GridView>
    </div>

</div>

