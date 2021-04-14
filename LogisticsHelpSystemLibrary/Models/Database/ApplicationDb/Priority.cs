using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class Priority
    {
        [Key]
        public int PriorityID { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
