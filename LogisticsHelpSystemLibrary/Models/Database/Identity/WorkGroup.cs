using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationTestApp.Areas.Identity.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTestApp.Models
{
    public class WorkGroup
    {
        public int ID { get; set; }
        [Required]
        public string WorkGroupNumber { get; set; }
        [Required]
        public string WorkGroupName { get; set; }

        public ICollection<WebApplicationTestAppUser> Users { get; set; }
    }
}
