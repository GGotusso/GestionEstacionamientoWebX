using Services.DAL.Implementations.SqlServer.Helpers;
using Services.DOMAIN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DAL.Implementations.SqlServer
{
    public class FamiliaRepository
    {
        public List<Patente> GetPatentesByFamiliaId(Guid familiaId)
        {
            List<Patente> patentes = new List<Patente>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(
                "SELECT p.IdPatente, p.Nombre, p.DataKey, p.TipoAcceso FROM Patente p " +
                "INNER JOIN Familia_Patente fp ON p.IdPatente = fp.IdPatente " +
                "WHERE fp.IdFamilia = @IdFamilia",
                CommandType.Text,
                new SqlParameter("@IdFamilia", familiaId)))
            {
                while (reader.Read())
                {
                    patentes.Add(new Patente
                    {
                        Id = reader.GetGuid(0),
                        Nombre = reader.GetString(1),
                        DataKey = reader.GetString(2),
                        TipoAcceso = (TipoAcceso)reader.GetInt32(3)
                    });
                }
            }

            return patentes;
        }
        public void SaveFamilia(Familia familia)
        {
            SqlHelper.ExecuteNonQuery(
                "INSERT INTO Familia (IdFamilia, Nombre) VALUES (@IdFamilia, @Nombre)",
                CommandType.Text,
                new SqlParameter("@IdFamilia", familia.Id),
                new SqlParameter("@Nombre", familia.Nombre)
            );

            foreach (var patente in familia.Accesos.OfType<Patente>())
            {
                SqlHelper.ExecuteNonQuery(
                    "INSERT INTO Familia_Patente (IdFamilia, IdPatente) VALUES (@IdFamilia, @IdPatente)",
                    CommandType.Text,
                    new SqlParameter("@IdFamilia", familia.Id),
                    new SqlParameter("@IdPatente", patente.Id)
                );
            }
        }

        public void UpdateFamilia(Familia familia)
        {
            // Actualizar el nombre de la familia
            SqlHelper.ExecuteNonQuery(
                "UPDATE Familia SET Nombre = @Nombre WHERE IdFamilia = @IdFamilia",
                CommandType.Text,
                new SqlParameter("@IdFamilia", familia.Id),
                new SqlParameter("@Nombre", familia.Nombre)
            );

            // Eliminar patentes actuales de la familia en Familia_Patente
            SqlHelper.ExecuteNonQuery(
                "DELETE FROM Familia_Patente WHERE IdFamilia = @IdFamilia",
                CommandType.Text,
                new SqlParameter("@IdFamilia", familia.Id)
            );

            // Reinsertar las patentes actualizadas
            foreach (var patente in familia.Accesos.OfType<Patente>())
            {
                SqlHelper.ExecuteNonQuery(
                    "INSERT INTO Familia_Patente (IdFamilia, IdPatente) VALUES (@IdFamilia, @IdPatente)",
                    CommandType.Text,
                    new SqlParameter("@IdFamilia", familia.Id),
                    new SqlParameter("@IdPatente", patente.Id)
                );
            }
        }

        public void SaveUsuarioFamilia(Guid IdUsuario, Guid IdFamilia)
        {
            SqlHelper.ExecuteNonQuery(
                "INSERT INTO Usuario_Familia (IdUsuario, IdFamilia) VALUES (@IdUsuario, @IdFamilia)",
                CommandType.Text,
                new SqlParameter("@IdUsuario", IdUsuario),
                new SqlParameter("@IdFamilia", IdFamilia)
            );
        }

        public void UpdateUsuarioFamilia(Guid usuarioId, List<Familia> familias)
        {
            // Eliminar relaciones actuales
            SqlHelper.ExecuteNonQuery(
                "DELETE FROM Usuario_Familia WHERE IdUsuario = @IdUsuario",
                CommandType.Text,
                new SqlParameter("@IdUsuario", usuarioId)
            );

            // Reinsertar relaciones actualizadas
            foreach (var familia in familias)
            {
                SaveUsuarioFamilia(usuarioId, familia.Id);
            }
        }

        public void RemoveUsuarioFamilia(Guid usuarioId, Guid familiaId)
        {
            SqlHelper.ExecuteNonQuery(
                "DELETE FROM Usuario_Familia WHERE IdUsuario = @IdUsuario AND IdFamilia = @IdFamilia",
                CommandType.Text,
                new SqlParameter("@IdUsuario", usuarioId),
                new SqlParameter("@IdFamilia", familiaId)
            );
        }
        public List<Familia> GetAll()
        {
            List<Familia> familias = new List<Familia>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(
                "SELECT IdFamilia, Nombre FROM Familia",
                CommandType.Text))
            {
                while (reader.Read())
                {
                    familias.Add(new Familia
                    {
                        Id = reader.GetGuid(0),
                        Nombre = reader.GetString(1)
                    });
                }
            }

            return familias;
        }
        public List<Patente> GetAllPatentes()
        {
            List<Patente> patentes = new List<Patente>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(
                "SELECT IdPatente, Nombre, DataKey, TipoAcceso FROM Patente",
                CommandType.Text))
            {
                while (reader.Read())
                {
                    patentes.Add(new Patente
                    {
                        Id = reader.GetGuid(0),
                        Nombre = reader.GetString(1),
                        DataKey = reader.GetString(2),
                        TipoAcceso = (TipoAcceso)reader.GetInt32(3)
                    });
                }
            }

            return patentes;
        }
    }
}
