using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderLogisticsManagerApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using OrderLogisticsManagerApplication.Data;
using LogisticsHelpSystemLibrary.Models.Filters;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderLogisticsManagerApplication.Areas.Api.Controllers
{ //TODO FIX Use api
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationIdentityContext applicationIdentityContext;
        private readonly ApplicationDbContext applicationDbContext;

        public UserController(UserManager<ApplicationUser> userManager, ApplicationIdentityContext applicationIdentityContext, ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.applicationIdentityContext = applicationIdentityContext;
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<ApiUserModel>> Get()
        {
            List<ApiUserModel> returnList = new();

            foreach (var user in applicationDbContext.Users)
            {
                var userIdentity = await userManager.FindByIdAsync(user.ApplicationUserGUID);

                returnList.Add(new ApiUserModel()
                {
                    UserId = user.UserID,
                    Email = userIdentity.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserStatus = user.Status.StatusDescription,
                    Role = string.Join<string>(",", await userManager.GetRolesAsync(userIdentity)),
                    WorkGroupNumber = user.WorkGroup.WorkGroupNumber
                });
            }

            return returnList;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ApiUserModel> GetAsync(int id)
        {
            var user = applicationDbContext.Users.Where(x => x.UserID == id).FirstOrDefault();
            var userIdentity = await userManager.FindByIdAsync(user.ApplicationUserGUID);

            return new ApiUserModel() 
            {
                UserId = user.UserID,
                Email = userIdentity.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserStatus = user.Status.StatusDescription,
                Role = string.Join<string>(",", await userManager.GetRolesAsync(userIdentity)),
                WorkGroupNumber = user.WorkGroup.WorkGroupNumber
            };
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ApiUserModel value)
        {
            if (!applicationIdentityContext.Roles.Where(x => x.Name == value.Role).Any())
                return BadRequest($"Role does not exist - InputValue: {value.Role}");

            if (!applicationDbContext.UserStatuses.Where(x => x.StatusDescription == value.UserStatus).Any())
                return BadRequest($"UserStatus does not exist - InputValue: {value.UserStatus}");

            if (!applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == value.WorkGroupNumber).Any())
                return BadRequest($"WorkGroupNumber does not exist - InputValue: {value.WorkGroupNumber}");

            var result = await userManager.CreateAsync(new ApplicationUser()
            {
                Email = value.Email,
                UserName = value.Email,
                EmailConfirmed = true
            }, "User2Pass@DSB");

            if (result.Succeeded)
            {
                applicationDbContext.Users.Add(new User()
                {
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    Status = applicationDbContext.UserStatuses.Where(x => x.StatusDescription == value.UserStatus).FirstOrDefault(),
                    WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == value.WorkGroupNumber).FirstOrDefault(),
                    ApplicationUserGUID = userManager.Users.Where(x => x.Email == value.Email).FirstOrDefault().Id
                }); 

                var addRoleResult = await userManager.AddToRoleAsync(await userManager.FindByNameAsync(value.Email), value.Role);
                if (!addRoleResult.Succeeded)
                {
                    return BadRequest(addRoleResult.Errors);
                }
            }
            else
                return BadRequest(result.Errors);

            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ApiUserModel value)
        {
            if (applicationIdentityContext.Users.Where(x => x.Email == value.Email).Any())
                return BadRequest($"User does not exist - with InputValue: {value.Email}");

            if (applicationDbContext.Users.Where(x => x.UserID == id).Any())
                return BadRequest($"User does not exist - with InputValue: {id}");

            if (!applicationDbContext.UserStatuses.Where(x => x.StatusDescription == value.UserStatus).Any())
                return BadRequest($"UserStatus does not exist - InputValue: {value.UserStatus}");

            if (!applicationIdentityContext.Roles.Where(x => x.Name == value.Role).Any())
                return BadRequest($"Role does not exist - InputValue: {value.Role}");

            if (!applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == value.WorkGroupNumber).Any())
                return BadRequest($"WorkGroupNumber does not exist - InputValue: {value.WorkGroupNumber}");

            var user = applicationDbContext.Users.Where(x => x.UserID == id).FirstOrDefault();

            user.FirstName = value.FirstName;
            user.LastName = value.LastName;
            user.Status = applicationDbContext.UserStatuses.Where(x => x.StatusDescription == value.UserStatus).FirstOrDefault();
            user.WorkGroup = applicationDbContext.WorkGroups.Where(x => x.WorkGroupNumber == value.WorkGroupNumber).FirstOrDefault();

            applicationDbContext.Update(user);

            var userIdentity = await userManager.FindByIdAsync(user.ApplicationUserGUID);

            userIdentity.Email = value.Email;
            userIdentity.UserName = value.Email;
            
            var updateResult = await userManager.UpdateAsync(userIdentity);
            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);

            applicationDbContext.SaveChanges();

            var userRoles = await userManager.GetRolesAsync(userIdentity);
            if (!userRoles.Contains(value.Role))
            {
                var removeResult = await userManager.RemoveFromRolesAsync(userIdentity, userRoles);
                if (!removeResult.Succeeded)
                    return BadRequest(removeResult.Errors);
                
                var addResult = await userManager.AddToRoleAsync(userIdentity, value.Role);
                if (!addResult.Succeeded)
                    return BadRequest(addResult.Errors);
            }

            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.Users.Where(x => x.UserID == id).Any())
                return BadRequest($"User does not exist - with InputValue: {id}");

            var user = applicationDbContext.Users.Where(x => x.UserID == id).FirstOrDefault();

            user.Status = applicationDbContext.UserStatuses.Where(x => x.StatusDescription == "Inactive").FirstOrDefault();

            applicationDbContext.Update(user);
            applicationDbContext.SaveChanges();

            return Ok();
        }
    }
}
