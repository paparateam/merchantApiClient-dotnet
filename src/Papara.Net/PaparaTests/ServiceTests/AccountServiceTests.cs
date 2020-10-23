using Microsoft.VisualStudio.TestTools.UnitTesting;
using Papara;
using System;
using System.Collections.Generic;

namespace PaparaTests.ServiceTests
{
    [TestClass]
    public class AccountServiceTests : PaparaTest
    {
        private readonly PaparaClient paparaClient;

        public AccountServiceTests()
        {
            paparaClient = new PaparaClient(Options.ApiKey, Options.Env);
        }

        ///
        /// Test Case:
        ///     Getting account information from account service
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaPagingResult{Account}"/>
        ///     data should be instance of <see cref="Account"/>
        ///
        [TestMethod]
        public void Should_AccountService_GetAccount_Returns_Account()
        {
            var result = paparaClient.AccountService.GetAccount();

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Account>));
            Assert.IsInstanceOfType(result.Data, typeof(Account));
        }

        /// 
        /// Test Case:
        ///     Listing ledgers from account service
        ///     
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaPagingResult{AccountLedger}"/>
        ///     data should be instance of <see cref="List{AccountLedger}"/>
        ///     
        [TestMethod]
        public void Should_AccountService_ListLedgers_Returns_List_of_Ledgers()
        {
            var ledgerListOptions = new LedgerListOptions
            {
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now,
                Page = 1,
                PageSize = 20
            };

            var result = paparaClient.AccountService.ListLedgers(ledgerListOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result.Data, typeof(PaparaPagingResult<AccountLedger>));
            Assert.IsInstanceOfType(result.Data.Items, typeof(List<AccountLedger>));
        }

        /// 
        /// Test Case:
        ///     Getting settlement from account service
        ///     
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{Settlement}"/>
        ///     data should be instance of <see cref="Settlement"/>
        ///     
        [TestMethod]
        public void Should_AccountService_GetSettlement_Returns_Settlement()
        {
            var settlementGetOptions = new SettlementGetOptions
            {
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now
            };

            var result = paparaClient.AccountService.GetSettlement(settlementGetOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Settlement>));
            Assert.IsInstanceOfType(result.Data, typeof(Settlement));
        }
    }
}
