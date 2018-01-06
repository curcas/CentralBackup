using System;
using System.Collections.Generic;
using System.Text;
using SimpleMigrations;

namespace CentralBackup.Migrations
{
    [Migration(3, "Create Configurations table")]
    public class CreateConfigurationsTable003 : Migration
    {
        protected override void Up()
        {
            Execute(@"CREATE TABLE dbo.Configurations (
                Id INT NOT NULL IDENTITY(1,1),
                [Key] VARCHAR(128) NOT NULL,
                Value VARCHAR(2048) NOT NULL,
                StepId INT NOT NULL,
                CONSTRAINT PK_Configurations PRIMARY KEY CLUSTERED (Id ASC),
                CONSTRAINT FK_Configurations_Steps FOREIGN KEY (StepId) REFERENCES dbo.Steps(Id)
            )");
        }

        protected override void Down()
        {
            Execute("DROP TABLE dbo.Configurations");
        }
    }
}
