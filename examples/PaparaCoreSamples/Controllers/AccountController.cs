using Microsoft.AspNetCore.Mvc;
using Papara;
using System;
using System.Threading.Tasks;

namespace PaparaCoreSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IPaparaClient PaparaClient;

        public AccountController(IPaparaClient paparaClient)
        {
            this.PaparaClient = paparaClient;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Account>> GetAccount()
        {
            PaparaSingleResult<Account> accountResult = await this.PaparaClient.AccountService.GetAccountAsync();

            if (accountResult.Succeeded)
            {
                Account account = accountResult.Data;

                return Ok(account);
            }
            else
            {
                return BadRequest(accountResult.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<PaparaPagingResult<AccountLedger>>> ListLedgers()
        {
            LedgerListOptions ledgerListOptions = new LedgerListOptions
            {
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now,
                Page = 1,
                PageSize = 20
            };

            PaparaListResult<AccountLedger> listLedgersResult = await this.PaparaClient.AccountService.ListLedgersAsync(ledgerListOptions);

            if (listLedgersResult.Succeeded)
            {
                PaparaPagingResult<AccountLedger> accountLedgers = listLedgersResult.Data;

                return Ok(accountLedgers);
            }
            else
            {
                return BadRequest(listLedgersResult.Error);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Settlement>> GetSettlement()
        {
            SettlementGetOptions settlementGetOptions = new SettlementGetOptions
            {
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now
            };

            PaparaSingleResult<Settlement> settlementResult = await this.PaparaClient.AccountService.GetSettlementAsync(settlementGetOptions);

            if (settlementResult.Succeeded)
            {
                Settlement settlement = settlementResult.Data;

                return Ok(settlement);
            }
            else
            {
                return BadRequest(settlementResult.Error);
            }
        }
    }
}