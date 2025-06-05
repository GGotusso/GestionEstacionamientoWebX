using Services.DOMAIN;
using Services.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade
{
    public static class FamiliaService
    {
        private static readonly FamiliaLogic _familiaLogic = new FamiliaLogic();

        public static void CrearFamiliaConPatentes(string nombreFamilia, List<Patente> patentes)
        {
            var familia = new Familia { Nombre = nombreFamilia };
            foreach (var patente in patentes)
            {
                familia.Add(patente);
            }

            _familiaLogic.CrearFamiliaConPatentes(familia);
        }

        public static void AsignarFamiliaAUsuario(Guid usuarioId, Familia familia)
        {
            _familiaLogic.AsignarFamiliaAUsuario(usuarioId, familia);


        }

        public static void ActualizarFamilia(Familia familia)
        {
            _familiaLogic.ActualizarFamilia(familia);
        }

        public static void ActualizarFamiliasDeUsuario(Guid usuarioId, List<Familia> familias)
        {
            _familiaLogic.ActualizarFamiliasDeUsuario(usuarioId, familias);
        }
        public static List<Familia> GetAllFamilias()
        {
            return _familiaLogic.GetAllFamilias();
        }
        public static List<Patente> GetAllPatentes()
        {
            return _familiaLogic.GetAllPatentes();
        }
    }
}
