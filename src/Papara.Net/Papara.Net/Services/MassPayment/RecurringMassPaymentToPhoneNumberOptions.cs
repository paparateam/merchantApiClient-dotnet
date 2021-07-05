using Newtonsoft.Json;
using Papara.Infrastructure;

namespace Papara
{
    /// <summary>
    /// RecurringMassPaymentToPhoneNumberOptions is used by mass payment service for providing request parameters.
    /// </summary>

    public class RecurringMassPaymentToPhoneNumberOptions : BaseOptions
    {

        /// <summary>
        /// Gets or sets user's phone number. The mobile number of the user who will receive the payment, registered in Papara. It should contain a country code and start with +.
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

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