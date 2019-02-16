using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool isPagingEnabled { get; private set; } = false;

        public List<Expression<Func<T, object>>> Includes { get; }= new List<Expression<Func<T, object>>>();

        public void AddInclude (Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            isPagingEnabled = true;
        }
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
    }
}
