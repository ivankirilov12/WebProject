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

        [Required]
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(10, 5)")]
        public decimal Price { get; set; }

        public string Type { get; set; }

        public string Capacity { get; set; }

        public ICollection<SystemBuild> SystemBuilds { get; set; }
    }
}
