using Common.EmailHelper;
using Common.EmailHelper.Classes;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions
{
    public static  class ServiceExtensions
    {
        public static void RegisterEmailServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
