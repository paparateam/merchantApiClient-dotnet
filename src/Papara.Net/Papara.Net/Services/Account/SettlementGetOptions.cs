// <copyright file="SettlementGetOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;
using System;

namespace Papara
{
    /// <summary>
    ///  SettlementGetOptions is used by account service for providing settlement request parameters.
    /// </summary>
    public class SettlementGetOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets start date filter for transactions.
        /// </summary>
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date filter for transactions.
        /// </summary>
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets entry types.
        /// BankTransfer = 1
        /// CorporateCardTransaction = 2,
        /// LoadingMoneyFromPhysicalPoint = 6,
        /// MerchantPayment = 8,
        /// PaymentDistribution = 9,
        /// EduPos = 11.
        /// </summary>
        [JsonProperty("entryType")]
        public EntryType? EntryType { get; set; }
    }
}