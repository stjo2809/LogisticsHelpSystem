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
    public class CardController : ControllerBase
    {
        private readonly ApplicationIdentityContext applicationIdentityContext;

        public CardController(ApplicationIdentityContext applicationIdentityContext)
        {
            this.applicationIdentityContext = applicationIdentityContext;
        }

        // GET: api/<CardController>
        [HttpGet]
        public IEnumerable<ApiCardModel> Get()
        {
            List<ApiCardModel> returnList = new();

            foreach (var card in applicationIdentityContext.Card)
            {
                returnList.Add(new ApiCardModel()
                {
                    CardId = card.CardId,
                    CardNumber = card.CardNumber,
                    CardStatusId = card.CardStatusId,
                    UserId = card.UserId
                });
            }

            return returnList;
        }

        // GET api/<CardController>/5
        [HttpGet("{id}")]
        public ApiCardModel Get(int id)
        {
            var card = applicationIdentityContext.Card.Where(x => x.CardId == id).FirstOrDefault();

            return new ApiCardModel()
            {
                CardId = card.CardId,
                CardNumber = card.CardNumber,
                CardStatusId = card.CardStatusId,
                UserId = card.UserId
            };
        }

        // POST api/<CardController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiCardModel value)
        {
            if (applicationIdentityContext.Card.Where(x => x.CardNumber == value.CardNumber).Any())
                return BadRequest($"CardNumber already exist - with InputValue: {value.CardNumber}");

            if (!applicationIdentityContext.Card.Where(x => x.CardStatusId == value.CardStatusId).Any())
                return BadRequest($"CardStatusId does not exist - with InputValue: {value.CardStatusId}");

            if (!applicationIdentityContext.Card.Where(x => x.UserId == value.UserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserId}");

            applicationIdentityContext.Add(new Card()
            {
                CardId = value.CardId,
                CardNumber = value.CardNumber,
                CardStatusId = value.CardStatusId,
                UserId = value.UserId
            });

            applicationIdentityContext.SaveChanges();

            return Ok();
        }

        // PUT api/<CardController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiCardModel value)
        {
            if (!applicationIdentityContext.Card.Where(x => x.CardId == id).Any())
                return BadRequest($"Card does not exist");

            if (!applicationIdentityContext.Card.Where(x => x.CardStatusId == value.CardStatusId).Any())
                return BadRequest($"CardStatusId does not exist - with InputValue: {value.CardStatusId}");

            if (!applicationIdentityContext.Card.Where(x => x.UserId == value.UserId).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserId}");

            var card = applicationIdentityContext.Card.Where(x => x.CardId == id).FirstOrDefault();

            card.CardNumber = value.CardNumber;
            card.CardStatusId = value.CardStatusId;
            card.UserId = value.UserId;
            
            applicationIdentityContext.Update(card);
            applicationIdentityContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<CardController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationIdentityContext.Card.Where(x => x.CardId == id).Any())
                return BadRequest($"Card does not exist");

            var card = applicationIdentityContext.Card.Where(x => x.CardId == id).FirstOrDefault();

            card.CardStatusId = applicationIdentityContext.CardStatuses.Where(x => x.StatusDescription == "Inactive").FirstOrDefault().CardStatusId;

            applicationIdentityContext.Update(card);
            applicationIdentityContext.SaveChanges();

            return Ok();
        }
    }
}
