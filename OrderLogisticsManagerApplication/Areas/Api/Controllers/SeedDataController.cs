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
            #region UserStatues
                applicationDbContext.UserStatuses.Add(new UserStatus() { StatusDescription = "Active" });
                applicationDbContext.UserStatuses.Add(new UserStatus() { StatusDescription = "Inactive" });
            #endregion

            applicationDbContext.SaveChanges();

            #region CardStatus
            applicationDbContext.CardStatuses.Add(new CardStatus() { StatusDescription = "Active" });
                applicationDbContext.CardStatuses.Add(new CardStatus() { StatusDescription = "Inactive" });
            #endregion

            applicationDbContext.SaveChanges();

            #region Priority
            applicationDbContext.Priorities.Add(new Priority() { Description = "High" });
                applicationDbContext.Priorities.Add(new Priority() { Description = "Normal" });
                applicationDbContext.Priorities.Add(new Priority() { Description = "Low" });
            #endregion

            applicationDbContext.SaveChanges();

            #region Component
            applicationDbContext.Components.Add(new Component()
                {
                    ComponentPartNumber = "100",
                    ComponentName = "Radio -> TestComponent",
                    ComponentHeigth = (float)30,
                    ComponentWidth = (float)40,
                    ComponentDepth = (float)30,
                    ComponentWeigth = (float)2.5,
                });
                applicationDbContext.Components.Add(new Component()
                {
                    ComponentPartNumber = "200",
                    ComponentName = "ATC hoveddatamat -> TestComponent",
                    ComponentHeigth = (float)50,
                    ComponentWidth = (float)60,
                    ComponentDepth = (float)50,
                    ComponentWeigth = (float)3.7,
                });
                applicationDbContext.Components.Add(new Component()
                {
                    ComponentPartNumber = "300",
                    ComponentName = "InformationsDisplay -> TestComponent",
                    ComponentHeigth = (float)30,
                    ComponentWidth = (float)120,
                    ComponentDepth = (float)15,
                    ComponentWeigth = (float)5,
                });
            #endregion

            applicationDbContext.SaveChanges();

            #region PackingMaterial
            applicationDbContext.PackingMaterials.Add(new PackingMaterial()
                {
                    MaterialPartNumber = "400",
                    MaterialName = "PapKasse -> TestMateriale",
                    HasDimension = true,
                    MaterialHeigth = (float)60,
                    MaterialWidth = (float)60,
                    MaterialDepth = (float)60,
                    MaterialWeigth = (float)0.5
                });
                applicationDbContext.PackingMaterials.Add(new PackingMaterial()
                {
                    MaterialPartNumber = "500",
                    MaterialName = "Anti Statisk BoblePlast -> TestMateriale",
                    HasDimension = false,
                    MaterialWidth = (float)60
                });
            #endregion

            applicationDbContext.SaveChanges();

            #region  WorkGroup
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
            #endregion

            applicationDbContext.SaveChanges();

            #region Order
            applicationDbContext.Orders.Add(new Order()
            {
                OrderNumber = "PO100",
                OrderFeedbackNumber = "FB100",
                OrderAmount = 2,
                OrderStartDate = DateTime.Now,
                OrderEndDate = DateTime.Now,
                Priority = applicationDbContext.Priorities.FirstOrDefault(),
                WorkGroup = applicationDbContext.WorkGroups.FirstOrDefault(),
                Component = applicationDbContext.Components.FirstOrDefault()
            });
            #endregion

            applicationDbContext.SaveChanges();

            #region  user
                var result = await userManager.CreateAsync(new ApplicationUser()
                {
                    Email = "testman@test.com",
                    UserName = "testman@test.com",
                    EmailConfirmed = true
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
            #endregion

            #region  card
                applicationDbContext.Card.Add(new Card()
                {
                    CardNumber = "1234567890",
                    Status = applicationDbContext.CardStatuses.Where(x => x.StatusDescription == "Active").FirstOrDefault(),
                    User = applicationDbContext.Users.Where(x => x.FirstName == "Test").FirstOrDefault(),
                });
            #endregion

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
