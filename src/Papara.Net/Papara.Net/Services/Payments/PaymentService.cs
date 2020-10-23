// <copyright file="PaymentService.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Papara.Infrastructure;
using System.Threading.Tasks;

namespace Papara
{
    /// <summary>
    /// Payment service will be used for getting, creating or listing payments and refunding.
    /// </summary>
    public class PaymentService : PaparaService
    {
        private readonly RequestOptions requestOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService"/> class.
        /// </summary>
        /// <param name="options">API Key, Base URL and test | prod environment setter.</param>
        public PaymentService(RequestOptions options = null)
        {
            this.requestOptions = options;
        }

        /// <inheritdoc/>
        protected override string BasePath => "/payments";

        /// <summary>
        /// Returns payment and balance information for authorized merchant.
        /// </summary>
        /// <param name="options">Unique payment ID.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Payment> GetPayment(PaymentGetOptions options)
        {
            return this.GetSingleResult<Payment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Returns payment and balance information for authorized merchant.
        /// </summary>
        /// <param name="options">Unique payment ID.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Payment>> GetPaymentAsync(PaymentGetOptions options)
        {
            return await this.GetSingleResultAsync<Payment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a payment for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Payment Create Options.</param>
        public PaparaSingleResult<Payment> CreatePayment(PaymentCreateOptions options)
        {
            return this.PostSingleResult<Payment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a payment for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Payment Create Options.</param>
        public async Task<PaparaSingleResult<Payment>> CreatePaymentAsync(PaymentCreateOptions options)
        {
            return await this.PostSingleResultAsync<Payment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a refund for a completed payment for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Payment Refund Options.</param>
        public PaparaServiceResult Refund(PaymentRefundOptions options)
        {
            return this.PutServiceResult("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a refund for a completed payment for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Payment Refund Options.</param>
        public async Task<PaparaServiceResult> RefundAsync(PaymentRefundOptions options)
        {
            return await this.PutServiceResultAsync("/", options, this.requestOptions);
        }

        /// <summary>
        /// Returns a list of completed payments sorted by newest to oldest for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Payment List Options.</param>
        public PaparaListResult<PaymentListItem> List(PaymentListOptions options)
        {
            return this.GetListResult<PaymentListItem>("/list", options, this.requestOptions);
        }

        /// <summary>
        /// Returns a list of completed payments sorted by newest to oldest for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Payment List Options.</param>
        public async Task<PaparaListResult<PaymentListItem>> ListAsync(PaymentListOptions options)
        {
            return await this.GetListResultAsync<PaymentListItem>("/list", options, this.requestOptions);
        }

        /// <summary>
        /// Returns payment and balance information for authorized merchant.
        /// </summary>
        /// <param name="options">Unique payment reference ID.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Payment> GetPaymentByReference(PaymentByReferenceOptions options)
        {
            return this.GetSingleResult<Payment>("/reference", options, this.requestOptions);
        }

        /// <summary>
        /// Returns payment and balance information for authorized merchant.
        /// </summary>
        /// <param name="options">Unique payment reference ID.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Payment>> GetPaymentByReferenceAsync(PaymentByReferenceOptions options)
        {
            return await this.GetSingleResultAsync<Payment>("/reference", options, this.requestOptions);
        }
    }
}
