<%@ Page Title="Gestion estacionamiento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GestionEstacionamiento._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Como hereda de Site.master no necesita repetir html, lo ponemos como contenido -->
    <asp:Label ID="lblBienvenida" runat="server" CssClass="h4" ForeColor="Green" />

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

        <!-- Tarifa -->
        <div class="col">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Tarifa</h5>
                    <a href="WebForms/Tarifa.aspx" class="btn btn-primary w-100">Gestionar tarifa</a>
                </div>
            </div>
        </div>

        <!-- Recibo -->
        <div class="col">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Recibo</h5>
                    <a href="WebForms/Recibo.aspx" class="btn btn-primary w-100">Generar recibo</a>
                </div>
            </div>
        </div>

        <!-- Reportes -->
        <div class="col">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Reportes</h5>
                    <a href="WebForms/Reportes.aspx" class="btn btn-primary w-100">Ver reportes</a>
                </div>
            </div>
        </div>

        <!-- Gestionar espacios -->
        <div class="col">
            <div class="card shadow-sm">
                <div class="card-body">
                    <h5 class="card-title">Gestionar espacios</h5>
                    <a href="WebForms/GestionarEspacios.aspx" class="btn btn-primary w-100">Administrar espacios</a>
                </div>
            </div>
        </div>

    </div>
     <!-- Más módulos proximamente -->



    <!-- Aplico bootstrap ->Activo flexbox en el contenedor, empujo el contenido hacia la derecha con justify, agrego un margen para separar del otro div -->
    <div class="d-flex justify-content-end mt-4">
    <asp:Button ID="btnLogout" runat="server" Text="Cerrar sesión" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
    </div>

</asp:Content>
