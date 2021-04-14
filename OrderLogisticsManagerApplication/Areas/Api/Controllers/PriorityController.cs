using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Filters;
using LogisticsHelpSystemLibrary.Models.Api;
using Microsoft.AspNetCore.Mvc;
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
    public class PriorityController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PriorityController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<PriorityController>
        [HttpGet]
        public IEnumerable<ApiPriorityModel> Get()
        {
            List<ApiPriorityModel> returnList = new();

            foreach (var priority in applicationDbContext.Priorities)
            {
                returnList.Add(new ApiPriorityModel()
                {
                    PriorityID = priority.PriorityID,
                    Description = priority.Description
                });
            }

            return returnList;
        }

        // GET api/<PriorityController>/5
        [HttpGet("{id}")]
        public ApiPriorityModel Get(int id)
        {
            var priority = applicationDbContext.Priorities.Where(x => x.PriorityID == id).FirstOrDefault();

            return new ApiPriorityModel()
            {
                PriorityID = priority.PriorityID,
                Description = priority.Description
            };
        }

        // POST api/<PriorityController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiPriorityModel value)
        {
            if (applicationDbContext.Priorities.Where(x => x.Description == value.Description).Any())
                return BadRequest($"Priority already exist - with InputValue: {value.Description}");

            applicationDbContext.Add(new Priority()
            {
                PriorityID = value.PriorityID,
                Description = value.Description
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<PriorityController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiPriorityModel value)
        {
            if (!applicationDbContext.Priorities.Where(x => x.PriorityID == id).Any())
                return BadRequest($"Priority does not exist - with InputValue: {id}");

            var priority = applicationDbContext.Priorities.Where(x => x.PriorityID == id).FirstOrDefault();

            priority.Description = value.Description;

            applicationDbContext.Update(priority);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<PriorityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.Priorities.Where(x => x.PriorityID == id).Any())
                return BadRequest($"Priority does not exist - with InputValue: {id}");

            var priority = applicationDbContext.Priorities.Where(x => x.PriorityID == id).FirstOrDefault();

            if (priority.Orders.Count == 0)
            {
                applicationDbContext.Remove(priority);
                applicationDbContext.SaveChanges();
            }
            else
                return BadRequest("The Priority has orders that uses it, therefore is not deleted.");

            return Ok();
        }
    }
}
