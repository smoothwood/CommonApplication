using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class UserFilterByLoginIdPasswordSpecification : BaseSpecification<User>
    {
        public UserFilterByLoginIdPasswordSpecification(string loginId, string password) : base(u => u.LoginId == loginId && u.Password == password && u.Active == true)
        {

        }
    }
}
