using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Database.ApplicationDb
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(450)]
        public string ApplicationUserGUID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public int UserStatusId { get; set; }

        [Required]
        public UserStatus Status { get; set; }

        public WorkGroup WorkGroup { get; set; }

        public virtual ICollection<Card> Cards { get; set; }

        public virtual string FullName { get { return $"{ FirstName } { LastName }"; } }
    }
}
