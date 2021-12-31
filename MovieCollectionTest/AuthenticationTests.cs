using MovieCollection.Services.App.AuthenticationServices;
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
using MovieCollection.Services.App;

namespace MovieCollectionTest
{
    public class AuthenticationTests
    {
        private Mock<IAuthenticationService> _authenticationService;
        private Mock<IUserService> _userService;
        private Mock<IRoleService> _roleService;

        private ApplicationDbContext _db;

        public AuthenticationTests()
        {
            _authenticationService = new Mock<IAuthenticationService>();
            _userService = new Mock<IUserService>();
            _roleService = new Mock<IRoleService>();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _db = new ApplicationDbContext(builder.Options);
        }

        [Fact]
        public void LoginTest()
        {
            User returnUser = new User();
            returnUser.UserName = "a@a.com";

            _userService.Setup(x => x.login("a@a.com", "123456")).Returns(returnUser);
            var controller = new AuthenticationController(_authenticationService.Object, _userService.Object, _roleService.Object);

            LoginReqViewEntity loginReq = new LoginReqViewEntity();
            loginReq.UserName = "a@a.com";
            loginReq.Password = "123456";
            var result = controller.Login(loginReq);
            Assert.NotNull(result);
        }
        [Fact]
        public void SignUptest()
        {
            User returnUser = new User();
            returnUser.UserName = "a@a.com";

            User user = new User();
            user.UserName = "a@a.com";
            user.Password = "123456";

            _userService.Setup(x => x.CreateUser(user)).Returns(returnUser);
            var controller = new AuthenticationController(_authenticationService.Object, _userService.Object, _roleService.Object);

            User signupReq = new User();
            signupReq.UserName = "a@a.com";
            signupReq.Password = "123456";
            var result = controller.Signup(signupReq);
            Assert.NotNull(result);
        }
        [Fact]
        public void IsInUnitTest()
        {
            Assert.True(UnitTestDetector.IsInUnitTest,
                "Should detect that we are running inside a unit test."); // lol
        }
        [Fact]
        public void SignupDbTest()
        {
            User user = new User();
            user.UserName = "test1";
            user.Password = "pass1";

            _db.Users.Add(user);
            _db.SaveChanges();
            Assert.Equal(1, _db.Users.Count(a => a.UserName == "test1"));
        }
    }
}