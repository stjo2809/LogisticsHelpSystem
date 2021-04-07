using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.ApplicationDb
{
    public class Pickup
    {
        [Key]
        public int PickupID { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public DateTime PickupTime { get; set; }

        public virtual ICollection<PickupRequest> PickupRequests { get; set; }
    }
}
