namespace Papara
{
    public interface IPaparaClient
    {
        AccountService AccountService { get; }
        BankingService BankingService { get; }
        CashDepositService CashDepositService { get; }
        MassPaymentService MassPaymentService { get; }
        PaymentService PaymentService { get; }
        ValidationService ValidationService { get; }
        void SetOptions(string apiKey, PaparaEnv env = PaparaEnv.Live);
    }
}
