using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    class UserFilterByLoginIdSpecification : BaseSpecification<User>
    {
        public UserFilterByLoginIdSpecification(string loginId):base(u=>u.LoginId == loginId)
        {

        }
    }
}
