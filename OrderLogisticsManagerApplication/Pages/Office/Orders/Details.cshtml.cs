using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;

namespace OrderLogisticsManagerApplication.Pages.Office.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly LogisticsHelpSystemLibrary.Models.Database.ApplicationDb.ApplicationDbContext _context;

        public DetailsModel(LogisticsHelpSystemLibrary.Models.Database.ApplicationDb.ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(o => o.Component)
                .Include(o => o.Priority)
                .Include(o => o.WorkGroup).FirstOrDefaultAsync(m => m.OrderID == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
