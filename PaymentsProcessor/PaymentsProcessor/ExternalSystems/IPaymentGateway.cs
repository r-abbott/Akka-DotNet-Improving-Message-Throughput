using System.Threading.Tasks;

namespace PaymentsProcessor.ExternalSystems
{
    internal interface IPaymentGateway
    {
        Task<PaymentReceipt> Pay(string accountNumber, decimal amount);
    }
}