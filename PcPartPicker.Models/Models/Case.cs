using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Models.Models
{
    public class Case
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int CaseId { get; set; }

        [Required]
        public string Model { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        public decimal Price { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Type { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
