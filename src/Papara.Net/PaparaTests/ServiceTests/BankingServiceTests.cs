using Microsoft.VisualStudio.TestTools.UnitTesting;
using Papara;

namespace PaparaTests.ServiceTests
{
    [TestClass]
    public class BankingServiceTests : PaparaTest
    {
        private readonly PaparaClient paparaClient;

        public BankingServiceTests()
        {
            paparaClient = new PaparaClient(Options.ApiKey, Options.Env);
        }

        ///
        /// Test Case:
        ///     Getting bank account from banking service
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaArrayResult{TEntity}"/>
        ///     data should be instance of <see cref="BankAccount[]"/>
        ///    
        [TestMethod]
        public void Should_BankingService_GetBankAccounts_Returns_BankAccount()
        {
            var result = paparaClient.BankingService.GetBankAccounts();

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaArrayResult<BankAccount>));
            Assert.IsInstanceOfType(result.Data, typeof(BankAccount[]));
        }

        ///
        /// Test Case:
        ///     Withdrawal to bank account
        ///     
        /// Conditions:
        ///     Account must have at least 1 bank account information
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaServiceResult"/>
        ///   
        [TestMethod]
        public void Should_BankingService_Withdrawal_Returns_Success()
        {
            var bankAccountResult = paparaClient.BankingService.GetBankAccounts();

            Assert.IsTrue(bankAccountResult.Succeeded);
            Assert.IsInstanceOfType(bankAccountResult.Data, typeof(BankAccount[]));
            Assert.AreNotEqual(0, bankAccountResult.Data.Length, "Merchant must define at least 1 bank account from Papara portal.");

            var bankAccount = bankAccountResult.Data[0];

            var withdrawalOptions = new BankingWithdrawalOptions
            {
                Amount = 10,
                BankAccountId = bankAccount.BankAccountId
            };

            var withdrawalResult = paparaClient.BankingService.Withdrawal(withdrawalOptions);

            Assert.IsTrue(withdrawalResult.Succeeded);
            Assert.IsNull(withdrawalResult.Error);
            Assert.IsInstanceOfType(withdrawalResult, typeof(PaparaServiceResult));
        }
    }
}
