// <copyright file="PaymentByReferenceOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// PaymentGetOptions will be used as parameter while acquiring payment information.
    /// </summary>
    public class PaymentByReferenceOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets unique payment ReferenceId.
        /// </summary>
        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }
    }
}