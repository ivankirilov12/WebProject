using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Data.Models
{
    public class Cpu
    {
        public int CpuId { get; set; }

        public string Model { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public string Manufacturer { get; set; }

        public string Socket { get; set; }

        public int NumberOfCores { get; set; }

        public int CacheMemory { get; set; }

        public SystemBuild SystemBuild { get; set; }

        public int SystemBuildId { get; set; }
    }
}
