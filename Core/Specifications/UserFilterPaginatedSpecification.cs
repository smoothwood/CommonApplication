using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class UserFilterPaginatedSpecification : BaseSpecification<User>
    {
        public UserFilterPaginatedSpecification(int? skip,int? take):base(u=>true)
        {
            if(skip.HasValue && take.HasValue)
            {
                ApplyPaging(skip.Value, take.Value);
            }
        }
    }
}
