using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OrderLogisticsManagerApplication.Models.Database.ApplicationIdentity;

namespace OrderLogisticsManagerApplication.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public int UserStatusId { get; set; }
        [Required]
        public UserStatus Status { get; set; }

        public int WorkGroupId { get; set; }
        [Required]
        public WorkGroup WorkGroup { get; set; }

        public virtual ICollection<Card> Cards { get; set; }

        public virtual string FullName { get { return $"{ FirstName } { LastName }"; } }
    }
}
