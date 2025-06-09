<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GestionEstacionamiento.WebForms.Bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4" style="max-width: 900px;">
        <h3 class="mb-4 text-center">Bitácora</h3>
        <div class="row mb-3">
            <div class="col-md-3">
                <asp:Label runat="server" Text="Desde" AssociatedControlID="txtDesde" />
                <asp:TextBox ID="txtDesde" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" Text="Hasta" AssociatedControlID="txtHasta" />
                <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" Text="Nivel" AssociatedControlID="ddlNivel" />
                <asp:DropDownList ID="ddlNivel" runat="server" CssClass="form-select">
                    <asp:ListItem Text="-- Todos --" Value="" />
                    <asp:ListItem Text="Error" Value="Error" />
                    <asp:ListItem Text="Warning" Value="Warning" />
                    <asp:ListItem Text="Info" Value="Info" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" Text="Mensaje" AssociatedControlID="txtMensaje" />
                <asp:TextBox ID="txtMensaje" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 text-center">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
        </div>
        <asp:GridView ID="gvLogs" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Date" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="Level" HeaderText="Nivel" />
                <asp:BoundField DataField="Message" HeaderText="Mensaje" />
            </Columns>
        </asp:GridView>
        <div class="text-center mt-4">
            <a href="../Default.aspx" class="btn btn-secondary">Volver al inicio</a>
        </div>
    </div>
</asp:Content>
