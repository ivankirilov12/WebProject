using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Models.Models
{
    public class Gpu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int GpuId { get; set; }

        [Required]
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        public decimal Price { get; set; }

        public string Memory { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
