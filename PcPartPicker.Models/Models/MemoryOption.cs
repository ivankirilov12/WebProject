using PcPartPicker.Definitions;
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

        [Required(AllowEmptyStrings = false)]
        public string Model { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Manufacturer { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Memory Type")]
        public string MemoryType { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Memory Capacity")]
        [Range(Constants.MINIMUM_VALUE, Constants.MAXIMUM_VALUE)]
        public int MemoryCapacity { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Memory Frequency")]
        [Range(Constants.MINIMUM_VALUE, Constants.MAXIMUM_VALUE)]
        public float MemoryFrequency { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        [Range(Constants.MINIMUM_VALUE, Constants.MAXIMUM_VALUE)]
        public decimal Price { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
