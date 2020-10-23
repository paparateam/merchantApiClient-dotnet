// <copyright file="AccountBalance.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// AccountBalance class is used by account service to match returning account balance value from API.
    /// </summary>
    public class AccountBalance : PaparaEntity
    {
        /// <summary>
        /// Gets or sets currency.
        /// </summary>
        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets total balance.
        /// </summary>
        [JsonProperty("totalBalance")]
        public decimal TotalBalance { get; set; }

        /// <summary>
        /// Gets or sets locked balance.
        /// </summary>
        [JsonProperty("lockedBalance")]
        public decimal LockedBalance { get; set; }

        /// <summary>
        /// Gets or sets available balance.
        /// </summary>
        [JsonProperty("availableBalance")]
        public decimal AvailableBalance { get; set; }
    }
}