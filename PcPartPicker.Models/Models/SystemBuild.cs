using PcPartPicker.Definitions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Models.Models
{
    public class SystemBuild
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int SystemBuildId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(Constants.DESCRIPTION_MAXLENGTH)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        [Range(Constants.MINIMUM_VALUE, Constants.MAXIMUM_VALUE)]
        public decimal Price { get; set; }

        public Case Case { get; set; }

        public Cpu Cpu { get; set; }

        public Gpu Gpu { get; set; }

        public Motherboard Motherboard { get; set; }

        public MemoryOption MemoryOption { get; set; }

        public StorageOption StorageOption { get; set; }

        public string ImgUrl { get; set; }
    }
}
