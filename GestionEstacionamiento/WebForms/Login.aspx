<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="GestionEstacionamiento.WebForms.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center mt-5">
        <div class="col-md-4">
            <div class="card shadow-lg p-4">
                <h3 class="text-center mb-4">Iniciar sesión</h3>

                <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
                
                <div class="form-group mb-3">
                    <asp:Label runat="server" Text="Usuario:" AssociatedControlID="txtUser" CssClass="form-label" />
                    <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group mb-3">
                    <asp:Label runat="server" Text="Contraseña:" AssociatedControlID="txtPass" CssClass="form-label" />
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" />
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                    <asp:Button ID="btnNuevoUsuario" runat="server" Text="Crear nuevo usuario" CssClass="btn btn-secondary" PostBackUrl="~/WebForms/NuevoUsuario.aspx" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
