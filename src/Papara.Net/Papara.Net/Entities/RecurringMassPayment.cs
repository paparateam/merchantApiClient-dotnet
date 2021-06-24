using Newtonsoft.Json;


namespace Papara
{
    /// <summary>
    /// RecurringMassPayment interface is used by mass payment service to match returning recurring mass payment values from API.
    /// </summary>


    public class RecurringMassPayment : PaparaEntity
    {
        /// <summary>
        /// Gets or sets merchant id.
        /// </summary>
        [JsonProperty("merchantId")]
        public string merchantId { get; set; }


        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets period. Values are "0" (Monthly), "1" (Weekly), "2" (Daily).
        /// </summary>
        [JsonProperty("period")]
        public int Period { get; set; }

        /// <summary>
        /// Gets or sets ...th day of period. (Weeks start with Monday).
        /// </summary>
        [JsonProperty("executionDay")]
        public int ExecutionDay { get; set; }

        /// <summary>
        /// Gets or sets account number.
        /// </summary>
        [JsonProperty("accountNumber")]
        public int AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets amount.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets currency.Values are “0” (TRY), “1” (USD), “2” (EUR), “3” (GBP).
        /// </summary>
        [JsonProperty("currency")]
        public Currency Currency { get; set; }

    }

}
