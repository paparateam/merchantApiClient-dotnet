namespace Papara
{
    /// <summary>
    /// Papara merchant service facade.
    /// </summary>
    public class PaparaClient : IPaparaClient
    {
        private string apiKey;
        private PaparaEnv env;

        private AccountService accountService;
        private BankingService bankingService;
        private CashDepositService cashDepositService;
        private MassPaymentService massPaymentService;
        private PaymentService paymentService;
        private ValidationService validationService;

        /// <summary>
        /// Gets <see cref="Papara.AccountService"/> instance.
        /// </summary>
        public AccountService AccountService
        {
            get
            {
                if (this.accountService == null)
                {
                    var requestOptions = new RequestOptions
                    {
                        ApiKey = this.apiKey,
                        Env = this.env,
                    };

                    this.accountService = new AccountService(requestOptions);
                }

                return this.accountService;
            }
        }

        /// <summary>
        /// Gets <see cref="Papara.BankingService"/> instance.
        /// </summary>
        public BankingService BankingService
        {
            get
            {
                if (this.bankingService == null)
                {
                    var requestOptions = new RequestOptions
                    {
                        ApiKey = this.apiKey,
                        Env = this.env,
                    };

                    this.bankingService = new BankingService(requestOptions);
                }

                return this.bankingService;
            }
        }

        /// <summary>
        /// Gets <see cref="Papara.CashDepositService"/> instance.
        /// </summary>
        public CashDepositService CashDepositService
        {
            get
            {
                if (this.cashDepositService == null)
                {
                    var requestOptions = new RequestOptions
                    {
                        ApiKey = this.apiKey,
                        Env = this.env,
                    };

                    this.cashDepositService = new CashDepositService(requestOptions);
                }

                return this.cashDepositService;
            }
        }

        /// <summary>
        /// Gets <see cref="Papara.MassPaymentService"/> instance.
        /// </summary>
        public MassPaymentService MassPaymentService
        {
            get
            {
                if (this.massPaymentService == null)
                {
                    var requestOptions = new RequestOptions
                    {
                        ApiKey = this.apiKey,
                        Env = this.env,
                    };

                    this.massPaymentService = new MassPaymentService(requestOptions);
                }

                return this.massPaymentService;
            }
        }

        /// <summary>
        /// Gets <see cref="Papara.PaymentService"/> instance.
        /// </summary>
        public PaymentService PaymentService
        {
            get
            {
                if (this.paymentService == null)
                {
                    var requestOptions = new RequestOptions
                    {
                        ApiKey = this.apiKey,
                        Env = this.env,
                    };

                    this.paymentService = new PaymentService(requestOptions);
                }

                return this.paymentService;
            }
        }

        /// <summary>
        /// Gets <see cref="Papara.ValidationService"/> instance.
        /// </summary>
        public ValidationService ValidationService
        {
            get
            {
                if (this.validationService == null)
                {
                    var requestOptions = new RequestOptions
                    {
                        ApiKey = this.apiKey,
                        Env = this.env,
                    };

                    this.validationService = new ValidationService(requestOptions);
                }

                return this.validationService;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PaparaClient"/> class.
        /// </summary>
        public PaparaClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaparaClient"/> class.
        /// </summary>
        /// <param name="apiKey">Api key for service authentication. Apikey could be find in https://merchant.test.papara.com/APIInfo. </param>
        /// <param name="env">Papara environment. <see cref="PaparaEnv.Live"/> for live, <see cref="PaparaEnv.Test"/> for sandbox mode. </param>
        public PaparaClient(string apiKey, PaparaEnv env = PaparaEnv.Live)
        {
            this.apiKey = apiKey;
            this.env = env;
        }

        /// <summary>
        /// Set options for papara client.
        /// </summary>
        /// <param name="apiKey">Api key for service authentication. Apikey could be find in https://merchant.test.papara.com/APIInfo. </param>
        /// <param name="env">Papara environment. <see cref="PaparaEnv.Live"/> for live, <see cref="PaparaEnv.Test"/> for sandbox mode. </param>
        public void SetOptions(string apiKey, PaparaEnv env = PaparaEnv.Live)
        {
            this.apiKey = apiKey;
            this.env = env;
        }
    }
}
