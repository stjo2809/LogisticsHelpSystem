using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;

namespace OrderLogisticsManagerApplication.Pages.Office.ApplicationLogs
{
    public class IndexModel : PageModel
    {
        private readonly LogisticsHelpSystemLibrary.Models.Database.ApplicationDb.ApplicationDbContext _context;

        public IndexModel(LogisticsHelpSystemLibrary.Models.Database.ApplicationDb.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Log> Log { get;set; }

        public async Task OnGetAsync()
        {
            Log = await _context.Logs
                .Include(l => l.User).ToListAsync();
        }
    }
}
