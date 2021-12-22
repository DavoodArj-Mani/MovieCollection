using MovieCollection.Services.App.AuthenticationServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieCollection.Controllers.App;
using MovieCollection.Services.App.UserServices;
using MovieCollection.ViewEntity.App.AuthenticationServicesViewEntity;
using Xunit;
using MovieCollection.Model.Core;
using Microsoft.EntityFrameworkCore;
using System;
using MovieCollection.Model;
using System.Linq;
using MovieCollection.Services.App.RoleServices;

namespace MovieCollectionTest
{
    public class AuthenticationTests
    {
        private Mock<IAuthenticationService> _authenticationService;
        private Mock<IUserService> _userService;
        private Mock<IRoleService> _roleService;

        private ApplicationDbContext _context;


        public AuthenticationTests()
        {
            _authenticationService = new Mock<IAuthenticationService>();
            _userService = new Mock<IUserService>();
            _roleService = new Mock<IRoleService>();


            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new ApplicationDbContext(builder.Options);
        }

        [Fact]
        public void LoginNotFoundTest()
        { 
            var controller = new AuthenticationController(_authenticationService.Object, _userService.Object, _roleService.Object);

            LoginReqViewEntity loginReq = new LoginReqViewEntity();
            loginReq.UserName = "test1";
            loginReq.Password = "pass1";
            var result = controller.Login(loginReq);
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
        }
        [Fact]
        public void SignupForbidenTest()
        {
            var controller = new AuthenticationController(_authenticationService.Object, _userService.Object, _roleService.Object);

            User user = new User();
            user.UserName = "test1";
            user.Password = "pass1";
            var result = controller.Signup(user);
            var forbidResult = result as ForbidResult;

            // Assert 
            Assert.NotNull(forbidResult);
        }
        [Fact]
        public void SignupDbTest()
        {
            User user = new User();
            user.UserName = "test1";
            user.Password = "pass1";

            _context.Users.Add(user);
            _context.SaveChanges();
            Assert.Equal(1, _context.Users.Count(a => a.UserName == "test1"));
        }
    }
}
