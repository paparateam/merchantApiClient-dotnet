// <copyright file="CashDepositGetOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// CashDepositGetOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositGetOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets cash deposit ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}