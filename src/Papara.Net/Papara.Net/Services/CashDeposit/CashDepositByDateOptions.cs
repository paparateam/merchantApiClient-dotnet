// <copyright file="CashDepositByDateOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Papara.Infrastructure;
using System;

namespace Papara
{
    /// <summary>
    ///  CashDepositByDateOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositByDateOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets start date of cash deposit.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date of cash deposit.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets page index. It is the index number of the page that is wanted to display from the pages calculated on the basis of the number of records (pageItemCount) desired to be displayed on a page. Note: the first page is always 1.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets page item count. The number of records that are desired to be displayed on a page.
        /// </summary>
        public int PageItemCount { get; set; }
    }
}