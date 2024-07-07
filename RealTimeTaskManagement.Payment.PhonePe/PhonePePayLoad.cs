using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Payment.PhonePe
{
    public class PhonePePayLoad
    {
        public string PayLoad { get; set; }
        public string RequestUri { get; set; }
        public string SaltKey { get; set; }
        public string SaltIndex { get; set; }
        public string BaseUrl { get; set; }
        public string RestAPIRootUri { get; set; }
    }
}
