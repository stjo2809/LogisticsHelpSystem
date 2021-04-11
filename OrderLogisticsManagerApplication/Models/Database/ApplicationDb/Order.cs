using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database.ApplicationDb
{
    public class Order
    {
        [Key]
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

        [Required]
        [Display(Name = "Component")]
        public Component Component { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderStartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderEndDate { get; set; }

        [Required]
        [Display(Name = "Entered By")]
        public User OrderEnteredBy { get; set; }

        public virtual ICollection<Delivery> Delivered { get; set; }

        public virtual ICollection<PickupRequest> PickupRequested { get; set; }

        public virtual ICollection<PackingMaterialUsedOnOrder> PackingMaterialUsed { get; set; }
    }
}
