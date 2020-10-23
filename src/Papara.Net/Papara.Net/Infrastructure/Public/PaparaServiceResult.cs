// <copyright file="PaparaServiceResult.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// Papara Service Result type. Handles response data types returning from API.
    /// </summary>
    public class PaparaServiceResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether operation resulted successfully or not.
        /// </summary>
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }

        /// <summary>
        ///  Gets or sets a value indicating whether operation failed or not.
        /// </summary>
        [JsonProperty("error")]
        public PaparaError Error { get; set; }

        /// <summary>
        /// Gets or sets success result.
        /// </summary>
        [JsonProperty("result")]
        public PaparaSuccess Result { get; set; }
    }
}