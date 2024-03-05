<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="DPLKCORE.Form.Pension.Company" %>

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
                                <label for="txtCompanyNm" class="col-md-2 col-form-label">Company Name:</label>
                                <div class="col-md-4">

                                    <asp:TextBox ID="txtCompanyNm" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="DropDownListHasPay" class="col-md-2 col-form-label">Has PayCenter:</label>
                                <div class="col-md-1">
                                    <asp:DropDownList ID="DropDownListHasPay" runat="server" class="form-select" style="width: auto;">
                                        <asp:ListItem Text="Yes" Value="0" />
                                        <asp:ListItem Text="No" Value="1" />
                                    </asp:DropDownList><br />
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="txtNpwp" class="col-md-2 col-form-label">NPWP:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtNpwp" runat="server" class="form-control" /><br />
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="ddlBusinessLine" class="col-md-2 col-form-label">Business Line:</label>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlBusinessLine" runat="server" class="form-select">
                                    </asp:DropDownList><br />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="txtContactPerson" class="col-md-2 col-form-label">Contact Person:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtContactPerson" runat="server" class="form-control" /><br />
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="txtSiup" class="col-md-2 col-form-label">SIUP:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSiup" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="ddlMnySrcType" class="col-md-2 col-form-label">Money Source Type:</label>
                                <div class="col-md-4">

                                    <asp:DropDownList ID="ddlMnySrcType" runat="server" class="form-select">
                                    </asp:DropDownList><br />
                                </div>
                            </div>

                            <div class="mb-3 row">
                                <label for="txtPayorNm" class="col-md-2 col-form-label">Payor Name:</label>
                                <div class="col-md-4">

                                    <asp:TextBox ID="txtPayorNm" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                            <div class="mb-3 row">

                                <label for="txtBankNm" class="col-md-2 col-form-label">Bank Name:</label>
                                <div class="col-md-4">

                                    <asp:TextBox ID="txtBankNm" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <label for="txtAccountNmbr" class="col-md-2 col-form-label">Account Number:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtAccountNmbr" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                            <div class="mb-3 row">

                                <label for="txtAccountNm" class="col-md-2 col-form-label">Account Name:</label>
                                <div class="col-md-4">

                                    <asp:TextBox ID="txtAccountNm" runat="server" class="form-control" /><br />
                                </div>
                            </div>


                            <div class="mb-3 row">

                                <label for="txtEmail" class="col-md-2 col-form-label">Email:</label>
                                <div class="col-md-4">

                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                            <div class="mb-3 row">

                                <label for="txtAdArt" class="col-md-2 col-form-label">Ad Art:</label>
                                <div class="col-md-4">

                                    <asp:TextBox ID="txtAdArt" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                            <div class="mb-3 row">

                                <label for="DropDownListPdp" class="col-md-2 col-form-label">PDP Flag:</label>
                                <div class="col-md-1">

                                    <asp:DropDownList ID="DropDownListPdp" runat="server" class="form-select" style="width: auto">
                                        <asp:ListItem Text="Yes" Value="0" />
                                        <asp:ListItem Text="No" Value="1" />
                                    </asp:DropDownList><br />
                                </div>
                            </div>
                            <div class="mb-3 row">

                                <label for="txtOldClientNmbr" class="col-md-2 col-form-label">Old Client Number:</label>
                                <div class="col-md-4">

                                    <asp:TextBox ID="txtOldClientNmbr" runat="server" class="form-control" /><br />
                                </div>
                            </div>
                            <div class="mb-3 row">
                                <div class="col-md-10 offset-md-2">
                                    <asp:Button ID="btnCompanyAdd" runat="server" class="btn btn-primary" Text="Insert Company" />
                                </div>
                            </div>

<%--                            <asp:Button ID="btnCompanyAdd" runat="server" Text="Insert Company" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
