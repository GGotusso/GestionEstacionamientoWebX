using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Services.DOMAIN;
using Services.Facade;

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
                LogService.WriteLog(TraceLevel.Info, $"{username} ingreso a la plataforma");
                Usuario usuario = UserService.GetUsuarioByUsername(username);
                Session["Usuario"] = usuario;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                LogService.WriteLog(TraceLevel.Warning, $"Intento fallido de login para {username}");
                lblError.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}
