using Core.Interfaces.Gateways.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data.Auth;
using Infrastructure.Data.Gateways.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure Db Context for Api
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="connectionStringName"></param>
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration, string connectionStringName = "Default")
        {
            services.AddDbContext<Data.EF.ShoppingCartDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(connectionStringName)));
        }
        /// <summary>
        /// Register Repositories to Ioc container
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            
        }
        /// <summary>
        /// Register services to container
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtFactory, JwtFactory>();
        }
    }
}
