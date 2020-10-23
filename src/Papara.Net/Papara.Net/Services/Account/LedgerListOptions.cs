// <copyright file="LedgerListOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;
using System;

namespace Papara
{
    /// <summary>
    ///  LedgerListOptions is used by account service for providing request parameters.
    /// </summary>
    public class LedgerListOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets start date for transactions.
        /// </summary>
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date for transactions.
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

        /// <summary>
        /// Gets or sets merchant account number.
        /// </summary>
        [JsonProperty("accountNumber")]
        public int? AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the requested page number. If the requested date has more than 1 page of results for the requested PageSize, use this to iterate through pages.
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets number of elements you want to receive per request page. Min=1, Max=50.
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
