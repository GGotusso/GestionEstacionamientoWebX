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
                using (SqlCommand cmd = new SqlCommand($"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand($"RESTORE DATABASE [{databaseName}] FROM DISK = @Path WITH REPLACE", conn))
                {
                    cmd.Parameters.AddWithValue("@Path", backupPath);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand($"ALTER DATABASE [{databaseName}] SET MULTI_USER", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
