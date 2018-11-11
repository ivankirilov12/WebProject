using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Data.Models
{
    public class Ram
    {
        public int RamId { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public string MemoryType { get; set; }

        public int MemoryCapacity { get; set; }

        public float MemoryFrequency { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public SystemBuild SystemBuild { get; set; }

        public int SystemBuildId { get; set; }
    }
}
