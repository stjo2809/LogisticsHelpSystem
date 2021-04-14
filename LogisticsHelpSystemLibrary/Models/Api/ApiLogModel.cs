using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Api
{
    public class ApiLogModel
    {
        public int LogID { get; set; }

        public DateTime LogTime { get; set; }

        public string LogAction { get; set; }

        public string LogOn { get; set; }

        public string LogDescription { get; set; }

        public int UserId { get; set; }
    }
}
