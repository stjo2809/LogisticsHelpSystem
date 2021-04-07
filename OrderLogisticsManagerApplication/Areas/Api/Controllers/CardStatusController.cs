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
    public class CardStatusController : ControllerBase
    {
        private readonly ApplicationIdentityContext applicationIdentityContext;

        public CardStatusController(ApplicationIdentityContext applicationIdentityContext )
        {
            this.applicationIdentityContext = applicationIdentityContext;
        }

        // GET: api/<CardStatusController>
        [HttpGet]
        public IEnumerable<ApiCardStatusModel> Get()
        {
            List<ApiCardStatusModel> returnList = new();

            foreach (var cardStatus in applicationIdentityContext.CardStatuses)
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
            var cardstatus = applicationIdentityContext.CardStatuses.Where(x => x.CardStatusId == id).FirstOrDefault();

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
            applicationIdentityContext.Add(new CardStatus() { StatusDescription = statusDescription });
            applicationIdentityContext.SaveChanges();

            return Ok();
        }

        // PUT api/<CardStatusController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string statusDescription)
        {
            var cardstatus = applicationIdentityContext.CardStatuses.Where(x => x.CardStatusId == id).FirstOrDefault();
            cardstatus.StatusDescription = statusDescription;
            applicationIdentityContext.Update(cardstatus);
            applicationIdentityContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<CardStatusController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cardstatus = applicationIdentityContext.CardStatuses.Where(x => x.CardStatusId == id).FirstOrDefault();
            if (cardstatus.Cards.Count() == 0)
            {
                applicationIdentityContext.Remove(cardstatus);
                applicationIdentityContext.SaveChanges();
            }
            else
                return BadRequest("The CardStatus has Cards that uses it, therefore is not deleted.");

            return Ok();                
        }
    }
}
