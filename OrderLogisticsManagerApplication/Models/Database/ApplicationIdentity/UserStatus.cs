﻿using OrderLogisticsManagerApplication.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.ApplicationIdentity
{
    public class UserStatus
    {
        [Key]
        public int UserStatusId { get; set; }
        [Required]
        public String StatusDescription { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
