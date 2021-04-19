using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrderLogisticsManagerApplication.Pages.Workshop
{
    public class ConfirmDeliveryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [Required]
        [CardInDbValidationAttribute]
        [BindProperty]
        public string CardNumber { get; set; }

        [Required]
        [OrderInDbValidation]
        [BindProperty]
        public string OrderNumber { get; set; }

        [Required]
        [BindProperty]
        public int Amount { get; set; }

        public ConfirmDeliveryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        { 
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var delivery = new Delivery()
            {
                Order = _context.Orders.Where(x => x.OrderNumber == OrderNumber).FirstOrDefault(),
                DeliveryAmount = Amount,
                DeliveryTime = DateTime.Now,
                UserID = _context.Card.Where(x => x.CardNumber == CardNumber).FirstOrDefault().UserId,
            };

            _context.Add(delivery);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
