using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollection.Model;
using MovieCollection.Model.App;
using MovieCollection.Model.Core;
using MovieCollection.Services.App.AuthenticationServices;
using MovieCollection.Services.App.RoleServices;
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
        public IRoleService _roleService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, IRoleService roleService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _roleService = roleService;
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
            {
                Role role =  _roleService.QueryRoleByName("User");
                UserRole userRole = new UserRole();
                userRole.UserId = result.UserId;
                userRole.RoleId = role.RoleId;
                _userService.CreateUserRole(userRole);

                return Ok(result);
            }

        }

        
    }
}
