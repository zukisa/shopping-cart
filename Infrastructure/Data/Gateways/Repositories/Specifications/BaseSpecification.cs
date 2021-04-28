using Core.Interfaces.Gateways.Repositories;
using System;
using System.Linq.Expressions;

namespace Infrastructure.Data.Gateways.Repositories.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
        where T : class
    {
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
    }
}
