using System;
using System.Threading;

namespace PaymentsProcessor.ExternalSystems
{
    class DemoPaymentGateway : IPaymentGateway
    {
        private static readonly Random Random = new Random();

        public void Pay(string accountNumber, decimal amount)
        {
            // Simulate communicating with external payment gateway
            Thread.Sleep(200);
        }
    }
}
