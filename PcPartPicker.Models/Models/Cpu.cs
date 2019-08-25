using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PcPartPicker.Definitions;

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

        [Required(AllowEmptyStrings = false)]
        public string Manufacturer { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Socket { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [Range(Constants.MINIMUM_VALUE, Constants.MAXIMUM_VALUE)]
        [Display(Name = "Number of Physical Cores")]
        public int NumberOfCores { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [Range(Constants.MINIMUM_VALUE, Constants.MAXIMUM_VALUE)]
        [Display(Name = "Cache Memory Size")]
        public int CacheMemory { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
