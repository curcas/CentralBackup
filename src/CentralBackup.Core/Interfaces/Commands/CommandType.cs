namespace CentralBackup.Core.Interfaces.Commands
{
    public enum CommandType
    {
        SqlServerFullBackup = 0,
        SqlServerTransactionLog,
        DeleteFilesOlderThanXDays
    }
}