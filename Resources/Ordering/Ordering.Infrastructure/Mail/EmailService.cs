using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Application.Contracts.Infrastructures;
using Ordering.Application.Models;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService:IEmailService
    {
        public Task<bool> SendEmail(Email email)
        {
            throw new NotImplementedException();
        }
    }
}
