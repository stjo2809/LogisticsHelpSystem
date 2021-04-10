using Microsoft.AspNetCore.Mvc;
using OrderLogisticsManagerApplication.Areas.Api.Models;
using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderLogisticsManagerApplication.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PickupController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<PickupController>
        [HttpGet]
        public IEnumerable<ApiPickupModel> Get()
        {
            List<ApiPickupModel> returnList = new();

            foreach (var pickup in applicationDbContext.Pickups)
            {
                returnList.Add(new ApiPickupModel()
                {
                    PickupID = pickup.PickupID,
                    PickupTime = pickup.PickupTime,
                    UserId = pickup.User.ApplicationUserGUID
                });
            }

            return returnList;
        }

        // GET api/<PickupController>/5
        [HttpGet("{id}")]
        public ApiPickupModel Get(int id)
        {
            var pickup = applicationDbContext.Pickups.Where(x => x.PickupID == id).FirstOrDefault();

            return new ApiPickupModel()
            {
                PickupID = pickup.PickupID,
                PickupTime = pickup.PickupTime,
                UserId = pickup.User.ApplicationUserGUID
            };
        }

        // POST api/<PickupController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiPickupModel value)
        {
            if (!applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserId}");

            applicationDbContext.Add(new Pickup()
            {
                User = applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).FirstOrDefault(),
                PickupTime = value.PickupTime
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<PickupController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiPickupModel value)
        {
            if (!applicationDbContext.Pickups.Where(x => x.PickupID == id).Any())
                return BadRequest($"Pickup does not exist - with InputValue: {id}");

            if (!applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserId}");

            var pickup = applicationDbContext.Pickups.Where(x => x.PickupID == id).FirstOrDefault();

            pickup.User = applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).FirstOrDefault();
            pickup.PickupTime = value.PickupTime;

            applicationDbContext.Update(pickup);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<PickupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.Pickups.Where(x => x.PickupID == id).Any())
                return BadRequest($"Pickup does not exist - with InputValue: {id}");

            var pickup = applicationDbContext.Pickups.Where(x => x.PickupID == id).FirstOrDefault();

            if (pickup.PickupRequests.Count() == 0)
            {
                applicationDbContext.Remove(pickup);
                applicationDbContext.SaveChanges();
            }
            else
                return BadRequest("The pickup has PickupRequests that uses it, therefore is not deleted.");

            return Ok();
        }
    }
}
