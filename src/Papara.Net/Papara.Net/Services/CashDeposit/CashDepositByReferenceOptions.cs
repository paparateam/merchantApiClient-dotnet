// <copyright file="CashDepositByReferenceOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    ///  CashDepositByReferenceOptions is used by cash deposit service for providing request parameters.
    /// </summary>
    public class CashDepositByReferenceOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets cash deposit reference no. Reference no is required.
        /// </summary>
        public string Reference { get; set; }
    }
}