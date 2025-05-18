<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GestionEstacionamiento.WebForms.NuevoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-5">
        <div class="col-md-4">
            <div class="card shadow-lg p-4">
                <h3 class="text-center mb-4">Crear nuevo usuario</h3>

                <asp:Label ID="lblResultado" runat="server" CssClass="text-success" />

                <div class="form-group mb-3">
                    <asp:Label runat="server" Text="Usuario:" AssociatedControlID="txtUser" CssClass="form-label" />
                    <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group mb-3">
                    <asp:Label runat="server" Text="Contraseña:" AssociatedControlID="txtPass" CssClass="form-label" />
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" />
                </div>

                <div class="form-group mb-4">
                    <asp:Label runat="server" Text="Teléfono:" AssociatedControlID="txtTel" CssClass="form-label" />
                    <asp:TextBox ID="txtTel" runat="server" CssClass="form-control" />
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="btnCrear" runat="server" Text="Crear usuario" OnClick="btnCrear_Click" CssClass="btn btn-success" />
                    <asp:Button ID="btnVolver" runat="server" Text="Volver al login" PostBackUrl="~/WebForms/Login.aspx" CssClass="btn btn-secondary" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
