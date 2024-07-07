using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Payment.PhonePe
{
    public class PhonePeData
    {
        public string MerchantId { get; set; }
        public string MerchantTransactionId { get; set; }
        public PhonePeInstrumentResponse InstrumentResponse { get; set; }
    }
}
