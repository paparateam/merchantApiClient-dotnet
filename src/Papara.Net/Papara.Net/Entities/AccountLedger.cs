// <copyright file="AccountLedger.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System;

namespace Papara
{
    /// <summary>
    /// AccountLedger class is used by account service to match returning ledger values from API.
    /// </summary>
    public class AccountLedger : PaparaEntity
    {
        /// <summary>
        /// Gets or sets merchant ID.
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets created date of a ledger.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets entry type.
        /// BankTransfer = 1
        /// CorporateCardTransaction = 2,
        /// LoadingMoneyFromPhysicalPoint = 6,
        /// MerchantPayment = 8,
        /// PaymentDistribution = 9,
        /// EduPos = 11.
        /// </summary>
        [JsonProperty("entryType")]
        public EntryType? EntryType { get; set; }

        /// <summary>
        /// Gets or sets entry type name.
        /// </summary>
        [JsonProperty("entryTypeName")]
        public string EntryTypeName { get; set; }

        /// <summary>
        /// Gets or sets amount.
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets fee.
        /// </summary>
        [JsonProperty("fee")]
        public decimal? Fee { get; set; }

        /// <summary>
        /// Gets or sets currency.
        /// </summary>
        [JsonProperty("currency")]
        public Currency? Currency { get; set; }

        /// <summary>
        /// Gets or sets currency information.
        /// </summary>
        [JsonProperty("currencyInfo")]
        public CurrencyInfo CurrencyInfo { get; set; }

        /// <summary>
        /// Gets or sets resulting balance.
        /// </summary>
        [JsonProperty("resultingBalance")]
        public decimal? ResultingBalance { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets mass payment Id. It is the unique value sent by the merchant to prevent duplicate repetition in payment transactions. It is displayed in transaction records of masspayment type in account transactions to ensure control of the transaction. It will be null in other payment types.
        /// </summary>
        [JsonProperty("massPaymentId")]
        public string MassPaymentId { get; set; }

        /// <summary>
        /// Gets or sets checkout payment ID. It is the ID field in the data object in the payment record transaction. It is the unique identifier of the payment transaction. It is displayed in transaction records of checkout type in account transactions. It will be null in other payment types.
        /// </summary>
        [JsonProperty("checkoutPaymentId")]
        public string CheckoutPaymentId { get; set; }

        /// <summary>
        /// Gets or sets checkout reference ID. This is the referenceId field sent when creating the payment transaction record. It is the reference information of the payment transaction in the merchant system. It is displayed in transaction records of checkout type in account transactions. It will be null in other payment types.
        /// </summary>
        [JsonProperty("checkoutReferenceId")]
        public string CheckoutReferenceId { get; set; }
    }
}