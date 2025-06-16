<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PanelAdministrador.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GestionEstacionamiento.WebForms.PanelAdministrador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4" style="max-width: 600px;">
        <h3 class="mb-4 text-center">Panel Administrador</h3>

        <!-- Backup folder input -->
        <div class="mb-3">
            <asp:Label runat="server" Text="Carpeta de backup" AssociatedControlID="txtRuta" />
            <asp:TextBox ID="txtRuta" runat="server" CssClass="form-control" />
        </div>

        <!-- Archivos de restauración separados -->
        <div class="mb-3">
            <asp:Label runat="server" Text="Archivo backup de Usuarios (TC_User)" AssociatedControlID="fuRestoreUsuario" />
            <asp:FileUpload ID="fuRestoreUsuario" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <asp:Label runat="server" Text="Archivo backup de Logs (TC_Log)" AssociatedControlID="fuRestoreLog" />
            <asp:FileUpload ID="fuRestoreLog" runat="server" CssClass="form-control" />
        </div>

        <!-- Botones -->
        <div class="d-flex justify-content-between">
            <asp:Button ID="btnBackup" runat="server" Text="Crear Backup" CssClass="btn btn-primary" OnClick="btnBackup_Click" />
            <asp:Button ID="btnRestore" runat="server" Text="Restaurar" CssClass="btn btn-danger" OnClick="btnRestore_Click" />
            <asp:Button ID="btnRecalcularDVH" runat="server" Text="Recalcular DVH" CssClass="btn btn-warning" OnClick="btnRecalcularDVH_Click" />
        </div>

        <!-- Mensaje de resultado -->
        <div class="mt-3">
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
        </div>

        <div class="text-center mt-4">
            <a href="../Default.aspx" class="btn btn-secondary">Volver al inicio</a>
        </div>
    </div>
</asp:Content>
