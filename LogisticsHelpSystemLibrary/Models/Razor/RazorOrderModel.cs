using LogisticsHelpSystemLibrary.Models.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Razor
{
    public class RazorOrderModel
    {
        
        [Display(Name = "Order Id")]
        public int OrderID { get; set; }

        [Required]
        [Display(Name = "Order Number")]
        public long OrderNumber { get; set; }

        [Required]
        [Display(Name = "Order FeedBack Number")]
        public long OrderFeedbackNumber { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public int OrderAmount { get; set; }

        [ComponentInDbValidation]
        [Required]
        [Display(Name = "Component Part Number")]
        public long ComponentPartNumber { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderStartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderEndDate { get; set; }

        
        [Display(Name = "Entered By")]
        public string OrderEnteredBy { get; set; }
    }
}
