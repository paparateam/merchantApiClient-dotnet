﻿// <copyright file="PaymentCreateOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// PaymentCreateOptions is used by payment service for providing request parameters.
    /// </summary>
    public class PaymentCreateOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets amount. The amount of the payment transaction. Exactly this amount will be taken from the account of the user who made the payment, and this amount will be displayed to the user on the payment screen. Amount field can be minimum 1.00 and maximum 500000.00.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets reference ID. Reference information of the payment transaction in the merchant system. The transaction will be returned to the merchant without being changed in the result notifications as it was sent to Papara. Must be no more than 100 characters. This area does not have to be unique and Papara does not make such a check.
        /// </summary>
        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets order description. Description of the payment transaction. The sent value will be displayed to the user on the Papara checkout page. Having a description that accurately identifies the transaction initiated by the user, will increase the chance of successful payment.
        /// </summary>
        [JsonProperty("orderDescription")]
        public string OrderDescription { get; set; }

        /// <summary>
        /// Gets or sets notification URL. The URL to which payment notification requests (IPN) will be sent. With this field, the URL where the POST will be sent to the payment merchant must be sent. To the URL sent with "notificationUrl", Papara will send a payment object containing all information of the payment with an HTTP POST request immediately after the payment is completed. If the merchant returns 200 OK to this request, no notification will be made again. If the merchant does not return 200 OK to this notification, Papara will continue to make payment notification (IPN) requests for 24 hours until the merchant returns to 200 OK.
        /// </summary>
        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; set; }

        /// <summary>
        /// Gets or sets redirect URL. URL to which the user will be redirected at the end of the process.
        /// </summary>
        [JsonProperty("redirectUrl")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets national identity number.It provides the control of the identity information sent by the user who will receive the payment, in the Papara system. In case of a conflict of credentials, the transaction will not take place.
        /// </summary>
        [JsonProperty("turkishNationalId")]
        public long? TurkishNationalId { get; set; }

        /// <summary>
        /// Gets or sets payment currency.
        /// </summary>
        [JsonProperty("currency")]
        public Currency? Currency { get; set; }
    }
}