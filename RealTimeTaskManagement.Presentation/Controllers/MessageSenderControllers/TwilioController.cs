using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeTaskManagement.Notification.Twilio;

namespace RealTimeTaskManagement.Presentation.Controllers.PaymentControllers
{
    [Authorize]
    public class TwilioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MessageSent()
        {
            return View();
        }

        public IActionResult OTPSent()
        {
            return View();
        }

        public IActionResult SendWhatsAppNotification(WhatsAppMessagePayLoad payLoad)
        {
            payLoad.AccountSid = "ACb8a3ffdd5972f27c8b9c9c0483918fa3";
            payLoad.AuthToken = "26be3648c02bac8d29188495f931bbb9";
            payLoad.FromNumber = "+14155238886";

            var waClient = new WhatsAppMessageSender();
            waClient.SendMessage(payLoad);
            return RedirectToAction("MessageSent");
        }

        public IActionResult SendMobileOTP(MobileMessagePayLoad payLoad)
        {
            payLoad.AccountSid = "ACb8a3ffdd5972f27c8b9c9c0483918fa3";
            payLoad.AuthToken = "26be3648c02bac8d29188495f931bbb9";
            payLoad.ServiceSid = "VAa65721b718ce9b7fd5b1fd18b12bba09"; // Need proper SID. Free accounts cannot have WhatsApp sender.
            payLoad.Channel = "sms"; // "sms" for mobile OTP

            var waClient = new MobileMessageSender();
            waClient.SendMessage(payLoad);
            return RedirectToAction("OTPSent");
        }
    }
}
