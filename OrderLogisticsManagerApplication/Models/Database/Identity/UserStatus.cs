using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.Identity
{
    public class UserStatus
    {
        public int ID { get; set; }
        [Required]
        public String StatusDescription { get; set; }
    }
}
