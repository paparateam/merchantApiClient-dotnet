// <copyright file="CashDepositSettlementOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;
using System;

namespace Papara
{
    /// <summary>
    ///  CashDepositSettlementOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositSettlementOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets start date for settlement.
        /// </summary>
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date for settlement.
        /// </summary>
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets entry type for settlement.
        ///  Entry type. 1: Bank Transfer(Deposits/Withdrawals) 6: Cash Deposit 8: Received Payment(Checkout) 9: Sent Payment (MassPayment) = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17'].
        /// </summary>
        [JsonProperty("EntryType")]
        public EntryType? EntryType { get; set; }
    }
}