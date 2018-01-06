using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CentralBackup.Core.Interfaces.Commands;

namespace CentralBackup.Core.Entities
{
    public class Step 
    {
        public int Id { get; set; }

        [MaxLength(1024)]
        [Required]
        public string Name { get; set; }

        [Required]
        public CommandType CommandType { get; set; }

        public Job Job { get; set; }
        public IList<Configuration> Configurations { get; set; }
    }
}