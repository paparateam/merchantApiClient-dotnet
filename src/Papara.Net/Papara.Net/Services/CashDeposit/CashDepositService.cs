// <copyright file="CashDepositService.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Papara.Infrastructure;
using System.Threading.Tasks;

namespace Papara
{
    /// <summary>
    /// Cash deposit service will be used for deposit operations for an end user.
    /// </summary>
    public class CashDepositService : PaparaService
    {
        private readonly RequestOptions requestOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CashDepositService"/> class.
        /// </summary>
        /// <param name="options">Request Options.</param>
        public CashDepositService(RequestOptions options = null)
        {
            this.requestOptions = options;
        }

        /// <inheritdoc/>
        protected override string BasePath => "/cashdeposit";

        /// <summary>
        /// Returns a cash deposit information.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Get Cash Deposit Options.</param>
        public PaparaSingleResult<CashDeposit> GetCashDeposit(CashDepositGetOptions options)
        {
            return this.GetSingleResult<CashDeposit>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Returns a cash deposit information.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Get Cash Deposit Options.</param>
        public async Task<PaparaSingleResult<CashDeposit>> GetCashDepositAsync(CashDepositGetOptions options)
        {
            return await this.GetSingleResultAsync<CashDeposit>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request using end users's phone number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit To Phone Number Options.</param>
        public PaparaSingleResult<CashDeposit> CreateWithPhoneNumber(CashDepositToPhoneOptions options)
        {
            return this.PostSingleResult<CashDeposit>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request using end users's phone number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit To Phone Number Options.</param>
        public async Task<PaparaSingleResult<CashDeposit>> CreateWithPhoneNumberAsync(CashDepositToPhoneOptions options)
        {
            return await this.PostSingleResultAsync<CashDeposit>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request using end user's account number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit To Account Number Options.</param>
        public PaparaSingleResult<CashDeposit> CreateWithAccountNumber(CashDepositToAccountNumberOptions options)
        {
            return this.PostSingleResult<CashDeposit>("/accountNumber", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request using end user's account number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit To Account Number Options.</param>
        public async Task<PaparaSingleResult<CashDeposit>> CreateWithAccountNumberAsync(CashDepositToAccountNumberOptions options)
        {
            return await this.PostSingleResultAsync<CashDeposit>("/accountNumber", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request using end users's national identity number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit To National Identity Number Options.</param>
        public PaparaSingleResult<CashDeposit> CreateWithTckn(CashDepositToTcknOptions options)
        {
            return this.PostSingleResult<CashDeposit>("/tckn", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request using end users's national identity number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit To National Identity Number Options.</param>
        public async Task<PaparaSingleResult<CashDeposit>> CreateWithTcknAsync(CashDepositToTcknOptions options)
        {
            return await this.PostSingleResultAsync<CashDeposit>("/tckn", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request without upfront payment using end user's national identity number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With National Identity Options.</param>
        public PaparaSingleResult<CashDepositProvision> CreateProvisionWithTckn(CashDepositTcknControlOptions options)
        {
            return this.PostSingleResult<CashDepositProvision>("/provision/withtckncontrol", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request without upfront payment using end user's national identity number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With National Identity Options.</param>
        public async Task<PaparaSingleResult<CashDepositProvision>> CreateProvisionWithTcknAsync(CashDepositTcknControlOptions options)
        {
            return await this.PostSingleResultAsync<CashDepositProvision>("/provision/withtckncontrol", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request without upfront payment using end users's phone number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With Phone Number Options.</param>
        public PaparaSingleResult<CashDepositProvision> CreateProvisionWithPhoneNumber(CashDepositToPhoneOptions options)
        {
            return this.PostSingleResult<CashDepositProvision>("/provision/phonenumber", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request without upfront payment using end users's phone number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With Phone Number Options.</param>
        public async Task<PaparaSingleResult<CashDepositProvision>> CreateProvisionWithPhoneNumberAsync(CashDepositToPhoneOptions options)
        {
            return await this.PostSingleResultAsync<CashDepositProvision>("/provision/phonenumber", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request without upfront payment using merchant's account number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With Account Number Options.</param>
        public PaparaSingleResult<CashDepositProvision> CreateProvisionWithAccountNumber(CashDepositToAccountNumberOptions options)
        {
            return this.PostSingleResult<CashDepositProvision>("/provision/accountnumber", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit request without upfront payment using merchant's account number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With Account Number Options.</param>
        public async Task<PaparaSingleResult<CashDepositProvision>> CreateProvisionWithAccountNumberAsync(CashDepositToAccountNumberOptions options)
        {
            return await this.PostSingleResultAsync<CashDepositProvision>("/provision/accountnumber", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit provision request without upfront payment using national identity number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With TCKN Options.</param>
        public PaparaSingleResult<CashDepositProvision> CreateProvisionWithTckn(CashDepositToTcknOptions options)
        {
            return this.PostSingleResult<CashDepositProvision>("/provision/tckn", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a cash deposit provision request without upfront payment using national identity number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With TCKN Options.</param>
        public async Task<PaparaSingleResult<CashDepositProvision>> CreateProvisionWithTcknAsync(CashDepositToTcknOptions options)
        {
            return await this.PostSingleResultAsync<CashDepositProvision>("/provision/tckn", options, this.requestOptions);
        }

        /// <summary>
        /// Makes a cash deposit ready to be complete.
        /// </summary>
        /// <returns><see cref="PaparaServiceResult"/>.</returns>
        /// <param name="options">Cash Deposit Provision With Account Number Options.</param>
        public PaparaServiceResult ProvisionByReferenceControl(CashDepositControlOptions options)
        {
            return this.PostServiceResult("/provisionbyreference/control", options, this.requestOptions);
        }

        /// <summary>
        /// Makes a cash deposit ready to be complete.
        /// </summary>
        /// <returns><see cref="PaparaServiceResult"/>.</returns>
        /// <param name="options">Cash Deposit Provision With Account Number Options.</param>
        public async Task<PaparaServiceResult> ProvisionByReferenceControlAsync(CashDepositControlOptions options)
        {
            return await this.PostServiceResultAsync("/provisionbyreference/control", options, this.requestOptions);
        }

        /// <summary>
        /// Makes a cash deposit ready to be complete.
        /// </summary>
        /// <returns><see cref="PaparaServiceResult"/>.</returns>
        /// <param name="options">Cash Deposit Provision With Account Number Options.</param>
        public PaparaServiceResult CompleteProvisionByReference(CashDepositControlOptions options)
        {
            return this.PostServiceResult("/provisionbyreference/complete", options, this.requestOptions);
        }

        /// <summary>
        /// Makes a cash deposit ready to be complete.
        /// </summary>
        /// <returns><see cref="PaparaServiceResult"/>.</returns>
        /// <param name="options">Cash Deposit Provision With Account Number Options.</param>
        public async Task<PaparaServiceResult> CompleteProvisionByReferenceAsync(CashDepositControlOptions options)
        {
            return await this.PostServiceResultAsync("/provisionbyreference/control", options, this.requestOptions);
        }

        /// <summary>
        /// Completes a cash deposit request without upfront payment.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Complete Options.</param>
        public PaparaSingleResult<CashDeposit> CompleteProvision(CashDepositCompleteOptions options)
        {
            return this.PostSingleResult<CashDeposit>("/provision/complete", options, this.requestOptions);
        }

        /// <summary>
        /// Completes a cash deposit request without upfront payment.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Complete Options.</param>
        public async Task<PaparaSingleResult<CashDeposit>> CompleteProvisionAsync(CashDepositCompleteOptions options)
        {
            return await this.PostSingleResultAsync<CashDeposit>("/provision/complete", options, this.requestOptions);
        }

        /// <summary>
        /// Returns a cash deposit information by given date.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With By Date Options.</param>
        public PaparaArrayResult<CashDeposit> GetCashDepositByDate(CashDepositByDateOptions options)
        {
            return this.GetArrayResult<CashDeposit>("/bydate", options, this.requestOptions);
        }

        /// <summary>
        /// Returns a cash deposit information by given date.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Provision With By Date Options.</param>
        public async Task<PaparaArrayResult<CashDeposit>> GetCashDepositByDateAsync(CashDepositByDateOptions options)
        {
            return await this.GetArrayResultAsync<CashDeposit>("/bydate", options, this.requestOptions);
        }

        /// <summary>
        /// Returns total transaction volume and count between given dates. Start and end dates are included.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Settlement Options.</param>
        public PaparaSingleResult<CashDepositSettlement> Settlements(CashDepositSettlementOptions options)
        {
            return this.PostSingleResult<CashDepositSettlement>("/settlement", options, this.requestOptions);
        }

        /// <summary>
        /// Returns total transaction volume and count between given dates. Start and end dates are included.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Settlement Options.</param>
        public async Task<PaparaSingleResult<CashDepositSettlement>> SettlementsAsync(CashDepositSettlementOptions options)
        {
            return await this.PostSingleResultAsync<CashDepositSettlement>("/settlement", options, this.requestOptions);
        }

        /// <summary>
        /// Returns total transaction volume and count between given dates. Start and end dates are included.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Settlement Options.</param>
        public PaparaSingleResult<CashDepositSettlement> ProvisionSettlements(CashDepositSettlementOptions options)
        {
            return this.PostSingleResult<CashDepositSettlement>("/provision/settlement", options, this.requestOptions);
        }

        /// <summary>
        /// Returns total transaction volume and count between given dates. Start and end dates are included.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit Settlement Options.</param>
        public async Task<PaparaSingleResult<CashDepositSettlement>> ProvisionSettlementsAsync(CashDepositSettlementOptions options)
        {
            return await this.PostSingleResultAsync<CashDepositSettlement>("/provision/settlement", options, this.requestOptions);
        }

        /// <summary>
        /// Returns a cash deposit object using merchant's unique reference number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit By Reference Options.</param>
        public PaparaSingleResult<CashDeposit> GetCashDepositByReference(CashDepositByReferenceOptions options)
        {
            return this.GetSingleResult<CashDeposit>("/byreference", options, this.requestOptions);
        }

        /// <summary>
        /// Returns a cash deposit object using merchant's unique reference number.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Cash Deposit By Reference Options.</param>
        public async Task<PaparaSingleResult<CashDeposit>> GetCashDepositByReferenceAsync(CashDepositByReferenceOptions options)
        {
            return await this.GetSingleResultAsync<CashDeposit>("/byreference", options, this.requestOptions);
        }

    }
}
