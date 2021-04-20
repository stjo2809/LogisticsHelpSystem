using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrderLogisticsManagerApplication.Pages.Office
{

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext applicationDbContext;

        [BindProperty]
        public int ComponentNotDeliveret { get; set; }

        [BindProperty]
        public int WorkedOn { get; set; }

        [BindProperty]
        public int PickupRequested { get; set; }

        [BindProperty]
        public int InSystem { get; set; }

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void OnGet()
        {
            var orders = applicationDbContext.Orders.ToList();

            ComponentNotDeliveret = orders.Where(x => x.Delivered == null).Count();
            WorkedOn = orders.Where(x => x.Delivered != null && x.PickupRequested == null).Count();
            PickupRequested = orders.Where(x => x.Delivered != null && x.PickupRequested != null).Count();
            InSystem = orders.Count;
        }
    }
}
