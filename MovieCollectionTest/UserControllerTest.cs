using Moq;
using MovieCollection.Controllers.App;
using MovieCollection.Services.App.UserServices;
using Xunit;
using MovieCollection.Model.Core;
using Microsoft.EntityFrameworkCore;
using System;
using MovieCollection.Model;
using MovieCollection.Services.App.RoleServices;
using System.Collections.Generic;
using MovieCollection.Model.App;

namespace MovieCollectionTest
{
    public class UserControllerTest
    {
        private Mock<IUserService> _userService;
        private Mock<IRoleService> _roleService;

        private ApplicationDbContext _db;

        
        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _roleService = new Mock<IRoleService>();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _db = new ApplicationDbContext(builder.Options);
        }

        [Fact]
        public void QueryAll()
        {
            List<User> returnUsers = new List<User>();
            returnUsers.Add(new User() { UserName = "a@a.com" });
            returnUsers.Add(new User() { UserName = "a@b.com" });

            _userService.Setup(x => x.QueryAllUsers()).Returns(returnUsers);
            var controller = new UserController(_userService.Object, _roleService.Object);

            var result = controller.QueryAll();
            Assert.NotNull(result);
        }

        [Fact]
        public void QueryById()
        {
            var guid = new Guid();
            _userService.Setup(x => x.QueryUser(guid)).Returns(new User() { UserName = "a@a.com" });
            var controller = new UserController(_userService.Object, _roleService.Object);

            var result = controller.QueryById(guid);
            Assert.NotNull(result);
        }

        [Fact]
        public void QueryUserByName()
        {
            List<User> returnUsers = new List<User>();
            returnUsers.Add(new User() { UserName = "a@a.com" });
            returnUsers.Add(new User() { UserName = "a@b.com" });

            _userService.Setup(x => x.QueryUserByName("a@")).Returns(returnUsers);
            var controller = new UserController(_userService.Object, _roleService.Object);

            var result = controller.QueryUserByName("a@");
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateUserRole()
        {

            var userGuid = new Guid();
            _userService.Setup(x => x.QueryUser(userGuid)).Returns(new User() { UserName = "a@a.com" });

            var roleGuid = new Guid();
            _roleService.Setup(x => x.QueryRole(roleGuid)).Returns(new Role() { RoleName = "Admin" });



            UserRole userRole = new UserRole();
            userRole.UserId = new Guid();
            userRole.RoleId = new Guid();
            _userService.Setup(x => x.CreateUserRole(userRole)).Returns(userRole);

            var controller = new UserController(_userService.Object, _roleService.Object);
            var result = controller.CreateUserRole(userGuid, roleGuid);
            Assert.NotNull(result);
        } 
    }
}
