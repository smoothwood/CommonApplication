using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserRoleService : IUserRoleService
    {
        private IAsyncRepository<Role> _roleRepository;
        private IAsyncRepository<User> _userRepository;
        private IAsyncRepository<UserRole> _userRoleRepository;
        public UserRoleService(IAsyncRepository<Role> roleRepository, 
            IAsyncRepository<User> userRepository,
            IAsyncRepository<UserRole> userRoleRepository
        )
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IReadOnlyList<Role>> ListRoles()
        {
            return await _roleRepository.ListAllAsync();
        }

        public async Task<Role> GetRole(int roleId)
        {
            return await _roleRepository.GetByIdAsync(roleId);
        }

        public Task<Role> AddRole(Role role)
        {
            return _roleRepository.AddAsync(role);
        }

        public async Task<Role> UpdateRole(Role role)
        {
            Role _role = await _roleRepository.GetByIdAsync(role.RoleId);
            if (_role != null)
            {
                _role.RoleName = role.RoleName;
                _role.Description = role.Description;
                _role.UpdatedDate = DateTime.UtcNow;
                return await _roleRepository.UpdateAsync(_role);
            }
            else
            {
                return null;
            }  
        }

        public async Task<Role> DeleteRole(int roleId)
        {
            var spec = new UserFilterByRoleIdSpecification(null,null,roleId);
            var userRoles =await _userRoleRepository.ListAsync(spec);
            //there is user attached with this role
            if (userRoles.Count > 0)
            {
                return null;
            }
            Role _role = await _roleRepository.GetByIdAsync(roleId);
            if (_role != null)
            {
                return await _roleRepository.DeleteAsync(_role);
            }
            else
            {
                return null;
            }  
        }

        public async Task<IReadOnlyList<User>> ListUsers(int skip,int take, int? roleId)
        {
            if (roleId.HasValue)
            {
                var spec = new UserFilterByRoleIdSpecification(skip, take,roleId.Value);
                var userRoles = await _userRoleRepository.ListAsync(spec);
                List<User> users = new List<User>();
                for (int i = 0; i < userRoles.Count; i++)
                {
                    users.Add(userRoles[i].User);
                }
                return users;
            }
            else
            {
                var spec = new UserFilterPaginatedSpecification(skip, take);
                var users = await _userRepository.ListAsync(spec);
                return users;
            }
            
        }

        public async Task<User> GetUser(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<User> AddUser(User user,int[] roles)
        {
            var spec = new UserFilterByLoginIdSpecification(user.LoginId);
            var users = await _userRepository.ListAsync(spec);
            //duplicate loginId
            if (users.Count > 0)
            {
                return null;
            }

            User _user = await _userRepository.AddAsync(user);
            for(int i = 0; i < roles.Length; i++)
            {
                await _userRoleRepository.AddAsync(new UserRole() {UserId=_user.UserId,RoleId=roles[i] });
            }
            return _user;
        }

        public async Task<User> UpdateUser(User user,int[] roles)
        {
            User _user = await _userRepository.GetByIdAsync(user.UserId);
            if (_user != null)
            {
                var spec = new RoleFilterByUserIdSpecification(user.UserId);
                var userRoles = await _userRoleRepository.ListAsync(spec);
                for (int i = 0; i < userRoles.Count; i++)
                {
                    await _userRoleRepository.DeleteAsync(userRoles[i]);
                }

                _user.Active = user.Active;
                _user.DateOfBirth = user.DateOfBirth;
                _user.Email = user.Email;
                _user.FirstName = user.FirstName;
                _user.Gender = user.Gender;
                _user.LastName = user.LastName;
                _user.Password = user.Password;
                _user.PhoneNumber = user.PhoneNumber;
                _user.Portait = user.Portait;
                _user.UpdatedDate = DateTime.UtcNow;
                for(int i = 0; i < roles.Length; i++)
                {
                    _user.userRoles.Add(new UserRole() {UserId=user.UserId,RoleId=roles[i] });
                }
                return await _userRepository.UpdateAsync(_user);
            }
            else
            {
                return null;
            }
        }

        public async Task<User> DeleteUser(int userId)
        {
            User _user = await _userRepository.GetByIdAsync(userId);
            if (_user != null)
            {
                var spec = new RoleFilterByUserIdSpecification(userId);
                var userRoles = await _userRoleRepository.ListAsync(spec);
                //delete user's roles
                for (int i = 0; i < userRoles.Count; i++)
                {
                    await _userRoleRepository.DeleteAsync(userRoles[i]);
                }
                return await _userRepository.DeleteAsync(_user);
            }
            else
            {
                return null;
            }
        }

        public async Task<int> CountUser(int? roleId)
        {
            if (roleId.HasValue)
            {
                var spec = new UserFilterByRoleIdSpecification(null, null, roleId.Value);
                return await _userRoleRepository.CountAsync(spec);
            }
            else
            {
                var spec = new UserFilterPaginatedSpecification(null, null);
                return await _userRepository.CountAsync(spec);
            }
        }
    }
}
