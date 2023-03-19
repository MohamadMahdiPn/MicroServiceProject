using System;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ordering.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDataBase<TContext>(this IHost host,
                                            Action<TContext,IServiceProvider> seeder,
                                            int? retry = 0) where TContext:DbContext
        {
            int retryForAvailability = retry.HasValue ? retry.Value : 0;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TContext>();
                try
                {
                    
                   InvokeSeeder(seeder,context,services);
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    if (retryForAvailability <50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDataBase<TContext>(host, seeder, retryForAvailability);
                    }
                }
               
            }
            return host;
        }

        private static void InvokeSeeder<TContext>(
            Action<TContext, IServiceProvider> seeder,
            TContext context,
            IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
