using Microsoft.VisualStudio.TestTools.UnitTesting;
using Papara;

namespace PaparaTests.ServiceTests
{
    [TestClass]
    public class ValidationServiceTests : PaparaTest
    {
        private readonly PaparaClient paparaClient;

        public ValidationServiceTests()
        {
            paparaClient = new PaparaClient(Options.ApiKey, Options.Env);
        }

        ///
        /// Test Case:
        ///     Validating account with account ID
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{Validation}"/> object
        ///     data should be instance of <see cref="Validation"/> object
        ///   
        [TestMethod]
        public void Should_ValidationService_ValidateById_Returns_Validation()
        {
            var validationByIdOptions = new ValidationByIdOptions
            {
                UserId = base.Settings.Papara.PersonalAccountId
            };

            var result = paparaClient.ValidationService.ValidateById(validationByIdOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Validation>));
            Assert.IsInstanceOfType(result.Data, typeof(Validation));
        }

        ///
        /// Test Case:
        ///     Validating account with account number
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{Validation}"/> object
        ///     data should be instance of <see cref="Validation"/> object
        ///   
        [TestMethod]
        public void Should_ValidationService_ValidateByAccountNumber_Returns_Validation()
        {
            var validationByAccountNumberOptions = new ValidationByAccountNumberOptions
            {
                AccountNumber = Settings.Papara.ParsedPersonalAccountNumber
            };

            var result = paparaClient.ValidationService.ValidateByAccountNumber(validationByAccountNumberOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Validation>));
            Assert.IsInstanceOfType(result.Data, typeof(Validation));
        }

        ///
        /// Test Case:
        ///     Validating account with phone number
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{Validation}"/> object
        ///     data should be instance of <see cref="Validation"/> object
        ///   
        [TestMethod]
        public void Should_ValidationService_ValidateByPhoneNumber_Returns_Validation()
        {
            var validationByPhoneNumberOptions = new ValidationByPhoneNumberOptions
            {
                PhoneNumber = Settings.Papara.PersonalPhoneNumber
            };

            var result = paparaClient.ValidationService.ValidateByPhoneNumber(validationByPhoneNumberOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Validation>));
            Assert.IsInstanceOfType(result.Data, typeof(Validation));
        }

        ///
        /// Test Case:
        ///     Validating account with email
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{Validation}"/> object
        ///     data should be instance of <see cref="Validation"/> object
        ///
        [TestMethod]
        public void Should_ValidationService_ValidateByEmail_Returns_Validation()
        {
            var validationByEmailOptions = new ValidationByEmailOptions
            {
                Email = Settings.Papara.PersonalEmail
            };

            var result = paparaClient.ValidationService.ValidateByEmail(validationByEmailOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Validation>));
            Assert.IsInstanceOfType(result.Data, typeof(Validation));
        }

        ///
        /// Test Case:
        ///     Validating Papara account with Turkish identity number (TCKN)
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{Validation}"/> object
        ///     data should be instance of <see cref="Validation"/> object
        ///           
        [TestMethod]
        public void Should_ValidationService_ValidateByTckn_Returns_Validation()
        {
            var validationByTcknOptions = new ValidationByTcknOptions
            {
                Tckn = Settings.Papara.TCKN
            };

            var result = paparaClient.ValidationService.ValidateByTckn(validationByTcknOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Validation>));
            Assert.IsInstanceOfType(result.Data, typeof(Validation));
        }
    }
}
