namespace Papara
{
    public enum PaymentStatus
    {
        /// <summary>
        /// Ödeme işlemi bekliyor.
        /// </summary>
        // [Display(Name = "Bekliyor")]
        Pending = 0,

        /// <summary>
        /// Kullanıcı ödeme işlemini tamamladı.
        /// </summary>
        // [Display(Name = "Tamamlandı")]
        Completed = 1,

        /// <summary>
        /// Siparişin ödemesi iade edildi
        /// </summary>
        // [Display(Name = "İade edildi")]
        Refunded = 2,
    }
}
