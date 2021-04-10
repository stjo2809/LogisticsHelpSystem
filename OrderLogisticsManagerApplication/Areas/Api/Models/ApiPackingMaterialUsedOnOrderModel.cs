using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Areas.Api.Models
{
    public class ApiPackingMaterialUsedOnOrderModel
    {
        public int ID { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int MaterialId { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}
