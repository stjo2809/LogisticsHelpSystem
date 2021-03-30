using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTestApp.Models.Database
{
    public class Log
    {
        public int LogID { get; set; }

        public DateTime LogTime { get; set; }

        public string LogAction { get; set; }

        public string LogOn { get; set; }

        public string LogDescription { get; set; }

        public User User { get; set; }
    }
}
