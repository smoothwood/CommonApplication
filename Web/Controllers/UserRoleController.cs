using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Core.Helpers;

namespace Web.Controllers
{
    /// <summary>
    /// Controller for user and role
    /// </summary>
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private IUserRoleService _userRoleService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRoleService"></param>
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }
        /// <summary>
        /// List all roles
        /// </summary>
        /// <returns>Role list</returns>
        [HttpGet]
        [Route("RoleList")]
        public async Task<IReadOnlyList<Role>> ListRoles()
        {
            return await _userRoleService.ListRoles();
        }
        /// <summary>
        /// Get details of a single role based on roleId
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <returns>Role</returns>
        [HttpGet]
        [Route("Role")]
        public async Task<Role> GetRole([FromQuery(Name = "roleId")] int roleId)
        {
            return await _userRoleService.GetRole(roleId);
        }
        /// <summary>
        /// Add a new role
        /// Include roleName and description in POST
        /// </summary>
        /// <returns>Json object with roleId and roleName</returns>
        [HttpPost]
        [Route("Role")]
        public async Task<IActionResult> AddRole()
        {
            JObject jsonObj = Request.Body.GetJObject();
            string roleName = jsonObj["roleName"].ToString();
            string description = jsonObj["description"].ToString();
            Role role = new Role()
            {
                RoleName = roleName,
                Description = description
            };
            var result = await _userRoleService.AddRole(role);
            if (result != null)
            {
                return Ok(new { status = "success", roleId = result.RoleId, roleName = result.RoleName });
            }
            else
            {
                return Ok(new { status = "error" });
            }
        }
        /// <summary>
        /// Update an existing role
        /// Include roleId, roleName, description in POST
        /// </summary>
        /// <returns>Json object with roleId and roleName</returns>
        [HttpPut]
        [Route("Role")]
        public async Task<IActionResult> UpdateRole()
        {
            JObject jsonObj = Request.Body.GetJObject();
            int roleId = jsonObj.SelectToken("roleId").Value<int>();
            string roleName = jsonObj["roleName"].ToString();
            string description = jsonObj["description"].ToString();
            Role role = new Role()
            {
                RoleId = roleId,
                RoleName = roleName,
                Description = description
            };
            var result = await _userRoleService.UpdateRole(role);
            if (result != null)
            {
                return Ok(new { status = "success", roleId = result.RoleId, roleName = result.RoleName });
            }
            else
            {
                return Ok(new { status = "error" });
            }

        }
        /// <summary>
        /// Delete an existing role
        /// Include roleId in POST
        /// </summary>
        /// <returns>Json object with roleId and roleName</returns>
        [HttpDelete]
        [Route("Role")]
        public async Task<IActionResult> DeleteRole()
        {
            JObject jsonObj = Request.Body.GetJObject();
            int roleId = jsonObj.SelectToken("roleId").Value<int>();
            var result = await _userRoleService.DeleteRole(roleId);
            if (result != null)
            {
                return Ok(new { status = "success", roleId = result.RoleId, roleName = result.RoleName });
            }
            else
            {
                return Ok(new { status = "error" });
            }
        }
        /// <summary>
        /// Get all users
        /// Include skip and take for server side pagination, roleId is optional
        /// </summary>
        /// <returns>UserDTO</returns>
        [HttpPost]
        [Route("UserList")]
        public async Task<JsonResult> ListUsers()
        {
            JObject jsonObj = Request.Body.GetJObject();
            int skip = jsonObj.SelectToken("skip").Value<int>();
            int take = jsonObj.SelectToken("take").Value<int>();
            int? roleId = jsonObj["roleId"] != null ? jsonObj.SelectToken("roleId").Value<int>() : null as int?;
            IReadOnlyList<User> users = await _userRoleService.ListUsers(skip, take, roleId);
            List<UserDTO> usersDTO = new List<UserDTO>();
            for (int i = 0; i < users.Count; i++)
            {
                usersDTO.Add(new UserDTO()
                {
                    UserId = users[i].UserId,
                    LoginId = users[i].LoginId,
                    FirstName = users[i].FirstName,
                    LastName = users[i].LastName,
                    Portait = users[i].Portait,
                    Gender = users[i].Gender,
                    DateOfBirth = users[i].DateOfBirth,
                    PhoneNumber = users[i].PhoneNumber,
                    Email = users[i].PhoneNumber
                });
            }
            int totalRecord = await _userRoleService.CountUser(roleId);
            //Adding recordsTotal and recordsFiltered in order to be compatible with Jquery datatable
            return Json(new
            {
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = usersDTO
            });
        }
        /// <summary>
        /// Get details of a single user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>UserDTO</returns>
        [HttpGet]
        [Route("User")]
        public async Task<UserDTO> GetUser([FromQuery(Name = "userId")] int userId)
        {
            User user = await _userRoleService.GetUser(userId);
            return new UserDTO()
            {
                UserId = user.UserId,
                LoginId = user.LoginId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Portait = user.Portait,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                Email = user.PhoneNumber
            };
        }
        /// <summary>
        /// Add a new user
        /// Include loginId, active, email, firstName, lastname, password, phoneNumber, portait in POST
        /// </summary>
        /// <returns>Json object with userId and loginId</returns>
        [HttpPost]
        [Route("User")]
        public async Task<IActionResult> AddUser()
        {
            JObject jsonObj = Request.Body.GetJObject();
            User user = new User()
            {
                LoginId = jsonObj["loginId"].ToString(),
                Active = !String.IsNullOrEmpty(jsonObj["active"].ToString()) ? Convert.ToBoolean(jsonObj["active"]) : true,
                Email = jsonObj["email"].ToString(),
                FirstName = jsonObj["firstName"].ToString(),
                Gender = !String.IsNullOrEmpty(jsonObj["gender"].ToString()) ? Convert.ToBoolean(jsonObj["gender"]) : null as bool?,
                LastName = jsonObj["lastName"].ToString(),
                Password = jsonObj["password"].ToString().GetMD5HashedValue(),
                PhoneNumber = jsonObj["phoneNumber"].ToString(),
                Portait = jsonObj["portait"].ToString(),
                DateOfBirth = !String.IsNullOrEmpty(jsonObj["dateOfBirth"].Value<string>()) ? Convert.ToDateTime(jsonObj["dateOfBirth"]) : null as DateTime?
            };

            int[] roles = new int[jsonObj["roles"].Count()];
            for (int i = 0; i < roles.Count(); i++)
            {
                roles[i] = Convert.ToInt32(jsonObj["roles"][i]);
            }

            var result = await _userRoleService.AddUser(user, roles);
            if (result != null)
            {
                return Ok(new { status = "success", userId = result.UserId, loginId = result.LoginId });
            }
            else
            {
                return Ok(new { status = "error" });
            }

        }
        /// <summary>
        /// Update an existing user
        /// Include userId, loginId, active, email, firstName, lastname, password, phoneNumber, portait in POST
        /// </summary>
        /// <returns>Json object with userId and loginId</returns>
        [HttpPut]
        [Route("User")]
        public async Task<IActionResult> UpdateUser()
        {
            JObject jsonObj = Request.Body.GetJObject();
            User user = new User()
            {
                UserId = jsonObj.SelectToken("userId").Value<int>(),
                LoginId = jsonObj["loginId"].ToString(),
                Active = !String.IsNullOrEmpty(jsonObj["active"].ToString()) ? Convert.ToBoolean(jsonObj["active"]) : true,
                Email = jsonObj["email"].ToString(),
                FirstName = jsonObj["firstName"].ToString(),
                Gender = !String.IsNullOrEmpty(jsonObj["gender"].ToString()) ? Convert.ToBoolean(jsonObj["gender"]) : null as bool?,
                LastName = jsonObj["lastName"].ToString(),
                Password = jsonObj["password"].ToString().GetMD5HashedValue(),
                PhoneNumber = jsonObj["phoneNumber"].ToString(),
                Portait = jsonObj["portait"].ToString(),
                DateOfBirth = !String.IsNullOrEmpty(jsonObj["dateOfBirth"].Value<string>()) ? Convert.ToDateTime(jsonObj["dateOfBirth"]) : null as DateTime?
            };

            int[] roles = new int[jsonObj["roles"].Count()];
            for (int i = 0; i < roles.Count(); i++)
            {
                roles[i] = Convert.ToInt32(jsonObj["roles"][i]);
            }
            var result = await _userRoleService.UpdateUser(user, roles);
            if (result != null)
            {
                return Ok(new { status = "success", userId = result.UserId, loginId = result.LoginId });
            }
            else
            {
                return Ok(new { status = "error" });
            }
        }
        /// <summary>
        /// Delete an existing user
        /// Include userId in POST
        /// </summary>
        /// <returns>Json object with userId and loginId</returns>
        [HttpDelete]
        [Route("User")]
        public async Task<IActionResult> DeleteUser()
        {
            JObject jsonObj = Request.Body.GetJObject();
            int userId = jsonObj.SelectToken("userId").Value<int>();
            var result = await _userRoleService.DeleteUser(userId);
            if (result != null)
            {
                return Ok(new { status = "success", userId = result.UserId, loginId = result.LoginId });
            }
            else
            {
                return Ok(new { status = "error" });
            }

        }
    }
}