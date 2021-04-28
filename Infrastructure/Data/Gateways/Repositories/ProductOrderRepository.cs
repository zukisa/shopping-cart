using Core.Entities;
using Core.Interfaces.Gateways.Repositories;
using Infrastructure.Data.EF;

namespace Infrastructure.Data.Gateways.Repositories
{
    public class ProductOrderRepository : RepositoryBase<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(ShoppingCartDbContext dbContext) : base(dbContext)
        {
        }
    }
}
