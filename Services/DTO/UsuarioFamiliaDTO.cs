using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class UsuarioFamiliaDTO
    {
        public string Usuario { get; set; }
        public string Familia { get; set; }
        public string Estado { get; set; }


        // Propiedad calculada que convierte el valor de string a texto legible
        public string EstadoDescripcion
        {
            get
            {
                // Intenta convertir el string a int, y usa el valor convertido para la comparación
                if (int.TryParse(Estado, out int estadoValue))
                {
                    return estadoValue == 1 ? "habilitado" : "deshabilitado";
                }
                else
                {
                    // Manejo de error si Estado no es un valor numérico esperado
                    return "Estado desconocido";
                }
            }
        }
    }
}
