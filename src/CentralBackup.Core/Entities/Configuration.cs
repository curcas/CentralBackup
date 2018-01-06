using System.ComponentModel.DataAnnotations;

namespace CentralBackup.Core.Entities
{
    public class Configuration 
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Key { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Value { get; set; }

        public Step Step { get; set; }
    }
}