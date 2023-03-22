﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BasketCheckoutEvent :IntegrationBusEvent
    {
        public string UserName { get; set; }
        public int TotalPrice { get; set; }


        //Address
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        //public string ZipCode { get; set; }

        public string BankName { get; set; }
        public int PaymentMethod { get; set; }
        public string RefCode { get; set; }
    }
}
