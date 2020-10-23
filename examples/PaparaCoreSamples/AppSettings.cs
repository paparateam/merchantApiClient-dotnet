using Newtonsoft.Json;
using Papara;
using System;

namespace PaparaCoreSamples
{
    public class PaparaSettings
    {
        public string ApiKey { get; set; }
        public PaparaEnv Env { get; set; }
        public string AccountNumber { get; set; }
        public string PersonalAccountId { get; set; }
        public string PersonalAccountNumber { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public string PersonalEmail { get; set; }
        public int ParsedPersonalAccountNumber { get => Convert.ToInt32(PersonalAccountNumber); }
        public string BankAccount { get; set; }
        public long TCKN { get; set; }
    }
}
