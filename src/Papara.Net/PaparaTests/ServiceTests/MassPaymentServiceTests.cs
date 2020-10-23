using Microsoft.VisualStudio.TestTools.UnitTesting;
using Papara;
using System;

namespace PaparaTests.ServiceTests
{
    [TestClass]
    public class MassPaymentServiceTests : PaparaTest
    {
        private readonly PaparaClient paparaClient;

        public MassPaymentServiceTests()
        {
            paparaClient = new PaparaClient(Options.ApiKey, Options.Env);
        }

        /// [TestMethod]
        ///  
        /// This test case is covered by:
        ///     <see cref="Should_MassPaymentService_PostMassPayment_Returns_MassPayment"/>
        ///     <see cref="Should_MassPaymentService_PostMassPaymentToEmail_Returns_MassPayment"/>
        ///     <see cref="Should_MassPaymentService_PostMassPaymentToPhone_Returns_MassPayment"/>
        ///
        public void Should_MassPaymentService_GetMassPayment_Returns_MassPayment()
        {
            var massPaymentGetOptions = new MassPaymentGetOptions
            {
                Id = 1
            };

            var result = paparaClient.MassPaymentService.GetMassPayment(massPaymentGetOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result.Data, typeof(MassPayment));
        }

        ///
        /// Test Case:
        ///     Creating mass payment with Papara account number
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{MassPayment}"/> object
        ///     data should be instance of <see cref="MassPayment"/> object
        ///   
        [TestMethod]
        public void Should_MassPaymentService_PostMassPayment_Returns_MassPayment()
        {
            var massPaymentToPaparaNumberOptions = new MassPaymentToPaparaNumberOptions
            {
                AccountNumber = Settings.Papara.PersonalAccountNumber,
                Amount = 1,
                Description = "Unit Test: MassPaymentToPaparaNumber",
                MassPaymentId = Guid.NewGuid().ToString(),
                ParseAccountNumber = 1,
                TurkishNationalId = Settings.Papara.TCKN
            };

            var result = paparaClient.MassPaymentService.CreateMassPaymentWithAccountNumber(massPaymentToPaparaNumberOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<MassPayment>));
            Assert.IsInstanceOfType(result.Data, typeof(MassPayment));

            var masspaymentOptions = new MassPaymentGetOptions
            {
                Id = result.Data.Id.Value
            };

            var masspayment = paparaClient.MassPaymentService.GetMassPayment(masspaymentOptions);

            Assert.IsTrue(masspayment.Succeeded);
        }

        ///
        /// Test Case:
        ///     Creating mass payment with email
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{MassPayment}"/> object
        ///     data should be instance of <see cref="MassPayment"/> object
        ///   
        [TestMethod]
        public void Should_MassPaymentService_PostMassPaymentToEmail_Returns_MassPayment()
        {
            var massPaymentToEmailOptions = new MassPaymentToEmailOptions
            {
                Amount = 1,
                MassPaymentId = Guid.NewGuid().ToString(),
                Description = "Unit Test: MassPaymentToEmail",
                Email = Settings.Papara.PersonalEmail,
                TurkishNationalId = Settings.Papara.TCKN
            };

            var result = paparaClient.MassPaymentService.CreateMassPaymentWithEmail(massPaymentToEmailOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<MassPayment>));
            Assert.IsInstanceOfType(result.Data, typeof(MassPayment));

            var masspaymentOptions = new MassPaymentGetOptions
            {
                Id = result.Data.Id.Value
            };

            var masspayment = paparaClient.MassPaymentService.GetMassPayment(masspaymentOptions);

            Assert.IsTrue(masspayment.Succeeded);
        }

        ///
        /// Test Case:
        ///     Creating mass payment with phone number
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{MassPayment}"/> object
        ///     data should be instance of <see cref="MassPayment"/> object
        ///   
        [TestMethod]
        public void Should_MassPaymentService_PostMassPaymentToPhone_Returns_MassPayment()
        {
            var massPaymentToPhoneNumberOptions = new MassPaymentToPhoneNumberOptions
            {
                Amount = 1,
                MassPaymentId = Guid.NewGuid().ToString(),
                Description = "Unit Test: MassPaymentToPhoneNumber",
                PhoneNumber = Settings.Papara.PersonalPhoneNumber,
                TurkishNationalId = Settings.Papara.TCKN
            };

            var result = paparaClient.MassPaymentService.CreateMassPaymentWithPhoneNumber(massPaymentToPhoneNumberOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<MassPayment>));
            Assert.IsInstanceOfType(result.Data, typeof(MassPayment));

            var masspaymentOptions = new MassPaymentGetOptions
            {
                Id = result.Data.Id.Value
            };

            var masspayment = paparaClient.MassPaymentService.GetMassPayment(masspaymentOptions);

            Assert.IsTrue(masspayment.Succeeded);
        }

        ///
        /// Test Case:
        ///     Get MassPayment By Reference
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{MassPayment}"/> object
        ///     data should be instance of <see cref="MassPayment"/> object
        ///   
        [TestMethod]
        public void Should_MassPaymentService_GetMassPaymentByReference_Returns_MassPayment()
        {
            var massPaymentToPhoneNumberOptions = new MassPaymentToPhoneNumberOptions
            {
                Amount = 1,
                MassPaymentId = Guid.NewGuid().ToString(),
                Description = "Unit Test: MassPaymentToPhoneNumber",
                PhoneNumber = Settings.Papara.PersonalPhoneNumber,
                TurkishNationalId = Settings.Papara.TCKN
            };

            var result = paparaClient.MassPaymentService.CreateMassPaymentWithPhoneNumber(massPaymentToPhoneNumberOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<MassPayment>));
            Assert.IsInstanceOfType(result.Data, typeof(MassPayment));

            var masspaymentOptions = new MassPaymentByReferenceOptions
            {
                Reference = result.Data.MassPaymentId
            };

            var masspayment = paparaClient.MassPaymentService.GetMassPaymentByReference(masspaymentOptions);

            Assert.IsTrue(masspayment.Succeeded);
        }
    }
}
