using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsHelpSystemLibrary.Models.Filters
{
    public class ComponentInDbValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var applicationDbContext = validationContext.GetRequiredService<ApplicationDbContext>();

            if (applicationDbContext.Components.Where(x => x.ComponentPartNumber == (long)value).Any())
                return ValidationResult.Success;
            else
                return new ValidationResult("Component Part Numbr not found in Database");
        }
    }
}
