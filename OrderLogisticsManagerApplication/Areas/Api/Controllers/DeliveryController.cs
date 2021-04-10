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
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DeliveryController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<DeliveryController>
        [HttpGet]
        public IEnumerable<ApiDeliveryModel> Get()
        {
            List<ApiDeliveryModel> returnList = new();

            foreach (var delivery in applicationDbContext.Deliveries)
            {
                returnList.Add(new ApiDeliveryModel()
                {
                     DeliveryID = delivery.DeliveryID,
                     OrderId = delivery.Order.OrderID,
                     UserId = delivery.User.ApplicationUserGUID,
                     DeliveryAmount = delivery.DeliveryAmount,
                     DeliveryTime = delivery.DeliveryTime
                });
            }

            return returnList;
        }

        // GET api/<DeliveryController>/5
        [HttpGet("{id}")]
        public ApiDeliveryModel Get(int id)
        {
            var delivery = applicationDbContext.Deliveries.Where(x => x.DeliveryID == id).FirstOrDefault();
            
            return new ApiDeliveryModel()
            {
                DeliveryID = delivery.DeliveryID,
                OrderId = delivery.Order.OrderID,
                UserId = delivery.User.ApplicationUserGUID,
                DeliveryAmount = delivery.DeliveryAmount,
                DeliveryTime = delivery.DeliveryTime
            };
        }

        // POST api/<DeliveryController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiDeliveryModel value)
        {
            if (!applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).Any())
                return BadRequest($"Order does not exist - with InputValue: {value.OrderId}");

            if (!applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserId}");

            applicationDbContext.Add(new Delivery()
            {
                Order = applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).FirstOrDefault(),
                User = applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).FirstOrDefault(),
                DeliveryAmount = value.DeliveryAmount,
                DeliveryTime = value.DeliveryTime
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<DeliveryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiDeliveryModel value)
        {
            if (!applicationDbContext.Deliveries.Where(x => x.DeliveryID == id).Any())
                return BadRequest($"Delivery does not exist - with InputValue: {id}");

            if (!applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).Any())
                return BadRequest($"Order does not exist - with InputValue: {value.OrderId}");

            if (!applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserId}");

            var delivery = applicationDbContext.Deliveries.Where(x => x.DeliveryID == id).FirstOrDefault();

            delivery.Order = applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).FirstOrDefault();
            delivery.User = applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.UserId).FirstOrDefault();
            delivery.DeliveryAmount = value.DeliveryAmount;
            delivery.DeliveryTime = value.DeliveryTime;

            applicationDbContext.Update(delivery);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<DeliveryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.Deliveries.Where(x => x.DeliveryID == id).Any())
                return BadRequest($"Delivery does not exist - with InputValue: {id}");

            var delivery = applicationDbContext.Deliveries.Where(x => x.DeliveryID == id).FirstOrDefault();

            applicationDbContext.Remove(delivery);
            applicationDbContext.SaveChanges();

            return Ok();
        }
    }
}
