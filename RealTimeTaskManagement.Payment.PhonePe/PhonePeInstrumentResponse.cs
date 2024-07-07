using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Payment.PhonePe
{
    public class PhonePeInstrumentResponse
    {
        public string Type { get; set; }
        public PhonePeRedirectInfo RedirectInfo { get; set; }
    }
}
