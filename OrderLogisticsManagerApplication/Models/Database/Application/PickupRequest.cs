using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.Application
{
    public class PickupRequest
    {
        [Key]
        public int PickupRequestID { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int PickupRequestAmount { get; set; }

        [Required]
        public DateTime PickupRequestTime { get; set; }

        public Pickup Pickup { get; set; }
    }
}
