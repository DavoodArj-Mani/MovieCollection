using System;
using System.Collections.Generic;
using MovieCollection.Model.App;
using MovieCollection.Model.Core;

namespace MovieCollection.Services.App.UserServices
{
    public interface IUserService
    {
        IEnumerable<User> QueryAllUsers();

        User QueryUser(Guid userId);

        User QueryUserByName(string userName);

        User CreateUser(User user);

        User login(string userName, string password);

        IEnumerable<Role> QueryUserRoles(Guid userId);

        UserRole CreateUserRole(UserRole userRole);
    }
}
