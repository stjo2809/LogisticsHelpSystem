﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.ApplicationDb
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public long OrderNumber { get; set; }

        [Required]
        public long OrderFeedbackNumber { get; set; }

        [Required]
        public int OrderAmount { get; set; }

        [Required]
        public Component Component { get; set; }

        [Required]
        public DateTime OrderStartDate { get; set; }

        [Required]
        public DateTime OrderEndDate { get; set; }

        [Required]
        public User OrderEnteredBy { get; set; }

        public ICollection<Delivery> Delivered { get; set; }

        public ICollection<PickupRequest> PickupRequested { get; set; }

        public ICollection<PackingMaterialUsedOnOrder> PackingMaterialUsed { get; set; }
    }
}
