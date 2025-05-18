using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DOMAIN
{
    public class Usuario
    {

        public Guid IdUsuario { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Estado { get; set; } // Deshabilitado  habilitado 


        public string PhoneNumber { get; set; } 


        public Usuario()
        {
            IdUsuario = Guid.NewGuid();  // Esto genera un nuevo Id cada vez
        }

        public Usuario(Guid idUsuario)
        {
            this.IdUsuario = idUsuario;
        }



       


        public enum EstadoUsuario
        {
            Deshabilitado = 0,
            Habilitado = 1
        }

    }
}
