using System.Collections.Generic;
using CentralBackup.Core.Entities;

namespace CentralBackup.Core.Interfaces.Repositories
{
    public interface IJobRepository
    {
        IList<Job> LoadAll();
        Job Load(int id);
    }
}