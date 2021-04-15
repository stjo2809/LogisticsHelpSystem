using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;

namespace OrderLogisticsManagerApplication.Pages.Office.ApplicationLogs
{
    public class CreateModel : PageModel
    {
        private readonly LogisticsHelpSystemLibrary.Models.Database.ApplicationDb.ApplicationDbContext _context;

        public CreateModel(LogisticsHelpSystemLibrary.Models.Database.ApplicationDb.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserID"] = new SelectList(_context.Users, "UserID", "ApplicationUserGUID");
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
