using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrderLogisticsManagerApplication.Pages.Warehouse
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext applicationDbContext;

        [BindProperty]
        public List<RazorWarehouseModel> Groups { get; set; }

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void OnGet()
        {
            Groups = ProcessPickupRequests();
        }

        private List<RazorWarehouseModel> ProcessPickupRequests()
        {
            List<RazorWarehouseModel> returnList = new();
            var DbGroups = applicationDbContext.WorkGroups.ToList();
            var ActivePickupRequests = applicationDbContext.PickupRequests.Where(x => x.Pickup == null);

            foreach (var group in DbGroups)
            {
                returnList.Add(new RazorWarehouseModel()
                {
                    WorkGroupName = group.WorkGroupName,
                    WorkGroupNumber = group.WorkGroupNumber
                });
            }

            foreach (var pickupRequest in ActivePickupRequests)
            {
                var userIngroup = applicationDbContext.Users.Where(x => x.UserID == pickupRequest.UserID).FirstOrDefault();
                var order = applicationDbContext.Orders.Where(x => x.OrderID == pickupRequest.OrderID).FirstOrDefault();
                returnList.Where(x => x.WorkGroupNumber == userIngroup.WorkGroup.WorkGroupNumber).FirstOrDefault().Requests.Add(new()
                {
                    Amount = pickupRequest.PickupRequestAmount,
                    ComponentName = applicationDbContext.Components.Where(x => x.ComponentID == order.ComponentID).FirstOrDefault().ComponentName
                });
            }

            return returnList;
        }
    }
}
