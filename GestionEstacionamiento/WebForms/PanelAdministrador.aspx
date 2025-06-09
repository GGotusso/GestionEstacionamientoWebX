<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelAdministrador.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GestionEstacionamiento.WebForms.PanelAdministrador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4" style="max-width: 600px;">
        <h3 class="mb-4 text-center">Panel Administrador</h3>
        <div class="mb-3">
            <asp:Label runat="server" Text="Carpeta de backup" AssociatedControlID="txtRuta" />
            <asp:TextBox ID="txtRuta" runat="server" CssClass="form-control" />
        </div>
        <div class="mb-3">
            <asp:Label runat="server" Text="Archivo para restaurar" AssociatedControlID="fuRestore" />
            <asp:FileUpload ID="fuRestore" runat="server" CssClass="form-control" />
        </div>
        <div class="d-flex justify-content-between">
            <asp:Button ID="btnBackup" runat="server" Text="Crear Backup" CssClass="btn btn-primary" OnClick="btnBackup_Click" />
            <asp:Button ID="btnRestore" runat="server" Text="Restaurar" CssClass="btn btn-danger" OnClick="btnRestore_Click" />
        </div>
        <div class="mt-3">
            <asp:Label ID="lblMensaje" runat="server" />
        </div>
        <div class="text-center mt-4">
            <a href="../Default.aspx" class="btn btn-secondary">Volver al inicio</a>
        </div>
    </div>
</asp:Content>
