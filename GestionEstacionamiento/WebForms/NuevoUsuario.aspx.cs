using Services.DAL;
using Services.DOMAIN;
using Services.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionEstacionamiento.WebForms
{
    public partial class NuevoUsuario : System.Web.UI.Page
    {
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Usuario nuevoUsuario = new Usuario
            {
                UserName = txtUser.Text.Trim(),
                Password = CryptographyService.HashMd5(txtPass.Text.Trim()), // Hasheo de contraseña
                Estado = "Habilitado", //Empieza habilitado por defecto
                PhoneNumber = txtTel.Text.Trim()
            };

            UsuarioDAL dal = new UsuarioDAL();
            bool exito = dal.InsertarUsuario(nuevoUsuario);

            lblResultado.Text = exito ? "Usuario creado exitosamente." : "Error al crear usuario.";
        }
    }
}