// <copyright file="BankingService.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Papara.Infrastructure;
using System.Threading.Tasks;

namespace Papara
{
    /// <summary>
    /// Banking service will be used for listing merchant's bank accounts and making withdrawal operations. 
    /// </summary>
    public class BankingService : PaparaService
    {
        private readonly RequestOptions requestOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankingService"/> class.
        /// </summary>
        /// <param name="options">Request Options.</param>
        public BankingService(RequestOptions options = null)
        {
            this.requestOptions = options;
        }

        /// <inheritdoc/>
        protected override string BasePath => "/banking";

        /// <summary>
        /// Returns bank accounts for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public PaparaArrayResult<BankAccount> GetBankAccounts()
        {
            return this.GetArrayResult<BankAccount>("/bankaccounts", null, this.requestOptions);
        }

        /// <summary>
        /// Returns bank accounts for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        public async Task<PaparaArrayResult<BankAccount>> GetBankAccountsAsync()
        {
            return await this.GetArrayResultAsync<BankAccount>("/bankaccounts", null, this.requestOptions);
        }

        /// <summary>
        /// Creates a withdrawal request from given bank account for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaServiceResult"/>
        /// Error codes:
        /// 105 - Insufficient funds.
        /// 115 - Requested amount is lower then minimum limit.
        /// 120 - Bank account not found.
        /// 247 - Merchant's account is not active.
        /// </returns>
        /// <param name="options">Banking Withdrawal Options.</param>
        public PaparaServiceResult Withdrawal(BankingWithdrawalOptions options)
        {
            return this.PostServiceResult("/withdrawal", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a withdrawal request from given bank account for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaServiceResult"/>
        /// Error codes:
        /// 105 - Insufficient funds.
        /// 115 - Requested amount is lower then minimum limit.
        /// 120 - Bank account not found.
        /// 247 - Merchant's account is not active.
        /// </returns>
        /// <param name="options">Banking Withdrawal Options.</param>
        public async Task<PaparaServiceResult> WithdrawalAsync(BankingWithdrawalOptions options)
        {
            return await this.PostServiceResultAsync("/withdrawal", options, this.requestOptions);
        }
    }
}
