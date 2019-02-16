using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Core.Helpers;

namespace Web.Controllers
{
    /// <summary>
    /// Controller for validating user and return the JWT token
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private IAuthService _authService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authService"></param>
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Pass loginId and password to validate user
        /// </summary>
        /// <returns>JWT token</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            JObject jsonObj = Request.Body.GetJObject();
            string loginId = jsonObj["loginId"].ToString();
            string password = jsonObj["password"].ToString();

            string token = await _authService.AuthenticateUser(loginId, password);
            IActionResult response = BadRequest();
            if (token != "")
            {
                response = Ok(new { token = token });
            }
            return response;

        }
    }
}