using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        [Required]
        public string CardNumber { get; set; }

        public int CardStatusId { get; set; }
        [Required]
        public CardStatus Status { get; set; }

        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
