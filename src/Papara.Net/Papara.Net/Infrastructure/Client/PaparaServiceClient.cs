// <copyright file="PaparaClient.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("PaparaTests")]

namespace Papara.Infrastructure
{
    /// <summary>
    /// PaparaClient handles HTTP requests and responses made to API.
    /// </summary>
    internal class PaparaServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaparaServiceClient"/> class.
        /// </summary>
        public PaparaServiceClient()
        {
        }

        /// <summary>
        /// Creates an asynchronous HTTP request.
        /// </summary>
        /// <typeparam name="T">Generic request entity.</typeparam>
        /// <param name="method">HTTP Method.</param>
        /// <param name="path">Endpoint.</param>
        /// <param name="options">The parameters for request body.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <param name="cancellationToken">Token for cancel an async operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<T> RequestAsync<T>(
            HttpMethod method,
            string path,
            BaseOptions options = null,
            RequestOptions requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            using (var httpClient = new PaparaHttpClient())
            {
                // New request object
                var paparaRequest = new PaparaRequest(method, path, options, requestOptions);

                // New response object
                var paparaResponse = await httpClient.SendRequestAsync(paparaRequest, cancellationToken).ConfigureAwait(false);

                return this.HandleResponse<T>(paparaResponse);
            }
        }

        /// <summary>
        /// Handles incoming HTTP response.
        /// </summary>
        /// <typeparam name="T">Generic response type</typeparam>
        /// <param name="response">HTTP Response that contains status code, headers and content</param>
        /// <returns>A <see cref="PaparaResponse"/> .</returns>
        private T HandleResponse<T>(PaparaResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new PaparaException("Invalid credentials");
            }

            return PaparaJsonUtils.DeserializeObject<T>(response.Content);
        }
    }
}
