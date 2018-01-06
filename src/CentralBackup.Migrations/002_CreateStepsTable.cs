using SimpleMigrations;

namespace CentralBackup.Migrations
{
    [Migration(2, "Create Steps table")]
    public class CreateStepsTable002 : Migration
    {
        protected override void Up()
        {
            Execute(@"CREATE TABLE dbo.Steps (
                Id INT NOT NULL IDENTITY(1,1),
                Name VARCHAR(1024) NOT NULL,
                CommandType INT NOT NULL,
                JobId INT NOT NULL,
                CONSTRAINT PK_Steps PRIMARY KEY CLUSTERED (Id ASC),
                CONSTRAINT FK_Steps_Jobs FOREIGN KEY (JobId) REFERENCES dbo.Jobs(Id)
            )");
        }

        protected override void Down()
        {
            Execute("DROP TABLE dbo.Steps");
        }
    }
}