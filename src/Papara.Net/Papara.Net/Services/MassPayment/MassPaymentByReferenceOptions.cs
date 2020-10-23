// <copyright file="MassPaymentByReferenceOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// MassPaymentByReferenceOptions is used by mass payment service for providing request parameters.
    /// </summary>
    public class MassPaymentByReferenceOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets payment reference number.
        /// </summary>
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}
