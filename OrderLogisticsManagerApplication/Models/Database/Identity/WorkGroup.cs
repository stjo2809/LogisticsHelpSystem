using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderLogisticsManagerApplication.Areas.Identity.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.Identity
{
    public class WorkGroup
    {
        public int ID { get; set; }
        [Required]
        public string WorkGroupNumber { get; set; }
        [Required]
        public string WorkGroupName { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
