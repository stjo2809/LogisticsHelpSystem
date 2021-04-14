using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiDeliveryModel
    {
        
        public int DeliveryID { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int DeliveryAmount { get; set; }

        [Required]
        public DateTime DeliveryTime { get; set; }
    }
}
