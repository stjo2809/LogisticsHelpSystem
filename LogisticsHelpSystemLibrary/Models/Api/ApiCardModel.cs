using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiCardModel
    {
        
        public int CardId { get; set; }

        [Required]
        public string CardNumber { get; set; }

        public int CardStatusId { get; set; }

        public int UserId { get; set; }
    }
}
