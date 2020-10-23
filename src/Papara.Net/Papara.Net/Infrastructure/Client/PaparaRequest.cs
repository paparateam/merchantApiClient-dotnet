// <copyright file="PaparaRequest.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using System;
using System.Net.Http;
using System.Text;

namespace Papara.Infrastructure
{
    /// <summary>
    /// PaparaRequest class is used by PaparaClient and PaparaHttpClient to create http requests to Papara API.
    /// </summary>
    internal class PaparaRequest
    {
        /// <summary>
        /// Production environment URL.
        /// </summary>
        private readonly string liveUrl = "https://merchant-api.papara.com";

        /// <summary>
        /// Test environment URL.
        /// </summary>
        private readonly string testUrl = "https://merchant.test.api.papara.com";

        /// <summary>
        /// Gets or sets a value indicating whether target environment selection. Default value is true.
        /// </summary>
        public PaparaEnv Env { get; set; }

        /// <summary>
        /// Gets http method selection.
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// Gets URI.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// Gets base Options.
        /// </summary>
        public BaseOptions Options { get; }

        /// <summary>
        /// Gets request options. Contains API Key, Target Environment and Base URL.
        /// </summary>
        public RequestOptions RequestOptions { get; }

        /// <summary>
        /// Gets HTTP Content.
        /// </summary>
        public HttpContent Content => this.BuildContent(this.Method, this.Options);

        /// <summary>
        /// Initializes a new instance of the <see cref="PaparaRequest"/> class.
        /// </summary>
        /// <param name="method">HTTP Method.</param>
        /// <param name="path">Path</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        public PaparaRequest(HttpMethod method, string path, BaseOptions options, RequestOptions requestOptions)
        {
            this.Method = method;
            this.Options = options;
            this.RequestOptions = requestOptions ?? new RequestOptions();
            this.Env = this.RequestOptions.Env;

            this.Uri = this.GenerateUri(path);
        }

        /// <summary>
        /// Generates URI for HTTP Request.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <returns>Generated URI.</returns>
        private Uri GenerateUri(string path)
        {
            var stringBuilder = new StringBuilder();

            // add base url and target environment
            stringBuilder.Append(this.RequestOptions.BaseUrl ?? (this.Env == PaparaEnv.Live ? this.liveUrl : this.testUrl));

            // add path
            stringBuilder.Append(path);

            // If method is GET and request options not null, generate a querystring
            if (this.Method != HttpMethod.Post && this.Options != null)
            {
                var queryString = this.Options.ToQueryString();
                if (!string.IsNullOrEmpty(queryString))
                {
                    stringBuilder.Append("?");
                    stringBuilder.Append(queryString);
                }
            }

            return new Uri(stringBuilder.ToString());
        }

        /// <summary>
        /// Serializes base options.
        /// </summary>
        /// <param name="method">HTTP Method.</param>
        /// <param name="options">Base Options.</param>
        /// <returns> Serialized String Content.</returns>
        private HttpContent BuildContent(HttpMethod method, BaseOptions options)
        {
            if (method != HttpMethod.Post)
            {
                return null;
            }

            return new StringContent(PaparaJsonUtils.SerializeObject(options));
        }
    }
}
