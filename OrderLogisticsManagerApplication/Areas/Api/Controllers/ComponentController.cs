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
    public class ComponentController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ComponentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<ComponentController>
        [HttpGet]
        public IEnumerable<ApiComponentModel> Get()
        {
            List<ApiComponentModel> returnList = new();

            foreach (var component in applicationDbContext.Components)
            {
                returnList.Add(new ApiComponentModel()
                {
                    ComponentID = component.ComponentID,
                    ComponentPartNumber = component.ComponentPartNumber,
                    ComponentName = component.ComponentName,
                    ComponentDepth = component.ComponentDepth,
                    ComponentHeigth = component.ComponentHeigth,
                    ComponentWeigth = component.ComponentWeigth,
                    ComponentWidth = component.ComponentWidth
                });
            }

            return returnList;
        }

        // GET api/<ComponentController>/5
        [HttpGet("{id}")]
        public ApiComponentModel Get(int id)
        {
            var component = applicationDbContext.Components.Where(x => x.ComponentID == id).FirstOrDefault();

            return new ApiComponentModel()
            {
                ComponentID = component.ComponentID,
                ComponentPartNumber = component.ComponentPartNumber,
                ComponentName = component.ComponentName,
                ComponentDepth = component.ComponentDepth,
                ComponentHeigth = component.ComponentHeigth,
                ComponentWeigth = component.ComponentWeigth,
                ComponentWidth = component.ComponentWidth
            };
        }

        // POST api/<ComponentController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiComponentModel value)
        {
            if (applicationDbContext.Components.Where(x => x.ComponentPartNumber == value.ComponentPartNumber).Any())
                return BadRequest($"Component Part Number already exist - with InputValue: {value.ComponentPartNumber}");

            applicationDbContext.Add( new Component()
            {
                ComponentPartNumber = value.ComponentPartNumber,
                ComponentName = value.ComponentName,
                ComponentDepth = value.ComponentDepth,
                ComponentHeigth = value.ComponentHeigth,
                ComponentWeigth = value.ComponentWeigth,
                ComponentWidth = value.ComponentWidth
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<ComponentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiComponentModel value)
        {
            if (!applicationDbContext.Components.Where(x => x.ComponentID == id).Any())
                return BadRequest($"Component does not exist - with InputValue: {value.ComponentID}");

            var component = applicationDbContext.Components.Where(x => x.ComponentID == id).FirstOrDefault();

            component.ComponentPartNumber = value.ComponentPartNumber;
            component.ComponentName = value.ComponentName;
            component.ComponentDepth = value.ComponentDepth;
            component.ComponentHeigth = value.ComponentHeigth;
            component.ComponentWeigth = value.ComponentWeigth;
            component.ComponentWidth = value.ComponentWidth;

            applicationDbContext.Update(component);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<ComponentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.Components.Where(x => x.ComponentID == id).Any())
                return BadRequest($"Component does not exist - with InputValue: {id}");

            var component = applicationDbContext.Components.Where(x => x.ComponentID == id).FirstOrDefault();

            if (component.Orders.Count == 0)
            {
                applicationDbContext.Remove(component);
                applicationDbContext.SaveChanges();
            }
            else
                return BadRequest("The component has orders that uses it, therefore is not deleted.");

            return Ok();
        }
    }
}
