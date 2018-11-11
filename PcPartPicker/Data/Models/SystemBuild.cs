using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Data.Models
{
    public class SystemBuild
    {
        public int SystemBuildId { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public Case Case { get; set; }

        public Cpu Cpu { get; set; }

        public Gpu Gpu { get; set; }

        public Motherboard Motherboard { get; set; }

        public Ram Ram { get; set; }

        public Storage Storage { get; set; }
    }
}
