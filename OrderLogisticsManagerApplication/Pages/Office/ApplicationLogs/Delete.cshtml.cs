using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;

namespace OrderLogisticsManagerApplication.Pages.Office.Logs
{
    public class DeleteModel : PageModel
    {
        private readonly OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext _context;

        public DeleteModel(OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Log Log { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Log = await _context.Logs.FirstOrDefaultAsync(m => m.LogID == id);

            if (Log == null)
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

            Log = await _context.Logs.FindAsync(id);

            if (Log != null)
            {
                _context.Logs.Remove(Log);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
