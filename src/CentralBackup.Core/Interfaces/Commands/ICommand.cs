using System.Collections.Generic;
using CentralBackup.Core.Entities;

namespace CentralBackup.Core.Interfaces.Commands
{
    public interface ICommand
    {
        void Execute(IDictionary<string, string> configuration);
    }
}