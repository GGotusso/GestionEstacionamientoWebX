using Services.DOMAIN;
using Services.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DAL
{
    public class UsuarioDAL
    {
        private readonly string _connStr = ConfigurationManager.ConnectionStrings["UsuariosConnection"].ConnectionString;
        /// Método para obtener un usuario por sus credenciales (nombre de usuario y contraseña)
        public Usuario ObtenerPorCredenciales(string userName, string password)
        {
            Usuario usuario = null;

            string hashedPassword = CryptographyService.HashMd5(password); // HASH de la contraseña ingresada

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string query = "SELECT * FROM Usuarios WHERE UserName = @UserName AND Password = @Password AND Estado = 'Habilitado'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = (Guid)reader["IdUsuario"],
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        Estado = reader["Estado"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                }

                reader.Close();
            }

            return usuario;
        }
        /// Método para insertar un nuevo usuario en la base de datos
        public bool InsertarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string query = @"INSERT INTO Usuarios (IdUsuario, UserName, Password, Estado, PhoneNumber)
                                 VALUES (@IdUsuario, @UserName, @Password, @Estado, @PhoneNumber)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                cmd.Parameters.AddWithValue("@Password", usuario.Password); // ya viene hasheada
                cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
                cmd.Parameters.AddWithValue("@PhoneNumber", usuario.PhoneNumber);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
    }


}
