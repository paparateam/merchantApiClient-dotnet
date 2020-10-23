// <copyright file="ApplicationOptions.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    public class ApplicationOptions : BaseOptions
    {
        [JsonProperty("authorityName")]
        public string AuthorityName { get; set; }

        [JsonProperty("legalName")]
        public string LegalName { get; set; }

        [JsonProperty("webSite")]
        public string WebSite { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("captcha")]
        public string Captcha { get; set; }

        [JsonProperty("taxOffice")]
        public string TaxOffice { get; set; }

        [JsonProperty("taxNumber")]
        public string TaxNumber { get; set; }
    }
}
