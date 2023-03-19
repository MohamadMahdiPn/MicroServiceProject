using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Feutures.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidatior :AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidatior()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty")
                                   .NotNull().WithMessage("UserName cannot be null")
                                   .MaximumLength(50);
        }
    }
}
