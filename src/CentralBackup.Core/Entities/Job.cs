using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentralBackup.Core.Entities
{
    public class Job
    {
        public int Id { get; set; }

        [MaxLength(1024)]
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid HangfireJobId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Cron { get; set; }

        public IList<Step> Steps { get; set; }
    }
}