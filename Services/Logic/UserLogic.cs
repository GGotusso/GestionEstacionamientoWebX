using Services.DAL.Contracts;
using Services.DAL.FactoryServices;
using Services.DOMAIN;
using Services.DTO;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Logic
{
    /// <summary>
    /// Lógica para la gestión de usuarios, incluyendo autenticación, recuperación de contraseña, y manejo de accesos.
    /// </summary>
    public class UserLogic
    {
        private readonly IUsuarioDAL _usuarioRepository;
        /// <summary>
        /// Constructor que inicializa el repositorio de usuarios.
        /// </summary>
        public UserLogic()
        {
            _usuarioRepository = FactoryDAL.UsuarioRepository;
        }
        /// <summary>
        /// Valida las credenciales de un usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>True si las credenciales son válidas; de lo contrario, false.</returns>
        public bool ValidateUser(string username, string password)
        {
            // Obtener el usuario por su nombre de usuario
            var usuario = _usuarioRepository.GetUsuarioByUsername(username);
            if (usuario != null)
            {
                // Verificar si el usuario está deshabilitado
                if (usuario.Estado == "0")
                {
                    // Usuario está deshabilitado
                    return false;
                }

                // Verificar la contraseña
                string hashedPassword = CryptographyService.HashMd5(password);
                return usuario.Password == hashedPassword;
            }
            return false;
        }

        /// <summary>
        /// Crea un nuevo usuario con la contraseña encriptada.
        /// </summary>
        /// <param name="usuario">Objeto de usuario a crear.</param>
        /// <param name="plainPassword">Contraseña en texto plano.</param>
        public void CreateUser(Usuario usuario, string plainPassword)
        {

            // Verificar si el nombre de usuario ya existe
            var existingUser = _usuarioRepository.GetUsuarioByUsername(usuario.UserName);
            if (existingUser != null)
            {
                throw new Exception($"El nombre de usuario '{usuario.UserName}' ya está en uso.");


            }
            //Hash de la contraseña en texto plano
            usuario.Password = CryptographyService.HashMd5(plainPassword);
            // Asignar el estado del usuario como habilitado (1)
            usuario.Estado = ((int)Usuario.EstadoUsuario.Habilitado).ToString();

            // Calcular DVH con los campos ya definidos
            usuario.DVH = CryptographyService.CalcularDVH(usuario);

            _usuarioRepository.CreateUsuario(usuario);
        }
        /// <summary>
        /// Deshabilita un usuario.
        /// </summary>
        /// <param name="idUsuario">ID del usuario a deshabilitar.</param>
        public void DisableUser(Guid idUsuario)
        {
            _usuarioRepository.DisableUsuario(idUsuario);
        }
        /// <summary>
        /// Habilita un usuario.
        /// </summary>
        /// <param name="idUsuario">ID del usuario a habilitar.</param>
        public void EnabledUsuario(Guid idUsuario)
        {
            _usuarioRepository.EnabledUsuario(idUsuario);
        }
        /// <summary>
        /// Actualiza los accesos de un usuario.
        /// </summary>
        /// <param name="idUsuario">ID del usuario.</param>
        /// <param name="accesos">Lista de accesos a asignar.</param>
        public void UpdateUserAccesos(Guid idUsuario, List<Acceso> accesos)
        {
            _usuarioRepository.UpdateAccesos(idUsuario, accesos);
        }
        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public List<Usuario> GetAllUsuarios()
        {
            return _usuarioRepository.GetAll();
        }
        /// <summary>
        /// Obtiene un usuario por su nombre de usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <returns>Usuario encontrado o null si no existe.</returns>
        public Usuario GetUsuarioByUsername(string username)
        {
            return _usuarioRepository.GetUsuarioByUsername(username);
        }
        /// <summary>
        /// Obtiene una lista de usuarios junto con sus familias (roles).
        /// </summary>
        /// <returns>Lista de objetos <see cref="UsuarioFamiliaDTO"/>.</returns>
        public List<UsuarioFamiliaDTO> GetUsuariosConFamilias()
        {
            return _usuarioRepository.GetUsuariosConFamilias();
        }
        /// <summary>
        /// Crea una nueva patente.
        /// </summary>
        /// <param name="patente">Objeto de patente a crear.</param>
        public void CreatePatente(Patente patente)
        {
            _usuarioRepository.CreatePatente(patente);
        }


        public void ChangePassword(string username, string newPassword)
        {
            var usuario = _usuarioRepository.GetUsuarioByUsername(username);
            if (usuario != null)
            {
                string hashedPassword = CryptographyService.HashMd5(newPassword);
                _usuarioRepository.UpdatePassword(usuario.IdUsuario, hashedPassword);
            }
        }

        /// <summary>
        /// Recalcula el DVH (Dígito Verificador Horizontal) para todos los usuarios existentes.
        /// </summary>
        public void RecalcularDVHUsuariosExistentes()
        {
            var usuarios = _usuarioRepository.GetAll(); // Este método debe retornar UserName, Password, Estado y PhoneNumber

            foreach (var usuario in usuarios)
            {
                var usuarioCompleto = _usuarioRepository.GetUsuarioByUsername(usuario.UserName); // Asegurarte de que trae todos los campos
                usuarioCompleto.DVH = CryptographyService.CalcularDVH(usuarioCompleto);
                _usuarioRepository.ActualizarDVH(usuarioCompleto.IdUsuario, usuarioCompleto.DVH);
            }
        }

        //Error de integridad: Verifica si el DVH de cada usuario es correcto
        public bool HayErroresDeIntegridad()
        {
            var usuarios = _usuarioRepository.GetAll();

            foreach (var usuario in usuarios)
            {
                var completo = _usuarioRepository.GetUsuarioByUsername(usuario.UserName);
                string dvh = CryptographyService.CalcularDVH(completo);
                if (completo.DVH != dvh)
                    return true;
            }

            return false;
        }


    }
}
