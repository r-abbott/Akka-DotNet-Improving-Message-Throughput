namespace PaymentsProcessor.Messages
{
    internal class PaymentSentMessage
    {
        public string AccountNumber { get; private set; }
        public string PaymentConfirmationReceipt { get; private set; }

        public PaymentSentMessage(string accountNumber, string paymentConfirmationReceipt)
        {
            AccountNumber = accountNumber;
            PaymentConfirmationReceipt = paymentConfirmationReceipt;
        }
    }
}