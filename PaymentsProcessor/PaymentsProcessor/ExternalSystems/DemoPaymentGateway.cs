using System;
using System.Threading.Tasks;

namespace PaymentsProcessor.ExternalSystems
{
    class DemoPaymentGateway : IPaymentGateway
    {
        private static readonly Random Random = new Random();

        public async Task<PaymentReceipt> Pay(string accountNumber, decimal amount)
        {
            return await Task.Delay(Random.Next(1000,5000))
                .ContinueWith(
                task =>
                {
                    return new PaymentReceipt
                    {
                        AccountNumber = accountNumber,
                        PaymentConfirmationReceipt = Guid.NewGuid().ToString()
                    };
                });
        }
    }
}
