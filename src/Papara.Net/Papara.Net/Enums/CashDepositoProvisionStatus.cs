namespace Papara
{
    public enum CashDepositProvisionStatus
    {
    /// <summary>
    /// Cash deposit is pending provision.
    /// [Display(Name = "Bekliyor")]
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Cash deposit is completed
    /// [Display(Name = "Tamamlandı")]
    /// </summary>
    Complete = 1,

    /// <summary>
    /// Cash deposit is cancelled
    /// [Display(Name = "İptal")]
    /// </summary>
    Cancel = 2,

    /// <summary>
    /// Cash deposit is ready for completion
    /// [Display(Name = "Tamamlamaya Hazır")]
    /// </summary>
    ReadyToComplete = 3
    }
}
