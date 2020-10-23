// <copyright file="PaymentListOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// PaymentListOptions is used by payment service for providing request parameters.
    /// </summary>
    public class PaymentListOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets page index. It is the index number of the page that is wanted to display from the pages calculated on the basis of the number of records (pageItemCount) desired to be displayed on a page. Note: the first page is always 1.
        /// </summary>
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets page item count. The number of records that are desired to be displayed on a page.
        /// </summary>
        [JsonProperty("pageItemCount")]
        public int PageItemCount { get; set; }
    }
}