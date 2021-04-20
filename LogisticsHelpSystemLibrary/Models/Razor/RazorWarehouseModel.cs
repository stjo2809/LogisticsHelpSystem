using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Razor
{
    public class RazorWarehouseModel
    {
        public string WorkGroupName { get; set; }
        public string WorkGroupNumber { get; set; }
        public List<RazorWarehousePickupRequestModel> Requests { get; set; } = new();
    }
}
