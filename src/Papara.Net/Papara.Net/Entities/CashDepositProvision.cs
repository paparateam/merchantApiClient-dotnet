// <copyright file="CashDepositProvision.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System;

namespace Papara
{
    /// <summary>
    /// CashDepositProvision class is used by cash deposit service to match returning cash deposit provision values from API.
    /// </summary>
    public class CashDepositProvision : PaparaEntity
    {
        /// <summary>
        /// Gets or sets cash deposit ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets created date of cash deposit.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets amount of cash deposit.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets currency of cash deposit.
        /// </summary>
        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets merchant reference code.
        /// </summary>
        [JsonProperty("merchantReference")]
        public string MerchantReference { get; set; }

        /// <summary>
        /// Gets or sets end user's full name.
        /// </summary>
        [JsonProperty("userFullName")]
        public string UserFullName { get; set; }
    }
}
