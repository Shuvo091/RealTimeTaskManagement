﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Notification.Twilio
{
    public class WhatsAppMessagePayLoad
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string FromNumber { get; set; }
        public string ToNumber { get; set; }
        public string Message { get; set; }
    }
}
