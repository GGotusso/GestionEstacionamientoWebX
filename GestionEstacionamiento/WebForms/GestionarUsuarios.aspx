<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionarUsuarios.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GestionEstacionamiento.WebForms.GestionarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="mb-4">Gestionar Usuarios</h3>
    <asp:Label ID="lblMensaje" runat="server" CssClass="text-success" />
    <div class="mb-3">
        <asp:Label runat="server" Text="Usuario:" AssociatedControlID="ddlUsuarios" />
        <asp:DropDownList ID="ddlUsuarios" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <div class="row">
        <div class="col-md-5">
            <asp:Label runat="server" Text="Roles disponibles" />
            <asp:ListBox ID="lbRolesDisponibles" runat="server" CssClass="form-control" SelectionMode="Single"></asp:ListBox>
        </div>
        <div class="col-md-2 text-center">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar &gt;&gt;" CssClass="btn btn-primary mb-2" OnClick="btnAgregar_Click" />
            <br />
            <asp:Button ID="btnQuitar" runat="server" Text="&lt;&lt; Quitar" CssClass="btn btn-danger" OnClick="btnQuitar_Click" />
        </div>
        <div class="col-md-5">
            <asp:Label runat="server" Text="Roles asignados" />
            <asp:ListBox ID="lbRolesAsignados" runat="server" CssClass="form-control" SelectionMode="Single"></asp:ListBox>
        </div>
    </div>
</asp:Content>
