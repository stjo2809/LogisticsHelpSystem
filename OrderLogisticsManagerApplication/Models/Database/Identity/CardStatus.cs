using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTestApp.Models
{
    public class CardStatus
    {
        public int ID { get; set; }
        
        [Required]
        public string StatusDescription { get; set; }
    }
}
