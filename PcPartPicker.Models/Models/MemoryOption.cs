using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Models.Models
{
    public class MemoryOption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int MemoryOptionId { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public string MemoryType { get; set; }

        public int MemoryCapacity { get; set; }

        public float MemoryFrequency { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        public decimal Price { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
