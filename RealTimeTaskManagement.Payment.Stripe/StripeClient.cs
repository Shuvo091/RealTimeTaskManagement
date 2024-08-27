using Microsoft.Extensions.Options;
using Stripe;

namespace RealTimeTaskManagement.Payment.Stripe;

public class StripeClient
{
    private readonly StripeSettings _stripeSettings;

    public StripeClient(IOptions<StripeSettings> stripeSettings)
    {
        _stripeSettings = stripeSettings.Value;
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
    }

    public async Task<PaymentIntent> CreatePaymentIntentAsync(long amount)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = "usd",
            PaymentMethodTypes = new List<string> { "card" },
        };
        var service = new PaymentIntentService();
        return await service.CreateAsync(options);
    }
}
