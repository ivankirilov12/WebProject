﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PcPartPicker.Data.Models
{
    public class Motherboard
    {
        public int MotherboardId { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public string CpuSocket { get; set; }

        public SystemBuild SystemBuild { get; set; }

        public int SystemBuildId { get; set; }
    }
}
