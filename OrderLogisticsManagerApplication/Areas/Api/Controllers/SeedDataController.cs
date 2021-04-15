using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderLogisticsManagerApplication.Areas.Identity.Data;
using OrderLogisticsManagerApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderLogisticsManagerApplication.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDataController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ApplicationIdentityContext applicationIdentityContext;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public SeedDataController(ApplicationDbContext applicationDbContext, ApplicationIdentityContext applicationIdentityContext, UserManager<ApplicationUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.applicationIdentityContext = applicationIdentityContext;
            this.userManager = userManager;
        }

        // GET: api/<SeedDataController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            // UserStatues
            applicationDbContext.UserStatuses.Add(new UserStatus() { StatusDescription = "Active" });
            applicationDbContext.UserStatuses.Add(new UserStatus() { StatusDescription = "Inactive" });

            // CardStatus
            applicationDbContext.CardStatuses.Add(new CardStatus() { StatusDescription = "Active" });
            applicationDbContext.CardStatuses.Add(new CardStatus() { StatusDescription = "Inactive" });

            // Priority
            applicationDbContext.Priorities.Add(new Priority() { Description = "High" });
            applicationDbContext.Priorities.Add(new Priority() { Description = "Normal" });
            applicationDbContext.Priorities.Add(new Priority() { Description = "Low" });

            // Component
            applicationDbContext.Components.Add(new Component()
            {
                ComponentPartNumber = 100,
                ComponentName = "Radio -> TestComponent",
                ComponentHeigth = 30,
                ComponentWidth = 40,
                ComponentDepth = 30,
                ComponentWeigth = 2.5,
            });
            applicationDbContext.Components.Add(new Component()
            {
                ComponentPartNumber = 200,
                ComponentName = "ATC hoveddatamat -> TestComponent",
                ComponentHeigth = 50,
                ComponentWidth = 60,
                ComponentDepth = 50,
                ComponentWeigth = 3.7,
            });
            applicationDbContext.Components.Add(new Component()
            {
                ComponentPartNumber = 300,
                ComponentName = "InformationsDisplay -> TestComponent",
                ComponentHeigth = 30,
                ComponentWidth = 120,
                ComponentDepth = 15,
                ComponentWeigth = 5,
            });

            // PackingMaterial
            applicationDbContext.PackingMaterials.Add(new PackingMaterial()
            {
                MaterialPartNumber = 400,
                MaterialName = "PapKasse -> TestMateriale",
                HasDimension = true,
                MaterialHeigth = 60,
                MaterialWidth = 60,
                MaterialDepth = 60,
                MaterialWeigth = 0.5
            });
            applicationDbContext.PackingMaterials.Add(new PackingMaterial()
            {
                MaterialPartNumber = 500,
                MaterialName = "Anti Statisk BoblePlast -> TestMateriale",
                HasDimension = false,
                MaterialWidth = 60
            });

            // WorkGroup
            applicationDbContext.WorkGroups.Add(new WorkGroup()
            {
                WorkGroupNumber = "100-120",
                WorkGroupName = "Location A"
            });
            applicationDbContext.WorkGroups.Add(new WorkGroup()
            {
                WorkGroupNumber = "100-130",
                WorkGroupName = "Location B"
            });
            applicationDbContext.WorkGroups.Add(new WorkGroup()
            {
                WorkGroupNumber = "100-140",
                WorkGroupName = "Location C"
            });
            applicationDbContext.WorkGroups.Add(new WorkGroup()
            {
                WorkGroupNumber = "100-150",
                WorkGroupName = "Location D"
            });
            applicationDbContext.WorkGroups.Add(new WorkGroup()
            {
                WorkGroupNumber = "100-160",
                WorkGroupName = "Location E"
            });

            applicationDbContext.SaveChanges();

            // user
            var result = await userManager.CreateAsync(new ApplicationUser()
            {
                Email = "testman@test.com",
                UserName = "testman@test.com"
            }, "User2Pass@DSB");

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var userIdentity = await userManager.FindByNameAsync("testman@test.com");

            applicationDbContext.Users.Add(new User()
            {
                FirstName = "Test",
                LastName = "Man",
                ApplicationUserGUID = userIdentity.Id,
                Status = applicationDbContext.UserStatuses.Where(x => x.StatusDescription == "Active").FirstOrDefault(),
                WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-120").FirstOrDefault()
            });

            applicationDbContext.SaveChanges();

            // card
            applicationDbContext.Card.Add(new Card()
            {
                CardNumber = "1234567890",
                Status = applicationDbContext.CardStatuses.Where(x => x.StatusDescription == "Active").FirstOrDefault(),
                User = applicationDbContext.Users.Where(x => x.FirstName == "Test").FirstOrDefault(),
            });

            // Log Seeding
            applicationDbContext.Logs.Add(new Log() 
            { 
                LogAction = "Create",
                LogDescription = "Seeding Testdata",
                LogOn = "Both Databases",
                LogTime = DateTime.Now,
                User = applicationDbContext.Users.Where(x => x.FirstName == "Test").FirstOrDefault()
            });

            applicationDbContext.SaveChanges();

            return Ok("Database Seeding Done");
        }
    }
}
