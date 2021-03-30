using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.Application
{
    public class PackingMaterialUsedOnOrder
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public PackingMaterial Material { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}
