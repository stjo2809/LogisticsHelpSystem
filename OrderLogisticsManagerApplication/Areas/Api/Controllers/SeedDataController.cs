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
                    ComponentName = "Radio",
                    ComponentHeigth = (float)30,
                    ComponentWidth = (float)40,
                    ComponentDepth = (float)30,
                    ComponentWeigth = (float)2.5,
                });
                applicationDbContext.Components.Add(new Component()
                {
                    ComponentPartNumber = "200",
                    ComponentName = "ATC hoveddatamat",
                    ComponentHeigth = (float)50,
                    ComponentWidth = (float)60,
                    ComponentDepth = (float)50,
                    ComponentWeigth = (float)3.7,
                });
                applicationDbContext.Components.Add(new Component()
                {
                    ComponentPartNumber = "300",
                    ComponentName = "InformationsDisplay",
                    ComponentHeigth = (float)30,
                    ComponentWidth = (float)120,
                    ComponentDepth = (float)15,
                    ComponentWeigth = (float)5,
                });
                applicationDbContext.Components.Add(new Component()
                {
                    ComponentPartNumber = "400",
                    ComponentName = "BremseComputer",
                    ComponentHeigth = (float)20,
                    ComponentWidth = (float)60,
                    ComponentDepth = (float)30,
                    ComponentWeigth = (float)3,
                });
                applicationDbContext.Components.Add(new Component()
                {
                    ComponentPartNumber = "500",
                    ComponentName = "FBC TogComputer",
                    ComponentHeigth = (float)10,
                    ComponentWidth = (float)40,
                    ComponentDepth = (float)40,
                    ComponentWeigth = (float)2.3,
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
                    OrderEndDate = DateTime.Now.AddDays(10),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "High").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-120").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "100").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO200",
                    OrderFeedbackNumber = "FB200",
                    OrderAmount = 1,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(5),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "Normal").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-120").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "200").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO300",
                    OrderFeedbackNumber = "FB300",
                    OrderAmount = 5,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(25),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "Normal").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-130").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "300").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO400",
                    OrderFeedbackNumber = "FB400",
                    OrderAmount = 1,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(5),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "Normal").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-130").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "400").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO500",
                    OrderFeedbackNumber = "FB500",
                    OrderAmount = 3,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(15),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "Low").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-140").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "500").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO600",
                    OrderFeedbackNumber = "FB600",
                    OrderAmount = 4,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(20),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "Normal").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-140").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "100").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO700",
                    OrderFeedbackNumber = "FB700",
                    OrderAmount = 1,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(5),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "High").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-150").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "200").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO800",
                    OrderFeedbackNumber = "FB800",
                    OrderAmount = 2,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(10),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "Normal").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-150").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "300").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO900",
                    OrderFeedbackNumber = "FB900",
                    OrderAmount = 1,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(5),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "Normal").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-160").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "400").FirstOrDefault()
                });
                applicationDbContext.Orders.Add(new Order()
                {
                    OrderNumber = "PO1000",
                    OrderFeedbackNumber = "FB1000",
                    OrderAmount = 1,
                    OrderStartDate = DateTime.Now,
                    OrderEndDate = DateTime.Now.AddDays(5),
                    Priority = applicationDbContext.Priorities.Where(x => x.Description == "Normal").FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == "100-160").FirstOrDefault(),
                    Component = applicationDbContext.Components.Where(x => x.ComponentPartNumber == "500").FirstOrDefault()
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

            applicationDbContext.SaveChanges();

            #region  card
            applicationDbContext.Card.Add(new Card()
                {
                    CardNumber = "1234567890",
                    Status = applicationDbContext.CardStatuses.Where(x => x.StatusDescription == "Active").FirstOrDefault(),
                    User = applicationDbContext.Users.Where(x => x.FirstName == "Test").FirstOrDefault(),
                });
            #endregion

            applicationDbContext.SaveChanges();

            #region Deliveries
            foreach (var item in applicationDbContext.Orders.ToList())
            {
                if (item.OrderNumber == "PO1000")
                continue;

                applicationDbContext.Deliveries.Add(new Delivery()
                {
                    DeliveryAmount = item.OrderAmount,
                    DeliveryTime = DateTime.Now,
                    OrderID = item.OrderID,
                    UserID = applicationDbContext.Users.FirstOrDefault().UserID
                });
            }
            #endregion

            applicationDbContext.SaveChanges();

            #region PickupRequests
            var orderlist = applicationDbContext.Orders.ToList();

            for (int i = 0; i < orderlist.Count; i++)
            {
                if (i % 2 != 0)
                    continue;

                applicationDbContext.PickupRequests.Add(new PickupRequest()
                {
                    OrderID = orderlist[i].OrderID,
                    PickupRequestAmount = orderlist[i].OrderAmount,
                    PickupRequestTime = DateTime.Now,
                    UserID = applicationDbContext.Users.FirstOrDefault().UserID
                });
            };
            
            #endregion

            applicationDbContext.SaveChanges();

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
