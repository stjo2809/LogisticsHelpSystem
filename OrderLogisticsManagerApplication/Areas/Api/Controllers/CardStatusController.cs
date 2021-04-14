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
    public class CardStatusController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CardStatusController(ApplicationDbContext applicationDbContext )
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<CardStatusController>
        [HttpGet]
        public IEnumerable<ApiCardStatusModel> Get()
        {
            List<ApiCardStatusModel> returnList = new();

            foreach (var cardStatus in applicationDbContext.CardStatuses)
            {
                returnList.Add(new ApiCardStatusModel()
                {
                    CardStatusId = cardStatus.CardStatusId,
                    StatusDescription = cardStatus.StatusDescription
                });
            }

            return returnList;
        }

        // GET api/<CardStatusController>/5
        [HttpGet("{id}")]
        public ApiCardStatusModel Get(int id)
        {
            var cardstatus = applicationDbContext.CardStatuses.Where(x => x.CardStatusId == id).FirstOrDefault();

            return new ApiCardStatusModel()
            {
                CardStatusId = cardstatus.CardStatusId,
                StatusDescription = cardstatus.StatusDescription
            };
        }

        // POST api/<CardStatusController>
        [HttpPost]
        public IActionResult Post([FromBody] string statusDescription)
        {
            applicationDbContext.Add(new CardStatus() { StatusDescription = statusDescription });
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<CardStatusController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string statusDescription)
        {
            var cardstatus = applicationDbContext.CardStatuses.Where(x => x.CardStatusId == id).FirstOrDefault();
            cardstatus.StatusDescription = statusDescription;
            applicationDbContext.Update(cardstatus);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<CardStatusController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cardstatus = applicationDbContext.CardStatuses.Where(x => x.CardStatusId == id).FirstOrDefault();
            if (cardstatus.Cards.Count() == 0)
            {
                applicationDbContext.Remove(cardstatus);
                applicationDbContext.SaveChanges();
            }
            else
                return BadRequest("The CardStatus has Cards that uses it, therefore is not deleted.");

            return Ok();                
        }
    }
}
