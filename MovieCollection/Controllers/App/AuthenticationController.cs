using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollection.Model;
using MovieCollection.Model.Core;
using MovieCollection.Services.App.AuthenticationServices;
using MovieCollection.Services.App.UserServices;
using MovieCollection.ViewEntity.App.AuthenticationServicesViewEntity;

namespace MovieCollection.Controllers.App
{
    [Route("api/{controller}")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        public IAuthenticationService _authenticationService;
        public IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginReqViewEntity authenticationReq)
        {
            var authResult = _authenticationService.Login(authenticationReq);
            if (authResult == null)
                return NotFound();
            else
                return Ok(authResult);
        }

        [HttpPost]
        [Route("SignUp")]
        public IActionResult Signup([FromBody] User user)
        {
            var userIsExist = _userService.QueryUserByName(user.UserName);
            if (userIsExist != null)
            {
                return BadRequest();
            }
            var result = _userService.CreateUser(user);
            if (result == null)
                return Forbid();
            else
                return Ok(result);

        }

        
    }
}
