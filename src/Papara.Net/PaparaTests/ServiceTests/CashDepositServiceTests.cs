using Microsoft.VisualStudio.TestTools.UnitTesting;
using Papara;
using System;

namespace PaparaTests.ServiceTests
{
    [TestClass]
    public class CashDepositServiceTests : PaparaTest
    {
        private readonly PaparaClient paparaClient;

        public CashDepositServiceTests()
        {
            paparaClient = new PaparaClient(Options.ApiKey, Options.Env);
        }

        /// [TestMethod]
        /// 
        /// This test case is covered by:
        ///     <see cref="Should_CashDepositService_CreateWithPhoneNumber_Returns_CashDeposit"/> 
        ///     <see cref="Should_CashDepositService_CreateWithAccountNumber_Returns_CashDeposit"/> 
        ///     <see cref="Should_CashDepositService_CreateWithTckn_Returns_CashDeposit"/> 
        ///     
        public void Should_CashDepositService_GetCashDeposit_Returns_CashDeposit()
        {
            var cashDepositGetOptions = new CashDepositGetOptions
            {
                Id = 1
            };

            var result = paparaClient.CashDepositService.GetCashDeposit(cashDepositGetOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsInstanceOfType(result.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Creating cash deposit with phone number.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDeposit}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///     should be validated with <see cref="CashDepositService.GetCashDeposit(CashDepositGetOptions)"/>
        [TestMethod]
        public void Should_CashDepositService_CreateWithPhoneNumber_Returns_CashDeposit()
        {
            var cashDepositToPhoneOptions = new CashDepositToPhoneOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                PhoneNumber = Settings.Papara.PersonalPhoneNumber,
            };

            var result = paparaClient.CashDepositService.CreateWithPhoneNumber(cashDepositToPhoneOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDeposit>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDeposit));

            var cashDepositGetOptions = new CashDepositGetOptions
            {
                Id = result.Data.Id.Value
            };

            var cashDepositResult = paparaClient.CashDepositService.GetCashDeposit(cashDepositGetOptions);

            Assert.IsTrue(cashDepositResult.Succeeded);
            Assert.IsNull(cashDepositResult.Error);
            Assert.IsInstanceOfType(cashDepositResult.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Creating cash deposit with account number.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDeposit}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///     should be validated with <see cref="CashDepositService.GetCashDeposit(CashDepositGetOptions)"/>
        [TestMethod]
        public void Should_CashDepositService_CreateWithAccountNumber_Returns_CashDeposit()
        {
            var cashDepositToAccountNumberOptions = new CashDepositToAccountNumberOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                AccountNumber = Settings.Papara.ParsedPersonalAccountNumber,
            };

            var result = paparaClient.CashDepositService.CreateWithAccountNumber(cashDepositToAccountNumberOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDeposit>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDeposit));

            var cashDepositGetOptions = new CashDepositGetOptions
            {
                Id = result.Data.Id.Value
            };

            var cashDepositResult = paparaClient.CashDepositService.GetCashDeposit(cashDepositGetOptions);

            Assert.IsTrue(cashDepositResult.Succeeded);
            Assert.IsInstanceOfType(cashDepositResult.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Creating cash deposit with Turkish Identity (TCKN) number.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDeposit}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///     should be validated with <see cref="CashDepositService.GetCashDeposit(CashDepositGetOptions)"/>
        [TestMethod]
        public void Should_CashDepositService_CreateWithTckn_Returns_CashDeposit()
        {
            var cashDepositToTcknOptions = new CashDepositToTcknOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                Tckn = Settings.Papara.TCKN,
            };

            var result = paparaClient.CashDepositService.CreateWithTckn(cashDepositToTcknOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDeposit>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDeposit));

            var cashDepositResult = paparaClient.CashDepositService.GetCashDeposit(new CashDepositGetOptions
            {
                Id = result.Data.Id.Value
            });

            Assert.IsTrue(cashDepositResult.Succeeded);
            Assert.IsInstanceOfType(cashDepositResult.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Creating cash deposit provision with Turkish Identity (TCKN) number.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDepositProvision}"/>
        ///     data should be instance of <see cref="CashDepositProvision"/>
        ///     should be completed with <see cref="CashDepositService.CompleteProvision(CashDepositCompleteOptions)"/>
        [TestMethod]
        public void Should_CashDepositService_CreateProvisionWithTcknControl_Returns_CashDepositProvision()
        {
            // Create cash deposit provision
            var cashDepositTcknControlOptions = new CashDepositTcknControlOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                PhoneNumber = Settings.Papara.PersonalPhoneNumber,
                Tckn = Settings.Papara.TCKN
            };

            var result = paparaClient.CashDepositService.CreateProvisionWithTckn(cashDepositTcknControlOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDepositProvision>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDepositProvision));

            // Complete provision
            var cashDepositCompleteOptions = new CashDepositCompleteOptions
            {
                Id = result.Data.Id,
                TransactionDate = result.Data.CreatedAt
            };

            var completeResult = paparaClient.CashDepositService.CompleteProvision(cashDepositCompleteOptions);

            Assert.IsTrue(completeResult.Succeeded);
            Assert.IsInstanceOfType(completeResult.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Creating cash deposit provision with phone number.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDepositProvision}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///     should be completed with <see cref="CashDepositService.CompleteProvision(CashDepositCompleteOptions)"/>
        [TestMethod]
        public void Should_CashDepositService_CreateProvisionWithPhoneNumber_Returns_CashDepositProvision()
        {
            // Create cash deposit provision
            var cashDepositToPhoneOptions = new CashDepositToPhoneOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                PhoneNumber = Settings.Papara.PersonalPhoneNumber,
            };

            var result = paparaClient.CashDepositService.CreateProvisionWithPhoneNumber(cashDepositToPhoneOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDepositProvision>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDepositProvision));

            // Complete provision
            var cashDepositCompleteOptions = new CashDepositCompleteOptions
            {
                Id = result.Data.Id,
                TransactionDate = result.Data.CreatedAt
            };

            var completeResult = paparaClient.CashDepositService.CompleteProvision(cashDepositCompleteOptions);

            Assert.IsTrue(completeResult.Succeeded);
            Assert.IsInstanceOfType(completeResult.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Creating cash deposit provision with account number.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDepositProvision}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///     should be completed with <see cref="CashDepositService.CompleteProvision(CashDepositCompleteOptions)"/>
        [TestMethod]
        public void Should_CashDepositService_CreateProvisionWithAccountNumber_Returns_CashDepositProvision()
        {
            // Create cash deposit provision
            var cashDepositToAccountNumberOptions = new CashDepositToAccountNumberOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                AccountNumber = Settings.Papara.ParsedPersonalAccountNumber
            };

            var result = paparaClient.CashDepositService.CreateProvisionWithAccountNumber(cashDepositToAccountNumberOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDepositProvision>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDepositProvision));

            // Complete provision
            var cashDepositCompleteOptions = new CashDepositCompleteOptions
            {
                Id = result.Data.Id,
                TransactionDate = result.Data.CreatedAt
            };

            var completeResult = paparaClient.CashDepositService.CompleteProvision(cashDepositCompleteOptions);

            Assert.IsTrue(completeResult.Succeeded);
            Assert.IsInstanceOfType(completeResult.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Creating cash deposit provision with TCKN.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDepositProvision}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///     should be completed with <see cref="CashDepositService.CompleteProvision(CashDepositCompleteOptions)"/>
        [TestMethod]
        public void Should_CashDepositService_CreateProvisionWithTckn_Returns_CashDepositProvision()
        {
            // Create cash deposit provision
            var cashDepositToTcknOptions = new CashDepositToTcknOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                Tckn = Settings.Papara.TCKN
            };

            var result = paparaClient.CashDepositService.CreateProvisionWithTckn(cashDepositToTcknOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDepositProvision>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDepositProvision));

            // Complete provision
            var cashDepositCompleteOptions = new CashDepositCompleteOptions
            {
                Id = result.Data.Id,
                TransactionDate = result.Data.CreatedAt
            };

            var completeResult = paparaClient.CashDepositService.CompleteProvision(cashDepositCompleteOptions);

            Assert.IsTrue(completeResult.Succeeded);
            Assert.IsInstanceOfType(completeResult.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Control cash deposit provision by reference.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDepositProvision}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///     should be completed with <see cref="CashDepositService.CompleteProvision(CashDepositCompleteOptions)"/>
        // [TestMethod]
        public void Should_CashDepositService_ProvisionByReferenceControl()
        {
            // Create cash deposit provision
            var cashDepositToAccountNumberOptions = new CashDepositToAccountNumberOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                AccountNumber = Settings.Papara.ParsedPersonalAccountNumber
            };

            var createresult = paparaClient.CashDepositService.CreateProvisionWithAccountNumber(cashDepositToAccountNumberOptions);

            Assert.IsTrue(createresult.Succeeded);
            Assert.IsNull(createresult.Error);
            Assert.IsInstanceOfType(createresult, typeof(PaparaSingleResult<CashDepositProvision>));
            Assert.IsInstanceOfType(createresult.Data, typeof(CashDepositProvision));

            var cashdepositControlOptions = new CashDepositControlOptions
            {
                Amount = 10,
                ReferenceCode = createresult.Data.MerchantReference
            };

            var result = paparaClient.CashDepositService.ProvisionByReferenceControl(cashdepositControlOptions);

            Assert.IsTrue(result.Succeeded);
        }

        ///
        /// Test Case:
        ///     Complete cash deposit provision by reference.
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDepositProvision}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///     should be completed with <see cref="CashDepositService.CompleteProvision(CashDepositCompleteOptions)"/>
        // [TestMethod]
         public void Should_CashDepositService_CompleteProvisionByReference()
        {
            // Create cash deposit provision
            var cashDepositToAccountNumberOptions = new CashDepositToAccountNumberOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                AccountNumber = Settings.Papara.ParsedPersonalAccountNumber
            };

            var createresult = paparaClient.CashDepositService.CreateProvisionWithAccountNumber(cashDepositToAccountNumberOptions);

            Assert.IsTrue(createresult.Succeeded);
            Assert.IsNull(createresult.Error);
            Assert.IsInstanceOfType(createresult, typeof(PaparaSingleResult<CashDepositProvision>));
            Assert.IsInstanceOfType(createresult.Data, typeof(CashDepositProvision));

            var cashdepositControlOptions = new CashDepositControlOptions
            {
                Amount = 10,
                ReferenceCode = createresult.Data.MerchantReference
            };

            var result = paparaClient.CashDepositService.CompleteProvisionByReference(cashdepositControlOptions);

            Assert.IsTrue(result.Succeeded);
        }

        /// [TestMethod]
        /// 
        /// This test case is covered by:
        ///     <see cref="Should_CashDepositService_CreateProvisionWithTckn_Returns_CashDepositProvision"/> 
        ///     <see cref="Should_CashDepositService_CreateProvisionWithPhoneNumber_Returns_CashDepositProvision"/> 
        ///     <see cref="Should_CashDepositService_CreateProvisionWithAccountNumber_Returns_CashDepositProvision"/> 
        ///  
        public void Should_CashDepositService_CompleteProvision_Returns_CashDeposit()
        {
            var cashDepositCompleteOptions = new CashDepositCompleteOptions
            {
                Id = 1,
                TransactionDate = DateTime.Now
            };

            var result = paparaClient.CashDepositService.CompleteProvision(cashDepositCompleteOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsInstanceOfType(result.Data, typeof(CashDeposit));
        }

        ///
        /// Test Case:
        ///     Getting cash deposits with date range
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaArrayResult{CashDeposit}"/>
        ///     data should be instance of <see cref="CashDeposit[]"/>
        ///    
        [TestMethod]
        public void Should_CashDepositService_GetCashDepositByDate_Returns_CashDeposit()
        {
            var cashDepositByDateOptions = new CashDepositByDateOptions
            {
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now,
                PageIndex = 1,
                PageItemCount = 20
            };

            var result = paparaClient.CashDepositService.GetCashDepositByDate(cashDepositByDateOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaArrayResult<CashDeposit>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDeposit[]));
        }

        ///
        /// Test Case:
        ///     Getting settlements from cash deposit service 
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDepositSettlement}"/>
        ///     data should be instance of <see cref="CashDepositSettlement"/>
        ///    
        [TestMethod]
        public void Should_CashDepositService_Settlements_Returns_CashDepositSettlement()
        {
            var cashDepositSettlementOptions = new CashDepositSettlementOptions
            {
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now,
                EntryType = EntryType.BankTransfer
            };

            var result = paparaClient.CashDepositService.Settlements(cashDepositSettlementOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDepositSettlement>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDepositSettlement));
        }

        ///
        /// Test Case:
        ///     Getting provision settlements from cash deposit service 
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDepositSettlement}"/>
        ///     data should be instance of <see cref="CashDepositSettlement"/>
        ///    
        [TestMethod]
        public void Should_CashDepositService_ProvisionSettlements_Returns_CashDepositSettlement()
        {
            var cashDepositSettlementOptions = new CashDepositSettlementOptions
            {
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now,
                EntryType = EntryType.BankTransfer
            };

            var result = paparaClient.CashDepositService.ProvisionSettlements(cashDepositSettlementOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<CashDepositSettlement>));
            Assert.IsInstanceOfType(result.Data, typeof(CashDepositSettlement));
        }

        ///
        /// Test Case:
        ///     Getting cash deposit by merchant reference
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{CashDeposit}"/>
        ///     data should be instance of <see cref="CashDeposit"/>
        ///   
        [TestMethod]
        public void Should_CashDepositService_GetCashDepositByReference_Returns_CashDeposit()
        {
            var cashDepositByReferenceOptions = new CashDepositByReferenceOptions
            {
                Reference = "78cadfb9-71d1-42dd-9793-84e90af53b07"
            };

            var result = paparaClient.CashDepositService.GetCashDepositByReference(cashDepositByReferenceOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsInstanceOfType(result.Data, typeof(CashDeposit));
        }
    }
}
