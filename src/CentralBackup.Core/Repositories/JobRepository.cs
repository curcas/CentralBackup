using System.Collections.Generic;
using System.Linq;
using CentralBackup.Core.Entities;
using CentralBackup.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CentralBackup.Core.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly BackupContext _backupContext;

        public JobRepository(BackupContext backupContext)
        {
            _backupContext = backupContext;
        }

        public IList<Job> LoadAll()
        {
            return _backupContext.Jobs.ToList();
        }

        public Job Load(int id)
        {
            return _backupContext.Jobs.Include(p => p.Steps).SingleOrDefault(p => p.Id == id);
        }
    }
}