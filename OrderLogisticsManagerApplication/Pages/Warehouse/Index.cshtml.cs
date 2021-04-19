using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrderLogisticsManagerApplication.Pages.Warehouse
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext applicationDbContext;



        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void OnGet()
        {
            // linq group by
        }
    }
}
