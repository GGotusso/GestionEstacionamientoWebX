<%@ Page Title="Verificar Integridad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerificarIntegridad.aspx.cs" Inherits="GestionEstacionamiento.WebForms.VerificarIntegridad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4" style="max-width: 600px;">
        <h3 class="mb-4 text-center text-danger">¡Errores de integridad detectados!</h3>
        <p class="text-center">Elegí una de las siguientes opciones para continuar:</p>

        <!-- NUEVO: mensaje con tablas afectadas -->
        <div class="text-center mb-3">
            <asp:Label ID="lblTablasAfectadas" runat="server" CssClass="text-danger fw-bold" />
        </div>

        <div class="d-flex flex-column gap-2 text-center">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
            <asp:Button ID="btnRecalcular" runat="server" Text="Recalcular dígitos" OnClick="btnRecalcular_Click" CssClass="btn btn-warning" />
            <asp:Button ID="btnRestaurar" runat="server" Text="Restaurar backup" OnClick="btnRestaurar_Click" CssClass="btn btn-danger" />
        </div>

        <div class="mt-3 text-center">
            <asp:Label ID="lblResultado" runat="server" CssClass="text-danger" />
        </div>
    </div>
</asp:Content>

