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
    public class DeleteModel : PageModel
    {
        private readonly OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext _context;

        public DeleteModel(OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PackingMaterialUsedOnOrder = await _context.PackingMaterialUsedOnOrders.FindAsync(id);

            if (PackingMaterialUsedOnOrder != null)
            {
                _context.PackingMaterialUsedOnOrders.Remove(PackingMaterialUsedOnOrder);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
