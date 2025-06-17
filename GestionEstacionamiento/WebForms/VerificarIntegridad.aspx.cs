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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var errores = Session["ErroresIntegridad"] as List<string>;
                if (errores != null && errores.Count > 0)
                {
                    lblTablasAfectadas.Text = $"Tabla{(errores.Count > 1 ? "s" : "")} afectada{(errores.Count > 1 ? "s" : "")}: {string.Join(", ", errores)}";
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/WebForms/Login.aspx");
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
            Response.Redirect("~/WebForms/PanelAdministrador.aspx?modo=restaurar");
        }
    }
}