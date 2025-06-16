using Services.DAL.Implementations.SqlServer.Helpers;
using Services.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DAL.Contracts;
using Services.DTO;

using static Services.DOMAIN.Usuario;

namespace Services.DAL.Implementations.SqlServer
{
    public class UsuarioRepository : IUsuarioDAL
    {
        private readonly FamiliaRepository _familiaRepository;

        public UsuarioRepository()
        {
            _familiaRepository = new FamiliaRepository();
        }
        public Usuario GetUsuarioByUsername(string username)
        {
            Usuario usuario = null;


            using (SqlDataReader reader = SqlHelper.ExecuteReader(
                "SELECT IdUsuario, UserName, Password, Estado, PhoneNumber FROM Usuario WHERE UPPER(UserName) = UPPER(@UserName)",
                CommandType.Text,
                new SqlParameter("@UserName", username)))
            {
                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = reader.GetGuid(0),
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2),
                        Estado = reader.GetString(3),
                        PhoneNumber = reader.IsDBNull(4) ? null : reader.GetString(4)
                    };
                }
            }

            if (usuario != null)
            {
                // Obtener las familias del usuario si es necesario
                List<Familia> familias = GetFamiliasByUsuarioId(usuario.IdUsuario);
                usuario.Accesos.AddRange(familias);
            }

            return usuario;
        }

        private List<Familia> GetFamiliasByUsuarioId(Guid usuarioId)
        {
            List<Familia> familias = new List<Familia>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(
                "SELECT f.IdFamilia, f.Nombre FROM Familia f " +
                "INNER JOIN Usuario_Familia uf ON f.IdFamilia = uf.IdFamilia " +
                "WHERE uf.IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", usuarioId)))
            {
                while (reader.Read())
                {
                    var familia = new Familia
                    {
                        Id = reader.GetGuid(0),
                        Nombre = reader.GetString(1)
                    };

                    // Obtener las patentes asociadas a esta familia
                    familia.Accesos.AddRange(_familiaRepository.GetPatentesByFamiliaId(familia.Id));

                    // Añadir la familia a la lista
                    familias.Add(familia);
                }
            }

            return familias;
        }

        public void CreateUsuario(Usuario usuario)
        {
            SqlHelper.ExecuteNonQuery(
                "INSERT INTO Usuario (IdUsuario, UserName, Password, Estado, PhoneNumber, DVH) VALUES (@IdUsuario, @UserName, @Password, @Estado, @PhoneNumber, @DVH)",
                CommandType.Text,
                new SqlParameter("@IdUsuario", usuario.IdUsuario),
                new SqlParameter("@UserName", usuario.UserName),
                new SqlParameter("@Password", usuario.Password),
                new SqlParameter("@Estado", (int)EstadoUsuario.Habilitado),
                new SqlParameter("@PhoneNumber", usuario.PhoneNumber),
                new SqlParameter("@DVH", usuario.DVH)
            );
        }

        public void DisableUsuario(Guid idUsuario)
        {
            SqlHelper.ExecuteNonQuery(
                "UPDATE Usuario SET Estado = 0 WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", idUsuario)
            );
        }

        public void EnabledUsuario(Guid idUsuario)
        {
            SqlHelper.ExecuteNonQuery(
                "UPDATE Usuario SET Estado = 1 WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", idUsuario)
            );
        }

        public void UpdateAccesos(Guid idUsuario, List<Acceso> accesos)
        {
            // Primero eliminamos los accesos existentes
            SqlHelper.ExecuteNonQuery(
                "DELETE FROM Usuario_Patente WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", idUsuario)
            );

            // Luego insertamos los nuevos accesos
            foreach (var acceso in accesos)
            {
                SqlHelper.ExecuteNonQuery(
                    "INSERT INTO Usuario_Patente (IdUsuario, IdPatente) VALUES (@IdUsuario, @IdPatente)",
                    CommandType.Text,
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@IdPatente", acceso.Id)
                );
            }
        }



        public void UpdatePassword(Guid idUsuario, string newPassword)
        {
            SqlHelper.ExecuteNonQuery(
                "UPDATE Usuario SET Password = @Password, OTP = NULL, OTPExpiry = NULL WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@Password", newPassword),
                new SqlParameter("@IdUsuario", idUsuario)
            );
        }


        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            string query = "SELECT IdUsuario, UserName, Estado FROM Usuario";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);

                    usuarios.Add(new Usuario
                    {
                        IdUsuario = reader.GetGuid(0),

                        UserName = reader.GetString(1),

                        Estado = reader.GetString(2)
                    });
                }
            }

            return usuarios;
        }
        public List<UsuarioFamiliaDTO> GetUsuariosConFamilias()
        {
            string query = @"
            SELECT 
                u.UserName AS Usuario,
                f.Nombre AS Familia,
                u.Estado AS Estado
            FROM 
                Usuario u
            LEFT JOIN 
                Usuario_Familia uf ON u.IdUsuario = uf.IdUsuario
            LEFT JOIN 
                Familia f ON uf.IdFamilia = f.IdFamilia";

            List<UsuarioFamiliaDTO> usuariosFamilias = new List<UsuarioFamiliaDTO>();

            using (var reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    usuariosFamilias.Add(new UsuarioFamiliaDTO
                    {
                        Usuario = reader.GetString(0),
                        Familia = reader.IsDBNull(1) ? "Sin familia" : reader.GetString(1),
                        Estado = reader.GetString(2)
                    });
                }
            }

            return usuariosFamilias;
        }

        public void CreatePatente(Patente patente)
        {
            SqlHelper.ExecuteNonQuery(
                "INSERT INTO Patente (IdPatente, Nombre, DataKey, TipoAcceso) VALUES (@IdPatente, @Nombre, @DataKey, @TipoAcceso)",
                CommandType.Text,
                new SqlParameter("@IdPatente", patente.Id),
                new SqlParameter("@Nombre", patente.Nombre),
                new SqlParameter("@DataKey", patente.DataKey),
                new SqlParameter("@TipoAcceso", (int)patente.TipoAcceso) // Convertimos el enum a entero
            );
        }


        public void ActualizarDVH(Guid idUsuario, string dvh)
        {
            SqlHelper.ExecuteNonQuery(
                "UPDATE Usuario SET DVH = @DVH WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@DVH", dvh),
                new SqlParameter("@IdUsuario", idUsuario)
            );
        }

        public void Add(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }

}
