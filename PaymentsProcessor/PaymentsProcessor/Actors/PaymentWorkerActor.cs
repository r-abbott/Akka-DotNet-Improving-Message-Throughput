using System;
using System.Threading.Tasks;
using Akka.Actor;
using PaymentsProcessor.ExternalSystems;
using PaymentsProcessor.Messages;

namespace PaymentsProcessor.Actors
{
    internal class PaymentWorkerActor : ReceiveActor, IWithUnboundedStash
    {
        private readonly IPaymentGateway _paymentGateway;
        private ICancelable _unstashSchedule;

        public IStash Stash { get; set; }


        public PaymentWorkerActor(IPaymentGateway payementGateway)
        {
            _paymentGateway = payementGateway;

            Receive<SendPaymentMessage>(message => SendPayment(message));
            Receive<ProcessStashedPaymentsMessage>(message => HandleUnstash());
        }

        private void SendPayment(SendPaymentMessage message)
        {
            if(message.Amount > 100 && PeakTimeDemoSimulator.IsPeakHours)
            {
                Console.WriteLine($"Stashing payment message for {message.FirstName} {message.LastName}");

                Stash.Stash();
            }
            else
            {
                Console.WriteLine($"Sending payment for {message.FirstName} {message.LastName}");

                _paymentGateway.Pay(message.AccountNumber, message.Amount);

                Sender.Tell(new PaymentSentMessage(message.AccountNumber));
            }
        }

        private void HandleUnstash()
        {
            if (!PeakTimeDemoSimulator.IsPeakHours)
            {
                Console.WriteLine("Not in peak hours so unstashing");
                Stash.UnstashAll();
            }
        }

        protected override void PreStart()
        {
            _unstashSchedule = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), Self, new ProcessStashedPaymentsMessage(), Self);
        }

        protected override void PostStop()
        {
            _unstashSchedule.Cancel();
        }
    }
}