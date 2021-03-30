using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.Application
{
    public class PackingMaterial
    {
        [Key]
        public int MaterialID { get; set; }

        [Required]
        public int MaterialPartNumber { get; set; }

        [Required]
        public string MaterialName { get; set; }

        public bool HasDimension { get; set; }

        public float MaterialWidth { get; set; }

        public float MaterialHeigth { get; set; }

        public float MaterialDepth { get; set; }

        public float MaterialWeigth { get; set; }
    }
}
