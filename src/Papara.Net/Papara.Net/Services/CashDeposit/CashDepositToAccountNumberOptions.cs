// <copyright file="CashDepositToAccountNumberOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    ///  CashDepositToAccountNumberOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositToAccountNumberOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets account number. Papara account number of the user who will be loaded with cash.
        /// </summary>
        [JsonProperty("accountNumber")]
        public int AccountNumber { get; set; }

        /// <summary>
        ///  Gets or sets amount. The amount of the cash deposit. This amount will be transferred to the account of the user who received the payment. The amount to be deducted from the merchant account will be exactly this number.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets merchant reference. The unique value sent by the merchant to prevent false repetitions in cash loading transactions. If a previously submitted and successful merchantReference is resubmitted with a new request, the request will fail. MerchantReference sent with failed requests can be resubmitted.
        /// </summary>
        [JsonProperty("merchantReference")]
        public string MerchantReference { get; set; }
    }
}