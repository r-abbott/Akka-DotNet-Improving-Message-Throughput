using System;
using System.Threading.Tasks;
using Akka.Actor;
using PaymentsProcessor.ExternalSystems;
using PaymentsProcessor.Messages;

namespace PaymentsProcessor.Actors
{
    internal class PaymentWorkerActor : ReceiveActor
    {
        private readonly IPaymentGateway _paymentGateway;

        public PaymentWorkerActor(IPaymentGateway payementGateway)
        {
            _paymentGateway = payementGateway;

            Receive<SendPaymentMessage>(message => SendPayment(message));
        }

        private void SendPayment(SendPaymentMessage message)
        {
            Console.WriteLine($"Sending payment for {message.FirstName} {message.LastName}");

            _paymentGateway.Pay(message.AccountNumber, message.Amount);

            Sender.Tell(new PaymentSentMessage(message.AccountNumber));
        }
    }
}