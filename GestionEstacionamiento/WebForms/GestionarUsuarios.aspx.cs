using Services.DOMAIN;
using Services.Facade;
using System;
using System.Web.UI.WebControls;

namespace GestionEstacionamiento.WebForms
{
    public partial class GestionarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarRoles();
            }
        }

        private void CargarUsuarios()
        {
            ddlUsuarios.DataSource = UserService.GetAllUsuarios();
            ddlUsuarios.DataTextField = "UserName";
            ddlUsuarios.DataValueField = "IdUsuario";
            ddlUsuarios.DataBind();
            ddlUsuarios.Items.Insert(0, new ListItem("-- Seleccione --", ""));
        }

        private void CargarRoles()
        {
            lbRolesDisponibles.DataSource = FamiliaService.GetAllFamilias();
            lbRolesDisponibles.DataTextField = "Nombre";
            lbRolesDisponibles.DataValueField = "Id";
            lbRolesDisponibles.DataBind();
            lbRolesAsignados.Items.Clear();
        }

        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbRolesAsignados.Items.Clear();

            if (Guid.TryParse(ddlUsuarios.SelectedValue, out Guid idUsuario))
            {
                var usuario = UserService.GetUsuarioByUsername(ddlUsuarios.SelectedItem.Text);
                foreach (var fam in usuario.GetFamilias())
                {
                    lbRolesAsignados.Items.Add(new ListItem(fam.Nombre, fam.Id.ToString()));
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Guid.TryParse(ddlUsuarios.SelectedValue, out Guid userId) && Guid.TryParse(lbRolesDisponibles.SelectedValue, out Guid famId))
            {
                FamiliaService.AsignarFamiliaAUsuario(userId, new Familia { Id = famId });
                ddlUsuarios_SelectedIndexChanged(sender, e);
                lblMensaje.Text = "Rol agregado.";

            }
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            if (Guid.TryParse(ddlUsuarios.SelectedValue, out Guid userId) && Guid.TryParse(lbRolesAsignados.SelectedValue, out Guid famId))
            {
                FamiliaService.RemoverFamiliaDeUsuario(userId, famId);
                ddlUsuarios_SelectedIndexChanged(sender, e);
                lblMensaje.Text = "Rol quitado.";
                lblMensaje.CssClass = "text-danger"; // Rojo
            }
        }
    }
}
