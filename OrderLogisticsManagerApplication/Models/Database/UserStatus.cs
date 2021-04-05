using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database
{
    public class UserStatus
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String StatusDescription { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
