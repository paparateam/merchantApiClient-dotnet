﻿// <copyright file="PaparaSingleResult.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// Papara Single Result type. Handles object data types sending to and returning from API.
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity.</typeparam>
    public class PaparaSingleResult<TEntity> : PaparaServiceResult
        where TEntity : IPaparaEntity
    {
        /// <summary>
        /// Gets or sets single result data.
        /// </summary>
        [JsonProperty("data")]
        public TEntity Data { get; set; }
    }
}
