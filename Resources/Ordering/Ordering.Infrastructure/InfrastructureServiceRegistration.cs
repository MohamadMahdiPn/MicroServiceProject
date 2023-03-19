using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Ordering.Application.Contracts.Infrastructures;
using Ordering.Application.Contracts.Persistance;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistance;
using Ordering.Infrastructure.Repository;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }

   
}
