using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderLogisticsManagerApplication.Areas.Identity.Data;
using OrderLogisticsManagerApplication.Areas.Api.Models;
using Microsoft.AspNetCore.Identity;
using OrderLogisticsManagerApplication.Models.Database.ApplicationDb;
using OrderLogisticsManagerApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderLogisticsManagerApplication.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ApplicationUserManager userManager;

        public UserController(ApplicationDbContext applicationDbContext, ApplicationUserManager userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        #region User

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<ApiOutputUserModel>> GetAsync()
        {
            List<ApiOutputUserModel> returnList = new();

            var result = await userManager.CreateAsync(new ApplicationUser() { 
                Email = "sj@test.com", 
                UserName = "stjo2809",
                FirstName = "s123", LastName = "j123", 
                Status = userManager.GetUserStatusById(1), 
                WorkGroup = userManager.GetWorkGroupById(1) 
            }, "Pwd1234.");
           

            var cs = userManager.GetCardStatusById(1);
            var u = await userManager.FindByEmailAsync("sj@test.com");
            userManager.CreateCard("20873hu926", cs,u);

            var listcards = userManager.GetCards(); 

            
            //foreach (var user in applicationDbContext.Users)
            //{
            //    var UserIdentity = await userManager.FindByIdAsync(user.ApplicationUserGUID);

            //    returnList.Add(new ApiOutputUserModel()
            //    {
            //        Email = UserIdentity.Email,
            //        UserName = "temp",
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        UserStatus = user.Status.StatusDescription,
            //        Role = userManager.GetRolesAsync(UserIdentity).ToString(),
            //        WorkGroup = $"{user.WorkGroup.WorkGroupNumber} - {user.WorkGroup.WorkGroupName}",
            //        CardCount = user.Cards.Count,
            //        HasActiveCard = user.Cards.Where(x => x.Status.StatusDescription == "Active").Count() >= 1 ? true : false,
            //    });
            //}

            return returnList;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ApiInputUserModel Get(string id)
        {
            return null;
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
