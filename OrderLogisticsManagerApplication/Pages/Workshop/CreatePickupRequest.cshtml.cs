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
        public long OrderNumber { get; set; }

        [Required]
        [BindProperty]
        public int Amount { get; set; }

        public CreatePickupRequestModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }//order
        //cardnumber
        //amount
        //packingmaterial

    }
}
