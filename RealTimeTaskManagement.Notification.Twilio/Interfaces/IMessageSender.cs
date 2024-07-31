using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Notification.Twilio
{
    public interface IMessageSender<T>
    {
        public void SendMessage(T payLoad);
    }
}
