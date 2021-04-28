using Core.Dto;
using Core.Entities;
using Core.Interfaces.Gateways.Repositories;
using Infrastructure.Data.EF;
using Infrastructure.Extensions;
using System.Linq;

namespace Infrastructure.Data.Gateways.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ShoppingCartDbContext dbContext) : base(dbContext)
        {
        }

        public OrderDto GenerateOrderNo()
        {
            return _dbContext.LoadCommand("spGenerateOrderNo")
                             .ExecuteStoredProc<OrderDto>()
                             .FirstOrDefault();
        }
    }
}
