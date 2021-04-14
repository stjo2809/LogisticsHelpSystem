using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiUserStatusModel
    {
        public int UserStatusId { get; set; }
        [Required]
        public String StatusDescription { get; set; }
    }
}
