using OrderLogisticsManagerApplication.Models.Database.ApplicationIdentity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Areas.Api.Models
{
    public class ApiCardModel
    {
        
        public int CardId { get; set; }

        [Required]
        public string CardNumber { get; set; }

        public int CardStatusId { get; set; }

        public string UserId { get; set; }
    }
}
