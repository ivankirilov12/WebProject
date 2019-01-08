using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Models.Models
{
    public class Cpu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int CpuId { get; set; }

        [Required]
        public string Model { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        public decimal Price { get; set; }

        public string Manufacturer { get; set; }

        public string Socket { get; set; }

        public int NumberOfCores { get; set; }

        public int CacheMemory { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
