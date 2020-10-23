namespace Papara
{
    /// <summary>
    /// EntryType enum is used for defining entry types on transactions.
    /// </summary>
    public enum EntryType
    {
        /// <summary>
        /// Banka Transferi: Banka üzerinden Papara'ya para yükleme ya da çekme işlemi.
        /// Bank Transfer: Cash deposit or withdrawal
        /// </summary>
        BankTransfer = 1,

        /// <summary>
        /// Kurumsal Papara Card İşlemi: Üye işyeri hesabına tanımlı kurumsal kartla yapılmış işlem.
        /// Papara Corporate Card Transaction: Transaction which was operated by corporation card assigned to merchant
        /// </summary>
        CorporateCardTransaction = 2,

        /// <summary>
        /// Fiziksel Noktadan Para Yükleme: Anlaşmalı nokta ile Papara'ya para yükleme işlemi.
        /// Loading Money From Physical Point: Cash deposit operation from contracted location
        /// </summary>
        LoadingMoneyFromPhysicalPoint = 6,

        /// <summary>
        /// Üye İşyeri Ödeme: Papara ile checkout (ödeme al/kabul et) işlemi.
        /// Merchant Payment: Checkout via Papara
        /// </summary>
        MerchantPayment = 8,

        /// <summary>
        /// Ödeme Dağıtım: Papara ile masspayment (ödeme dağıt/gönder) işlemi.
        /// Payment Distribution: Masspayment via papara
        /// </summary>
        PaymentDistribution = 9,

        /// <summary>
        /// Kapalı devre ödeme kabul. Papara EDU POS işlemleri.
        /// Offline payment. EDU POS via Papara
        /// </summary>
        EduPos = 11,
    }
}
