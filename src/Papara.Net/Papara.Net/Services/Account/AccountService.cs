// <copyright file="AccountService.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Papara.Infrastructure;
using System.Threading.Tasks;

namespace Papara
{
    /// <summary>
    /// Account service will be used for obtaining account information, settlements and ledgers.
    /// </summary>
    public class AccountService : PaparaService
    {
        private readonly RequestOptions requestOptions = new RequestOptions();

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="options">Request Options.</param>
        public AccountService(RequestOptions options = null)
        {
            this.requestOptions = options;
        }

        /// <inheritdoc/>
        protected override string BasePath => "/account";

        /// <summary>
        /// Returns account information and current balance for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Account> GetAccount()
        {
            return this.GetSingleResult<Account>("/", null, this.requestOptions);
        }

        /// <summary>
        /// Returns account information and current balance for authorized merchant asynchronous.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Account>> GetAccountAsync()
        {
            return await this.GetSingleResultAsync<Account>("/", null, this.requestOptions);
        }

        /// <summary>
        /// Returns list of ledgers for authorized merchant.
        /// </summary>
        /// <param name="options"><see cref="LedgerListOptions"/>.</param>
        /// <returns><see cref="PaparaListResult{TEntity}"/>.</returns>
        public PaparaListResult<AccountLedger> ListLedgers(LedgerListOptions options)
        {
            return this.PostListResult<AccountLedger>("/ledgers", options, this.requestOptions);
        }

        /// <summary>
        /// Returns list of ledgers for authorized merchant.
        /// </summary>
        /// <param name="options"><see cref="LedgerListOptions"/>.</param>
        /// <returns><see cref="PaparaListResult{TEntity}"/>.</returns>
        public async Task<PaparaListResult<AccountLedger>> ListLedgersAsync(LedgerListOptions options)
        {
            return await this.PostListResultAsync<AccountLedger>("/ledgers", options, this.requestOptions);
        }

        /// <summary>
        /// Returns settlement for authorized merchant.
        /// </summary>
        /// <param name="options"><see cref="SettlementGetOptions"/>.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaSingleResult<Settlement> GetSettlement(SettlementGetOptions options)
        {
            return this.PostSingleResult<Settlement>("/settlement", options, this.requestOptions);
        }

        /// <summary>
        /// Returns settlement for authorized merchant.
        /// </summary>
        /// <param name="options"><see cref="SettlementGetOptions"/>.</param>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaSingleResult<Settlement>> GetSettlementAsync(SettlementGetOptions options)
        {
            return await this.PostSingleResultAsync<Settlement>("/settlement", options, this.requestOptions);
        }
    }
}
