using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Payment.PhonePe
{
    public class PhonePeRestResponseObject
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public PhonePeData Data { get; set; }
    }
}
