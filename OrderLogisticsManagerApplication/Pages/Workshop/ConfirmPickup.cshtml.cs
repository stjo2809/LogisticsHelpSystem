using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Filters;
using System.ComponentModel.DataAnnotations;

namespace OrderLogisticsManagerApplication.Pages.Workshop
{
    public class ConfirmPickupModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string WorkGroupNumber { get; set; }

        [Required]
        [CardInDbValidationAttribute]
        [BindProperty]
        public string CardNumber { get; set; }

        public ConfirmPickupModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string SelectedWorkGroup)
        {
            this.WorkGroupNumber = SelectedWorkGroup;
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var pickupEntity = new Pickup()
            {
                PickupTime = DateTime.Now,
                User = _context.Card.Where(x => x.CardNumber == CardNumber).FirstOrDefault().User
            };

            _context.Pickups.Add(pickupEntity);
            _context.SaveChanges();

            var pickupRequests = _context.PickupRequests.Where(x => x.Pickup == null && x.User.WorkGroup.WorkGroupNumber == WorkGroupNumber);

            foreach (var request in pickupRequests)
            {
                request.Pickup = pickupEntity;
                _context.Update(request);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}