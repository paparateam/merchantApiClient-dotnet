// <copyright file="PaparaHttpClient.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Papara.Infrastructure
{
    /// <summary>
    /// Base HTTP Client for PaparaClient.
    /// </summary>
    internal class PaparaHttpClient : HttpClient
    {
        /// <summary>
        /// Sends an asynchronous request.
        /// </summary>
        /// <param name="request">HTTP Request.</param>
        /// <param name="cancellationToken">Token for cancel an async operation.</param>
        /// <returns>Papara Response.</returns>
        public async Task<PaparaResponse> SendRequestAsync(PaparaRequest request, CancellationToken cancellationToken)
        {
            // NOTE(burak): NET45 is not supportung Tls 1.2 by default. To prevent it we need to define security protocol.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Build a HTTP request message
            var httpRequest = this.BuildRequestMessage(request);
            cancellationToken = CancellationToken.None;

            // send async request and assign the response to variable
            var response = await this.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

            // read response
            var reader = new StreamReader(await response.Content.ReadAsStreamAsync().ConfigureAwait(false));

            return new PaparaResponse(
                response.StatusCode,
                response.Headers,
                await reader.ReadToEndAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Builds HTTP request message and adds headers.
        /// </summary>
        /// <param name="request"> HTTP Request.</param>
        /// <returns> HTTP Request Message.</returns>
        private HttpRequestMessage BuildRequestMessage(PaparaRequest request)
        {
            // create requestMessage object
            var requestMessage = new HttpRequestMessage(request.Method, request.Uri);

            if (request.RequestOptions.ApiKey == null)
            {
                throw new PaparaException("Api key not defined. <link here>");
            }

            // add headers
            requestMessage.Headers.Add("ApiKey", request.RequestOptions.ApiKey);
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.TryAddWithoutValidation("Content-Type", "application/json");

            // if request type is POST then ignore null value handling.
            if (request.Method == HttpMethod.Post && request.Options != null)
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                // serialize request
                var serialized = PaparaJsonUtils.SerializeObject(request.Options, Formatting.None, serializerSettings);

                // convert serialized request to string content
                requestMessage.Content = new StringContent(serialized, Encoding.UTF8, "application/json");
            }

            return requestMessage;
        }

    }
}
