using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiWorkGroupModel
    {
        public int WorkGroupId { get; set; }
        [Required]
        public string WorkGroupNumber { get; set; }
        [Required]
        public string WorkGroupName { get; set; }
    }
}
