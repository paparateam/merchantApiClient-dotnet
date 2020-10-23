// <copyright file="ValidationByEmailOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// ValidationByEmailOptions is used by validation service for providing request parameters.
    /// </summary>
    public class ValidationByEmailOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets e-mail address registered to Papara.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}