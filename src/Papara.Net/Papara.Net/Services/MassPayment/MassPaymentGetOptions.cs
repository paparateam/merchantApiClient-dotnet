// <copyright file="MassPaymentGetOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// MassPaymentGetOptions is used by mass payment service for providing request parameters.
    /// </summary>
    public class MassPaymentGetOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets mass payment ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
