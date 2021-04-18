using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Filters
{
    public class OrderInDbValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var applicationDbContext = validationContext.GetRequiredService<ApplicationDbContext>();

            if (applicationDbContext.Orders.Where(x => x.OrderNumber == (string)value).Any())
                return ValidationResult.Success;
            else
                return new ValidationResult("Order Number not found in Database");
        }
    }
}
