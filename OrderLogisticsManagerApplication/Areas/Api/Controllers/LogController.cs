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
    public class LogController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public LogController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<LogController>
        [HttpGet]
        public IEnumerable<ApiLogModel> Get()
        {
            List<ApiLogModel> returnList = new();

            foreach (var log in applicationDbContext.Logs)
            {
                returnList.Add(new ApiLogModel()
                {
                    LogID = log.LogID,
                    LogAction = log.LogAction,
                    LogOn = log.LogOn,
                    LogDescription = log.LogDescription,
                    LogTime = log.LogTime,
                    UserId = log.User.UserID
                });
            }

            return returnList;
        }

        // GET api/<LogController>/5
        [HttpGet("{id}")]
        public ApiLogModel Get(int id)
        {
            var log = applicationDbContext.Logs.Where(x => x.LogID == id).FirstOrDefault();
            
            return new ApiLogModel()
            {
                LogID = log.LogID,
                LogAction = log.LogAction,
                LogOn = log.LogOn,
                LogDescription = log.LogDescription,
                LogTime = log.LogTime,
                UserId = log.User.UserID
            };
        }

        // POST api/<LogController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiLogModel value)
        {
            applicationDbContext.Add(new Log()
            {
                LogAction = value.LogAction,
                LogOn = value.LogOn,
                LogDescription = value.LogDescription,
                LogTime = value.LogTime,
                User = applicationDbContext.Users.Where(x => x.UserID == value.UserId).FirstOrDefault()
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<LogController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiLogModel value)
        {
            if (!applicationDbContext.Logs.Where(x => x.LogID == id).Any())
                return BadRequest($"Log does not exist - with InputValue: {id}");

            var log = applicationDbContext.Logs.Where(x => x.LogID == id).FirstOrDefault();

            log.LogAction = value.LogAction;
            log.LogOn = value.LogOn;
            log.LogDescription = value.LogDescription;
            log.LogTime = value.LogTime;
            log.User = applicationDbContext.Users.Where(x => x.UserID == value.UserId).FirstOrDefault();

            applicationDbContext.Update(log);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<LogController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.Logs.Where(x => x.LogID == id).Any())
                return BadRequest($"Log does not exist - with InputValue: {id}");

            var log = applicationDbContext.Logs.Where(x => x.LogID == id).FirstOrDefault();
            
            applicationDbContext.Remove(log);
            applicationDbContext.SaveChanges();

            return Ok();
        }
    }
}
