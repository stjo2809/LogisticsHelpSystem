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
    public class PickupRequestController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PickupRequestController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<PickupRequestController>
        [HttpGet]
        public IEnumerable<ApiPickupRequestModel> Get()
        {
            List<ApiPickupRequestModel> returnList = new();

            foreach (var pickupRequest in applicationDbContext.PickupRequests)
            {
                returnList.Add(new ApiPickupRequestModel()
                {
                    PickupRequestID = pickupRequest.PickupRequestID,
                    OrderId = pickupRequest.Order.OrderID,
                    UserId = pickupRequest.User.ApplicationUserGUID,
                    PickupRequestAmount = pickupRequest.PickupRequestAmount,
                    PickupRequestTime = pickupRequest.PickupRequestTime,
                    PickupId = pickupRequest.Pickup.PickupID
                });
            }

            return returnList;
        }

        // GET api/<PickupRequestController>/5
        [HttpGet("{id}")]
        public ApiPickupRequestModel Get(int id)
        {
            var pickupRequest = applicationDbContext.PickupRequests.Where(x => x.PickupRequestID == id).FirstOrDefault();

            return new ApiPickupRequestModel()
            {
                PickupRequestID = pickupRequest.PickupRequestID,
                OrderId = pickupRequest.Order.OrderID,
                UserId = pickupRequest.User.ApplicationUserGUID,
                PickupRequestAmount = pickupRequest.PickupRequestAmount,
                PickupRequestTime = pickupRequest.PickupRequestTime,
                PickupId = pickupRequest.Pickup.PickupID
            };
        }

        // POST api/<PickupRequestController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiPickupRequestModel value)
        {
            if (!applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).Any())
                return BadRequest($"Order does not exist - with InputValue: {value.OrderId}");

            if (!applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserId}");

            if (!applicationDbContext.Pickups.Where(x => x.PickupID == value.PickupId).Any() && value.PickupId != null)
                return BadRequest($"User does not exist - with InputValue: {value.PickupId}");

            applicationDbContext.Add(new PickupRequest()
            {
                Order = applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).FirstOrDefault(),
                User = applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).FirstOrDefault(),
                PickupRequestAmount = value.PickupRequestAmount,
                PickupRequestTime = value.PickupRequestTime,
                Pickup = value.PickupId == null ? null : applicationDbContext.Pickups.Where(x => x.PickupID == value.PickupId).FirstOrDefault()
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<PickupRequestController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiPickupRequestModel value)
        {
            if (!applicationDbContext.PickupRequests.Where(x => x.PickupRequestID == id).Any())
                return BadRequest($"PickupRequest does not exist - with InputValue: {id}");

            if (!applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).Any())
                return BadRequest($"Order does not exist - with InputValue: {value.OrderId}");

            if (!applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserId}");

            if (!applicationDbContext.Pickups.Where(x => x.PickupID == value.PickupId).Any() && value.PickupId != null)
                return BadRequest($"User does not exist - with InputValue: {value.PickupId}");

            var pickupRequest = applicationDbContext.PickupRequests.Where(x => x.PickupRequestID == id).FirstOrDefault();

            pickupRequest.Order = applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).FirstOrDefault();
            pickupRequest.User = applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).FirstOrDefault();
            pickupRequest.PickupRequestAmount = value.PickupRequestAmount;
            pickupRequest.PickupRequestTime = value.PickupRequestTime;
            pickupRequest.Pickup = value.PickupId == null ? null : applicationDbContext.Pickups.Where(x => x.PickupID == value.PickupId).FirstOrDefault();

            applicationDbContext.Update(pickupRequest);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<PickupRequestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.PickupRequests.Where(x => x.PickupRequestID == id).Any())
                return BadRequest($"PickupRequest does not exist - with InputValue: {id}");

            var pickupRequest = applicationDbContext.PickupRequests.Where(x => x.PickupRequestID == id).FirstOrDefault();

            applicationDbContext.Remove(pickupRequest);
            applicationDbContext.SaveChanges();

            return Ok();
        }
    }
}
