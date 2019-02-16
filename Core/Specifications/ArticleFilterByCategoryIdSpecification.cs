using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class ArticleFilterByCategoryIdSpecification:BaseSpecification<Article>
    {
        public ArticleFilterByCategoryIdSpecification(int? skip, int? take, int? categoryId):base( a=>(!categoryId.HasValue|| a.CategoryId == categoryId))
        {
            if(skip.HasValue && take.HasValue)
            {
                ApplyPaging(skip.Value, take.Value);
            }
            AddInclude(a => a.Category);
        }
    }
}
