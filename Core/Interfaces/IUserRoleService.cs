using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRoleService
    {
        Task<IReadOnlyList<Role>> ListRoles();
        Task<Role> GetRole(int roleId);
        Task<Role> AddRole(Role role);
        Task<Role> UpdateRole(Role role);
        Task<Role> DeleteRole(int roleId);
        Task<IReadOnlyList<User>> ListUsers(int skip, int take, int? roleId);
        Task<User> GetUser(int userId);
        Task<User> AddUser(User user,int[] roles);
        Task<User> UpdateUser(User user,int[] roles);
        Task<User> DeleteUser(int userId);
        Task<int> CountUser(int? roleId);
    }
}
