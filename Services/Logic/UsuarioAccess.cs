using Services.DAL;
using Services.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logic
{
    public class UsuarioAccess
    {
        // Creamos una instancia de la clase que entra a la base de datos de usuarios
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        //valida si un usuario existe y tiene las credenciales correctas
        public Usuario ValidarLogin(string userName, string password)
        {
            return usuarioDAL.ObtenerPorCredenciales(userName, password);
        }
    }
}
