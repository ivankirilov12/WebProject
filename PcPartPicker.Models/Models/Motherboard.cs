using PcPartPicker.Definitions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Models.Models
{
    public class Motherboard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int MotherboardId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Model { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        [Range(Constants.MINIMUM_VALUE, Constants.MAXIMUM_VALUE)]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Cpu Socket")]
        public string CpuSocket { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
