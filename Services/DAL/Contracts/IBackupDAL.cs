using System;

namespace Services.DAL.Contracts
{
    public interface IBackupDAL
    {
        void BackupDatabase(string connectionName, string backupPath);
        void RestoreDatabase(string connectionName, string backupPath);
    }
}
