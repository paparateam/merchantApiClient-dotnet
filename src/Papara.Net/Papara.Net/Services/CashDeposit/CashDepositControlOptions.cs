// <copyright file="CashDepositControlOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    ///  CashDepositControlOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositControlOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets reference number of cash deposit request.
        /// </summary>
        [JsonProperty("referenceCode")]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// Gets or sets cash deposit amount.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}