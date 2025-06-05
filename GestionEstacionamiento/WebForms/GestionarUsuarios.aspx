<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionarUsuarios.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GestionEstacionamiento.WebForms.GestionarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4" style="max-width: 900px;">
        <h3 class="mb-4 text-center">Gestionar Usuarios</h3>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />

        <div class="mb-3">
            <asp:Label runat="server" Text="Usuario:" AssociatedControlID="ddlUsuarios" />
            <asp:DropDownList ID="ddlUsuarios" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged" />
        </div>

        <div class="row align-items-center text-center">
            <!-- Roles disponibles -->
            <div class="col-md-4">
                <asp:Label runat="server" Text="Roles disponibles" CssClass="form-label d-block" />
                <asp:ListBox ID="lbRolesDisponibles" runat="server" CssClass="form-control" SelectionMode="Single" Rows="6" />
            </div>

            <!-- Botones -->
            <div class="col-md-4 d-flex flex-column align-items-center justify-content-center">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar &gt;&gt;" CssClass="btn btn-primary mb-2 w-75" OnClick="btnAgregar_Click" />
                <asp:Button ID="btnQuitar" runat="server" Text="&lt;&lt; Quitar" CssClass="btn btn-danger w-75" OnClick="btnQuitar_Click" />
            </div>

            <!-- Roles asignados -->
            <div class="col-md-4">
                <asp:Label runat="server" Text="Roles asignados" CssClass="form-label d-block" />
                <asp:ListBox ID="lbRolesAsignados" runat="server" CssClass="form-control" SelectionMode="Single" Rows="6" />
            </div>
        </div>

        <div class="text-center mt-4">
            <a href="../Default.aspx" class="btn btn-secondary">Volver al inicio</a>
        </div>
    </div>
</asp:Content>
