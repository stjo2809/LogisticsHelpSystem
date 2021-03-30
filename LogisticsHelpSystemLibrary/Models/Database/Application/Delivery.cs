using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTestApp.Models.Database
{
    public class Delivery
    {
        [Key]
        public int DeliveryID { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int DeliveryAmount { get; set; }

        [Required]
        public DateTime DeliveryTime { get; set; }
    }
}
