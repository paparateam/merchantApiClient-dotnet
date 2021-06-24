// <copyright file="MassPaymentService.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Papara.Infrastructure;
using System.Threading.Tasks;

namespace Papara
{
    /// <summary>
    /// Mass payment service will be used for getting mass payment info and sending payments to account number, mail address and phone number.
    /// </summary>
    public class MassPaymentService : PaparaService
    {
        /// <summary>
        /// A service for mass payment operations.
        /// </summary>
        private readonly RequestOptions requestOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassPaymentService"/> class.
        /// </summary>
        /// <param name="options"> Request options.</param>
        public MassPaymentService(RequestOptions options = null)
        {
            this.requestOptions = options;
        }

        /// <inheritdoc/>
        protected override string BasePath => "/masspayment";

        /// <summary>
        /// Returns mass payment information for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Get Mass Payment Options.</param>
        public PaparaSingleResult<MassPayment> GetMassPayment(MassPaymentGetOptions options)
        {
            return this.GetSingleResult<MassPayment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Returns mass payment information for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Get Mass Payment Options.</param>
        public async Task<PaparaSingleResult<MassPayment>> GetMassPaymentAsync(MassPaymentGetOptions options)
        {
            return await this.GetSingleResultAsync<MassPayment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a mass payment to given account number for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Mass Payment To Papara Number Options.</param>
        public PaparaSingleResult<MassPayment> CreateMassPaymentWithAccountNumber(MassPaymentToPaparaNumberOptions options)
        {
            return this.PostSingleResult<MassPayment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a mass payment to given account number for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Mass Payment To Papara Number Options.</param>
        public async Task<PaparaSingleResult<MassPayment>> CreateMassPaymentWithAccountNumberAsync(MassPaymentToPaparaNumberOptions options)
        {
            return await this.PostSingleResultAsync<MassPayment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a mass payment to given e-mail address for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Mass Payment To E-Mail Address Options.</param>
        public PaparaSingleResult<MassPayment> CreateMassPaymentWithEmail(MassPaymentToEmailOptions options)
        {
            return this.PostSingleResult<MassPayment>("/email", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a mass payment to given e-mail address for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Mass Payment To E-Mail Address Options.</param>
        public async Task<PaparaSingleResult<MassPayment>> CreateMassPaymentWithEmailAsync(MassPaymentToEmailOptions options)
        {
            return await this.PostSingleResultAsync<MassPayment>("/email", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a mass payment to given phone number for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Mass Payment To Phone Number Options.</param>
        public PaparaSingleResult<MassPayment> CreateMassPaymentWithPhoneNumber(MassPaymentToPhoneNumberOptions options)
        {
            return this.PostSingleResult<MassPayment>("/phone", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a mass payment to given phone number for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Mass Payment To Phone Number Options.</param>
        public async Task<PaparaSingleResult<MassPayment>> CreateMassPaymentWithPhoneNumberAsync(MassPaymentToPhoneNumberOptions options)
        {
            return await this.PostSingleResultAsync<MassPayment>("/phone", options, this.requestOptions);
        }

        /// <summary>
        /// Returns mass payment information by reference.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Get Mass Payment By Reference Options.</param>
        public PaparaSingleResult<MassPayment> GetMassPaymentByReference(MassPaymentByReferenceOptions options)
        {
            return this.GetSingleResult<MassPayment>("/byreference", options, this.requestOptions);
        }

        /// <summary>
        /// Returns mass payment information by reference.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">Get Mass Payment By Reference Options.</param>
        public async Task<PaparaSingleResult<MassPayment>> GetMassPaymentByReferenceAsync(MassPaymentByReferenceOptions options)
        {
            return await this.GetSingleResultAsync<MassPayment>("/byreference", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a recurring mass payment to given account number for authorized merchant 
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">RecurringMassPaymentToAccountNumberOptions recurring mass payment to account number options</param>
        public PaparaSingleResult<RecurringMassPayment> CreateRecurringMassPaymentWithAccountNumber(RecurringMassPaymentToAccountNumberOptions options)
        {
            return this.PostSingleResult<RecurringMassPayment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a recurring mass payment to given account number for authorized merchant 
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">RecurringMassPaymentToAccountNumberOptions recurring mass payment to account number options</param>
        public async Task<PaparaSingleResult<RecurringMassPayment>> CreateRecurringMassPaymentWithAccountNumberAsync (RecurringMassPaymentToAccountNumberOptions options)
        {
            return await this.PostSingleResultAsync<RecurringMassPayment>("/", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a recurring mass payment to given e-mail address for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">RecurringMassPaymentToEmailOptions recurring mass payment to account number options</param>
        public PaparaSingleResult<RecurringMassPayment> CreateRecurringMassPaymentWithEmail(RecurringMassPaymentToEmailOptions options)
        {
            return this.PostSingleResult<RecurringMassPayment>("/email", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a recurring mass payment to given e-mail address for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">RecurringMassPaymentToEmailOptions recurring mass payment to account number options</param>
        public async Task<PaparaSingleResult<RecurringMassPayment>> CreateRecurringMassPaymentWithEmailAsync(RecurringMassPaymentToEmailOptions options)
        {
            return await this.PostSingleResultAsync<RecurringMassPayment>("/email", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a recurring mass payment to given phone number for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">RecurringMassPaymentToPhoneNumberOptions recurring mass payment to Phone number options</param>
        public PaparaSingleResult<RecurringMassPayment> CreateRecurringMassPaymentWithPhoneNumber(RecurringMassPaymentToPhoneNumberOptions options)
        {
            return this.PostSingleResult<RecurringMassPayment>("/phone", options, this.requestOptions);
        }

        /// <summary>
        /// Creates a recurring mass payment to given phone number for authorized merchant.
        /// </summary>
        /// <returns><see cref="PaparaSingleResult{TEntity}"/>.</returns>
        /// <param name="options">RecurringMassPaymentToPhoneNumberOptions recurring mass payment to Phone number options</param>
        public async Task<PaparaSingleResult<RecurringMassPayment>> CreateRecurringMassPaymentWithPhoneNumberAsync(RecurringMassPaymentToPhoneNumberOptions options)
        {
            return await this.PostSingleResultAsync<RecurringMassPayment>("/phone", options, this.requestOptions);
        }        


    }
}
