using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiOrderModel
    {
        public int OrderID { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public string OrderFeedbackNumber { get; set; }

        [Required]
        public int OrderAmount { get; set; }

        [Required]
        public int ComponentId { get; set; }

        [Required]
        public int PriorityID { get; set; }

        [Required]
        public DateTime OrderStartDate { get; set; }

        [Required]
        public DateTime OrderEndDate { get; set; }

        [Required]
        public int WorkGroupID { get; set; }
    }
}
