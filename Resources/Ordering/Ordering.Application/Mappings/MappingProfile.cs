using AutoMapper;
using Ordering.Application.Feutures.Orders.Commands.CheckoutCommand;
using Ordering.Application.Feutures.Orders.Commands.UpdateOrder;
using Ordering.Application.Feutures.Orders.Queries.GetOrderslist;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Order , OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
