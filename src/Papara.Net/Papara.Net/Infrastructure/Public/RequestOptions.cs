// <copyright file="RequestOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

namespace Papara
{
    /// <summary>
    /// While sending a request to API, request options help configurating the request.
    /// </summary>
    public class RequestOptions
    {
        /// <summary>
        /// Gets or sets API KEY for merchant.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether target environment is test or production.
        /// </summary>
        public PaparaEnv Env { get; set; }

        /// <summary>
        /// Gets or sets base URL.
        /// </summary>
        internal string BaseUrl { get; set; }
    }
}
