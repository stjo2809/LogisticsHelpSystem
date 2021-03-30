using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTestApp.Models
{
    public class Card
    {
        public int ID { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public CardStatus Status { get; set; }
    }
}
