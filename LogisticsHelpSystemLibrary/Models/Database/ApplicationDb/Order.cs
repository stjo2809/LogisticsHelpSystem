using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
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

        [ForeignKey("Component")]
        public int ComponentID { get; set; }

        [Required]
        [Display(Name = "Component")]
        public Component Component { get; set; }
        
        [ForeignKey("Priority")]
        public int PriorityID { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderStartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderEndDate { get; set; }

        [ForeignKey("WorkGroup")]
        public int WorkGroupID { get; set; }

        [Required]
        public WorkGroup WorkGroup { get; set; }

        public virtual ICollection<Delivery> Delivered { get; set; }

        public virtual ICollection<PickupRequest> PickupRequested { get; set; }

        public virtual ICollection<PackingMaterialUsedOnOrder> PackingMaterialUsed { get; set; }
    }
}
