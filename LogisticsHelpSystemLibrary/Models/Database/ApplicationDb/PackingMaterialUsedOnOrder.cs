using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class PackingMaterialUsedOnOrder
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        public Order Order { get; set; }

        [ForeignKey("PackingMaterial")]
        public int MaterialID { get; set; }

        [Required]
        public PackingMaterial Material { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}
