// <copyright file="PaparaService.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using System.Net.Http;
using System.Threading.Tasks;

namespace Papara.Infrastructure
{
    /// <summary>
    /// Papara Service class contains service methods to be used in service classes.
    /// </summary>
    public abstract class PaparaService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaparaService"/> class.
        /// Papara service client.
        /// </summary>
        public PaparaService()
        {
            this.client = new PaparaServiceClient();
        }

        /// <summary>
        /// Gets base service path.
        /// </summary>
        protected abstract string BasePath { get; }

        // Client for creating requests.
        private readonly PaparaServiceClient client;

        /// <summary>
        /// Asynchronous get service result request from API.
        /// </summary>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Service result from given path.</returns>
        protected async Task<PaparaServiceResult> GetServiceResultAsync(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
        {
            return await this.client.RequestAsync<PaparaServiceResult>(HttpMethod.Get, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Get service result request from API.
        /// </summary>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Service result from given path.</returns>
        protected PaparaServiceResult GetServiceResult(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
        {
            return this.GetServiceResultAsync(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronous get service result request from API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Single object from given path.</returns>
        protected async Task<PaparaSingleResult<TEntity>> GetSingleResultAsync<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
           where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaSingleResult<TEntity>>(HttpMethod.Get, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Get service result request from API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Single object from given path.</returns>
        protected PaparaSingleResult<TEntity> GetSingleResult<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.GetSingleResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get array result request from API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Array from given path.</returns>
        protected async Task<PaparaArrayResult<TEntity>> GetArrayResultAsync<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaArrayResult<TEntity>>(HttpMethod.Get, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Get array result request from API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Array from given path.</returns>
        protected PaparaArrayResult<TEntity> GetArrayResult<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.GetArrayResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get service result request from API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Single object from given path.</returns>
        protected async Task<PaparaListResult<TEntity>> GetListResultAsync<TEntity>(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaListResult<TEntity>>(HttpMethod.Get, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Get service result request from API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Single object from given path.</returns>
        protected PaparaListResult<TEntity> GetListResult<TEntity>(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.GetListResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Post service result request to API.
        /// </summary>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Service result from given path.</returns>
        protected async Task<PaparaServiceResult> PostServiceResultAsync(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
        {
            return await this.client.RequestAsync<PaparaServiceResult>(HttpMethod.Post, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Post a service request to API.
        /// </summary>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Service result from given path.</returns>
        protected PaparaServiceResult PostServiceResult(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
        {
            return this.PostServiceResultAsync(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Post single result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Single object from given path.</returns>
        protected async Task<PaparaSingleResult<TEntity>> PostSingleResultAsync<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaSingleResult<TEntity>>(HttpMethod.Post, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Post single result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Single object from given path.</returns>
        protected PaparaSingleResult<TEntity> PostSingleResult<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.PostSingleResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Post array result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Array from given path.</returns>
        protected async Task<PaparaArrayResult<TEntity>> PostArrayResultAsync<TEntity>(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaArrayResult<TEntity>>(HttpMethod.Post, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Post array result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Array from given path.</returns>
        protected PaparaArrayResult<TEntity> PostArrayResult<TEntity>(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.PostArrayResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Post list result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>List from given path.</returns>
        protected async Task<PaparaListResult<TEntity>> PostListResultAsync<TEntity>(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaListResult<TEntity>>(HttpMethod.Post, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Post list result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>List from given path.</returns>
        protected PaparaListResult<TEntity> PostListResult<TEntity>(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.PostListResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Put service result request to API.
        /// </summary>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Service result from given path.</returns>
        protected async Task<PaparaServiceResult> PutServiceResultAsync(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
        {
            return await this.client.RequestAsync<PaparaServiceResult>(HttpMethod.Put, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Put service request to API.
        /// </summary>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Service result from given path.</returns>
        protected PaparaServiceResult PutServiceResult(string childPath, BaseOptions options = null, RequestOptions requestOptions = null)
        {
            return this.PutServiceResultAsync(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Put service result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Single object from given path.</returns>
        protected async Task<PaparaSingleResult<TEntity>> PutSingleResultAsync<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaSingleResult<TEntity>>(HttpMethod.Put, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Put service result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Single object from given path.</returns>
        protected PaparaSingleResult<TEntity> PutSingleResult<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.PutSingleResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Put array result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Array from given path.</returns>
        protected async Task<PaparaArrayResult<TEntity>> PutArrayResultAsync<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaArrayResult<TEntity>>(HttpMethod.Put, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Put array result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>Array from given path.</returns>
        protected PaparaArrayResult<TEntity> PutArrayResult<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.PutArrayResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Put service result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>List from given path.</returns>
        protected async Task<PaparaListResult<TEntity>> PutListResultAsync<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return await this.client.RequestAsync<PaparaListResult<TEntity>>(HttpMethod.Put, this.BasePath + childPath, options, requestOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Put service result request to API.
        /// </summary>
        /// <typeparam name="TEntity">Generic Entity.</typeparam>
        /// <param name="childPath">Child Path.</param>
        /// <param name="options">Base Options.</param>
        /// <param name="requestOptions">Request Options.</param>
        /// <returns>List from given path.</returns>
        protected PaparaListResult<TEntity> PutListResult<TEntity>(string childPath, BaseOptions options, RequestOptions requestOptions = null)
            where TEntity : IPaparaEntity
        {
            return this.PutListResultAsync<TEntity>(childPath, options, requestOptions).GetAwaiter().GetResult();
        }
    }
}
