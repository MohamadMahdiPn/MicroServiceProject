﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class NotFoundException :ApplicationException
    {
        public NotFoundException(string name, object key):base($"Entity \"{name}\" and key \"{key} \" was not found")
        {
            
        }
    }
}
