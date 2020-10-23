// <copyright file="PaparaPagingResult.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace Papara
{
    /// <summary>
    /// Papara Paging type. Handles paging data types sending to and returning from API.
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity.</typeparam>
    public class PaparaPagingResult<TEntity>
        where TEntity : IPaparaEntity
    {
        /// <summary>
        /// Gets or sets items returning from API.
        /// </summary>
        public List<TEntity> Items { get; set; }

        /// <summary>
        /// Gets or sets page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets listed item counts on a page.
        /// </summary>
        public int PageItemCount { get; set; }

        /// <summary>
        /// Gets or sets total item count in a request or response.
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// Gets or sets total page count in a request or response.
        /// </summary>
        public int TotalPageCount { get; set; }

        /// <summary>
        /// Gets or sets how many pages to be skipped.
        /// </summary>
        public int PageSkip { get; set; }
    }
}
