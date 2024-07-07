using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Payment.RazorPay
{
    public class PaymentDetails
    {
        public string PaymentId {get; set; }
        public string OrderId { get; set; }
        public string Signature { get; set; }
    }
}
