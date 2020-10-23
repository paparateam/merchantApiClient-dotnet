// <copyright file="Validation.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// Validation class is used by validation service to match returning user value from API.
    /// </summary>
    public class Validation : PaparaEntity
    {
        /// <summary>
        /// Gets or sets unique User ID.
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets user first name.
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user last name.
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user e-mail address.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets user phone number.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets user national identity number.
        /// </summary>
        [JsonProperty("tckn")]
        public string Tckn { get; set; }

        /// <summary>
        /// Gets or sets user account number.
        /// </summary>
        [JsonProperty("accountNumber")]
        public int? AccountNumber { get; set; }
    }
}