using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Filters;
using LogisticsHelpSystemLibrary.Models.Razor;
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
        public List<RazorMaterialUsedModel> MaterialUsed { get; set; }

        [BindProperty]
        public List<SelectListItem> PackingMaterialList { get; set; }
        [BindProperty]
        public string SelectedMaterial { get; set; }

        [Required]
        [BindProperty]
        public int PackingMaterialAmount { get; set; }

        public CreatePickupRequestModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string OrderNumber, string CardNumber)
        {
            if (MaterialUsed == null)
                MaterialUsed = new();

            foreach (var item in _context.PackingMaterialUsedOnOrders.Where(x => x.Order.OrderNumber == OrderNumber).ToList())
            {
                var material = _context.PackingMaterials.Where(x => x.MaterialID == item.MaterialID).FirstOrDefault();

                MaterialUsed.Add(new RazorMaterialUsedModel()
                {
                    
                    MaterialPartNumber = material.MaterialPartNumber,
                    MaterialName = material.MaterialName,
                    Amount = item.Amount
                });
            };
            
            
            PackingMaterialList = _context.PackingMaterials.Select(a => 
                                                            new SelectListItem 
                                                            { 
                                                                Value = a.MaterialPartNumber,
                                                                Text = a.MaterialName
                                                            }).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var buttonPressed = Request.Form["ButtonPressed"].FirstOrDefault();

            if (buttonPressed == "Create")
            {
                var request = new PickupRequest()
                {
                    Order = _context.Orders.Where(x => x.OrderNumber == OrderNumber).FirstOrDefault(),
                    PickupRequestAmount = Amount,
                    PickupRequestTime = DateTime.Now,
                    UserID = _context.Card.Where(x => x.CardNumber == CardNumber).FirstOrDefault().UserId,
                };

                _context.Add(request);
                _context.SaveChanges();

                return RedirectToPage("./Index");
            }
            else
            {
                var materialToOrder = new PackingMaterialUsedOnOrder()
                {
                    Order = _context.Orders.Where(x => x.OrderNumber == OrderNumber).FirstOrDefault(),
                    Material = _context.PackingMaterials.Where(x => x.MaterialPartNumber == SelectedMaterial).FirstOrDefault(),
                    Amount = PackingMaterialAmount
                };

                _context.PackingMaterialUsedOnOrders.Add(materialToOrder);
                _context.SaveChanges();

                return RedirectToPage("/Workshop/CreatePickupRequest", new { OrderNumber, CardNumber });
            }
        }
        //order
        //cardnumber
        //amount
        //packingmaterial

    }
}
