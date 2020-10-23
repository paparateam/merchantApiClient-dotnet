// <copyright file="CashDepositSettlement.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// Settlement class is used by account service to match returning settlement values API.
    /// </summary>
    public class CashDepositSettlement : PaparaEntity
    {
        /// <summary>
        /// Gets or sets transaction count.
        /// </summary>
        [JsonProperty("count")]
        public decimal? Count { get; set; }

        /// <summary>
        /// Gets or sets transaction volume.
        /// </summary>
        [JsonProperty("volume")]
        public decimal? Volume { get; set; }
    }
}
