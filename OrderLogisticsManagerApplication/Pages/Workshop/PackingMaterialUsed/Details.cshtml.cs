using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;

namespace OrderLogisticsManagerApplication.Pages.Workshop.PackingMaterialUsed
{
    public class DetailsModel : PageModel
    {
        private readonly OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext _context;

        public DetailsModel(OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext context)
        {
            _context = context;
        }

        public PackingMaterialUsedOnOrder PackingMaterialUsedOnOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PackingMaterialUsedOnOrder = await _context.PackingMaterialUsedOnOrders.FirstOrDefaultAsync(m => m.ID == id);

            if (PackingMaterialUsedOnOrder == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
