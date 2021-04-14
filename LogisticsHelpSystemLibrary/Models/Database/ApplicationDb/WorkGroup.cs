﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class WorkGroup
    {
        [Key]
        public int WorkGroupId { get; set; }
        [Required]
        public string WorkGroupNumber { get; set; }
        [Required]
        public string WorkGroupName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
