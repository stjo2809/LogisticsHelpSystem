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
    public class UserStatusController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserStatusController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<UserStatusController>
        [HttpGet]
        public IEnumerable<ApiUserStatusModel> Get()
        {
            List<ApiUserStatusModel> returnList = new();

            foreach (var userStatus in applicationDbContext.UserStatuses)
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
            var userStatus = applicationDbContext.UserStatuses.Where(x => x.UserStatusId == id).FirstOrDefault();

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
            applicationDbContext.Add(new UserStatus() { StatusDescription = statusDescription });
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<UserStatusController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string statusDescription)
        {
            if (!applicationDbContext.UserStatuses.Where(x => x.UserStatusId == id).Any())
                return BadRequest($"UserStatus does not exist");

            var userStatus = applicationDbContext.UserStatuses.Where(x => x.UserStatusId == id).FirstOrDefault();
            userStatus.StatusDescription = statusDescription;
            applicationDbContext.Update(userStatus);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<UserStatusController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userStatus = applicationDbContext.UserStatuses.Where(x => x.UserStatusId == id).FirstOrDefault();
            if (userStatus.Users.Count() == 0)
            {
                applicationDbContext.Remove(userStatus);
                applicationDbContext.SaveChanges();
            }
            else
                return BadRequest("The UserStatus has users that uses it, therefore is not deleted.");

            return Ok();
        }
    }
}
