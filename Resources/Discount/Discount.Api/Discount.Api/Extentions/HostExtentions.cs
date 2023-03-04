using System;
using System.Data.Common;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Api.Extentions
{
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvalabality = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("migrate pg db");
                   using var _connection = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSettings:ConnectionString"));
                    _connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = _connection
                    };
                    command.CommandText = "DROP TABLE IF EXISTS copon";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE copon(Id SERIAL PRIMARY KEY,
                                                                ProductName VARCHAR(200) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                    command.ExecuteNonQuery();
                    command.CommandText =
                        "INSERT INTO copon(ProductName,Description,amount) VALUES ('Xiaomi Note 9s','mamad',1);";
                    command.ExecuteNonQuery();
                    command.CommandText =
                        "INSERT INTO copon(ProductName,Description,amount) VALUES ('Huawei mate 9','Sajjad',1);";
                    command.ExecuteNonQuery();
                    logger.LogInformation("Migration Done");
                }
                catch (NpgsqlException e)
                {
                    logger.LogError(e.Message);
                    if (retryForAvalabality<50)
                    {
                        retryForAvalabality++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvalabality);
                    }
                }

            }

            return host;
        }
    }
}
