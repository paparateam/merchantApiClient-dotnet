// <copyright file="PaparaListResult.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// Papara List type. Handles list data types sending to and returning from API.
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity.</typeparam>
    public class PaparaListResult<TEntity> : PaparaServiceResult
        where TEntity : IPaparaEntity
    {
        /// <summary>
        /// Gets or sets array result data.
        /// </summary>
        [JsonProperty("data")]
        public PaparaPagingResult<TEntity> Data { get; set; }
    }
}