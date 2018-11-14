using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Data.Models
{
    public class Gpu
    {
        public int GpuId { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public string Memory { get; set; }

        public SystemBuild SystemBuild { get; set; }

        public int? SystemBuildId { get; set; }
    }
}
