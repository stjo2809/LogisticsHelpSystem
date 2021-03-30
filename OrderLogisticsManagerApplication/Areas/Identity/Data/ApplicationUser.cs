using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OrderLogisticsManagerApplication.Models.Database.Identity;


namespace OrderLogisticsManagerApplication.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public UserStatus Status { get; set; }

        [Required]
        public WorkGroup WorkGroup { get; set; }

        public ICollection<Card> Cards { get; set; }

        public string FullName { get { return $"{ FirstName } { LastName }"; } }
    }
}
