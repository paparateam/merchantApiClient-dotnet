// <copyright file="BankingWithdrawalOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    ///  BankingWithdrawalOptions is used by banking service for providing request parameters.
    /// </summary>
    public class BankingWithdrawalOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets target bank account id which money will be transferred to when withdrawal is completed.It will be obtained as a result of the request to list bank accounts.
        /// </summary>
        [JsonProperty("bankAccountId")]
        public int? BankAccountId { get; set; }

        /// <summary>
        /// Gets or sets withdrawal amount.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
