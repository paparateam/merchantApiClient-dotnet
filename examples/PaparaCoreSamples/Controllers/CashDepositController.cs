using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Papara;
using System;
using System.Threading.Tasks;

namespace PaparaCoreSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashDepositController : ControllerBase
    {
        private readonly IPaparaClient PaparaClient;
        private readonly PaparaSettings Settings;

        public CashDepositController(IPaparaClient paparaClient, IConfiguration configuration)
        {
            this.PaparaClient = paparaClient;
            this.Settings = new PaparaSettings();
            configuration.GetSection("Papara").Bind(this.Settings);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCashDeposit(int id)
        {
            CashDepositGetOptions cashDepositGetOptions = new CashDepositGetOptions
            {
                Id = id
            };

            PaparaSingleResult<CashDeposit> result = await this.PaparaClient.CashDepositService.GetCashDepositAsync(cashDepositGetOptions);

            if (result.Succeeded)
            {
                CashDeposit cashDeposit = result.Data;

                return Ok(cashDeposit);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateWithPhoneNumber()
        {
            CashDepositToPhoneOptions cashDepositToPhoneOptions = new CashDepositToPhoneOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                PhoneNumber = Settings.PersonalPhoneNumber,
            };

            PaparaSingleResult<CashDeposit> result = await this.PaparaClient.CashDepositService.CreateWithPhoneNumberAsync(cashDepositToPhoneOptions);

            if (result.Succeeded)
            {
                CashDeposit cashDeposit = result.Data;
                return Ok(cashDeposit);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateWithAccountNumber()
        {
            CashDepositToAccountNumberOptions cashDepositToAccountNumberOptions = new CashDepositToAccountNumberOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                AccountNumber = Settings.ParsedPersonalAccountNumber,
            };

            PaparaSingleResult<CashDeposit> result = await this.PaparaClient.CashDepositService.CreateWithAccountNumberAsync(cashDepositToAccountNumberOptions);

            if (result.Succeeded)
            {
                CashDeposit cashDeposit = result.Data;
                return Ok(cashDeposit);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateWithTckn()
        {
            CashDepositToTcknOptions cashDepositToTcknOptions = new CashDepositToTcknOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                Tckn = Settings.TCKN,
            };

            PaparaSingleResult<CashDeposit> result = await this.PaparaClient.CashDepositService.CreateWithTcknAsync(cashDepositToTcknOptions);

            if (result.Succeeded)
            {
                CashDeposit cashDeposit = result.Data;
                return Ok(cashDeposit);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateProvisionWithTckn()
        {
            CashDepositTcknControlOptions cashDepositTcknControlOptions = new CashDepositTcknControlOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                PhoneNumber = Settings.PersonalPhoneNumber,
                Tckn = Settings.TCKN
            };

            PaparaSingleResult<CashDepositProvision> result = await this.PaparaClient.CashDepositService.CreateProvisionWithTcknAsync(cashDepositTcknControlOptions);

            if (result.Succeeded)
            {
                CashDepositProvision cashDepositProvision = result.Data;
                return Ok(cashDepositProvision);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateProvisionWithPhoneNumber()
        {
            CashDepositToPhoneOptions cashDepositToPhoneOptions = new CashDepositToPhoneOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                PhoneNumber = Settings.PersonalPhoneNumber,
            };

            PaparaSingleResult<CashDepositProvision> result = await this.PaparaClient.CashDepositService.CreateProvisionWithPhoneNumberAsync(cashDepositToPhoneOptions);

            if (result.Succeeded)
            {
                CashDepositProvision cashDepositProvision = result.Data;
                return Ok(cashDepositProvision);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateProvisionWithAccountNumber()
        {
            CashDepositToAccountNumberOptions cashDepositToAccountNumberOptions = new CashDepositToAccountNumberOptions
            {
                Amount = 10,
                MerchantReference = Guid.NewGuid().ToString(),
                AccountNumber = Settings.ParsedPersonalAccountNumber
            };

            PaparaSingleResult<CashDepositProvision> result = await this.PaparaClient.CashDepositService.CreateProvisionWithAccountNumberAsync(cashDepositToAccountNumberOptions);

            if (result.Succeeded)
            {
                CashDepositProvision cashDepositProvision = result.Data;
                return Ok(cashDepositProvision);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CompleteProvision(int id)
        {
            CashDepositCompleteOptions cashDepositCompleteOptions = new CashDepositCompleteOptions
            {
                Id = id,
                TransactionDate = DateTime.Now
            };

            PaparaSingleResult<CashDeposit> result = await this.PaparaClient.CashDepositService.CompleteProvisionAsync(cashDepositCompleteOptions);

            if (result.Succeeded)
            {
                CashDeposit cashDeposit = result.Data;
                return Ok(cashDeposit);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCashDepositByReference(string reference)
        {
            CashDepositByReferenceOptions cashDepositByReferenceOptions = new CashDepositByReferenceOptions
            {
                Reference =  reference
            };

            PaparaSingleResult<CashDeposit> result = await this.PaparaClient.CashDepositService.GetCashDepositByReferenceAsync(cashDepositByReferenceOptions);

            if (result.Succeeded)
            {
                CashDeposit cashDeposit = result.Data;
                return Ok(cashDeposit);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCashDepositByDate()
        {
            var cashDepositByDateOptions = new CashDepositByDateOptions
            {
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now,
                PageIndex = 1,
                PageItemCount = 20
            };

            PaparaArrayResult<CashDeposit> result = await this.PaparaClient.CashDepositService.GetCashDepositByDateAsync(cashDepositByDateOptions);

            if (result.Succeeded)
            {
                CashDeposit[] cashDeposit = result.Data;
                return Ok(cashDeposit);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ProvisionSettlements()
        {
            CashDepositSettlementOptions cashDepositSettlementOptions = new CashDepositSettlementOptions
            {
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now,
                EntryType = EntryType.BankTransfer
            };

            var result = await this.PaparaClient.CashDepositService.ProvisionSettlementsAsync(cashDepositSettlementOptions);

            if (result.Succeeded)
            {
                CashDepositSettlement cashDepositSettlement = result.Data;
                return Ok(cashDepositSettlement);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

    }
}