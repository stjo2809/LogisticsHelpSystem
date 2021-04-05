using OrderLogisticsManagerApplication.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.ApplicationIdentity
{
    public class Card
    {
        [Key]
        public int CardID { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public CardStatus Status { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
    }
}
