using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>

    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public System.Linq.Expressions.Expression<Func<T, bool>> Criteria {get;}

        public List<System.Linq.Expressions.Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get;private set;}

        public Expression<Func<T, object>> OrderByDecending  {get;private set;}

        public int Take {get;private set;}

        public int Skip {get;private set;}

        public bool IsPagingEnables {get;private set;}

        protected void AddInclude(Expression<Func<T,object>> includeExpresion)
        {
            Includes.Add(includeExpresion) ;  
        }

        protected void AddOrderBy(Expression<Func<T,object>> orderByExp)
        {
            OrderBy = orderByExp;  
        }
        protected void AddOrderByDecending(Expression<Func<T,object>> orderByExp)
        {
            OrderByDecending = orderByExp;  
        }

        protected void AddPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnables = true;
        }
    }
}