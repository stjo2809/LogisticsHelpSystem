using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiUserModel
    {
        
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]        
        public string LastName { get; set; }

        [Required]
        public string UserStatus { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string WorkGroupNumber { get; set; }
    }
}
