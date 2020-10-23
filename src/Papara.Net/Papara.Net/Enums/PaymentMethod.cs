namespace Papara
{
    public enum PaymentMethod
    {
        /// <summary>
        /// Papara hesap bakiyesi
        /// </summary>
        // [Display(Name = "Papara Hesap Bakiyesi")]
        PaparaAccount = 0,

        /// <summary>
        /// Tanımlı kredi kartı
        /// </summary>
        // [Display(Name = "Kredi/Banka Kartı")]
        Card = 1,

        Mobile = 2
    }

}
