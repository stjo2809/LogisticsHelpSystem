using Microsoft.AspNetCore.Mvc;
using OrderLogisticsManagerApplication.Data;
using OrderLogisticsManagerApplication.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderLogisticsManagerApplication.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationIdentityContext applicationIdentityContext;

        public UserController(ApplicationIdentityContext applicationIdentityContext)
        {
            this.applicationIdentityContext = applicationIdentityContext;
        }

        #region User

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            return applicationIdentityContext.Users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ApplicationUser Get(string id)
        {
            return applicationIdentityContext.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion

        #region UserStatus

        #endregion

        #region Card

        #endregion

        #region CardStatus

        #endregion


    }
}
