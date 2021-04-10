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
    public class PackingMaterialUsedOnOrderController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PackingMaterialUsedOnOrderController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<PackingMaterialUsedOnOrderController>
        [HttpGet]
        public IEnumerable<ApiPackingMaterialUsedOnOrderModel> Get()
        {
            List<ApiPackingMaterialUsedOnOrderModel> returnList = new();

            foreach (var packingMaterialUsed in applicationDbContext.PackingMaterialUsedOnOrders)
            {
                returnList.Add(new ApiPackingMaterialUsedOnOrderModel()
                {
                    OrderId = packingMaterialUsed.Order.OrderID,
                    MaterialId= packingMaterialUsed.Material.MaterialID,
                    Amount = packingMaterialUsed.Amount
                });
            }

            return returnList;
        }

        // GET api/<PackingMaterialUsedOnOrderController>/5
        [HttpGet("{id}")]
        public ApiPackingMaterialUsedOnOrderModel Get(int id)
        {
            var packingMaterialUsed = applicationDbContext.PackingMaterialUsedOnOrders.Where(x => x.ID == id).FirstOrDefault();

            return new ApiPackingMaterialUsedOnOrderModel()
            {
                OrderId = packingMaterialUsed.Order.OrderID,
                MaterialId = packingMaterialUsed.Material.MaterialID,
                Amount = packingMaterialUsed.Amount
            };
        }

        // POST api/<PackingMaterialUsedOnOrderController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiPackingMaterialUsedOnOrderModel value)
        {
            if (!applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).Any())
                return BadRequest($"Order does not exist - with InputValue: {value.OrderId}");

            if (!applicationDbContext.PackingMaterials.Where(x => x.MaterialID == value.MaterialId).Any())
                return BadRequest($"PackingMaterial does not exist - with InputValue: {value.MaterialId}");

            applicationDbContext.Add(new PackingMaterialUsedOnOrder()
            {
                Order = applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).FirstOrDefault(),
                Material = applicationDbContext.PackingMaterials.Where(x => x.MaterialID == value.MaterialId).FirstOrDefault(),
                Amount = value.Amount
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<PackingMaterialUsedOnOrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiPackingMaterialUsedOnOrderModel value)
        {
            if (!applicationDbContext.PackingMaterialUsedOnOrders.Where(x => x.ID == id).Any())
                return BadRequest($"PackingMaterialUsedOnOrders does not exist - with InputValue: {id}");

            if (!applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).Any())
                return BadRequest($"Order does not exist - with InputValue: {value.OrderId}");

            if (!applicationDbContext.PackingMaterials.Where(x => x.MaterialID == value.MaterialId).Any())
                return BadRequest($"PackingMaterial does not exist - with InputValue: {value.MaterialId}");

            var packingMaterialUsed = applicationDbContext.PackingMaterialUsedOnOrders.Where(x => x.ID == id).FirstOrDefault();

            packingMaterialUsed.Order = applicationDbContext.Orders.Where(x => x.OrderID == value.OrderId).FirstOrDefault();
            packingMaterialUsed.Material = applicationDbContext.PackingMaterials.Where(x => x.MaterialID == value.MaterialId).FirstOrDefault();
            packingMaterialUsed.Amount = value.Amount;

            applicationDbContext.Update(packingMaterialUsed);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<PackingMaterialUsedOnOrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.PackingMaterialUsedOnOrders.Where(x => x.ID == id).Any())
                return BadRequest($"PackingMaterialUsedOnOrders does not exist - with InputValue: {id}");

            var packingMaterialUsed = applicationDbContext.PackingMaterialUsedOnOrders.Where(x => x.ID == id).FirstOrDefault();

            applicationDbContext.Remove(packingMaterialUsed);
            applicationDbContext.SaveChanges();
            
            return Ok();
        }
    }
}
