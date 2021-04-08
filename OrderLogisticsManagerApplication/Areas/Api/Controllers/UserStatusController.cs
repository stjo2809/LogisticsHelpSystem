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
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatusController : ControllerBase
    {
        private readonly ApplicationIdentityContext applicationIdentityContext;

        public UserStatusController(ApplicationIdentityContext applicationIdentityContext)
        {
            this.applicationIdentityContext = applicationIdentityContext;
        }

        // GET: api/<UserStatusController>
        [HttpGet]
        public IEnumerable<ApiUserStatusModel> Get()
        {
            List<ApiUserStatusModel> returnList = new();

            foreach (var userStatus in applicationIdentityContext.UserStatuses)
            {
                returnList.Add(new ApiUserStatusModel()
                {
                    UserStatusId = userStatus.UserStatusId,
                    StatusDescription = userStatus.StatusDescription
                });
            }

            return returnList;
        }

        // GET api/<UserStatusController>/5
        [HttpGet("{id}")]
        public ApiUserStatusModel Get(int id)
        {
            var userStatus = applicationIdentityContext.UserStatuses.Where(x => x.UserStatusId == id).FirstOrDefault();

            return new ApiUserStatusModel()
            {
                UserStatusId = userStatus.UserStatusId,
                StatusDescription = userStatus.StatusDescription
            };
        }

        // POST api/<UserStatusController>
        [HttpPost]
        public IActionResult Post([FromBody] string statusDescription)
        {
            applicationIdentityContext.Add(new UserStatus() { StatusDescription = statusDescription });
            applicationIdentityContext.SaveChanges();

            return Ok();
        }

        // PUT api/<UserStatusController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string statusDescription)
        {
            if (!applicationIdentityContext.UserStatuses.Where(x => x.UserStatusId == id).Any())
                return BadRequest($"UserStatus does not exist");

            var userStatus = applicationIdentityContext.UserStatuses.Where(x => x.UserStatusId == id).FirstOrDefault();
            userStatus.StatusDescription = statusDescription;
            applicationIdentityContext.Update(userStatus);
            applicationIdentityContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<UserStatusController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userStatus = applicationIdentityContext.UserStatuses.Where(x => x.UserStatusId == id).FirstOrDefault();
            if (userStatus.Users.Count() == 0)
            {
                applicationIdentityContext.Remove(userStatus);
                applicationIdentityContext.SaveChanges();
            }
            else
                return BadRequest("The UserStatus has users that uses it, therefore is not deleted.");

            return Ok();
        }
    }
}
