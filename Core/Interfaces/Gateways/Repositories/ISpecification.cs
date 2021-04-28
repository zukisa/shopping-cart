using System;
using System.Linq.Expressions;

namespace Core.Interfaces.Gateways.Repositories
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}
