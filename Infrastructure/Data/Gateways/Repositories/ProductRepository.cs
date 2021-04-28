using Core.Entities;
using Core.Interfaces.Gateways.Repositories;
using Infrastructure.Data.EF;

namespace Infrastructure.Data.Gateways.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ShoppingCartDbContext dbContext) : base(dbContext)
        {
        }
    }
}
