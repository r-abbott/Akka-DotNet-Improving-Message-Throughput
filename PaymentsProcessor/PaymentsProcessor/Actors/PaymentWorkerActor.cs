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
            Receive<PaymentReceipt>(message => HandlePaymentReceipt(message));
        }

        private void SendPayment(SendPaymentMessage message)
        {
            _paymentGateway.Pay(message.AccountNumber, message.Amount).PipeTo(Self, Sender);
        }

        private void HandlePaymentReceipt(PaymentReceipt message)
        {
            Sender.Tell(new PaymentSentMessage(message.AccountNumber, message.PaymentConfirmationReceipt));
        }
    }
}
