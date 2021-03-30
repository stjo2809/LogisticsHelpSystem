using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.Application
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserGUID { get; set; }
    }
}
