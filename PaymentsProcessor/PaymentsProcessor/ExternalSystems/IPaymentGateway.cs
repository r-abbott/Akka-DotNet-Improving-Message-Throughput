namespace PaymentsProcessor.ExternalSystems
{
    internal interface IPaymentGateway
    {
        void Pay(string accountNumber, decimal amount);
    }
}