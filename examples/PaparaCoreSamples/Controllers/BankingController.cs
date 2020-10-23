using Microsoft.AspNetCore.Mvc;
using Papara;
using System.Threading.Tasks;

namespace PaparaCoreSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly IPaparaClient PaparaClient;

        public BankingController(IPaparaClient paparaClient)
        {
            this.PaparaClient = paparaClient;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<BankAccount>> GetBankAccounts()
        {
            PaparaArrayResult<BankAccount> result = await this.PaparaClient.BankingService.GetBankAccountsAsync();

            if (result.Succeeded)
            {
                BankAccount[] bankAccounts = result.Data;

                return Ok(bankAccounts);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> Withdrawal(int bankAccountId)
        {
            BankingWithdrawalOptions withdrawalOptions = new BankingWithdrawalOptions
            {
                Amount = 10,
                BankAccountId = bankAccountId
            };

            PaparaServiceResult withdrawalResult = await this.PaparaClient.BankingService.WithdrawalAsync(withdrawalOptions);

            if (withdrawalResult.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(withdrawalResult.Error);
            }
        }

    }
}