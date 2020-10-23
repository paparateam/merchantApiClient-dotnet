using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Papara;
using System.Threading.Tasks;

namespace PaparaCoreSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly IPaparaClient PaparaClient;
        private readonly PaparaSettings Settings;

        public ValidationController(IPaparaClient paparaClient, IConfiguration configuration)
        {
            this.PaparaClient = paparaClient;
            this.Settings = new PaparaSettings();
            configuration.GetSection("Papara").Bind(this.Settings);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ValidateById()
        {
            ValidationByIdOptions validationByIdOptions = new ValidationByIdOptions
            {
                UserId = Settings.PersonalAccountId
            };

            var result = await this.PaparaClient.ValidationService.ValidateByIdAsync(validationByIdOptions);

            if (result.Succeeded)
            {
                Validation validation = result.Data;

                return Ok(validation);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ValidateByAccountNumber()
        {
            ValidationByAccountNumberOptions validationByAccountNumberOptions = new ValidationByAccountNumberOptions
            {
                AccountNumber = Settings.ParsedPersonalAccountNumber
            };

            var result = await this.PaparaClient.ValidationService.ValidateByAccountNumberAsync(validationByAccountNumberOptions);

            if (result.Succeeded)
            {
                Validation validation = result.Data;

                return Ok(validation);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ValidateByPhoneNumber()
        {
            ValidationByPhoneNumberOptions validationByPhoneNumberOptions = new ValidationByPhoneNumberOptions
            {
                PhoneNumber = Settings.PersonalPhoneNumber
            };

            var result = await this.PaparaClient.ValidationService.ValidateByPhoneNumberAsync(validationByPhoneNumberOptions);

            if (result.Succeeded)
            {
                Validation validation = result.Data;

                return Ok(validation);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ValidateByEmail()
        {
            var validationByEmailOptions = new ValidationByEmailOptions
            {
                Email = Settings.PersonalEmail
            };

            var result = await this.PaparaClient.ValidationService.ValidateByEmailAsync(validationByEmailOptions);

            if (result.Succeeded)
            {
                Validation validation = result.Data;

                return Ok(validation);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ValidateByTckn()
        {
            ValidationByTcknOptions validationByTcknOptions = new ValidationByTcknOptions
            {
                Tckn = Settings.TCKN
            };

            var result = await this.PaparaClient.ValidationService.ValidateByTcknAsync(validationByTcknOptions);

            if (result.Succeeded)
            {
                Validation validation = result.Data;

                return Ok(validation);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

    }
}