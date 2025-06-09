using Services.DAL.Contracts;
using Services.DAL.FactoryServices;

namespace Services.Logic
{
    public class BackupLogic
    {
        private readonly IBackupDAL _backupRepository;

        public BackupLogic()
        {
            _backupRepository = FactoryDAL.BackupRepository;
        }

        public void Backup(string connectionName, string path)
        {
            _backupRepository.BackupDatabase(connectionName, path);
        }

        public void Restore(string connectionName, string path)
        {
            _backupRepository.RestoreDatabase(connectionName, path);
        }
    }
}
