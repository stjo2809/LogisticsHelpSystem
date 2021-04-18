using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderLogisticsManagerApplication.Pages.Workshop
{
    public class CreatePickupRequestModel : PageModel
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

        [BindProperty]
        public List<PackingMaterialUsedOnOrder> MaterialUsed { get; set; }

        [BindProperty]
        public List<SelectListItem> PackingMaterialList { get; set; }

        public CreatePickupRequestModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            MaterialUsed = _context.PackingMaterialUsedOnOrders.Where(x => x.Order.OrderNumber == OrderNumber).ToList();
            PackingMaterialList = _context.PackingMaterials.Select(a => 
                                                            new SelectListItem 
                                                            { 
                                                                Value = a.MaterialPartNumber,
                                                                Text = a.MaterialName
                                                            }).ToList();

        }

        public IActionResult OnPost()
        {
            var buttonPressed = Request.Form["ButtonPressed"].FirstOrDefault();

            if (buttonPressed == "Create")
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var request = new PickupRequest()
                {
                    Order = _context.Orders.Where(x => x.OrderNumber == OrderNumber).FirstOrDefault(),
                    PickupRequestAmount = Amount,
                    PickupRequestTime = DateTime.Now,
                    User = _context.Card.Where(x => x.CardNumber == CardNumber).FirstOrDefault().User,
                };

                _context.Add(request);
                _context.SaveChanges();

                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Workshop/AddPackingMaterialOnOrder", new { OrderNumber });
            }
        }
        //order
        //cardnumber
        //amount
        //packingmaterial

    }
}
