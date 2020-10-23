// <copyright file="PaymentGetOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// PaymentGetOptions will be used as parameter while acquiring payment information.
    /// </summary>
    public class PaymentGetOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets unique payment ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}