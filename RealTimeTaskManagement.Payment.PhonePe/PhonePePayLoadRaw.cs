using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Payment.PhonePe
{
    public class PhonePePayLoadRaw
    {
        public string MerchantId { set; get; }
        public string MerchantTransactionId { set; get; }
        public string MerchantUserId { set; get; }
        public string Amount { set; get; }
        public string RedirectUrl { set; get; }
        public string RedirectMode { set; get; }
        public string CallBackUrl { set; get; }
        public string MobileNumber { set; get; }
        public PaymentInstrument PaymentInstrument { set; get; }
    }
    public class PaymentInstrument
    {
        public string Type { set; get; }
    }
}
