using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace OrderLogisticsManagerApplication.Pages.Office.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
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

            Order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderID == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
