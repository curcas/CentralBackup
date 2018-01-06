using Autofac;

namespace CentralBackup.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Repositories
            builder.RegisterType<Core.Repositories.JobRepository>().As<Core.Interfaces.Repositories.IJobRepository>();
            builder.RegisterType<Core.Repositories.ConfigurationRepository>().As<Core.Interfaces.Repositories.IConfigurationRepository>();

            //Commands
            builder.RegisterType<Commands.SqlServerFullBackup.SqlServerFullBackupCommand>()
                .Keyed<Core.Interfaces.Commands.ICommand>(Core.Interfaces.Commands.CommandType.SqlServerFullBackup);
            builder.RegisterType<Commands.DeleteFilesOlderThanXDays.DeleteFilesOlderThanXDays>()
                .Keyed<Core.Interfaces.Commands.ICommand>(Core.Interfaces.Commands.CommandType.DeleteFilesOlderThanXDays);
            builder.RegisterType<Commands.SqlServerTransactionLogBackup.SqlServerTransactionLogBackup>()
                .Keyed<Core.Interfaces.Commands.ICommand>(Core.Interfaces.Commands.CommandType.SqlServerTransactionLog);

            //Execution
            builder.RegisterType<Execution.JobExecutor>();
            builder.RegisterType<Execution.CommandFactory>();
        }
    }
}