using Microsoft.VisualStudio.TestTools.UnitTesting;
using Papara;
using System;

namespace PaparaTests.ServiceTests
{
    [TestClass]
    public class PaymentServiceTests : PaparaTest
    {
        private readonly PaparaClient paparaClient;

        public PaymentServiceTests()
        {
            paparaClient = new PaparaClient(Options.ApiKey, Options.Env);
        }

        ///
        /// Test Case:
        ///     Getting payment list from payment service
        /// 
        /// Conditions:
        ///     Account must have at least 1 payment information
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{Payment}"/>
        ///     data should be instance of <see cref="Payment"/>
        ///   
        [TestMethod]
        public void Should_PaymentService_GetPayment_Returns_Payment()
        {
            var paymentListOptions = new PaymentListOptions
            {
                PageIndex = 1,
                PageItemCount = 20
            };

            var paymentListResult = paparaClient.PaymentService.List(paymentListOptions);

            Assert.IsTrue(paymentListResult.Succeeded);

            if (paymentListResult.Data.Items.Count > 0)
            {
                Assert.AreNotEqual(0, paymentListResult.Data.Items.Count, "Merchant must have at least 1 payment transaction.");
                Assert.IsInstanceOfType(paymentListResult.Data, typeof(PaparaPagingResult<PaymentListItem>));

                var payment = paymentListResult.Data.Items[0];

                var paymentGetOptions = new PaymentGetOptions
                {
                    Id = payment.Id
                };

                var paymentResult = paparaClient.PaymentService.GetPayment(paymentGetOptions);

                Assert.IsTrue(paymentResult.Succeeded);
                Assert.IsInstanceOfType(paymentResult, typeof(PaparaSingleResult<Payment>));
                Assert.IsInstanceOfType(paymentResult.Data, typeof(Payment));
            }
        }

        ///
        /// Test Case:
        ///     Creating payment with payment service
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{TEntity}"/>
        ///     data should be instance of <see cref="Payment"/>
        ///   
        [TestMethod]
        public void Should_PaymentService_Create_Returns_Payment()
        {
            var referenceId = Guid.NewGuid().ToString();

            var paymentCreateOptions = new PaymentCreateOptions
            {
                Amount = 1,
                NotificationUrl = "https://testmerchant.com/notification",
                OrderDescription = "Payment Unit Test",
                RedirectUrl = "https://testmerchant.com/userredirect",
                ReferenceId = referenceId,
                TurkishNationalId = Settings.Papara.TCKN
            };

            var result = paparaClient.PaymentService.CreatePayment(paymentCreateOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaSingleResult<Payment>));
            Assert.IsInstanceOfType(result.Data, typeof(Payment));
            Assert.AreEqual(referenceId, result.Data.ReferenceId);
        }

        /// Test Case:
        ///     Refunding payment from payment service
        ///     
        /// Conditions:
        ///     Account must have at least 1 payment information
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaServiceResult"/>
        ///  
        [TestMethod]
        public void Should_PaymentService_Refund_Returns_Payment()
        {
            var paymentListOptions = new PaymentListOptions
            {
                PageIndex = 1,
                PageItemCount = 20
            };

            var paymentListResult = paparaClient.PaymentService.List(paymentListOptions);

            Assert.IsTrue(paymentListResult.Succeeded);
            Assert.IsNull(paymentListResult.Error);

            if (paymentListResult.Data.Items.Count > 0)
            {
                Assert.AreNotEqual(0, paymentListResult.Data.Items.Count, "Merchant must have at least 1 payment information.");
                Assert.IsInstanceOfType(paymentListResult.Data, typeof(PaparaPagingResult<PaymentListItem>));

                var payment = paymentListResult.Data.Items[0];

                var result = paparaClient.PaymentService.Refund(new PaymentRefundOptions
                {
                    Id = payment.Id
                });

                Assert.IsTrue(result.Succeeded);
                Assert.IsNull(result.Error);
                Assert.IsInstanceOfType(result, typeof(PaparaServiceResult));
            }
        }

        ///
        /// Test Case:
        ///     Getting payment list from payment service
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaListResult{PaymentListItem}"/>
        ///     data should be instance of <see cref="PaparaPagingResult{PaymentListItem}"/>
        ///   
        [TestMethod]
        public void Should_PaymentService_List_Returns_Payment()
        {
            var paymentListOptions = new PaymentListOptions
            {
                PageIndex = 1,
                PageItemCount = 20
            };

            var result = paparaClient.PaymentService.List(paymentListOptions);

            Assert.IsTrue(result.Succeeded);
            Assert.IsNull(result.Error);
            Assert.IsInstanceOfType(result, typeof(PaparaListResult<PaymentListItem>));
            Assert.IsInstanceOfType(result.Data, typeof(PaparaPagingResult<PaymentListItem>));
        }

        ///
        /// Test Case:
        ///     Getting payment by reference with payment service
        /// 
        /// Result
        ///     should be succeeded
        ///     should not have error
        ///     should be instance of <see cref="PaparaSingleResult{TEntity}"/>
        ///     data should be instance of <see cref="Payment"/>
        ///   
        [TestMethod]
        public void Should_PaymentService_GetByReference_Returns_Payment()
        {
            var referenceId = Guid.NewGuid().ToString();

            var paymentCreateOptions = new PaymentCreateOptions
            {
                Amount = 1,
                NotificationUrl = "https://testmerchant.com/notification",
                OrderDescription = "Payment Unit Test",
                RedirectUrl = "https://testmerchant.com/userredirect",
                ReferenceId = referenceId,
                TurkishNationalId = Settings.Papara.TCKN
            };

            var result = paparaClient.PaymentService.CreatePayment(paymentCreateOptions);

            var options = new PaymentByReferenceOptions
            {
                ReferenceId = referenceId
            };

            var payment = paparaClient.PaymentService.GetPaymentByReference(options);

            Assert.IsTrue(payment.Succeeded);
            Assert.IsInstanceOfType(result.Data, typeof(Payment));
            Assert.AreEqual(referenceId, result.Data.ReferenceId);
        }
    }
}
