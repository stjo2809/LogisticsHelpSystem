using OrderLogisticsManagerApplication.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.ApplicationIdentity
{
    public class WorkGroup
    {
        [Key]
        public int WorkGroupId { get; set; }
        [Required]
        public string WorkGroupNumber { get; set; }
        [Required]
        public string WorkGroupName { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
