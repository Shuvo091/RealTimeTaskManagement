using Microsoft.AspNetCore.Mvc;
using RealTimeTaskManagement.Payment.Stripe;

namespace RealTimeTaskManagement.Presentation.Controllers
{
    public class StripeController : Controller
    {
        private readonly StripeClient _stripeService;

        public StripeController(StripeClient stripeService)
        {
            _stripeService = stripeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent()
        {
            var paymentIntent = await _stripeService.CreatePaymentIntentAsync(5000); // Amount in cents
            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }

        public IActionResult PaymentResult(string status, string message, long? amount, string currency, string paymentMethod, string clientSecret)
        {
            ViewBag.Status = status;
            ViewBag.Message = message;
            ViewBag.Amount = amount;
            ViewBag.Currency = currency;
            ViewBag.PaymentMethod = paymentMethod;
            ViewBag.ClientSecret = clientSecret;
            return View();
        }
    }
}
