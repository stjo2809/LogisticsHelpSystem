﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.Application
{
    public class Component
    {
        [Key]
        public int ComponentID { get; set; }

        [Required]
        public int ComponentPartNumber { get; set; }

        [Required]
        public string ComponentName { get; set; }

        public float ComponentWidth { get; set; }

        public float ComponentHeigth { get; set; }

        public float ComponentDepth { get; set; }

        public float ComponentWeigth { get; set; }
    }
}
