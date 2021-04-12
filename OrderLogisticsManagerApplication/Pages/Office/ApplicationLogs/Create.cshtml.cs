using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;

namespace OrderLogisticsManagerApplication.Pages.Office.Logs
{
    public class CreateModel : PageModel
    {
        private readonly OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext _context;

        public CreateModel(OrderLogisticsManagerApplication.Models.Database.ApplicationDb.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Log Log { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Logs.Add(Log);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
