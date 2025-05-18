<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="GestionEstacionamiento.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Pagina de contacto SGDE.</h3>
        <address>
            Argentina<br />
            CABA<br />
            <abbr title="Phone">P:</abbr>
            11111111111
        </address>

        <address>
            <strong>Support:</strong>   <a href="mailto:gonzalo@example.com">gonzalo@GestionEstacionamiento.com</a><br />
            <strong>Support:</strong>   <a href="mailto:Facundo@example.com">Facundo@GestionEstacionamiento.com</a><br />

        </address>
    </main>
</asp:Content>
