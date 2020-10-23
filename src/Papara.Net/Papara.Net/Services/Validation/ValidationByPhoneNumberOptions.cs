// <copyright file="ValidationByPhoneNumberOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// ValidationByPhoneNumberOptions is used by validation service for providing request parameters.
    /// </summary>
    public class ValidationByPhoneNumberOptions : BaseOptions
    {
        /// <summary>
        ///  Gets or sets phone number registered to Papara.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}