using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services;
using Services.Logic;
using Services.DOMAIN;

namespace GestionEstacionamiento.WebForms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Limpiar sesión si venía de otro lugar
            if (!IsPostBack)
            {
                Session.Clear();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Crea una instancia de la clase UsuarioAccess
            UsuarioAccess usuarioService = new UsuarioAccess();
            // Llama al método para validar las credenciales del usuario ingresado
            // Usa .Trim() para eliminar espacios en blanco
            Usuario usuario = usuarioService.ValidarLogin(txtUser.Text.Trim(), txtPass.Text.Trim());

            if (usuario != null)
            {
                // Guarda el objeto Usuario en sesión para usarlo en otras páginas
                Session["Usuario"] = usuario;
                // Redirige a la página principal (Default.aspx)
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}