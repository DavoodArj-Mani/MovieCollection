using System;
using System.Collections.Generic;
using System.Linq;
using MovieCollection.Model;
using MovieCollection.Model.App;

namespace MovieCollection.Services.App.RoleServices
{
    public class RoleService : IRoleService
    {
        public readonly ApplicationDbContext _db;

        public RoleService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Role> QueryAllRoles()
        {
            var role = _db.Roles;
            return role;
        }
        public Role QueryRole(Guid RoleId)
        {

            if (_db.Roles.Any(a => a.RoleId == RoleId))
            {
                Role role = _db.Roles.Where(a => a.RoleId == RoleId).Single();
                return role;
            }
            else
            {
                return null;
            }
        }

        public Role QueryRoleByName(string roleName)
        {
            if(_db.Roles.Any(a => a.RoleName == roleName))
            {
                Role role = _db.Roles.Where(a => a.RoleName == roleName).Single();
                return role;
            }
            else
            {
                return null;
            }
            
        }

        public Role CreateRole(Role role)
        {
            _db.Roles.Add(role);
            _db.SaveChanges();
            return role;
        }
    }
}
