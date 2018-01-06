using System.Collections.Generic;
using System.Linq;
using CentralBackup.Core.Entities;
using CentralBackup.Core.Interfaces.Repositories;

namespace CentralBackup.Core.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly BackupContext _backupContext;

        public ConfigurationRepository(BackupContext backupContext)
        {
            _backupContext = backupContext;
        }

        public IDictionary<string, string> GetByStep(Step step)
        {
            return _backupContext.Configurations
                .Where(p => p.Step.Id == step.Id)
                .Select(p => new KeyValuePair<string, string>(p.Key, p.Value))
                .ToDictionary(p => p.Key, p => p.Value);
        }
    }
}