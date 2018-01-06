using System.Collections.Generic;
using CentralBackup.Core.Entities;

namespace CentralBackup.Core.Interfaces.Repositories
{
    public interface IConfigurationRepository
    {
        IDictionary<string, string> GetByStep(Step step);
    }
}