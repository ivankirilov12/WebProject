using PcPartPicker.Definitions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Models.Models
{
    public class StorageOption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int StorageOptionId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Model { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        [Range(Constants.MINIMUM_VALUE, Constants.MAXIMUM_VALUE)]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Capacity { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
