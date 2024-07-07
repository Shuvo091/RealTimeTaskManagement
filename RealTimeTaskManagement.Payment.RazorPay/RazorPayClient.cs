using Razorpay.Api;

namespace RealTimeTaskManagement.Payment.RazorPay
{
    public static class RazorPayClient
    {
        public static Response ProcessRazorPay(Payload payRequest)
        {
            var amount = (int.Parse(payRequest.Amount) * 100).ToString();
            var options = new Dictionary<string, object>
            {
                { "amount", amount },
                { "currency", payRequest.Currency },
                { "receipt", payRequest.Receipt },
                { "payment_capture", payRequest.PaymentCapture }
            };

            RazorpayClient client = new RazorpayClient(payRequest.ApiKey, payRequest.ApiSecret);
            var order = client.Order.Create(options);
            return new Response
            {
                OrderId = order["id"],
                RazorpayKey = payRequest.ApiKey,
                Amount = amount,
                Currency = payRequest.Currency,
                Name = payRequest.Name,
                Email = payRequest.Email,
                PhoneNumber = payRequest.PhoneNumber,
                Address = payRequest.Address,
                Description = "Order by Merchant"
            };
        }

        public static bool CheckPaymentSuccess(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            try
            {
                Dictionary<string, string> attributes = new Dictionary<string, string>
            {
                { "razorpay_payment_id", razorpay_payment_id },
                { "razorpay_order_id", razorpay_order_id },
                { "razorpay_signature", razorpay_signature }
            };

                Utils.verifyPaymentSignature(attributes);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
