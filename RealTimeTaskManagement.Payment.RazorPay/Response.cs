﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Payment.RazorPay
{
    public class Response
    {
        public string OrderId { get; set; }
        public string RazorpayKey { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
