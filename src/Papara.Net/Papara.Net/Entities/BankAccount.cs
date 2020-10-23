// <copyright file="BankAccount.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// BankAccount class is used by banking service to match returning bank accounts from API.
    /// </summary>
    public class BankAccount : PaparaEntity
    {
        /// <summary>
        /// Gets or sets merchant's bank account ID.
        /// </summary>
        [JsonProperty("bankAccountId")]
        public int? BankAccountId { get; set; }

        /// <summary>
        /// Gets or sets merchant bank name.
        /// </summary>
        [JsonProperty("bankName")]
        public string BankName { get; set; }

        /// <summary>
        /// Gets or sets merchant branch code.
        /// </summary>
        [JsonProperty("branchCode")]
        public string BranchCode { get; set; }

        /// <summary>
        /// Gets or sets IBAN Number.
        /// </summary>
        [JsonProperty("iban")]
        public string Iban { get; set; }

        /// <summary>
        /// Gets or sets merchant account Code.
        /// </summary>
        [JsonProperty("accountCode")]
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}