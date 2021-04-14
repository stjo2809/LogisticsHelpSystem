using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiCardStatusModel
    {
        public int CardStatusId { get; set; }

        [Required]
        public string StatusDescription { get; set; }
    }
}
