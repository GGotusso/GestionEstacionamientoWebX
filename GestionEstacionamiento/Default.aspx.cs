using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionEstacionamiento
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si no estamos logueados, redirigimos a la página de login
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/WebForms/Login.aspx");
            }
            // Si es la primera vez que se carga la página, mostramos el mensaje de bienvenida
            if (!IsPostBack)
            {
                var usuario = (Services.DOMAIN.Usuario)Session["Usuario"];
                lblBienvenida.Text = $"Bienvenido, {usuario.UserName}";
            }

        }

    protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/WebForms/Login.aspx");
        }
    }
}