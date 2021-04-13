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
using OrderLogisticsManagerApplication.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderLogisticsManagerApplication.Areas.Api.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationIdentityContext applicationIdentityContext;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ApplicationUserManager userManager;

        public UserController(ApplicationIdentityContext applicationIdentityContext ,ApplicationDbContext applicationDbContext, ApplicationUserManager userManager)
        {
            this.applicationIdentityContext = applicationIdentityContext;
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<ApiUserModel>> Get()
        {
            List<ApiUserModel> returnList = new();

            foreach (var user in userManager.Users)
            {
                returnList.Add(new ApiUserModel()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserStatus = userManager.GetUserStatusById(user.UserStatusId).StatusDescription,
                    Role = string.Join<string>(",", await userManager.GetRolesAsync(user)),
                    WorkGroupNumber = userManager.GetWorkGroupById(user.WorkGroupId).WorkGroupNumber                    
                });
            }

            return returnList;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ApiUserModel> GetAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return new ApiUserModel() 
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserStatus = userManager.GetUserStatusById(user.UserStatusId).StatusDescription,
                Role = string.Join<string>(",", await userManager.GetRolesAsync(user)),
                WorkGroupNumber = userManager.GetWorkGroupById(user.WorkGroupId).WorkGroupNumber
            };
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ApiUserModel value)
        {
            if (!applicationIdentityContext.Users.Where(x => x.Email == value.Email).Any())
                return BadRequest($"User already exist - with InputValue: {value.Email}");

            if (!applicationIdentityContext.Users.Where(x => x.UserName == value.UserName).Any())
                return BadRequest($"User already exist - with InputValue: {value.UserName}");

            if (!applicationIdentityContext.UserStatuses.Where(x => x.StatusDescription == value.UserStatus).Any())
                return BadRequest($"UserStatus does not exist - InputValue: {value.UserStatus}");

            if (!applicationIdentityContext.Roles.Where(x => x.Name == value.Role).Any())
                return BadRequest($"Role does not exist - InputValue: {value.Role}");

            if (!applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupNumber == value.WorkGroupNumber).Any())
                return BadRequest($"WorkGroupNumber does not exist - InputValue: {value.WorkGroupNumber}");

            var result = await userManager.CreateAsync(new ApplicationUser()
            {
                Email = value.Email,
                UserName = value.UserName,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Status = userManager.GetUserStatuses().Where(x => x.StatusDescription == value.UserStatus).FirstOrDefault(),
                WorkGroup = userManager.GetWorkGroupByWorkGroupNumber(value.WorkGroupNumber)
            }, "UserPass@DSB");

            if (result.Succeeded)
            {
                applicationDbContext.Users.Add(new User() 
                { 
                    ApplicationUserGUID = userManager.Users.Where(x => x.UserName == value.UserName).FirstOrDefault().Id
                }); 

                var addRoleResult = await userManager.AddToRoleAsync(await userManager.FindByNameAsync(value.UserName), value.Role);
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
        public async Task<IActionResult> PutAsync(string id, [FromBody] ApiUserModel value)
        {
            if (applicationIdentityContext.Users.Where(x => x.Email == value.Email).Any())
                return BadRequest($"User does not exist - with InputValue: {value.Email}");

            if (applicationIdentityContext.Users.Where(x => x.UserName == value.UserName).Any())
                return BadRequest($"User does not exist - with InputValue: {value.UserName}");

            if (!applicationIdentityContext.UserStatuses.Where(x => x.StatusDescription == value.UserStatus).Any())
                return BadRequest($"UserStatus does not exist - InputValue: {value.UserStatus}");

            if (!applicationIdentityContext.Roles.Where(x => x.Name == value.Role).Any())
                return BadRequest($"Role does not exist - InputValue: {value.Role}");

            if (!applicationIdentityContext.WorkGroups.Where(x => x.WorkGroupNumber == value.WorkGroupNumber).Any())
                return BadRequest($"WorkGroupNumber does not exist - InputValue: {value.WorkGroupNumber}");

            var user = await userManager.FindByIdAsync(id);

            user.Email = value.Email;
            user.UserName = value.UserName;
            user.UserStatusId = userManager.GetUserStatusByUser(user).UserStatusId;
            user.FirstName = value.FirstName;
            user.LastName = value.LastName;
            user.WorkGroupId = userManager.GetWorkGroupByUser(user).WorkGroupId;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);

            var userRoles = await userManager.GetRolesAsync(user);
            if (!userRoles.Contains(value.Role))
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return BadRequest(removeResult.Errors);
                
                var addResult = await userManager.AddToRoleAsync(user, value.Role);
                if (!addResult.Succeeded)
                    return BadRequest(addResult.Errors);
            }

            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (!applicationIdentityContext.Users.Where(x => x.Id == id).Any())
                return BadRequest($"User does not exist - with InputValue: {id}");

            var user = await userManager.FindByIdAsync(id);

            user.UserStatusId = userManager.GetUserStatuses().Where(x => x.StatusDescription == "Inactive").FirstOrDefault().UserStatusId;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return BadRequest(updateResult.Errors);

            return Ok();
        }
    }
}
