using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace RealTimeTaskManagement.Notification.Twilio
{
    public class MobileMessageSender : IMessageSender<MobileMessagePayLoad>
    {
        public void SendMessage(MobileMessagePayLoad payLoad)
        {
            TwilioClient.Init(payLoad.AccountSid, payLoad.AuthToken);

            var verification = VerificationResource.Create(
                to: payLoad.ToNumber,
                channel: payLoad.Channel,
                pathServiceSid: payLoad.ServiceSid
            );
        }
    }
}
