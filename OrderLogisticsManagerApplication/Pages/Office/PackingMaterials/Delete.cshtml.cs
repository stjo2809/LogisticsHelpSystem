using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace OrderLogisticsManagerApplication.Pages.Office.PackingMaterials
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PackingMaterial PackingMaterial { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PackingMaterial = await _context.PackingMaterials.FirstOrDefaultAsync(m => m.MaterialID == id);

            if (PackingMaterial == null)
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

            PackingMaterial = await _context.PackingMaterials.FindAsync(id);

            if (PackingMaterial != null)
            {
                _context.PackingMaterials.Remove(PackingMaterial);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
