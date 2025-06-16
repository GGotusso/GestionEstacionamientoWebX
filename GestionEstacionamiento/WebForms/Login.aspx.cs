using Services.DOMAIN;
using Services.Facade;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            bool isValid = UserService.Login(username, password);

            if (isValid)
            {
                Usuario usuario = UserService.GetUsuarioByUsername(username);
                Session["Usuario"] = usuario;

                if (UserService.HayErroresDeIntegridad())
                {
                    if (RolesHelper.EsAdministrador(usuario))
                    {
                        // Redirigir al panel de verificación
                        Session["UsuarioPendiente"] = usuario;
                        Response.Redirect("~/VerificarIntegridad.aspx");
                        return;
                    }
                    else
                    {
                        LogService.WriteLog(TraceLevel.Warning, $"Operador detectó errores de integridad: {username}");
                        lblError.Text = "Contacte al administrador: los datos del sistema parecen estar corruptos.";
                        return;
                    }
                }

                LogService.WriteLog(TraceLevel.Info, $"{username} ingresó al sistema");
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}
