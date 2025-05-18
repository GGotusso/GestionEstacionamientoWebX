<%@ Page Title="Gestion estacionamiento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GestionEstacionamiento._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Como hereda de Site.master no necesita repetir html, lo ponemos como contenido -->
    <asp:Label ID="lblBienvenida" runat="server" CssClass="h4" ForeColor="Green" />

    <asp:Button ID="btnLogout" runat="server" Text="Cerrar sesión" OnClick="btnLogout_Click" CssClass="btn btn-danger mb-4" />
    
    <div class="row row-cols-1 row-cols-md-3 g-4">

        <div class="col">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Clientes</h5>
            
                    <a href="WebForms/AgregarCliente.aspx" class="btn btn-primary mb-2 w-100">Agregar cliente</a>
                    <a href="WebForms/ModificarCliente.aspx" class="btn btn-secondary w-100">Modificar cliente</a>
                </div>
            </div>
        </div>

        <!-- Reservas -->
        <div class="col">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Reservas</h5>

                    <a href="WebForms/AgregarReserva.aspx" class="btn btn-primary mb-2 w-100">Generar reserva</a>
                    <a href="WebForms/ModificarReserva.aspx" class="btn btn-secondary w-100">Modificar reserva</a>
                </div>
            </div>
        </div>

        <!-- Más módulos proximamente -->
       

    </div>

</asp:Content>
