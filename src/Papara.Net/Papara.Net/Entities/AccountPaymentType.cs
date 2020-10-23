// <copyright file="AccountPaymentType.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
  /// <summary>
  /// AccountPaymentType class is used by account service to match returning payment types from API.
  /// </summary>
  public class AccountPaymentType : PaparaEntity
  {
    /// <summary>
    /// Gets or sets Payment method
    /// 0- PaparaAccount - Papara Account Balance.
    /// 1- Card	- Registered Credit Card.
    /// 2- Mobile	- Mobile Payment.
    ///  </summary>
    [JsonProperty("paymentMethod")]
    public PaymentMethod PaymentMethod { get; set; }
  }
}