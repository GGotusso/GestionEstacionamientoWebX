using Services.Logic;

namespace Services.Facade
{
    public static class BackupService
    {
        private static readonly BackupLogic _logic = new BackupLogic();

        public static void BackupDatabase(string connectionName, string path)
        {
            _logic.Backup(connectionName, path);
        }

        public static void RestoreDatabase(string connectionName, string path)
        {
            _logic.Restore(connectionName, path);
        }
    }
}
