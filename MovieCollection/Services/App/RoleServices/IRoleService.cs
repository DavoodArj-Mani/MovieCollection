using System;
using System.Collections.Generic;
using MovieCollection.Model.App;

namespace MovieCollection.Services.App.RoleServices
{
    public interface IRoleService
    {
        IEnumerable<Role> QueryAllRoles();

        Role QueryRole(Guid RoleId);

        Role QueryRoleByName(string roleName);

        Role CreateRole(Role role);
    }
}
