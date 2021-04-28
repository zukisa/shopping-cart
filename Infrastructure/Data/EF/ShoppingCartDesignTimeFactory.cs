using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.EF
{
    public class ShoppingCartDesignTimeFactory : DesignTimeDbContextFactoryBase<ShoppingCartDbContext>
    {
        protected override ShoppingCartDbContext CreateNewInstance(DbContextOptions<ShoppingCartDbContext> options)
        {
            return new ShoppingCartDbContext(options);
        }
    }
}
