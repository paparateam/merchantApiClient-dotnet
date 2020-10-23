// <copyright file="PaparaException.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using System;

namespace Papara.Infrastructure
{
    /// <summary>
    /// Papara Exception type. Handles exceptions happened while sending to and returning from API.
    /// </summary>
    /// <typeparam name="TEntity">Generic Entity.</typeparam>
    public class PaparaException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaparaException"/> class.
        /// </summary>
        public PaparaException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaparaException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public PaparaException(string message)
            : base(message)
        {
        }
    }
}
