using Services.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public static class RolesHelper
    {
        public static bool TieneRol(Usuario usuario, string nombreRol)
        {
            return usuario.GetFamilias().Any(f => f.Nombre.Equals(nombreRol, StringComparison.OrdinalIgnoreCase));
        }

        public static bool EsAdministrador(Usuario usuario) => TieneRol(usuario, "administrador");
        public static bool EsOperador(Usuario usuario) => TieneRol(usuario, "Operador");
    }
}
