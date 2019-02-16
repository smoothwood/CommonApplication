using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class UserFilterByRoleIdSpecification : BaseSpecification<UserRole>
    {
        public UserFilterByRoleIdSpecification(int? skip, int? take, int roleId):base(r=>r.RoleId==roleId)
        {
            if(skip.HasValue && take.HasValue)
            {
                ApplyPaging(skip.Value, take.Value);
            }
            AddInclude(r=>r.User);
            AddInclude(r=>r.Role);
        }
    }
}
