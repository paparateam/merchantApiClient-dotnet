// <copyright file="ValidationByAccountNumberOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// ValidationByAccountNumberOptions is used by validation service for providing request parameters.
    /// </summary>
    public class ValidationByAccountNumberOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets Papara account number.
        /// </summary>
        [JsonProperty("accountNumber")]
        public long AccountNumber { get; set; }
    }
}