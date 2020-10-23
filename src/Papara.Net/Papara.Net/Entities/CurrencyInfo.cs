// <copyright file="CurrencyInfo.cs" company="Crosstech Bilişim Teknolojileri">
// Copyright (c) Crosstech Bilişim Teknolojileri. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Papara
{
    /// <summary>
    /// CurrencyInfo class is used by account ledger model to get or set returning currency values from API.
    /// </summary>
    public class CurrencyInfo : PaparaEntity
    {

        /// <summary>
        /// Gets or sets currency type.
        /// </summary>
        [JsonProperty("currencyEnum")]
        public Currency CurrencyEnum { get; set; }

        /// <summary>
        /// Gets or sets currency symbol.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets currency code.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets currency's prefferred display code.
        /// </summary>
        [JsonProperty("prefferedDisplayCode")]
        public string PrefferedDisplayCode { get; set; }

        /// <summary>
        /// Gets or sets currency name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets if it is a cryptocurrency or not.
        /// </summary>
        [JsonProperty("isCryptocurrency")]
        public bool? IsCryptocurrency { get; set; }

        /// <summary>
        /// Gets or sets currency precision.
        /// </summary>
        [JsonProperty("precision")]
        public int Precision { get; set; }

        /// <summary>
        /// Gets or sets currency icon URL.
        /// </summary>
        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }
    }
}