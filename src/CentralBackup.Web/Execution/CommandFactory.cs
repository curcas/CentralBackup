using Autofac;
using CentralBackup.Core.Interfaces.Commands;

namespace CentralBackup.Web.Execution
{
    public class CommandFactory
    {
        private readonly ILifetimeScope _scope;

        public CommandFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public ICommand Create(CommandType commandType)
        {
            return _scope.ResolveKeyed<ICommand>(commandType);
        }
    }
}