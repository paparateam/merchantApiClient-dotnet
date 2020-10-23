using Microsoft.VisualStudio.TestTools.UnitTesting;
using Papara;
using Papara.Infrastructure;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaparaTests.Client
{
    public static class PaparaConfig
    {
        public static string ApiKey = "";
        public static PaparaEnv Env = PaparaEnv.Live;
    }

    [TestClass]
    public class ClientTests : PaparaTest
    {
        [TestMethod]
        public async Task Should_Client_Connect_to_Server()
        {
            var client = new PaparaServiceClient();

            var response = await client.RequestAsync<PaparaSingleResult<Account>>(HttpMethod.Get, "/account", null, base.Options);

            Assert.IsTrue(response.Succeeded);

        }

        [TestMethod]
        public async Task Should_Client_Throw_Exception_When_Path_is_Wrong()
        {
            var client = new PaparaServiceClient();

            try
            {
                var response = await client.RequestAsync<PaparaSingleResult<Account>>(HttpMethod.Get, "/account_wrong", null, base.Options);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is PaparaException);
            }

        }

        [TestMethod]
        public void Should_PaparaClient_Be_Created_Without_Options()
        {
            var client = new PaparaClient();

            client.SetOptions(Options.ApiKey, Options.Env);

            var result = client.AccountService.GetAccount();

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Account>));
            Assert.IsInstanceOfType(result.Data, typeof(Account));
        }

        [TestMethod]
        public void Should_PaparaClient_Throw_Exception_When_No_Apikey_Defined()
        {
            var client = new PaparaClient();

            try
            {
                var result = client.AccountService.GetAccount();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(PaparaException));
            }
        }

        [TestMethod]
        public void Should_PaparaRequest_Has_Valid_Uri()
        {
            var requestOptions = new RequestOptions { ApiKey = "API_KEY" };

            requestOptions.Env = PaparaEnv.Live;

            var paparaRequestLive = new PaparaRequest(HttpMethod.Get, "/account", null, requestOptions);

            Assert.AreEqual("https://merchant.api.papara.com/account", paparaRequestLive.Uri.ToString());

            requestOptions.Env = PaparaEnv.Test;

            var paparaRequestTest = new PaparaRequest(HttpMethod.Get, "/account", null, requestOptions);

            Assert.AreEqual("https://merchant.test.api.papara.com/account", paparaRequestTest.Uri.ToString());
        }


        [TestMethod]
        public void Should_ToQueryString_Method_Returns_Valid_QueryString()
        {
            // without optional properties
            var optionsWithoutOptionalProperties = new MassPaymentToPaparaNumberOptions
            {
                Amount = 100
            };

            var queryStringWithoutOptional = optionsWithoutOptionalProperties.ToQueryString();

            Assert.AreEqual("amount=100.0", queryStringWithoutOptional);

            // with optional properties
            var optionsWithOptionalProperties = new MassPaymentToPaparaNumberOptions
            {
                Amount = 100,
                AccountNumber = "12345",
                Description = "test",
                MassPaymentId = "100",
                ParseAccountNumber = 12345,
                TurkishNationalId = 12345678901
            };

            var queryStringWithOptional = optionsWithOptionalProperties.ToQueryString();

            Assert.AreEqual("accountNumber=12345&parseAccountNumber=12345&amount=100.0&massPaymentId=100&turkishNationalId=12345678901&description=test", queryStringWithOptional);
        }

    }
}
