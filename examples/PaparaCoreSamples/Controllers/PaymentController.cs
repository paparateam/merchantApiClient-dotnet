using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Papara;
using System;
using System.Threading.Tasks;

namespace PaparaCoreSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaparaClient PaparaClient;
        private readonly PaparaSettings Settings;

        public PaymentController(IPaparaClient paparaClient, IConfiguration configuration)
        {
            this.PaparaClient = paparaClient;
            this.Settings = new PaparaSettings();
            configuration.GetSection("Papara").Bind(this.Settings);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPayment(string id)
        {
            PaymentGetOptions paymentGetOptions = new PaymentGetOptions
            {
                Id = id
            };

            PaparaSingleResult<Payment> result = await this.PaparaClient.PaymentService.GetPaymentAsync(paymentGetOptions);

            if (result.Succeeded)
            {
                Payment payment = result.Data;

                return Ok(payment);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreatePayment()
        {
            string referenceId = Guid.NewGuid().ToString();

            PaymentCreateOptions paymentCreateOptions = new PaymentCreateOptions
            {
                Amount = 1,
                NotificationUrl = "https://testmerchant.com/notification",
                OrderDescription = "Payment Dotnet Core Sample",
                RedirectUrl = "https://testmerchant.com/userredirect",
                ReferenceId = referenceId,
                TurkishNationalId = Settings.TCKN
            };

            PaparaSingleResult<Payment> result = await this.PaparaClient.PaymentService.CreatePaymentAsync(paymentCreateOptions);

            if (result.Succeeded)
            {
                Payment payment = result.Data;

                return Ok(payment);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreatePaymentAndRedirect()
        {
            string referenceId = Guid.NewGuid().ToString();

            PaymentCreateOptions paymentCreateOptions = new PaymentCreateOptions
            {
                Amount = 1,
                NotificationUrl = "https://testmerchant.com/notification",
                OrderDescription = "Payment Dotnet Core Sample",
                RedirectUrl = "https://testmerchant.com/userredirect",
                ReferenceId = referenceId,
                TurkishNationalId = Settings.TCKN
            };

            PaparaSingleResult<Payment> result = await this.PaparaClient.PaymentService.CreatePaymentAsync(paymentCreateOptions);

            if (result.Succeeded)
            {
                Payment payment = result.Data;

                return Redirect(payment.PaymentUrl);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Refund(string id)
        {
            PaymentRefundOptions paymentRefundOptions = new PaymentRefundOptions
            {
                Id = id
            };

            PaparaServiceResult result = await this.PaparaClient.PaymentService.RefundAsync(paymentRefundOptions);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> List()
        {
            PaymentListOptions paymentListOptions = new PaymentListOptions
            {
                PageIndex = 1,
                PageItemCount = 20
            };

            PaparaListResult<PaymentListItem> result = await this.PaparaClient.PaymentService.ListAsync(paymentListOptions);

            if (result.Succeeded)
            {
                PaparaPagingResult<PaymentListItem> payment = result.Data;

                return Ok(payment);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

    }
}