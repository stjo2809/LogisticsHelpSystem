using Microsoft.AspNetCore.Mvc;
using OrderLogisticsManagerApplication.Areas.Api.Models;
using OrderLogisticsManagerApplication.Data;
using OrderLogisticsManagerApplication.Models.Database.ApplicationIdentity;
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
        private readonly ApplicationIdentityContext applicationIdentityContext;

        public WorkGroupController(ApplicationIdentityContext applicationIdentityContext)
        {
            this.applicationIdentityContext = applicationIdentityContext;
        }

        // GET: api/<WorkGroupController>
        [HttpGet]
        public IEnumerable<ApiWorkGroupModel> Get()
        {
            List<ApiWorkGroupModel> returnList = new();

            foreach (var workGroup in applicationIdentityContext.WorkGroups)
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
            var workGroup = applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupId == id).FirstOrDefault();

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
            if (applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupNumber == value.WorkGroupNumber).Any())
                return BadRequest($"WorkGroupNumber already exist - with InputValue: {value.WorkGroupNumber}");

            applicationIdentityContext.Add(new WorkGroup()
            {
                WorkGroupNumber = value.WorkGroupNumber,
                WorkGroupName = value.WorkGroupName
            });

            applicationIdentityContext.SaveChanges();

            return Ok();
        }

        // PUT api/<WorkGroupController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiWorkGroupModel value)
        {
            if (!applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupId == id).Any())
                return BadRequest($"WorkGroup does not exist");

            var workGroup = applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupId == id).FirstOrDefault();

            workGroup.WorkGroupName = value.WorkGroupName;
            workGroup.WorkGroupNumber = value.WorkGroupNumber;
            applicationIdentityContext.Update(workGroup);
            applicationIdentityContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<WorkGroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var workGroup = applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupId == id).FirstOrDefault();
            if (workGroup.Users.Count() == 0)
            {
                applicationIdentityContext.Remove(workGroup);
                applicationIdentityContext.SaveChanges();
            }
            else
                return BadRequest("The WorkGroup has users that uses it, therefore is not deleted.");

            return Ok();
        }
    }
}
