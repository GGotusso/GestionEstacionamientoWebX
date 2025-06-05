using Services.DAL.Implementations.SqlServer;
using Services.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logic
{
    public class FamiliaLogic
    {
        private readonly FamiliaRepository _familiaRepository;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FamiliaLogic"/>.
        /// </summary>
        public FamiliaLogic()
        {
            _familiaRepository = new FamiliaRepository();
        }
        /// <summary>
        /// Crea una nueva familia con sus respectivas patentes.
        /// </summary>
        /// <param name="familia">La familia a crear.</param>
        public void CrearFamiliaConPatentes(Familia familia)
        {
            _familiaRepository.SaveFamilia(familia);
        }
        /// <summary>
        /// Asigna una familia a un usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <param name="familia">Familia a asignar.</param>
        public void AsignarFamiliaAUsuario(Guid usuarioId, Familia familia)
        {
            _familiaRepository.SaveUsuarioFamilia(usuarioId, familia.Id);
        }
        /// <summary>
        /// Actualiza una familia existente.
        /// </summary>
        /// <param name="familia">Familia con los datos actualizados.</param>
        public void ActualizarFamilia(Familia familia)
        {
            _familiaRepository.UpdateFamilia(familia);
        }
        /// <summary>
        /// Actualiza las familias asignadas a un usuario específico.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <param name="familias">Lista de familias a asignar al usuario.</param>
        public void ActualizarFamiliasDeUsuario(Guid usuarioId, List<Familia> familias)
        {
            _familiaRepository.UpdateUsuarioFamilia(usuarioId, familias);
        }

        public void RemoverFamiliaDeUsuario(Guid usuarioId, Guid familiaId)
        {
            _familiaRepository.RemoveUsuarioFamilia(usuarioId, familiaId);
        }
        /// <summary>
        /// Obtiene todas las familias disponibles.
        /// </summary>
        /// <returns>Lista de todas las familias.</returns>
        public List<Familia> GetAllFamilias()
        {
            return _familiaRepository.GetAll();
        }

        /// <summary>
        /// Obtiene todas las patentes disponibles.
        /// </summary>
        /// <returns>Lista de todas las patentes.</returns>
        public List<Patente> GetAllPatentes()
        {
            return _familiaRepository.GetAllPatentes();
        }

    }
}
