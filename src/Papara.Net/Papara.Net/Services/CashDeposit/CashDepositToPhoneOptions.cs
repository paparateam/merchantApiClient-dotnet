// <copyright file="CashDepositToPhoneOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// CashDepositToPhoneOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositToPhoneOptions : BaseOptions
    {
        /// <summary>
        ///  Gets or sets phone number. The mobile phone number registered in the Papara account of the user to be loaded with cash.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets amount. The amount of the cash deposit. This amount will be transferred to the account of the user who received the payment. The amount to be deducted from the merchant account will be exactly this number.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets merchant reference. The unique value sent by the merchant to prevent false repetitions in cash loading transactions.
        /// If a previously submitted and successful merchantReference is resubmitted with a new request, the request will fail.
        /// MerchantReference sent with failed requests can be resubmitted.
        /// </summary>
        [JsonProperty("merchantReference")]
        public string MerchantReference { get; set; }
    }
}