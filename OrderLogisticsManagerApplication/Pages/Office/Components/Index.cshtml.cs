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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Component> Component { get;set; }

        public async Task OnGetAsync()
        {
            Component = await _context.Components.ToListAsync();
        }
    }
}
