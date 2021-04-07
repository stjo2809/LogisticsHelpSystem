using OrderLogisticsManagerApplication.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Areas.Api.Models
{
    public class ApiUserModel
    {
        
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

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
