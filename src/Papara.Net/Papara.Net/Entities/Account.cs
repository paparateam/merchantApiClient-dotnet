// <copyright file="Account.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Papara
{
    /// <summary>
    /// Account class is used by account service to match returning account value from API.
    /// </summary>
    public class Account : PaparaEntity
    {
        /// <summary>
        /// Gets or sets merchant's company title.
        /// </summary>
        [JsonProperty("legalname")]
        public string LegalName { get; set; }

        /// <summary>
        /// Gets or sets brand name.
        /// </summary>
        [JsonProperty("brandName")]
        public string BrandName { get; set; }

        /// <summary>
        /// Gets or sets allowed payment types.
        /// </summary>
        [JsonProperty("allowedPaymentTypes")]
        public List<AccountPaymentType> AllowedPaymentTypes { get; set; }

        /// <summary>
        /// Gets or sets balances.
        /// </summary>
        [JsonProperty("balances")]
        public List<AccountBalance> Balances { get; set; }
    }
}