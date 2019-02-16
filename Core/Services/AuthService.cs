using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<UserRole> _userRoleRepository;
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config,IAsyncRepository<User> userRepository,IAsyncRepository<UserRole> userRoleRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<string> AuthenticateUser(string loginId, string password)
        {
            var userSpec = new UserFilterByLoginIdPasswordSpecification(loginId, password.GetMD5HashedValue());
            var user = await _userRepository.GetSingleBySpecAsync(userSpec);
            string token = "";
            if (user != null)
            {
                //get user's roles
                var roleSpec = new RoleFilterByUserIdSpecification(user.UserId);
                var userRoles = await _userRoleRepository.ListAsync(roleSpec);
                string[] roles = new string[userRoles.Count];
                for(int i = 0; i < userRoles.Count; i++)
                {
                    roles[i] = userRoles[i].Role.RoleName;
                }
 
                token = GenerateToken(user,roles);
            }
            return token;
        }

        private string GenerateToken(User user, string[] roles)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserId", user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.LoginId));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName ?? ""));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName ?? ""));
            claims.Add(new Claim("Portait", user.Portait ?? ""));
            claims.Add(new Claim(ClaimTypes.Gender, Convert.ToString(user.Gender)));
            claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""));
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
            for(int i = 0; i < roles.Length; i++)
            {
                claims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                Audience = _config["JWT:Issuer"],
                Issuer = _config["JWT:Issuer"],
                Subject = claimsIdentity,
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
            return signedAndEncodedToken;
        }

    }
}
