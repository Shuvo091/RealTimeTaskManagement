using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeTaskManagement.Payment.RazorPay;

namespace RealTimeTaskManagement.Presentation.Controllers.PaymentControllers
{
    [Authorize]
    public class RazorPayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RazorPay(string paymentAmount)
        {
            try
            {
                Payload orderRequest = new Payload
                {
                    ApiKey = "rzp_test_PSawHylUJK5KZi",
                    ApiSecret = "Zz8yhAXr1BMLKHfPLSHR7TpH",
                    Name = "Fake name",
                    Email = "fake@fakemail.com",
                    PhoneNumber = "912084422881",
                    Currency = "INR",
                    Amount = paymentAmount,
                    Receipt = Guid.NewGuid().ToString(),
                    PaymentCapture = "1"
                };
                // Create an order using RazorpayIntegration class
                var order = RazorPayClient.ProcessRazorPay(orderRequest);
                return Json(order);
            }
            catch (Exception)
            {
                // Log or handle other exceptions here
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public ActionResult RazorPayReturn(PaymentDetails paymentDetails)
        {
            if (RazorPayClient.CheckPaymentSuccess(paymentDetails.PaymentId, paymentDetails.OrderId, paymentDetails.Signature))
            {
                return RedirectToAction("Success", paymentDetails);
            }
            else
            {
                return RedirectToAction("Failure", paymentDetails);
            }
        }

        public ActionResult Success(PaymentDetails paymentDetails)
        {
            return View(paymentDetails);
        }
        public ActionResult Failure(PaymentDetails paymentDetails)
        {
            return View(paymentDetails);
        }
    }
}
