// <copyright file="MassPayment.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System;

namespace Papara
{
    /// <summary>
    /// MassPayment class is used by mass payment service to match returning mass payment values from API.
    /// </summary>
    public class MassPayment : PaparaEntity
    {
        /// <summary>
        /// Gets or sets mass payment ID.
        /// </summary>
        [JsonProperty("massPaymentId")]
        public string MassPaymentId { get; set; }

        /// <summary>
        /// Gets or sets ID which is created after payment is done.
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets created date.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets amount of payment.
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets currency.Values are “0”, “1”, “2”, “3”.
        /// </summary>
        [JsonProperty("currency")]
        public Currency? Currency { get; set; }

        /// <summary>
        /// Gets or sets fee.
        /// </summary>
        [JsonProperty("fee")]
        public decimal? Fee { get; set; }

        /// <summary>
        /// Gets or sets resulting balance.
        /// </summary>
        [JsonProperty("resultingBalance")]
        public decimal? ResultingBalance { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
