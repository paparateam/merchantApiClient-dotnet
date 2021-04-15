// <copyright file="MassPaymentToPaparaNumberOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// MassPaymentToPaparaNumberOptions is used by mass payment service for providing request parameters.
    /// </summary>
    public class MassPaymentToPaparaNumberOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets Papara account number. The 10-digit Papara number of the user who will receive the payment. It can be in the format 1234567890 or PL1234567890. Before the Papara version transition, the Papara number was called the wallet number.Old wallet numbers have been changed to Papara number. Payment can be distributed to old wallet numbers.
        /// </summary>
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets parse account number. Parses the account number to long type. In old papara integrations, account / wallet number was made by starting with PL. The service was written in such a way that it accepts numbers starting with PL, in order not to cause problems to the member merchants that receive the papara number from their users.
        /// </summary>
        [JsonProperty("parseAccountNumber")]
        public int? ParseAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets amount. The amount of the payment transaction. This amount will be transferred to the account of the user who received the payment. This figure plus transaction fee will be charged to the merchant account.
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets mass payment ID. Unique value sent by merchant to prevent erroneous repetition in payment transactions. If a massPaymentId that was sent previously and succeeded is sent again with a new request, the request will fail.
        /// </summary>
        [JsonProperty("massPaymentId")]
        public string MassPaymentId { get; set; }

        /// <summary>
        /// Gets or sets national identity number.It provides the control of the identity information sent by the user who will receive the payment, in the Papara system. In case of a conflict of credentials, the transaction will not take place.
        /// </summary>
        [JsonProperty("turkishNationalId")]
        public long? TurkishNationalId { get; set; }

        /// <summary>
        /// Gets or sets payment currency.
        /// </summary>
        [JsonProperty("currency")]
        public Currency? Currency { get; set; }

        /// <summary>
        /// Gets or sets description. Description of the transaction provided by the merchant. It is not a required field. If sent, the customer sees in the transaction descriptions.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}