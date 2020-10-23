// <copyright file="PaparaArrayResult.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// Papara Array type. Handles array data types sending to and returning from API.
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity.</typeparam>
    public class PaparaArrayResult<TEntity> : PaparaServiceResult
        where TEntity : IPaparaEntity
    {
        /// <summary>
        /// Gets or sets array result data.
        /// </summary>
        [JsonProperty("data")]
        public TEntity[] Data { get; set; }
    }
}
