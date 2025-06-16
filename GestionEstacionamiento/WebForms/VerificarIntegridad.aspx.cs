using Services.DOMAIN;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionEstacionamiento.WebForms
{
    public partial class VerificarIntegridad : System.Web.UI.Page
    {
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                UserService.RecalcularDVHUsuarios();

                var usuario = (Usuario)Session["UsuarioPendiente"];
                Session["Usuario"] = usuario;
                Session.Remove("UsuarioPendiente");

                Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                lblResultado.Text = $"Error al recalcular DVH: {ex.Message}";
            }
        }

        protected void btnRestaurar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PanelAdministrador.aspx?modo=restaurar");
        }
    }
}