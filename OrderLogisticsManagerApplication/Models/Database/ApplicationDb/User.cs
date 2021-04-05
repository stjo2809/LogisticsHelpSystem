using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.ApplicationDb
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [MaxLength(450)]
        public string ApplicationUserGUID { get; set; }
    }
}
