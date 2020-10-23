// <copyright file="ServiceResultSuccess.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// Papara Service Success Result type. Handles success responses returning from API.
    /// </summary>
    public class PaparaSuccess
    {
        /// <summary>
        /// Gets or sets success messages.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets success codes.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}