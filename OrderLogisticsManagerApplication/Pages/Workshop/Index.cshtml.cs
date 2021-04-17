using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderLogisticsManagerApplication.Pages.Workshop
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<SelectListItem> WorkGroups { get; set; }
        public string SelectedWorkGroup { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            WorkGroups = _context.WorkGroups.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.WorkGroupNumber.ToString(),
                                      Text = $"{a.WorkGroupNumber} - {a.WorkGroupName}"
                                  }).ToList();
            return Page();
        }

        public IActionResult OnPost()
        {
            SelectedWorkGroup = Request.Form["SelectWorkGroup"];
            var buttonPressed = Request.Form["ButtonPressed"].FirstOrDefault();
            var pageName = Regex.Replace(buttonPressed, @"\s+", String.Empty);
            var fullPageName = $"/Workshop/{pageName}";


            return RedirectToPage(fullPageName,new { SelectedWorkGroup });
        }
    }
}
