using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Razor
{
    public class RazorWarehousePickupRequestModel
    {
        public int Amount { get; set; }

        public string ComponentName { get; set; }
    }
}
