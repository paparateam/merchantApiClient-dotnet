using Microsoft.VisualStudio.TestTools.UnitTesting;
using Papara;
using System;

namespace PaparaTests.IntegrationTests
{
    [TestClass]
    public class PaymentServiceIntegrationTests : PaparaTest
    {
        private readonly PaparaClient paparaClient;

        public PaymentServiceIntegrationTests()
        {
            paparaClient = new PaparaClient(Options.ApiKey, Options.Env);
        }

        ///
        /// Test Case:
        ///     1. Validate personal account
        ///     2. Sending money from merchant account to personal account
        ///     3. Validate payments
        ///     4. Check balance each step
        /// 
        /// Results
        ///     should be succeeded
        ///     should not have error
        ///  
        [TestMethod]
        public void Payment_Integration_Test()
        {
            decimal paymentAmount = 10;
            decimal totalPayment = 0;

            // Get merchant account
            var balance = this.GetBalance();

            // Validate personal account with papara number
            var paparaNumberValidationResult = paparaClient.ValidationService.ValidateByAccountNumber(new ValidationByAccountNumberOptions
            {
                AccountNumber = base.Settings.Papara.ParsedPersonalAccountNumber
            });

            Assert.IsTrue(paparaNumberValidationResult.Succeeded, paparaNumberValidationResult.Error?.Message);
            var personalAccount = paparaNumberValidationResult.Data;

            // Send cash deposit to personal account with account number
            var cashdeposittoAccountNumberResult = paparaClient.CashDepositService.CreateWithAccountNumber(new CashDepositToAccountNumberOptions
            {
                AccountNumber = personalAccount.AccountNumber.Value,
                MerchantReference = Guid.NewGuid().ToString(),
                Amount = paymentAmount
            });

            Assert.IsTrue(cashdeposittoAccountNumberResult.Succeeded, cashdeposittoAccountNumberResult.Error?.Message);
            totalPayment += paymentAmount;

            decimal newBalance = GetBalance();
            Assert.AreEqual(balance + totalPayment, newBalance);

            // Send payment with mass payment service
            var massPaymentResult = paparaClient.MassPaymentService.CreateMassPaymentWithAccountNumber(new MassPaymentToPaparaNumberOptions
            {
                AccountNumber = Settings.Papara.PersonalAccountNumber,
                Amount = paymentAmount,
                Description = "Unit Test: MassPaymentToPaparaNumber",
                MassPaymentId = Guid.NewGuid().ToString(),
                ParseAccountNumber = 1,
                TurkishNationalId = Settings.Papara.TCKN
            });

            Assert.IsTrue(massPaymentResult.Succeeded, massPaymentResult.Error?.Message);
            totalPayment += paymentAmount;

            newBalance = this.GetBalance();
            Assert.AreEqual(balance, newBalance);
        }

        private decimal GetBalance()
        {
            var accountResult = paparaClient.AccountService.GetAccount();

            Assert.IsTrue(accountResult.Succeeded);
            Assert.AreNotEqual(0, accountResult.Data.Balances.Count);

            var balance = accountResult.Data.Balances[0];
            var totalBalance = balance.TotalBalance;

            return totalBalance;
        }
    }
}

