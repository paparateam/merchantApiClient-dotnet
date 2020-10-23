// <copyright file="BaseOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Papara.Infrastructure
{
    /// <summary>
    /// BaseOptions class is used for converting deserialized objects to dictionary and query string.
    /// </summary>
    public class BaseOptions
    {
        /// <summary>
        /// Deserialize and convert to dictionary.
        /// </summary>
        /// <returns>Deserialized dictionary object.</returns>
        public Dictionary<string, string> ToDictionary()
        {
            var serialized = PaparaJsonUtils.SerializeObject(this, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);

            return dictionary;
        }

        /// <summary>
        /// Convert to query string.
        /// </summary>
        /// <returns>Query string.</returns>
        public string ToQueryString()
        {
            var dictionary = this.ToDictionary();
            return string.Join("&", dictionary.Select(d => string.Format("{0}={1}", WebUtility.UrlEncode(d.Key), WebUtility.UrlEncode(d.Value))));
        }
    }
}
