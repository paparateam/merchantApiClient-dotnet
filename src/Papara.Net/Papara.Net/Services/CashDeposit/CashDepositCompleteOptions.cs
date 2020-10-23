// <copyright file="CashDepositCompleteOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;
using System;

namespace Papara
{
    /// <summary>
    ///  CashDepositCompleteOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositCompleteOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets ID of cash deposit request.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets date of cash deposit transaction.
        /// </summary>
        [JsonProperty("transactionDate")]
        public DateTime TransactionDate { get; set; }
    }
}
