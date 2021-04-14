using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class PickupRequest
    {
        [Key]
        public int PickupRequestID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        public Order Order { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int PickupRequestAmount { get; set; }

        [Required]
        public DateTime PickupRequestTime { get; set; }
        
        public Pickup Pickup { get; set; }
    }
}
