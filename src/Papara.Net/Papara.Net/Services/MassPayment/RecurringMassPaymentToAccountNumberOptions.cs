using Newtonsoft.Json;
using Papara.Infrastructure;

namespace  Papara
{
    /// <summary>
    /// RecurringMassPaymentToAccountNumberOptions is used by mass payment service for providing request parameters.
    /// </summary>

    public class RecurringMassPaymentToAccountNumberOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets Papara account number. The 10-digit Papara number of the user who will receive the payment. It can be in the format 1234567890 or PL1234567890. Before the Papara version transition, the Papara number was called the wallet number.Old wallet numbers have been changed to Papara number. Payment can be distributed to old wallet numbers.
        /// </summary>
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }    

        /// <summary>
        /// Gets or sets amount. The amount of the payment transaction. This amount will be transferred to the account of the user who received the payment. This figure plus transaction fee will be charged to the merchant account.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }   

        /// <summary>
        /// Gets or sets national identity number.It provides the control of the identity information sent by the user who will receive the payment, in the Papara system. In case of a conflict of credentials, the transaction will not take place.
        /// </summary>
        [JsonProperty("turkishNationalId")]
        public long? TurkishNationalId { get; set; }     

        /// <summary>
        /// Gets or sets payment currency.
        /// </summary>
        [JsonProperty("currency")]
        public Currency? Currency { get; set; }

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
        /// Gets or sets description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

}