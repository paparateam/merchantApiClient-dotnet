// <copyright file="ValidationByTcknOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// ValidationByTcknOptions is used by validation service for providing request parameters.
    /// </summary>
    public class ValidationByTcknOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets national identity number.
        /// </summary>
        [JsonProperty("tckn")]

        public long Tckn { get; set; }
    }
}