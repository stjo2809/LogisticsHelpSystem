using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiPickupModel
    {
        public int PickupID { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime PickupTime { get; set; }
    }
}
