using System;
using System.Collections.Generic;
using System.Text;
using SimpleMigrations;

namespace CentralBackup.Migrations
{
    [Migration(1, "Create Jobs table")]
    public class CreateJobsTable001 : Migration
    {
        protected override void Up()
        {
            Execute(@"CREATE TABLE dbo.Jobs (
                Id INT NOT NULL IDENTITY(1,1),
                Name VARCHAR(1024) NOT NULL,
                HangfireJobId UNIQUEIDENTIFIER NOT NULL,
                Cron VARCHAR(128) NOT NULL,
                CONSTRAINT PK_Jobs PRIMARY KEY CLUSTERED (Id ASC)
            )");
        }

        protected override void Down()
        {
            Execute("DROP TABLE dbo.Jobs");
        }
    }
}
