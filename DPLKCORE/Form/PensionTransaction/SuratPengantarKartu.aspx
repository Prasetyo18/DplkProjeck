<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SuratPengantarKartu.aspx.cs" Inherits="DPLKCORE.Form.PensionTransaction.SuratPengantarKartu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="container-xxl flex-grow-1 container-p-y">
            <h4 class="fw-bold py-3 mb-4"><span class="text-muted fw-light">Pension  Transaction/ </span>Surat Pengantar Kartu </h4>

            <div class="tab-content">
                <div class="tab-pane fade show active" id="navs-pills-top-home" role="tabpanel">
                    <div class="card-body">

                        <div class="mb-3 row">

                            <label for="txtCertificate" class="col-md-2 col-form-label">Certificate Number:</label>
                            <div class="col-md-4">

                                <asp:TextBox ID="txtCompanyNm" runat="server" class="form-control" /><br />                                         <asp:Button ID="buttonSearch" runat="server" class="btn btn-primary" Text="Get Client" />

                         
                            </div>
                        </div>


                        <div class="mb-3 row">

                            <label for="txtClientName" class="col-md-2 col-form-label">Client Name:</label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtClientName" runat="server" class="form-control" /><br />
                            </div>
                        </div>
                        <%--        <label for="ddlClientAddress">Client Address:</label>
                        <asp:DropDownList ID="ddlClientAddress" runat="server">
                        </asp:DropDownList><br />--%>

                        <div class="mb-3 row">
                            <label for="ddlClientAddress" class="col-md-2 col-form-label">Client Address:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlClientAddress" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>


               <%--         <label for="ddlPIC">PIC DPLK:</label>
                        <asp:DropDownList ID="ddlPIC" runat="server">
                        </asp:DropDownList><br />--%>

                        <div class="mb-3 row">
                            <label for="ddlPIC" class="col-md-2 col-form-label">PIC DPLK:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlPIC" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>

<%--                        <label for="ddlUsul">Usul:</label>
                        <asp:DropDownList ID="ddlUsul" runat="server">
                        </asp:DropDownList><br />--%>
                        <div class="mb-3 row">
                            <label for="ddlUsul" class="col-md-2 col-form-label">DDL Usul :</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlUsul" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
<%--                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" />--%>
                                <div class="mb-3 row">
                                    <div class="col-md-10 offset-md-2">
                                        <asp:Button ID="btnSimpan" runat="server" class="btn btn-success" Text="Simpan" />
                                    </div>
                                </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
