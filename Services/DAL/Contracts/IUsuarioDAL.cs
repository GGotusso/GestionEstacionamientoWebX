using Services.DOMAIN;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.DAL.Contracts
{
    public interface IUsuarioDAL : IGenericServiceDAL<Usuario>
    {
        /// <summary>
        /// Obtiene un usuario por su nombre de usuario.
        /// </summary>
        /// <param name="username">El nombre de usuario.</param>
        /// <returns>El usuario correspondiente, si existe.</returns>
        Usuario GetUsuarioByUsername(string username);
        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">El usuario a crear.</param>
        void CreateUsuario(Usuario usuario);
        /// <summary>
        /// Deshabilita un usuario por su ID.
        /// </summary>
        /// <param name="idUsuario">El ID del usuario.</param>
        void DisableUsuario(Guid idUsuario);
        /// <summary>
        /// Actualiza los accesos de un usuario.
        /// </summary>
        /// <param name="idUsuario">El ID del usuario.</param>
        /// <param name="accesos">La lista de accesos a asignar al usuario.</param>
        void UpdateAccesos(Guid idUsuario, List<Acceso> accesos);
        /// <summary>
        /// Crea una nueva patente en la base de datos.
        /// </summary>
        /// <param name="patente">La patente a crear.</param>

        void CreatePatente(Patente patente);

        /// <summary>
        /// Habilita un usuario por su ID.
        /// </summary>
        /// <param name="idUsuario">El ID del usuario.</param>

        void EnabledUsuario(Guid idUsuario);
        /// <summary>
        /// Obtiene una lista de usuarios con sus respectivas familias, para generar un reporte.
        /// </summary>
        /// <returns>Una lista de <see cref="UsuarioFamiliaDTO"/> que representa los usuarios con sus familias asociadas.</returns>
        List<UsuarioFamiliaDTO> GetUsuariosConFamilias();



        void UpdatePassword(Guid idUsuario, string newPassword);

        /// <summary>
        /// Actualiza el DVH (Dígito Verificador Horizontal) de un usuario.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="dvh"></param>
        void ActualizarDVH(Guid idUsuario, string dvh);


    }
}
