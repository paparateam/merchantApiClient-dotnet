// <copyright file="CashDepositTcknControlOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    ///  CashDepositTcknControlOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositTcknControlOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets user's phone number. The phone number of the user to be sent money, including the country code and "+".
        /// </summary>
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///  Gets or sets national identity number which is linked to user's Papara account.
        /// </summary>
        [JsonProperty("Tckn")]
        public long Tckn { get; set; }

        /// <summary>
        ///  Gets or sets amount. The amount of the cash deposit. This amount will be transferred to the account of the user who received the payment. The amount to be deducted from the merchant account will be exactly this number.
        /// </summary>
        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets merchant reference. The unique value sent by the merchant to prevent false repetitions in cash loading transactions. If a previously submitted and successful merchantReference is resubmitted with a new request, the request will fail. MerchantReference sent with failed requests can be resubmitted.
        /// </summary>
        [JsonProperty("MerchantReference")]
        public string MerchantReference { get; set; }
    }
}
