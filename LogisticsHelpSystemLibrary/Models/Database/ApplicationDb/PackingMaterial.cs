using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class PackingMaterial
    {
        [Key]
        public int MaterialID { get; set; }

        [Required]
        public long MaterialPartNumber { get; set; }

        [Required]
        public string MaterialName { get; set; }

        public bool HasDimension { get; set; }

        public double? MaterialWidth { get; set; }

        public double? MaterialHeigth { get; set; }

        public double? MaterialDepth { get; set; }

        public double? MaterialWeigth { get; set; }

        public virtual ICollection<PackingMaterialUsedOnOrder> packingMaterialUsedOnOrders { get; set; }
    }
}
