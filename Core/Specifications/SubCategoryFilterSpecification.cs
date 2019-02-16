using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class SubCategoryFilterSpecification : BaseSpecification<Category>
    {
        public SubCategoryFilterSpecification(int parentCateogryId):base(c=>c.ParentCategoryId == parentCateogryId)
        {

        }
    }
}
