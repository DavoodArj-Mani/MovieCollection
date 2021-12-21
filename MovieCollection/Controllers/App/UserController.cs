using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollection.Model.App;
using MovieCollection.Model.Core;
using MovieCollection.Services.App.RoleServices;
using MovieCollection.Services.App.UserServices;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieCollection.Controllers.App
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    [Route("api/{controller}")]
    [ApiController]
    public class UserController : Controller
    {
        public IUserService _userService;
        public IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        [Route("QueryAll")]
        public IActionResult QueryAll()
        {
            var result = _userService.QueryAllUsers();
            return Ok(result);
        }

        [HttpPost]
        [Route("QueryById")]
        public IActionResult QueryById([FromForm] Guid userId)
        {
            var result = _userService.QueryUser(userId);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        [Route("CreateUserRole")]
        public IActionResult CreateUserRole([FromForm] Guid userId, [FromForm] Guid roleId)
        {
            var user = _userService.QueryUser(userId);
            if (user == null)
            {
                return NotFound(userId);
            }
            var role = _roleService.QueryRole(roleId);
            if (role == null)
            {
                return NotFound(roleId);
            }

            UserRole userRole = new UserRole();
            userRole.UserId = userId;
            userRole.RoleId = roleId;
            var result = _userService.CreateUserRole(userRole);
            return Ok(result);
        }

        [HttpGet]
        [Route("Install")]
        public IActionResult Install()
        {
            var user_admin = _userService.QueryUserByName("a@a.com");
            if (user_admin == null)
            {
                user_admin.UserName = "a@a.com";
                user_admin.Password = "123456";
                _userService.CreateUser(user_admin);
            }

            var admin_role = _roleService.QueryRoleByName("Admin");
            if (admin_role == null)
            {
                admin_role.RoleName = "Admin";
                _roleService.CreateRole(admin_role);

                UserRole userRole = new UserRole();
                userRole.RoleId = admin_role.RoleId;
                userRole.UserId = user_admin.UserId;
                _userService.CreateUserRole(userRole);
            }

            var user_role = _roleService.QueryRoleByName("User");
            if (user_role == null)
            {
                user_role.RoleName = "User";
                _roleService.CreateRole(user_role);

                UserRole userRole = new UserRole();
                userRole.RoleId = user_role.RoleId;
                userRole.UserId = user_admin.UserId;
                _userService.CreateUserRole(userRole);
            }
            return Ok();
        }
    }
}
