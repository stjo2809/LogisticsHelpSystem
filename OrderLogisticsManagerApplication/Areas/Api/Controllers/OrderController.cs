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
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OrderController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<ApiOrderModel> Get()
        {
            List<ApiOrderModel> returnList = new();

            foreach (var order in applicationDbContext.Orders)
            {
                returnList.Add(new ApiOrderModel()
                {
                    OrderID = order.OrderID,
                    OrderNumber = order.OrderNumber,
                    OrderFeedbackNumber = order.OrderFeedbackNumber,
                    OrderAmount = order.OrderAmount,
                    ComponentId = order.Component.ComponentID,
                    OrderStartDate = order.OrderStartDate,
                    OrderEndDate = order.OrderEndDate,
                    OrderEnteredByUserId = order.OrderEnteredBy.ApplicationUserGUID
                });
            }

            return returnList;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public ApiOrderModel Get(int id)
        {
            var order = applicationDbContext.Orders.Where(x => x.OrderID == id).FirstOrDefault();

            return new ApiOrderModel()
            {
                OrderID = order.OrderID,
                OrderNumber = order.OrderNumber,
                OrderFeedbackNumber = order.OrderFeedbackNumber,
                OrderAmount = order.OrderAmount,
                ComponentId = order.Component.ComponentID,
                OrderStartDate = order.OrderStartDate,
                OrderEndDate = order.OrderEndDate,
                OrderEnteredByUserId = order.OrderEnteredBy.ApplicationUserGUID
            };
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiOrderModel value)
        {
            if (applicationDbContext.Orders.Where(x => x.OrderNumber == value.OrderNumber).Any())
                return BadRequest($"Order already exist - with InputValue: {value.OrderNumber}");

            if (!applicationDbContext.Components.Where(x => x.ComponentID == value.ComponentId).Any())
                return BadRequest($"Component does not exist - with InputValue: {value.ComponentId}");

            if (!applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.OrderEnteredByUserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.OrderEnteredByUserId}");

            applicationDbContext.Add(new Order()
            {
                OrderNumber = value.OrderNumber,
                OrderFeedbackNumber = value.OrderFeedbackNumber,
                OrderAmount = value.OrderAmount,
                Component = applicationDbContext.Components.Where(x => x.ComponentID == value.ComponentId).FirstOrDefault(),
                OrderStartDate = value.OrderStartDate,
                OrderEndDate = value.OrderEndDate,
                OrderEnteredBy = applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.OrderEnteredByUserId).FirstOrDefault()                 
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiOrderModel value)
        {
            if (applicationDbContext.Orders.Where(x => x.OrderID == id).Any())
                return BadRequest($"Order already exist - with InputValue: {id}");

            if (applicationDbContext.Orders.Where(x => x.OrderNumber == value.OrderNumber).Any())
                return BadRequest($"Order already exist - with InputValue: {value.OrderNumber}");

            if (!applicationDbContext.Components.Where(x => x.ComponentID == value.ComponentId).Any())
                return BadRequest($"Component does not exist - with InputValue: {value.ComponentId}");

            if (!applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.OrderEnteredByUserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.OrderEnteredByUserId}");

            var order = applicationDbContext.Orders.Where(x => x.OrderID == id).FirstOrDefault();

            order.OrderNumber = value.OrderNumber;
            order.OrderFeedbackNumber = value.OrderFeedbackNumber;
            order.OrderAmount = value.OrderAmount;
            order.Component = applicationDbContext.Components.Where(x => x.ComponentID == value.ComponentId).FirstOrDefault();
            order.OrderStartDate = value.OrderStartDate;
            order.OrderEndDate = value.OrderEndDate;
            order.OrderEnteredBy = applicationDbContext.Users.Where(x => x.ApplicationUserGUID == value.OrderEnteredByUserId).FirstOrDefault();

            applicationDbContext.Update(order);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.Orders.Where(x => x.OrderID == id).Any())
                return BadRequest($"Order does not exist - with InputValue: {id}");

            var order = applicationDbContext.Orders.Where(x => x.OrderID == id).FirstOrDefault();

            if (order.Delivered.Count == 0 || order.PackingMaterialUsed.Count == 0 || order.PickupRequested.Count == 0)
            {
                applicationDbContext.Remove(order);
                applicationDbContext.SaveChanges();
            }
            else
                return BadRequest("The order is in use, therefore is not deleted.");

            return Ok();
        }
    }
}
