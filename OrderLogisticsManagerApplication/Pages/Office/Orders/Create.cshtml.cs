using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderLogisticsManagerApplication.Models;
using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;
using OrderLogisticsManagerApplication.Models.RazorPageModels;

namespace OrderLogisticsManagerApplication.Pages.Office.Orders
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUserManager userManager;

        public CreateModel(ApplicationDbContext context, ApplicationUserManager userManager)
        {
            _context = context;
            this.userManager = userManager;
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

            var LoggedInUser = await userManager.FindByNameAsync(User.Identity.Name);

            _context.Orders.Add(new Order()
            {
                OrderNumber = Order.OrderNumber,
                OrderFeedbackNumber = Order.OrderFeedbackNumber,
                Component = _context.Components.Where(x => x.ComponentPartNumber == Order.ComponentPartNumber).FirstOrDefault(),
                OrderAmount = Order.OrderAmount,
                OrderStartDate = Order.OrderStartDate,
                OrderEndDate = Order.OrderEndDate,
                OrderEnteredBy = _context.Users.Where(x => x.ApplicationUserGUID == LoggedInUser.Id).FirstOrDefault()
            });

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
