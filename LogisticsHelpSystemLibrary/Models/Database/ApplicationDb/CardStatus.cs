using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class CardStatus
    {
        [Key]
        public int CardStatusId { get; set; }
        
        [Required]
        public string StatusDescription { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
