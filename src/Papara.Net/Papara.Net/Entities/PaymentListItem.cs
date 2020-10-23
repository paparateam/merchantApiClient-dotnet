// <copyright file="PaymentListItem.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System;

namespace Papara
{
    /// <summary>
    /// PaymentListItem class is used by payment service to match returning completed payment list values list API.
    /// </summary>
    public class PaymentListItem : PaparaEntity
    {
        /// <summary>
        /// Gets or sets payment ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets created date.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets merchant ID.
        /// </summary>
        [JsonProperty("merchantId")]
        public string MerchantId { get; set; }

        /// <summary>
        /// Gets or sets user ID.
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets payment Method.
        /// 0 - User completed transaction with existing Papara balance
        /// 1 - User completed the transaction with a debit / credit card that was previously defined.
        /// 2 - User completed transaction via mobile payment.
        /// </summary>
        [JsonProperty("paymentMethod")]
        public PaymentMethod? PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets payment method description.
        /// </summary>
        [JsonProperty("paymentMethodDescription")]
        public string PaymentMethodDescription { get; set; }

        /// <summary>
        /// Gets or sets reference ID.
        /// </summary>
        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets order description.
        /// </summary>
        [JsonProperty("orderDescription")]
        public string OrderDescription { get; set; }

        /// <summary>
        /// Gets or sets status.
        /// 0 - Awaiting, payment is not done yet.
        /// 1 - Payment is done, transaction is completed.
        /// 2 - Transactions is refunded by merchant.
        /// </summary>
        [JsonProperty("status")]
        public PaymentStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets status description.
        /// </summary>
        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

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
        /// Gets or sets currency. Values are “0”, “1”, “2”, “3”.
        /// </summary>
        [JsonProperty("currency")]
        public Currency? Currency { get; set; }

        /// <summary>
        /// Gets or sets notification URL.
        /// </summary>
        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; set; }

        /// <summary>
        /// Gets or sets if notification was made.
        /// </summary>
        [JsonProperty("notificationDone")]
        public bool? NotificationDone { get; set; }

        /// <summary>
        /// Gets or sets redirect URL.
        /// </summary>
        [JsonProperty("redirectUrl")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets payment URL.
        /// </summary>
        [JsonProperty("paymentUrl")]
        public string PaymentUrl { get; set; }

        /// <summary>
        /// Gets or sets merchant secret key.
        /// </summary>
        [JsonProperty("merchantSecretKey")]
        public string MerchantSecretKey { get; set; }

        /// <summary>
        /// Gets or sets returning Redirect URL.
        /// </summary>
        [JsonProperty("returningRedirectUrl")]
        public string ReturningRedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets national identity number.
        /// </summary>
        [JsonProperty("turkishNationalId")]
        public long? TurkishNationalId { get; set; }
    }
}