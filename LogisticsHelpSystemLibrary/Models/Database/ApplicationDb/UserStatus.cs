using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class UserStatus
    {
        [Key]
        public int UserStatusId { get; set; }
        [Required]
        public String StatusDescription { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
