using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLogisticsManagerApplication.Areas.Api.Models
{
    public class ApiOutputUserModel
    {
        
        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserStatus { get; set; }

        public string Role { get; set; }

        public string WorkGroup { get; set; }

        public int CardCount { get; set; }

        public bool HasActiveCard { get; set; }
    }
}
