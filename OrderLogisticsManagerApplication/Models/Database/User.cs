using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Models.Database
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [MaxLength(450)]
        public string ApplicationUserGUID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public UserStatus Status { get; set; }

        [Required]
        public WorkGroup WorkGroup { get; set; }

        public ICollection<Card> Cards { get; set; }

        public string FullName { get { return $"{ FirstName } { LastName }"; } }
    }
}
