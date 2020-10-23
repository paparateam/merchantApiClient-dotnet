// <copyright file="ServiceResultError.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// Papara Service Error Result type. Handles error responses returning from API.
    /// </summary>
    public class PaparaError
    {
        /// <summary>
        /// Gets or sets error messages.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets error codes.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}