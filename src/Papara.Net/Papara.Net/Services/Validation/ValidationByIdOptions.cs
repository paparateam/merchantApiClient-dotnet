// <copyright file="ValidationByIdOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// ValidationByIdOptions is used by validation service for providing request parameters.
    /// </summary>
    public class ValidationByIdOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets papara User ID.
        /// </summary>
        [JsonProperty("UserId")]
        public string UserId { get; set; }
    }
}