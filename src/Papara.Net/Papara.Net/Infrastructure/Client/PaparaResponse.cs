// <copyright file="PaparaResponse.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using System.Net;
using System.Net.Http.Headers;

namespace Papara.Infrastructure
{
    /// <summary>
    /// PaparaResponse class is used by PaparaClient and PaparaHttpClient to handle http requests from Papara API.
    /// </summary>
    internal class PaparaResponse
    {
        /// <summary>Gets the HTTP status code of the response.</summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>Gets the HTTP headers of the response.</summary>
        public HttpResponseHeaders Headers { get; }

        /// <summary>Gets the body of the response.</summary>
        public string Content { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaparaResponse"/> class.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="headers">The HTTP headers of the response.</param>
        /// <param name="content">The body of the response.</param>
        public PaparaResponse(HttpStatusCode httpStatusCode, HttpResponseHeaders headers, string content)
        {
            this.StatusCode = httpStatusCode;
            this.Headers = headers;
            this.Content = content;
        }
    }
}
