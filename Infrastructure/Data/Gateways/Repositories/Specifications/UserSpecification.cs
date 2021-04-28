using Core.Entities;
using System;
using System.Linq.Expressions;

namespace Infrastructure.Data.Gateways.Repositories.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(Expression<Func<User, bool>> criteria) : base(criteria)
        {
        }
    }
}
