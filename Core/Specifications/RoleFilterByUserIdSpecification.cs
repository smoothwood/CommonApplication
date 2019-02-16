using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class RoleFilterByUserIdSpecification : BaseSpecification<UserRole>
    {
        public RoleFilterByUserIdSpecification(int userId):base(ur => ur.UserId == userId)
        {
            AddInclude(ur => ur.Role);
        }
    }
}
