using Moq;
using MovieCollection.Controllers.App;
using Xunit;
using MovieCollection.Model.Core;
using Microsoft.EntityFrameworkCore;
using System;
using MovieCollection.Model;
using System.Linq;
using MovieCollection.Services.App.RoleServices;
using MovieCollection.Services.App;
using MovieCollection.Model.App;
using System.Collections.Generic;

namespace MovieCollectionTest
{
    public class RoleControllerTest
    {
        private Mock<IRoleService> _roleService;
        private ApplicationDbContext _db;

        public RoleControllerTest()
        {
            _roleService = new Mock<IRoleService>();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _db = new ApplicationDbContext(builder.Options);
        }

        //QueryAll
        [Fact]
        public void QueryAll()
        {
            List<Role> returnRoles = new List<Role>();
            returnRoles.Add(new Role() { RoleName = "Admin" });
            returnRoles.Add(new Role() { RoleName = "User" });
            _roleService.Setup(x => x.QueryAllRoles()).Returns(returnRoles);

            var controller = new RoleController(_roleService.Object);

            var result = controller.QueryAll();
            Assert.NotNull(result);
        }
        //CrateRole
        [Fact]
        public void CrateRole()
        {
            Role role = new Role();
            role.RoleName = "Admin";
            _roleService.Setup(x => x.CreateRole(role)).Returns(role);

            var controller = new RoleController(_roleService.Object);
            var result = controller.CrateRole(role);
            Assert.NotNull(result);

        }
    }
}
