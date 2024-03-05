<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayCenterAdd.aspx.cs" Inherits="DPLKCORE.Form.Pension.PayCenterAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <div class="card my-4">
            <div class="card-header">
                <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension / </span>New Business - Group Info</h4>
                <div class="tab-content">
                    <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                        <div class="card-body">
                            <div class="mb-3 row">
                                <label for="txtClientNmbr" class="col-md-2 col-form-label">Pay Center Name:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPaycenterNm" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <!-- Move this closing div tag to the end -->
                            <div class="mb-3 row">
                                <label for="ddlPayName" class="col-md-2 col-form-label">Company Name:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlPayName" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPayName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="ddlMaster" class="col-md-2 col-form-label">Master Paycenter Number:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlMaster" runat="server" class="form-select">
                                        <asp:ListItem Text="None" Value="0" />
                                        <asp:ListItem Text="1" Value="1" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="txtContactPerson" class="col-md-2 col-form-label">Contact Person:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtContactPerson" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <div class="col-md-10 offset-md-2">
                                    <asp:Button ID="btnInsertPaycenter" runat="server" class="btn btn-primary" Text="Insert Paycenter" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
