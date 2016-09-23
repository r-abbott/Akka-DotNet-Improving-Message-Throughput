namespace PaymentsProcessor.Messages
{
    internal class PaymentSentMessage
    {
        public string AccountNumber { get; private set; }

        public PaymentSentMessage(string accountNumber)
        {
            AccountNumber = accountNumber;
        }
    }
}