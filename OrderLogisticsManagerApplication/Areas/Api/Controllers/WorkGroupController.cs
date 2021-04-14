using LogisticsHelpSystemLibrary.Models.Api;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Filters;
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
    public class WorkGroupController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public WorkGroupController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<WorkGroupController>
        [HttpGet]
        public IEnumerable<ApiWorkGroupModel> Get()
        {
            List<ApiWorkGroupModel> returnList = new();

            foreach (var workGroup in applicationDbContext.WorkGroups)
            {
                returnList.Add(new ApiWorkGroupModel()
                {
                    WorkGroupId = workGroup.WorkGroupId,
                    WorkGroupNumber = workGroup.WorkGroupNumber,
                    WorkGroupName = workGroup.WorkGroupName
                });
            }

            return returnList;
        }

        // GET api/<WorkGroupController>/5
        [HttpGet("{id}")]
        public ApiWorkGroupModel Get(int id)
        {
            var workGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupId == id).FirstOrDefault();

            return new ApiWorkGroupModel()
            {
                WorkGroupId = workGroup.WorkGroupId,
                WorkGroupNumber = workGroup.WorkGroupNumber,
                WorkGroupName = workGroup.WorkGroupName
            };
        }

        // POST api/<WorkGroupController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiWorkGroupModel value)
        {
            if (applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == value.WorkGroupNumber).Any())
                return BadRequest($"WorkGroupNumber already exist - with InputValue: {value.WorkGroupNumber}");

            applicationDbContext.Add(new WorkGroup()
            {
                WorkGroupNumber = value.WorkGroupNumber,
                WorkGroupName = value.WorkGroupName
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<WorkGroupController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiWorkGroupModel value)
        {
            if (!applicationDbContext.WorkGroups.Where(x => x.WorkGroupId == id).Any())
                return BadRequest($"WorkGroup does not exist");

            var workGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupId == id).FirstOrDefault();

            workGroup.WorkGroupName = value.WorkGroupName;
            workGroup.WorkGroupNumber = value.WorkGroupNumber;
            applicationDbContext.Update(workGroup);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<WorkGroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var workGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupId == id).FirstOrDefault();
            if (workGroup.Users.Count() == 0)
            {
                applicationDbContext.Remove(workGroup);
                applicationDbContext.SaveChanges();
            }
            else
                return BadRequest("The WorkGroup has users that uses it, therefore is not deleted.");

            return Ok();
        }
    }
}
