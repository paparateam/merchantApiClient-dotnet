// <copyright file="ValidationService.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Papara.Infrastructure;
using System.Threading.Tasks;

namespace Papara
{
    /// <summary>
    /// Validation service will be used for validating an end user. Validation can be performed by account number, e-mail address, phone number, national identity number.
    /// </summary>
    public class ValidationService : PaparaService
    {
        private readonly RequestOptions requestOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationService"/> class.
        /// </summary>
        /// <param name="options">API Key, Base URL and test | prod environment setter.</param>
        public ValidationService(RequestOptions options = null)
        {
            this.requestOptions = options;
        }

        /// <inheritdoc/>
        protected override string BasePath => "/validation";

        /// <summary>
        /// Returns end user information for validation by given user ID.
        /// </summary>
        /// <param name="options">The unique user ID.</param>
        /// /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Validation> ValidateById(ValidationByIdOptions options)
        {
            return this.GetSingleResult<Validation>("/id", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given user ID.
        /// </summary>
        /// <param name="options">The unique user ID.</param>
        /// /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Validation>> ValidateByIdAsync(ValidationByIdOptions options)
        {
            return await this.GetSingleResultAsync<Validation>("/id", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given user account number.
        /// </summary>
        /// <param name="options">User account number.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Validation> ValidateByAccountNumber(ValidationByAccountNumberOptions options)
        {
            return this.GetSingleResult<Validation>("/accountNumber", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given user account number.
        /// </summary>
        /// <param name="options">User account number.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Validation>> ValidateByAccountNumberAsync(ValidationByAccountNumberOptions options)
        {
            return await this.GetSingleResultAsync<Validation>("/accountNumber", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given phone number.
        /// </summary>
        /// <param name="options">User phone number.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Validation> ValidateByPhoneNumber(ValidationByPhoneNumberOptions options)
        {
            return this.GetSingleResult<Validation>("/phoneNumber", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given phone number.
        /// </summary>
        /// <param name="options">User phone number.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Validation>> ValidateByPhoneNumberAsync(ValidationByPhoneNumberOptions options)
        {
            return await this.GetSingleResultAsync<Validation>("/phoneNumber", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given user e-mail address.
        /// </summary>
        /// <param name="options">User e-mail address.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Validation> ValidateByEmail(ValidationByEmailOptions options)
        {
            return this.GetSingleResult<Validation>("/email", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given user e-mail address.
        /// </summary>
        /// <param name="options">User e-mail address.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Validation>> ValidateByEmailAsync(ValidationByEmailOptions options)
        {
            return await this.GetSingleResultAsync<Validation>("/email", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given user national identity number.
        /// </summary>
        /// <param name="options">User national identity number.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Validation> ValidateByTckn(ValidationByTcknOptions options)
        {
            return this.GetSingleResult<Validation>("/tckn", options, this.requestOptions);
        }

        /// <summary>
        /// Returns end user information for validation by given user national identity number.
        /// </summary>
        /// <param name="options">User national identity number.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Validation>> ValidateByTcknAsync(ValidationByTcknOptions options)
        {
            return await this.GetSingleResultAsync<Validation>("/tckn", options, this.requestOptions);
        }
    }
}
