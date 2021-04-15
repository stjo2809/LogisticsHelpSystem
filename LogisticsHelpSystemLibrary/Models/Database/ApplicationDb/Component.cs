﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class Component
    {
        [Key]
        public int ComponentID { get; set; }

        [Required]
        public long ComponentPartNumber { get; set; }

        [Required]
        public string ComponentName { get; set; }

        public double? ComponentWidth { get; set; }

        public double? ComponentHeigth { get; set; }

        public double? ComponentDepth { get; set; }

        public double? ComponentWeigth { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
