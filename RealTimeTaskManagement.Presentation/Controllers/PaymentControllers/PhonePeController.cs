using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RealTimeTaskManagement.Payment.PhonePe;

namespace RealTimeTaskManagement.Presentation.Controllers.PaymentControllers
{
    [Authorize]
    public class PhonePeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PhonePe(string paymentAmount)
        {
            PhonePePayLoadRaw phonePePayLoad = new PhonePePayLoadRaw
            {
                MerchantId = "PGTESTPAYUAT91",
                MerchantTransactionId = Guid.NewGuid().ToString(),//"b0e9f024-c617-49ce-bb57-70b28fa9ae84",
                MerchantUserId = "MUID123",
                Amount = (int.Parse(paymentAmount) * 100 ).ToString(),
                RedirectUrl = "https://localhost:44369/PhonePe/PhonePeReturn",
                RedirectMode = "POST",
                CallBackUrl = "https://localhost:44369/PhonePe/PhonePeReturnCallBack",
                MobileNumber = "9999999999",
                PaymentInstrument = new PaymentInstrument{Type = "PAY_PAGE"}
            };
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            PhonePePayLoad phonePePayload = new PhonePePayLoad
            {
                PayLoad = JsonConvert.SerializeObject(phonePePayLoad, settings),
                RequestUri = "/pg/v1/pay",
                SaltKey = "05992a0b-5254-4f37-86fb-e23bb79ea7e7",
                SaltIndex = "1",
                BaseUrl = "https://api-preprod.phonepe.com/",
                RestAPIRootUri = "https://api-preprod.phonepe.com/apis/pg-sandbox",
            };
            PhonePeRestResponseObject phonePeRestResponseObject = PhonePeClient.ProcessPhonePe(phonePePayload);
            return Json(phonePeRestResponseObject);
        }


        public delegate Task<PhonePeStatusResult> CheckPaymentStatusDelegate(PhonePePaymentResponseModel responseObj, string restAPIRootUri, string requestUri, string saltKey);

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> PhonePeReturn(PhonePePaymentResponseModel responseObj)
        {
            if (false) // Check (from database or cache) if server to server has been recieved from PhonePe server.
            {
                // Decide if transaction is success or failure
            }
            else // This assumes server to server response hasn't been recieved. So, transaction status has to be checked.
            {
                var paymentStatus = await PhonePeClient.CheckApiStatusAsync("https://api-preprod.phonepe.com/apis/pg-sandbox", "/pg/v1/status", responseObj.MerchantId, responseObj.TransactionId, "05992a0b-5254-4f37-86fb-e23bb79ea7e7");
                if (paymentStatus.Code == PhonePeResponseCode.PAYMENT_PENDING)
                {
                    Task.Run(async () =>
                    {
                        var result = await PhonePeClient.CheckPhonePePaymentStatus(responseObj, "https://api-preprod.phonepe.com/apis/pg-sandbox", "/pg/v1/status", "05992a0b-5254-4f37-86fb-e23bb79ea7e7");

                        // You can handle the result of the background task if needed
                        Console.WriteLine($"Background Task Result: {result}");
                    });

                }
                return View(paymentStatus);
            }
        }

        [HttpPost]
        public void PhonePeReturnCallBack(PhonePeServerToServerResponseModel responseObj)
        {
            // Decode the response and save the result to database for future uses.
            // A response should come from PhonePe server after transaction is success or failure
        }

        public ActionResult PhonePeTimeOut()
        {
            ViewBag.Message = "Payment has timed Out.";

            return View();
        }
    }
}
