using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Areas.Api.Models
{
    public class ApiPickupRequestModel
    {
        public int PickupRequestID { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int PickupRequestAmount { get; set; }

        [Required]
        public DateTime PickupRequestTime { get; set; }

        public int? PickupId { get; set; }
    }
}
