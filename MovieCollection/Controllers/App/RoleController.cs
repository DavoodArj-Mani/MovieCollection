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
    public class RoleController : Controller
    {
        public IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Route("QueryAll")]
        public IActionResult QueryAll()
        {
            var result = _roleService.QueryAllRoles();
            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CrateRole([FromForm] Role role)
        {
            var roleIsExist = _roleService.QueryRoleByName(role.RoleName);
            if (roleIsExist != null)
            {
                return BadRequest();
            }
            var result = _roleService.CreateRole(role);
            return Ok(result);
        }
    }
}
