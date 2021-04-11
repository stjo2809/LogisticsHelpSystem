using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;

namespace OrderLogisticsManagerApplication.Pages.Workshop.Pickups
{
    public class IndexModel : PageModel
    {
        private readonly OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext _context;

        public IndexModel(OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Pickup> Pickup { get;set; }

        public async Task OnGetAsync()
        {
            Pickup = await _context.Pickups.ToListAsync();
        }
    }
}
