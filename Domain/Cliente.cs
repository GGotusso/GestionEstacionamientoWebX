using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN
{
    public class Cliente
    {

        public Guid ID_Cliente { get; set; }


        public int DNI { get; set; }


        public string Nombre { get; set; }


        public string Apellido { get; set; }

        public string Mail { get; set; }



        public string Estado { get; set; }


        public int Telefono { get; set; }


        public DateTime? FechaRegistro { get; set; }

   

    }
}
