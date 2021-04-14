using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace OrderLogisticsManagerApplication.Pages.Office.Components
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Component Component { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Component = await _context.Components.FirstOrDefaultAsync(m => m.ComponentID == id);

            if (Component == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
