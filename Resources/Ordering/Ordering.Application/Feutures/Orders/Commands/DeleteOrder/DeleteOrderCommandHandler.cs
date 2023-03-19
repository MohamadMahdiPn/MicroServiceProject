using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Feutures.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }


        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForDelete = await _orderRepository.GetByIdAsync(request.Id);
            if (orderForDelete == null)
            {
                throw new NotFoundException(nameof(orderForDelete), request.Id);
            }
            else
            {
                await _orderRepository.DeleteAsync(orderForDelete);
            }




            return Unit.Value;
        }
    }
}
