using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database
{
    public class CardStatus
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string StatusDescription { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
