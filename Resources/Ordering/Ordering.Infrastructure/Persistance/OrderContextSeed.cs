using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext)
        {
            if (!orderContext.Orders.Any())
            {
                await orderContext.Orders.AddRangeAsync(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
            }
        }
        public static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>()
            {
                new()
                {
                    FirstName = "Mohammad",
                    LastName ="mamadi",
                    UserName="1920",
                    EmailAddress ="mmmp@hotmail.com",
                    City = "Tehran",
                    Country = "Iran",
                    TotalPrice = 1000000
                },
                new Order
                {
                    FirstName = "mohammad",
                    LastName = "ordookhani",
                    UserName = "mohammad",
                    EmailAddress = "test@test.com",
                    City = "tehran",
                    Country = "iran",
                    TotalPrice = 10000
                }
            };
        }
    }
}
