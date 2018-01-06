using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CentralBackup.Core.Interfaces.Commands;

namespace CentralBackup.Commands.SqlServerTransactionLogBackup
{
    public class SqlServerTransactionLogBackup : ICommand
    {
        public void Execute(IDictionary<string, string> configuration)
        {
            using (var connection = new SqlConnection(configuration["ConnectionString"]))
            {
                using (var command = GetCommand(connection, configuration))
                {
                    connection.Open();
                    var result = command.ExecuteNonQuery();

                    if (result != -1)
                    {
                        throw new Exception("Transaction Log Backup failed");
                    }
                }
            }
        }

        private SqlCommand GetCommand(SqlConnection connection, IDictionary<string, string> configuration)
        {
            const string text = @"
            DECLARE @BackupName VARCHAR(256) = (SELECT CONCAT(@Path, @Database, '_', REPLACE(convert(nvarchar(20), GetDate(), 120), ':', '-'), '.trn'))
            BACKUP LOG @Database
            TO DISK = @BackupName
            WITH COMPRESSION";

            var command = new SqlCommand(text, connection);

            var databaseName = command.CreateParameter();
            databaseName.ParameterName = "@Database";
            databaseName.Value = configuration["Database"];

            var path = command.CreateParameter();
            path.ParameterName = "@Path";
            path.Value = configuration["Path"];

            command.Parameters.Add(databaseName);
            command.Parameters.Add(path);

            return command;
        }
    }
}