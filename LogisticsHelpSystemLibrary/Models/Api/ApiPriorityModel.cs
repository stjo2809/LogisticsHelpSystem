using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiPriorityModel
    {
        public int PriorityID { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
