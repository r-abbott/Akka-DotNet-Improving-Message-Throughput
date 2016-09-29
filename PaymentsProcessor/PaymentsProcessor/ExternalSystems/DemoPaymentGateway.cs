using System;
using System.Threading;

namespace PaymentsProcessor.ExternalSystems
{
    class DemoPaymentGateway : IPaymentGateway
    {
        private static readonly Random Random = new Random();

        public void Pay(string accountNumber, decimal amount)
        {
            if (PeakTimeDemoSimulator.IsPeakHours && amount > 100)
            {
                Console.WriteLine($"Account Number {accountNumber} payment takes longer because is peak & > 100 ");

                Thread.Sleep(200);
            }
            else
            {
                Thread.Sleep(200);
            }
            
        }
    }
}
