using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Areas.Api.Models
{
    public class ApiOrderModel
    {
        public int OrderID { get; set; }

        [Required]
        public long OrderNumber { get; set; }

        [Required]
        public long OrderFeedbackNumber { get; set; }

        [Required]
        public int OrderAmount { get; set; }

        [Required]
        public int ComponentId { get; set; }

        [Required]
        public DateTime OrderStartDate { get; set; }

        [Required]
        public DateTime OrderEndDate { get; set; }

        [Required]
        public string OrderEnteredByUserId { get; set; }
    }
}
