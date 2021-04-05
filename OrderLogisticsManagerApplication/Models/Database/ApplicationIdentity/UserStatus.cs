using OrderLogisticsManagerApplication.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.ApplicationIdentity
{
    public class UserStatus
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String StatusDescription { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
