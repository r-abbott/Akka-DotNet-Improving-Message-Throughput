using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using PaymentsProcessor.Messages;
using System.Collections.Generic;
using System.IO;

namespace PaymentsProcessor.Actors
{
    internal class JobCoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _paymentWorker;
        private int _numberOfRemainingPayments;

        public JobCoordinatorActor()
        {
            _paymentWorker = Context.ActorOf(Context.DI().Props<PaymentWorkerActor>(), "PaymentWorker");


            Receive<ProcessFileMessage>(
                message =>
                {
                    StartNewJob(message.FileName);
                });
            Receive<PaymentSentMessage>(
                message =>
                {
                    _numberOfRemainingPayments--;
                    var jobIsComplete = _numberOfRemainingPayments == 0;

                    if (jobIsComplete)
                    {
                        Context.System.Terminate();
                    }
                });
        }

        private void StartNewJob(string fileName)
        {
            List<SendPaymentMessage> requests = ParseCsvFile(fileName);
            _numberOfRemainingPayments = requests.Count;
            requests.ForEach(r => _paymentWorker.Tell(r));
        }

        private List<SendPaymentMessage> ParseCsvFile(string fileName)
        {
            var messagesToSend = new List<SendPaymentMessage>();

            var fileLines = File.ReadAllLines(fileName);
            foreach(var line in fileLines)
            {
                var values = line.Split(',');
                var message = new SendPaymentMessage(values[0], values[1], values[3], decimal.Parse(values[2]));
                messagesToSend.Add(message);
            }
            return messagesToSend;
        }

    }
}