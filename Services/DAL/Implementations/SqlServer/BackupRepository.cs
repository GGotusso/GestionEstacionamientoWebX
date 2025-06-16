using Services.DAL.Contracts;
using System.Configuration;
using System.Data.SqlClient;

namespace Services.DAL.Implementations.SqlServer
{
    public class BackupRepository : IBackupDAL
    {
        public void BackupDatabase(string connectionName, string backupPath)
        {
            string cs = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            var builder = new SqlConnectionStringBuilder(cs);
            string databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            string masterCs = builder.ToString();
            using (SqlConnection conn = new SqlConnection(masterCs))
            {
                string sql = $"BACKUP DATABASE [{databaseName}] TO DISK = @Path WITH INIT";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Path", backupPath);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void RestoreDatabase(string connectionName, string backupPath)
        {
            string cs = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            var builder = new SqlConnectionStringBuilder(cs);
            string databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            string masterCs = builder.ToString();

            using (SqlConnection conn = new SqlConnection(masterCs))
            {
                conn.Open();

                // Obtener nombres lógicos del backup
                string logicalDataName = null;
                string logicalLogName = null;

                using (SqlCommand cmd = new SqlCommand($"RESTORE FILELISTONLY FROM DISK = @Path", conn))
                {
                    cmd.Parameters.AddWithValue("@Path", backupPath);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader["Type"].ToString();
                            if (type == "D")
                                logicalDataName = reader["LogicalName"].ToString();
                            else if (type == "L")
                                logicalLogName = reader["LogicalName"].ToString();
                        }
                    }
                }

                // Rutas físicas actuales de la base de datos
                string dataFilePath = $@"C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\{databaseName}.mdf";
                string logFilePath = $@"C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\{databaseName}_log.ldf";

                try
                {
                    // Forzar SINGLE_USER para cerrar conexiones
                    using (SqlCommand cmd = new SqlCommand($"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Restaurar con MOVE
                    using (SqlCommand cmd = new SqlCommand($@"
                RESTORE DATABASE [{databaseName}]
                FROM DISK = @Path
                WITH REPLACE,
                     MOVE @LogicalDataName TO @DataPath,
                     MOVE @LogicalLogName TO @LogPath", conn))
                    {
                        cmd.Parameters.AddWithValue("@Path", backupPath);
                        cmd.Parameters.AddWithValue("@LogicalDataName", logicalDataName);
                        cmd.Parameters.AddWithValue("@DataPath", dataFilePath);
                        cmd.Parameters.AddWithValue("@LogicalLogName", logicalLogName);
                        cmd.Parameters.AddWithValue("@LogPath", logFilePath);

                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    // Restaurar MULTI_USER
                    using (SqlCommand cmd = new SqlCommand($"ALTER DATABASE [{databaseName}] SET MULTI_USER", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
