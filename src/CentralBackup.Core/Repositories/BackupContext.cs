using CentralBackup.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CentralBackup.Core.Repositories
{
    public class BackupContext : DbContext
    {
        public BackupContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
    }
}
