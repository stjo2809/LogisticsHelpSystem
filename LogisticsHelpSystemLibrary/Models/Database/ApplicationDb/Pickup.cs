using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class Pickup
    {
        [Key]
        public int PickupID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public DateTime PickupTime { get; set; }

        public virtual ICollection<PickupRequest> PickupRequests { get; set; }
    }
}
