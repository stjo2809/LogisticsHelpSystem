using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderLogisticsManagerApplication.Pages.Office.Orders
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RazorOrderModel Order { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var LoggedInUser =

            _context.Orders.Add(new Order()
            {
                OrderNumber = Order.OrderNumber,
                OrderFeedbackNumber = Order.OrderFeedbackNumber,
                Component = _context.Components.Where(x => x.ComponentPartNumber == Order.ComponentPartNumber).FirstOrDefault(),
                OrderAmount = Order.OrderAmount,
                OrderStartDate = Order.OrderStartDate,
                OrderEndDate = Order.OrderEndDate,
                //OrderEnteredBy = _context.Users.Where(x => x.ApplicationUserGUID == LoggedInUser.Id).FirstOrDefault()
            });

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
