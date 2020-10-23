using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Papara;
using System;
using System.Threading.Tasks;

namespace PaparaCoreSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MassPaymentController : ControllerBase
    {
        private readonly IPaparaClient PaparaClient;
        private readonly PaparaSettings Settings;

        public MassPaymentController(IPaparaClient paparaClient, IConfiguration configuration)
        {
            this.PaparaClient = paparaClient;
            this.Settings = new PaparaSettings();
            configuration.GetSection("Papara").Bind(this.Settings);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMassPayment(int id)
        {
            MassPaymentGetOptions massPaymentGetOptions = new MassPaymentGetOptions
            {
                Id = id
            };

            PaparaSingleResult<MassPayment> result = await this.PaparaClient.MassPaymentService.GetMassPaymentAsync(massPaymentGetOptions);

            if (result.Succeeded)
            {
                MassPayment massPayment = result.Data;

                return Ok(massPayment);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateWithAccountNumber()
        {
            MassPaymentToPaparaNumberOptions massPaymentToPaparaNumberOptions = new MassPaymentToPaparaNumberOptions
            {
                AccountNumber = Settings.PersonalAccountNumber,
                Amount = 1,
                Description = "Unit Test: MassPaymentToPaparaNumber",
                MassPaymentId = Guid.NewGuid().ToString(),
                ParseAccountNumber = 1,
                TurkishNationalId = Settings.TCKN
            };

            PaparaSingleResult<MassPayment> result = await this.PaparaClient.MassPaymentService.CreateMassPaymentWithAccountNumberAsync(massPaymentToPaparaNumberOptions);

            if (result.Succeeded)
            {
                MassPayment massPayment = result.Data;

                return Ok(massPayment);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateWithEmail()
        {
            MassPaymentToEmailOptions massPaymentToEmailOptions = new MassPaymentToEmailOptions
            {
                Amount = 1,
                MassPaymentId = Guid.NewGuid().ToString(),
                Description = "Dotnet Core Sample: MassPaymentToEmail",
                Email = Settings.PersonalEmail,
                TurkishNationalId = Settings.TCKN
            };

            PaparaSingleResult<MassPayment> result = await this.PaparaClient.MassPaymentService.CreateMassPaymentWithEmailAsync(massPaymentToEmailOptions);

            if (result.Succeeded)
            {
                MassPayment massPayment = result.Data;

                return Ok(massPayment);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateWithPhoneNumber()
        {
            MassPaymentToPhoneNumberOptions massPaymentToPhoneNumberOptions = new MassPaymentToPhoneNumberOptions
            {
                Amount = 1,
                MassPaymentId = Guid.NewGuid().ToString(),
                Description = "Dotnet Core Sample: MassPaymentToPhoneNumber",
                PhoneNumber = Settings.PersonalPhoneNumber,
                TurkishNationalId = Settings.TCKN
            };

            PaparaSingleResult<MassPayment> result = await this.PaparaClient.MassPaymentService.CreateMassPaymentWithPhoneNumberAsync(massPaymentToPhoneNumberOptions);

            if (result.Succeeded)
            {
                MassPayment massPayment = result.Data;

                return Ok(massPayment);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
    }
}