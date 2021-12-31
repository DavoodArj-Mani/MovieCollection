using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieCollection.Model;
using MovieCollection.Model.App;
using MovieCollection.Model.Core;

namespace MovieCollection.Services.App.UserServices
{
    public class UserService : IUserService
    {
        public readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> QueryAllUsers()
        {
            var users = _db.Users.Select(c => new User { UserId = c.UserId, UserName = c.UserName });
            return users;
        }
        public User QueryUser(Guid userId)
        {
            if(_db.Users.Any(a => a.UserId == userId))
            {
                User user = _db.Users.Where(a => a.UserId == userId).Select(c => new User { UserId = c.UserId, UserName = c.UserName }).Single();
                return user;
            }
            return null;
        }
        public IEnumerable<User> QueryUserByName(string userName)
        {
            var users = _db.Users.Where(a => a.UserName.Contains(userName)).Select(c => new User { UserId = c.UserId, UserName = c.UserName });
            return users;
        }
        public Boolean IsUserExist(string userName)
        {
            if (_db.Users.Any(a => a.UserName == userName))
            {
                return true;
            }
            return false;
        }

        public User QueryOneUserByName(string userName)
        {
            if (_db.Users.Any(a => a.UserName == userName))
            {
                User user = _db.Users.Where(a => a.UserName == userName).Select(c => new User { UserId = c.UserId, UserName = c.UserName }).Single();
                return user;
            }
            return null;
        }

        public User CreateUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User login(string userName, string password)
        {
            if (_db.Users.Any(a => a.UserName == userName && a.Password == password))
            {
                User user = _db.Users.Where(a => a.UserName == userName && a.Password == password).Single();
                return user;
            }
            return null;
        }

        public IEnumerable<Role> QueryUserRoles(Guid userId)
        {
            List<Role> roles = new List<Role>();

            IEnumerable<UserRole> userRoles = _db.UserRoles.Where(a => a.UserId == userId);
            foreach (var userRole in userRoles)
            {
                Role role = _db.Roles.Where(a => a.RoleId == userRole.RoleId).Single();
                roles.Add(role);
            }
            return roles;
        }

        public UserRole CreateUserRole(UserRole userRole)
        {
            _db.UserRoles.Add(userRole);
            _db.SaveChanges();
            return userRole;
        }
    }
}
