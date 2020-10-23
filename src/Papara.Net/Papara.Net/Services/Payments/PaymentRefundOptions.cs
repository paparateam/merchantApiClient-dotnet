// <copyright file="PaymentRefundOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// PaymentRefundOptions is used by payment service for providing request parameters.
    /// </summary>
    public class PaymentRefundOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets payment ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}