using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RealTimeTaskManagement.Notification.Twilio
{
    public class WhatsAppMessageSender : IMessageSender<WhatsAppMessagePayLoad>
    {
        public void SendMessage(WhatsAppMessagePayLoad payLoad)
        {
            TwilioClient.Init(payLoad.AccountSid, payLoad.AuthToken);

            var messageOptions = new CreateMessageOptions(
              new PhoneNumber("whatsapp:" + payLoad.ToNumber));
            messageOptions.From = new PhoneNumber("whatsapp:" + payLoad.FromNumber);
            messageOptions.Body = payLoad.Message;


            var message = MessageResource.Create(messageOptions);
        }
    }
}
