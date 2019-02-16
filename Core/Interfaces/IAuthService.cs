using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateUser(string loginId, string password);
    }
}
